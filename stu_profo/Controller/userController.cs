    using stu_profo.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.IO;
    using System.Text.Json;
using System.Data;

namespace stu_profo.Controller
{
    class userController
    {
        public static void validateUser()
        {
            //engine en = new engine();
            //en.dumpProgrammes();
            ////var dataset = en.getDump("https://www.nibmworldwide.com/", "exams/mis");
            ////System.Diagnostics.Debug.WriteLine(dataset.ToString());
            ///

            string userData = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "users.json"));
            var userBin = JsonSerializer.Deserialize<Users>(userData);
            foreach (var user in userBin.users)
            {
                System.Diagnostics.Debug.WriteLine(user.email);
            }
        }
        public class User
        {
            public string email { get; set; }
            public string pwd { get; set; }

        }
        public class Users
        {
            public List<User> users { get; set; }
        }
    }
}