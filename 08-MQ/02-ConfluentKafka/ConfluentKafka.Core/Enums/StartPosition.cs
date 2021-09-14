namespace ConfluentKafka.Core.Enums
{
    public enum StartPosition
    {
        /// <summary>
        /// Start from end of the partitions.
        /// </summary>
        End,

        /// <summary>
        /// Start from the beginning of the partitions.
        /// </summary>
        Beginning,

        /// <summary>
        /// Starting partition is unassigned, invalid, or default.
        /// </summary>
        Invalid,

        /// <summary>
        /// Starting from the stored position of the partition.
        /// </summary>
        Stored
    }
}
