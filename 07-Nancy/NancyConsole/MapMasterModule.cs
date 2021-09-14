using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Nancy;
using Newtonsoft.Json;

namespace NancyConsole
{
    public class MapMasterModule: NancyModule
    {
        public MapMasterModule()
        {
            Get("Get", _ =>
            {
                var path = AppDomain.CurrentDomain.BaseDirectory + "/Views/home/index.html";
                Response response = new Response();
                //response.ContentType = "text/html";
                response.Contents = stream =>
                {
                    using (var reader = new StreamReader(path))
                    {
                        using (var writer = new StreamWriter(stream))
                        {
                            var ret = GetList();
                            //byte[] b = SerializeToBinary(ret);
                            var b = JsonConvert.SerializeObject(ret);
                            writer.Write(b);
                            writer.Flush();
                        }
                    }
                };

                return response;
            });
            
        }

        #region Test1

        //private void Test1()
        //{
        //    Get["/a"] = parameters => "IndexModule";
        //    Delete["/Delete"] = parameters => "IndexModule/Delete";
        //    Get["/GetCal"] = getParkInfo;



        //    Get["/get"] = parameters =>
        //    {
        //        Response response = new Response();
        //        //response.ContentType = "text/html";
        //        response.ContentType = "application/json";
        //        response.StatusCode = HttpStatusCode.BadGateway;

        //        response.Contents = Contents;

        //        return response;

        //    };
        //}


        //public void Contents(Stream stream)
        //{
        //    //var path = AppDomain.CurrentDomain.BaseDirectory + "/Views/home/index.html";
        //    //using (var reader = new StreamReader(path))
        //    //{
        //    //    using (var writer = new StreamWriter(stream))
        //    //    {
        //    //        writer.Write(reader.ReadToEnd());
        //    //        writer.Flush();
        //    //    }
        //    //}


        //    User u = new User()
        //    {
        //        Id = 1,
        //        Name = "1234"
        //    };
        //    byte[] bytes = ProtobufUtils.SerializeClass(u);
        //    string json = JsonConvert.SerializeObject(u);

        //    using (var writer = new StreamWriter(stream))
        //    {
        //        writer.Write(json);
        //        writer.Flush();
        //    }
        //    //stream = new MemoryStream(bytes);
        //}

        //#region getParkInfo

        //private Response getParkInfo(dynamic o)
        //{
        //    var parkinfo = GetParkInfo();
        //    Console.WriteLine("接收到数据请求指令");
        //    ApiResultWithData<ParkInfo> res = new ApiResultWithData<ParkInfo>();
        //    res.ResultCode = (int)EErrorCode.OK;
        //    res.Message = "请求成功";
        //    res.Data = parkinfo;

        //    return Response.AsJson<ApiResultWithData<ParkInfo>>(res);
        //}

        //private ParkInfo GetParkInfo()
        //{
        //    ParkInfo _CurrentPark = new ParkInfo();
        //    _CurrentPark.ParkName = "园区停车场";

        //    DeviceInfo dev1 = new DeviceInfo();
        //    dev1.DeviceIP = "192.168.205.6";
        //    dev1.Name = "入口";
        //    dev1.MacNO = 1;
        //    dev1.WorkstationName = "大门岗亭";

        //    DeviceInfo dev2 = new DeviceInfo();
        //    dev2.DeviceIP = "192.168.205.7";
        //    dev2.Name = "出口";
        //    dev2.MacNO = 2;
        //    dev2.WorkstationName = "大门岗亭";

        //    _CurrentPark.DevList = new List<DeviceInfo>() { dev1, dev2 };
        //    return _CurrentPark;
        //}

        //#endregion


        //#region MyRegion
        //[Serializable]
        //[DataContract]
        //public class User
        //{
        //    [DataMember]
        //    public int Id { get; set; }
        //    [DataMember]
        //    public string Name { get; set; }
        //}


        //#endregion

        #endregion


        #region test2
        /// <summary>
        /// 将对象序列化为二进制数据 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static byte[] SerializeToBinary( object obj)
        {
            MemoryStream stream = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(stream, obj);

            byte[] data = stream.ToArray();
            stream.Close();

            return data;
        }

        public static List<UserInfo> GetList()
        {
            List<UserInfo> list = new List<UserInfo>()
            {
                new UserInfo() {Name = "1", Age = 1},
                new UserInfo() {Name = "2", Age = 2},
                new UserInfo() {Name = "3", Age = 3},
                new UserInfo() {Name = "4", Age = 4}
            };


            return list;
        }

        [Serializable]
        [DataContract]
        public class UserInfo
        {
            [DataMember]
            public string Name { get; set; }
            public int Age { get; set; }
        }
        #endregion
    }
}
