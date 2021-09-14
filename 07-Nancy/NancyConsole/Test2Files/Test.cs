using System.Collections.Generic;

namespace NancyConsole.Test2Files
{
    public class Test
    {
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

        public class UserInfo
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }
    }





}