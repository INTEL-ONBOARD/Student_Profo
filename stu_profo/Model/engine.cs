using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections;
using System.Data;


using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using HtmlAgilityPack;

namespace stu_profo.Model
{
    public class engine
    {
        HttpClient httpClient ;
        public engine() { try { httpClient = new HttpClient(); } catch (Exception e) { System.Diagnostics.Debug.WriteLine(e.Message); } }
        public void closeCon() { try { httpClient.Dispose(); }catch(Exception ex){ System.Diagnostics.Debug.WriteLine(ex.Message); } }
        public object getCon(string con, string path)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(con);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Add("Cookie", "_ga=GA1.2.823271897.1721246274; _gid=GA1.2.2047118751.1721246274; twk_idm_key=uI4l4is7BBP4bHh7gB8f0; TawkConnectionTime=0; twk_uuid_5f6c82264704467e89f1ee75=%7B%22uuid%22%3A%221.92OoxUrMGNfGvbmd4OAARBZLGcTlluRC4aKi61QtgVhqtx0U6Cyw7UV5ZrTzTp7JXEFxcHKukVB5cJuqL4q3eoBlGz3nZaYpDLv3CKCDJLePPrYaL04I6KjM4GUq%22%2C%22version%22%3A3%2C%22domain%22%3A%22nibmworldwide.com%22%2C%22ts%22%3A1721255742141%7D; _ga_8Y39KNRBM5=GS1.2.1721254228.3.1.1721255746.8.0.205452717");

