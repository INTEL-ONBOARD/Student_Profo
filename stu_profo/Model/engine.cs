using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;

namespace stu_profo.Model
{
    class engine
    {
        HttpClient httpClient ;
        public void initCon() {try { httpClient = new HttpClient(); }catch(Exception e){ Console.WriteLine(e.Message); }}
        public void closeCon() { try { httpClient.Dispose(); }catch(Exception ex){ Console.WriteLine(ex.Message); } }
        public void getCon(string con,string path) { 
            httpClient.BaseAddress = new Uri(con); 
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = httpClient.GetAsync(path).Result;
            if (response.IsSuccessStatusCode) { string dataset = response.Content.ReadAsStringAsync().Result; Console.WriteLine(dataset); } else { Console.WriteLine(response.StatusCode); }
        }
    }
}
