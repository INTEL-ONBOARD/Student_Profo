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
        engine() { try { httpClient = new HttpClient(); } catch (Exception e) { System.Diagnostics.Debug.WriteLine(e.Message); } }
        public void closeCon() { try { httpClient.Dispose(); }catch(Exception ex){ System.Diagnostics.Debug.WriteLine(ex.Message); } }
        public string getCon(string con,string path) { 
            httpClient.BaseAddress = new Uri(con); 
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = httpClient.GetAsync(path).Result;
            if (response.IsSuccessStatusCode) { string dataset = response.Content.ReadAsStringAsync().Result; System.Diagnostics.Debug.WriteLine(dataset);return dataset; } else { System.Diagnostics.Debug.WriteLine(response.StatusCode); return "not found!"; }
        }
    }
}
