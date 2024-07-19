using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using stu_profo.Model;

using Newtonsoft.Json;

namespace stu_profo.Controller
{
    class dataController
    {
        //public void getProgrammes() {
        //    if (engine.dumpProgrammes()) {
        //        List<programeModel> programeModels = readProgramms();
        //    }
        //}
        public static void getCourses(int programme) { }
        public List<programeModel> getProgramms() {
            List<programeModel> pm = new List<programeModel>();
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "programmes.json");
            using StreamReader reader = new StreamReader(path);
            var json = reader.ReadToEnd();
            pm = JsonConvert.DeserializeObject<List<programeModel>>(json);
            if (pm != null) { return pm; } else { return pm; };
        }
    }

}
