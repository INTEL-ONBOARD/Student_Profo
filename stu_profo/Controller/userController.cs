    using stu_profo.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace stu_profo.Controller
    {
        class userController
        {
            public static void validateUser()
            {
                engine en = new engine();
                var dataset = en.getData("https://www.nibmworldwide.com/", "/exams/mis");
                //System.Diagnostics.Debug.WriteLine(dataset.ToString());
        }
        }

    }
