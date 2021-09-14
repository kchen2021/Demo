using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NancyConsole
{
    public class ParkInfo
    {
        string _ParkName = "";
        /// <summary>
        /// 捷顺系统中的车场名。如果用不到可以不保存
        /// </summary>
        public string ParkName
        {
            get { return _ParkName; }
            set { _ParkName = value; }
        }

        string _PictureSharePath = "";
        /// <summary>
        /// 车场存放图片的网络地址如: \\192.168.1.89\pic\或\\192.168.1.89\pic，重要，取进出记录的图片需要用到
        /// </summary>
        public string PictureSharePath
        {
            get { return _PictureSharePath; }
            set { _PictureSharePath = value; }
        }

        List<DeviceInfo> _DevList = new List<DeviceInfo>();
        /// <summary>
        /// 设备列表
        /// </summary>
        public List<DeviceInfo> DevList
        {
            get { return _DevList; }
            set { _DevList = value; }
        }
    }
}
