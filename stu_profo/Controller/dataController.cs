using Newtonsoft.Json;
using stu_profo.Model;
using System.IO;
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
        //        List<blockModel> blockModels = readProgramms();
        //    }
        //}

        public static void getCourses(int programme) { 
        
        }
        public static List<blockModel> getProgramms() 
        {
            List<blockModel> pm = new List<blockModel>();

            if (engine.dumpProgrammes())
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "programmes.json");
                using StreamReader reader = new StreamReader(path);
                var json = reader.ReadToEnd();
                pm = JsonConvert.DeserializeObject<List<blockModel>>(json);
                if (pm != null) { return pm; } else { return pm; };
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Data cannot be loaded! issue(engine)");
                return pm;
            }


        }
        public static List<blockModel> getBatches()
        {
            List<blockModel> bm = new List<blockModel>();
            if (engine.dumpBatch()) {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "batches.json");
                using StreamReader reader = new StreamReader(path);
                var json = reader.ReadToEnd();
                bm = JsonConvert.DeserializeObject<List<blockModel>>(json);
                if (bm != null) { return bm; } else { return bm; };
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Data cannot be loaded! issue(engine)");
                return bm;
            }
            

        }
        public static List<blockModel> getStudents()
        {
            List<blockModel> sm = new List<blockModel>();
            if (engine.dumpStudents())
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "students.json");
                using StreamReader reader = new StreamReader(path);
                var json = reader.ReadToEnd();
                sm = JsonConvert.DeserializeObject<List<blockModel>>(json);
                if (sm != null) { return sm; } else { return sm; };
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Data cannot be loaded! issue(engine)");
                return sm;
            }

        }
        public static List<dataModel> getStudentsResults()
        {
            List<dataModel> dm = new List<dataModel>();   
            if (engine.dumpStudentGrade()) {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "stuData.json");
                using StreamReader reader = new StreamReader(path);
                var json = reader.ReadToEnd();
                dm = JsonConvert.DeserializeObject<List<dataModel>>(json);
                if (dm != null) { return dm; } else { return dm; };
            } else { return null; }
        }

        public static void setProgramm(string fileName , string data)
        {
            using (StreamWriter writer = new StreamWriter(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName))) { writer.Write(data);writer.Dispose(); }
        }

        public static void setBatch(string fileName, string data)
        {
            using (StreamWriter writer = new StreamWriter(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName))) { writer.Write(data); writer.Dispose(); }
        }

    }

}
