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
            try {
                engine en = new engine();
                en.getCon();
            }catch (Exception e) { }  
        }
    }

}