                httpClient.DefaultRequestHeaders.Add("Cache-Control", "max-age=0");
                httpClient.DefaultRequestHeaders.Add("Sec-Ch-Ua", "\"Not/A)Brand\";v=\"8\", \"Chromium\";v=\"126\"");
                httpClient.DefaultRequestHeaders.Add("Sec-Ch-Ua-Mobile", "?0");
                httpClient.DefaultRequestHeaders.Add("Sec-Ch-Ua-Platform", "\"Windows\"");
                httpClient.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("en-US"));
                httpClient.DefaultRequestHeaders.Add("Upgrade-Insecure-Requests", "1");
                httpClient.DefaultRequestHeaders.Add("Origin", "null");
                httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/126.0.6478.127 Safari/537.36");
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/html"));
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xhtml+xml"));
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml", 0.9));
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("image/avif"));
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("image/webp"));
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("image/apng"));
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*", 0.8));
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/signed-exchange", 0.7));
                httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Site", "same-origin");
                httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Mode", "navigate");
                httpClient.DefaultRequestHeaders.Add("Sec-Fetch-User", "?1");
                httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Dest", "document");
                //httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
                //httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));
                //httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("br"));
                httpClient.DefaultRequestHeaders.Add("Priority", "u=0, i");

                var content = new StringContent("F%5BProgramme%5D=2591&F%5BBatch%5D=6960&F%5BStudent%5D=84391&CK=890635be6a5dda6b650a5aa51586e5641ba07f0f", Encoding.UTF8, "application/x-www-form-urlencoded");

                HttpResponseMessage response = httpClient.PostAsync(path, content).Result;

                if (response.IsSuccessStatusCode)
                {

                    string htmlContent = response.Content.ReadAsStringAsync().Result;
                    var htmlDoc = new HtmlDocument();
                    htmlDoc.LoadHtml(htmlContent);
                    var jsonObject = new JObject();


                    var form = htmlDoc.DocumentNode.SelectSingleNode("//form[@id='resform']");
                    if (form != null)
                    {

                        var select = form.SelectSingleNode("//select[@name='F[Batch]']");
                        if (select != null)
                        {
                            var options = select.SelectNodes("option");
                            var optionsArray = new JArray();
                            foreach (var option in options)
                            {
                                var optionValue = option.GetAttributeValue("value", "");
                                var optionText = option.InnerText.Trim();

                                var innerHtmlDoc = new HtmlDocument();
                                innerHtmlDoc.LoadHtml(option.InnerHtml);
                                var textNode = innerHtmlDoc.DocumentNode.InnerText.Trim();

                                var optionObject = new JObject
                                {
                                    ["value"] = optionValue,
                                    ["text"] = string.IsNullOrEmpty(optionText) ? textNode : optionText
                                };
                                optionsArray.Add(optionObject);
                            }
                            jsonObject["Programmes"] = optionsArray;
                        }
                    }


                    System.Diagnostics.Debug.WriteLine(jsonObject.ToString());
                    return jsonObject;




                    //string dataset = response.Content.ReadAsStringAsync().Result;
                    ////System.Diagnostics.Debug.WriteLine(dataset);

                    //var htmlDoc = new HtmlDocument();
                    //htmlDoc.LoadHtml(dataset);

                    //// Extract the content inside the <body> tag
                    //var bodyNode = htmlDoc.DocumentNode.SelectSingleNode("//body");
                    //string bodyContent = "";
                    //if (bodyNode != null)
                    //{
                    //    bodyContent = bodyNode.InnerHtml;
                    //}
                    //else
                    //{
                    //    return "Body tag not found!";
                    //}

                    //var jsonObject = JsonConvert.DeserializeObject<object>(bodyContent);
                    //return jsonObject;
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine(response.StatusCode);
                    return "not found!";
                }
            }
        }
        public object getData(string con, string path)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(con);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Add("Cookie", "_ga=GA1.2.823271897.1721246274; _gid=GA1.2.2047118751.1721246274; twk_idm_key=uI4l4is7BBP4bHh7gB8f0; TawkConnectionTime=0; twk_uuid_5f6c82264704467e89f1ee75=%7B%22uuid%22%3A%221.92OoxUrMGNfGvbmd4OAARBZLGcTlluRC4aKi61QtgVhqtx0U6Cyw7UV5ZrTzTp7JXEFxcHKukVB5cJuqL4q3eoBlGz3nZaYpDLv3CKCDJLePPrYaL04I6KjM4GUq%22%2C%22version%22%3A3%2C%22domain%22%3A%22nibmworldwide.com%22%2C%22ts%22%3A1721255742141%7D; _ga_8Y39KNRBM5=GS1.2.1721254228.3.1.1721255746.8.0.205452717");

                httpClient.DefaultRequestHeaders.Add("Cache-Control", "max-age=0");
                httpClient.DefaultRequestHeaders.Add("Sec-Ch-Ua", "\"Not/A)Brand\";v=\"8\", \"Chromium\";v=\"126\"");
                httpClient.DefaultRequestHeaders.Add("Sec-Ch-Ua-Mobile", "?0");
                httpClient.DefaultRequestHeaders.Add("Sec-Ch-Ua-Platform", "\"Windows\"");
                httpClient.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("en-US"));
                httpClient.DefaultRequestHeaders.Add("Upgrade-Insecure-Requests", "1");
                httpClient.DefaultRequestHeaders.Add("Origin", "null");
                httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/126.0.6478.127 Safari/537.36");
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/html"));
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xhtml+xml"));
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml", 0.9));
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("image/avif"));
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("image/webp"));
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("image/apng"));
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*", 0.8));
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/signed-exchange", 0.7));
                httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Site", "same-origin");
                httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Mode", "navigate");
                httpClient.DefaultRequestHeaders.Add("Sec-Fetch-User", "?1");
                httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Dest", "document");
                //httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
                //httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));
                //httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("br"));
                httpClient.DefaultRequestHeaders.Add("Priority", "u=0, i");

                var content = new StringContent("F%5BProgramme%5D=2591&F%5BBatch%5D=6960&F%5BStudent%5D=84391&CK=890635be6a5dda6b650a5aa51586e5641ba07f0f", Encoding.UTF8, "application/x-www-form-urlencoded");

                HttpResponseMessage response = httpClient.PostAsync(path, content).Result;

                if (response.IsSuccessStatusCode)
                {
                    string htmlContent = response.Content.ReadAsStringAsync().Result;
                    var htmlDoc = new HtmlDocument();
                    htmlDoc.LoadHtml(htmlContent);
                    var jsonObject = new JObject();


                    jsonObject["Programmes"] = ExtractOptions(htmlDoc, "//select[@name='F[Programme]']");
                    jsonObject["Batches"] = ExtractOptions(htmlDoc, "//select[@name='F[Batch]']");
                    jsonObject["Students"] = ExtractOptions(htmlDoc, "//select[@name='F[Student]']");


                    var tableData = new JArray();
                    var tableRows = htmlDoc.DocumentNode.SelectNodes("//table[@class='dg c']//tbody//tr");
                    if (tableRows != null)
                    {
                        foreach (var row in tableRows)
                        {
                            var cells = row.SelectNodes("td");
                            if (cells != null)
                            {
                                var rowObject = new JObject
                                {
                                    ["Number"] = cells[0].InnerText.Trim(),
                                    ["Subject"] = cells[1].InnerText.Trim(),
                                    ["Special"] = cells[2].InnerText.Trim(),
                                    ["ExamDate"] = cells[3].InnerText.Trim(),
                                    ["CourseWork"] = cells[4].InnerText.Trim(),
                                    ["Exam"] = cells[5].InnerText.Trim(),
                                    ["FinalGrade"] = cells[6].InnerText.Trim(),
                                    ["Points"] = cells[7].InnerText.Trim()
                                };
                                tableData.Add(rowObject);
                            }
                        }
                    }
                    jsonObject["TableData"] = tableData;

                    System.Diagnostics.Debug.WriteLine(jsonObject.ToString());
                    return jsonObject;
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine(response.StatusCode);
                    return "not found!";
                }
            }
        }
        private JArray ExtractOptions(HtmlDocument htmlDoc, string xpath)
        {
            var select = htmlDoc.DocumentNode.SelectSingleNode(xpath);
            var optionsArray = new JArray();
            if (select != null)
            {
                var options = select.SelectNodes("option");
                if (options != null)
                {
                    foreach (var option in options)
                    {
                        var optionValue = option.GetAttributeValue("value", "");
                        var optionText = option.InnerText.Trim();

                        var optionObject = new JObject
                        {
                            ["value"] = optionValue,
                            ["text"] = optionText
                        };
                        optionsArray.Add(optionObject);
                    }
                }
            }
            return optionsArray;
        }
    }
}
