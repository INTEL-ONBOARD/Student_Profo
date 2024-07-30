    using stu_profo.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.IO;
    using System.Text.Json;
using System.Data;
using System.Configuration;
using System.Windows.Shapes;
using Newtonsoft.Json.Linq;


namespace stu_profo.Controller
{
    class userController
    {
        public bool validateUser(string useremail, string pwd)
        {

            //engine en = new engine();
            //en.dumpProgrammes();
            ////var dataset = en.getDump("https://www.nibmworldwide.com/", "exams/mis");
            ////System.Diagnostics.Debug.WriteLine(dataset.ToString());
            ///

            string userData = File.ReadAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "users.json"));
            var userBin = JsonSerializer.Deserialize<Users>(userData);
            int sizeOfUsers = userBin.users.Count();
            customArray userArray = new customArray(sizeOfUsers);
            foreach (var user in userBin.users)
            {
               userArray.insert(user);
            }
            if ((userArray.search("email", useremail)) && (userArray.search("password", pwd)))
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }
        public bool addUser(string username, string password)
        {

            try
            {
                string userData = File.ReadAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "users.json"));
                JObject jsonObj = JObject.Parse(userData);
                JArray userArray = (JArray)jsonObj["users"];
                JObject newuser = new JObject();
                
                newuser["email"] = username;
                newuser["password"] = password;
                userArray.Add(newuser);

                string updatedJson = jsonObj.ToString();
                File.WriteAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "users.json"), updatedJson);

                System.Diagnostics.Debug.WriteLine("file Updated!");
                return true;
            }
            catch(Exception e){
                System.Diagnostics.Debug.WriteLine(e.Message);

                System.Diagnostics.Debug.WriteLine("Issue occured while udpating the file!");
                return false;
            }

        }



        public class User
        {
            public string email { get; set; }
            public string password { get; set; }

        }
        public class Users
        {
            public List<User> users { get; set; }
        }
        
        public class customArray
        {
            User[] userSet;
            int arrayBin;
            public customArray(int length)
            {
                userSet = new User[length];
                arrayBin = 0;
            }
           public void insert(User user)
            {
                userSet[arrayBin] = user;
                arrayBin++;
            }
            public void remove(User user, int pos) {
                userSet[pos] = null;
            }
            public void clear() {
                userSet = null;
            }
            public bool search(string types,string pattern) {
                string type = types;
                if (type == "email")
                {
                    foreach (var user in userSet)
                    {
                        if (user.email == pattern) { return true; }
                    }
                }
                else if (type == "password")
                {
                    foreach (var user in userSet)
                    {
                        if(user.password == pattern) {return true; }
                    }
                }

                return false;
            }
        }
    }
}