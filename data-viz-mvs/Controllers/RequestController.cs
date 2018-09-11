﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace dataviz.Controllers
{
    public class Dataset //Automatically generated by http://json2csharp.com/ from dummy Request
    {
        public int id { get; set; }
        public string dataset_code { get; set; }
        public string database_code { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public DateTime refreshed_at { get; set; }
        public string newest_available_date { get; set; }
        public string oldest_available_date { get; set; }
        public List<string> column_names { get; set; }
        public string frequency { get; set; }
        public string type { get; set; }
        public bool premium { get; set; }
        public object limit { get; set; }
        public object transform { get; set; }
        public object column_index { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }
        public List<List<object>> data { get; set; }
        public string collapse { get; set; }
        public string order { get; set; }
        public int database_id { get; set; }
    }

    public class RootObject
    {
        public Dataset dataset { get; set; }
    }

    public class RequestController : Controller
    {
        // GET: /Request/
        static HttpClient client = new HttpClient();

        private static string quandlTemplate = "https://www.quandl.com/api/v3/datasets/{0}/{1}.json";
        private static string apiKey = "DKczFdjuL_16KZVxeZKk";



        public async Task<ActionResult> dummyRequest()
        {
            
            string[] required = { "database_code", "dataset_code", "start_date", "end_date"};
            foreach (string req in required){
                if (Request.QueryString[req] == null)
                    return View("Error");
             }
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string url = String.Format(quandlTemplate, Request.QueryString["database_code"], Request.QueryString["dataset_code"]);
            
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("api_key", apiKey);
            dict.Add("start_date", Request.QueryString["start_date"]);
            dict.Add("end_date", Request.QueryString["end_date"]);
            if(Request.QueryString["order"] != null)
                dict.Add("order", Request.QueryString["order"]);
            if (Request.QueryString["collapse"] != null)
                dict.Add("collapse", Request.QueryString["collapse"]);
            if (Request.QueryString["transformation"] != null)
                dict.Add("transformation", Request.QueryString["transformation"]);
            if (Request.QueryString["limit"] != null && Int32.Parse(Request.QueryString["limit"]) > 0)
                dict.Add("limit", Request.QueryString["limit"]);
            if (Request.QueryString["column_index"] != null && Int32.Parse(Request.QueryString["column_index"]) >= 0)
                dict.Add("column_index", Request.QueryString["column_index"]);
            bool first = true;
            foreach(var pair in dict)
            {
                if (first){
                    first = false;
                    url +=  "?";
                }
                else 
                    url += "&";
                url += pair.Key + "=" + pair.Value;
            }
            
            string str = null;
            
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                str = await response.Content.ReadAsStringAsync();
            }

            var json = JsonConvert.DeserializeObject<RootObject>(str);

            
            Debug.WriteLine(str);


            Debug.WriteLine(json);
            
            JsonResult ret = Json(json, JsonRequestBehavior.AllowGet);
 
            Response.ContentType = "application/json";
                
            return ret;
        }


    }
}
