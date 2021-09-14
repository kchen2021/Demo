using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace StackExchangeNetCoreTest.Extensions
{
	internal static class DebuggingExtensions
	{
		private const int MaxStartingLinesOfStack = 10;
		private const int MaxEndingLinesOfStack = 10;

		public static string ToStringX(
			this StackTrace stackTrace,
			int startingLinesCount,
			int endingLinesCount,
			Func<Tuple<StackFrame, int>, bool> filter = null,
			bool includeMethosArguments = false
		)
		{
			var stackFrames = stackTrace.GetFrames() ?? new StackFrame[] { };
			filter = filter ?? (i => true);

			var stackFramesList = stackFrames
				.Select((frame, index) => new Tuple<StackFrame, int>(frame, index))
				.Where(filter)
				.ToList();

			var totalLines = stackFramesList.Count;
			var actualStartingLines = Math.Min(totalLines, startingLinesCount);
			var actualEndingLines = Math.Min(totalLines - actualStartingLines, endingLinesCount);

			Func<Type, string> getFullTypeName = t =>
				t == null
					? string.Empty
					: t.FullName;

			Func<IEnumerable<Tuple<StackFrame, int>>, string> toStackString =
				indexedFrames => indexedFrames
					.Aggregate(
						new StringBuilder(),
						(stringBuilder, stackFrame) =>
							stringBuilder.AppendFormat(
								"{0,2}) {1}.{2}({3})line: {4}{5}",
								stackFrame.Item2,
								getFullTypeName(stackFrame.Item1.GetMethod().DeclaringType),
								stackFrame.Item1.GetMethod().Name,
								includeMethosArguments
								? string.Join(
									", ",
									stackFrame
										.Item1
										.GetMethod()
										.GetParameters()
										.Select(
											p => "{0} {1}".FormatX(
												p.ParameterType.Name,
												p.Name
											)
										)
									)
								: string.Empty,
								stackFrame.Item1.GetFileLineNumber(),
								Environment.NewLine
							)
					)
					.ToString();

			var result = "{0}{1}".FormatX(
				toStackString(stackFramesList.Take(actualStartingLines)),
				actualEndingLines > 0
					? "{0}{1}".FormatX(
						actualStartingLines + actualEndingLines < totalLines
							? "...{0}".FormatX(Environment.NewLine)
							: string.Empty,
						toStackString(
							stackFramesList.GetRange(
								stackFramesList.Count - actualEndingLines,
								actualEndingLines
							)
						)
					)
					: string.Empty
				);
			return result;
		}

		public static string GetCallStack(int skipFrames = 1, bool includeMethosArguments = false)
		{
			var stackTrace = new StackTrace(skipFrames, true);
			return stackTrace.GetCallStack(skipFrames, includeMethosArguments);
		}

		public static string GetCallStack(this StackTrace stackTrace, int skipFrames = 1, bool includeMethosArguments = false)
		{

			Func<Type, string> getFullTypeName = t =>
				t == null
					? string.Empty
					: t.FullName;

			var stackString = stackTrace.ToStringX(
				MaxStartingLinesOfStack,
				MaxEndingLinesOfStack,
				tuple =>
					!getFullTypeName(tuple.Item1.GetMethod().DeclaringType).StartsWith("System."),
				includeMethosArguments
			);

			return stackString;
		}

		public static string ThreadPoolState()
		{
			int workerThreads;
			int completionPortThreads;
			ThreadPool.GetAvailableThreads(out workerThreads, out completionPortThreads);

			int minWorkerThreads;
			int minCompletionPortThreads;
			ThreadPool.GetMinThreads(out minWorkerThreads, out minCompletionPortThreads);

			int maxWorkerThreads;
			int maxCompletionPortThreads;
			ThreadPool.GetMaxThreads(out maxWorkerThreads, out maxCompletionPortThreads);

			var threadPoolState = "[{0}][{1}][{2}]".FormatX(
				"available workerThreads {0}, completionPortThreads {1}".FormatX(
					workerThreads,
					completionPortThreads
				),
				"min workerThreads {0}, min completionPortThreads {1}".FormatX(
					minWorkerThreads,
					minCompletionPortThreads
				),
				"max workerThreads {0}, max completionPortThreads {1}".FormatX(
					maxWorkerThreads,
					maxCompletionPortThreads
				)
			);
			return threadPoolState;
		}
	}
}
