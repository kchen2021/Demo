using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NancyConsole
{
    public class DeviceInfo
    {
        string _Name = "";
        /// <summary>
        /// 设备昵称，例如 东门入口设备
        /// </summary>
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        int _MacNO = 1;
        /// <summary>
        /// 设备机号，捷顺系统中唯一，发卡会用到
        /// </summary>
        public int MacNO
        {
            get { return _MacNO; }
            set { _MacNO = value; }
        }
        string _DeviceIP = "";
        /// <summary>
        /// 设备控制器的IP
        /// </summary>
        public string DeviceIP
        {
            get { return _DeviceIP; }
            set { _DeviceIP = value; }
        }

        int _EntryType = (int)EDeviceEntryType.BigIn;
        /// <summary>
        /// 出入口的类型
        /// </summary>
        public int EntryType
        {
            get { return _EntryType; }
            set { _EntryType = value; }
        }


        string _WorkstationIP = "192.168.1.10";
        /// <summary>
        /// 岗亭电脑工作站IP
        /// </summary>
        public string WorkstationIP
        {
            get { return _WorkstationIP; }
            set { _WorkstationIP = value; }
        }


        string _WorkstationName = "南门岗亭";
        /// <summary>
        /// 岗亭电脑工作站名称，如果捷顺系统中所有电脑都是同一电脑管理，则所有设备的WorkstationName都相同
        /// </summary>
        public string WorkstationName
        {
            get { return _WorkstationName; }
            set { _WorkstationName = value; }
        }

        DateTime _StartTime = new DateTime(2000, 1, 1, 0, 0, 0);
        /// <summary>
        /// 设备禁止通行开始时间（如 23:30），开始时间=结束时间 则无禁止时间，此时间对领导车（特权车）无效
        /// </summary>
        public DateTime StartTime
        {
            get { return _StartTime; }
            set { _StartTime = value; }
        }


        DateTime _EndTime = new DateTime(2000, 1, 1, 0, 0, 0);
        /// <summary>
        /// 设备禁止通行结束时间（如 06:30）
        /// </summary>
        public DateTime EndTime
        {
            get { return _EndTime; }
            set { _EndTime = value; }
        }
    }

    public enum EDeviceEntryType
    {
        /// <summary>
        /// 大车场入口（一般为外围设备入口）
        /// </summary>
        BigIn = 1,
        /// <summary>
        /// 大车场出口（一般为外围设备出口）
        /// </summary>
        BigOut = 2,
        /// <summary>
        /// 小车场入口（一般为大车场内部，如地库入口），本项目可能现场没有
        /// </summary>
        SmallIn = 3,
        /// <summary>
        /// 小车场出口，本项目可能现场没有
        /// </summary>
        SmallOut = 4,
        /// <summary>
        /// 中央收费定点，本项目可能现场没有
        /// </summary>
        ZYDingDian = 5,
        /// <summary>
        /// 中央收费出口，本项目可能现场没有
        /// </summary>
        ZYOut = 6
    }
}
