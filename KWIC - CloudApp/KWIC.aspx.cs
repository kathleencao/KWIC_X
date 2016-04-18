using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection.Emit;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;


namespace KWIC___CloudApp
{
    public partial class KWIC : System.Web.UI.Page
    {
        public string txt;
        public string[] sub;
        public string[] lst;
        public string[] getLst;
        public string jsonString;
        JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();

        private class surl
        {
            public string sString { get; set; }

            public string sUrl { get; set; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            sub = new string[] { };
        }


        protected async void Button1_Click(object sender, EventArgs e)
        {

            txt = TextBox1.Text;
            //array of each line
            lst = txt.Split(new Char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            var surls = new List<surl>();
            foreach (string t in lst)
            {
                sub = t.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries);
                sub = t.Split((string[])null, StringSplitOptions.RemoveEmptyEntries);
                sub = t.Split(null as string[], StringSplitOptions.RemoveEmptyEntries);

                var first = new StringBuilder();
                for (int j = 0; j < sub.Length - 1; ++j)
                {
                    first.Append(sub[j]);
                    if (j < sub.Length - 2)
                    {
                        first.Append(" ");
                    }
                }

                surls.Add(new surl() { sString = first.ToString(), sUrl = sub[sub.Length - 1] });
            }

            jsonString = javaScriptSerializer.Serialize(surls);
            jsonString = jsonString.Replace("sString", "string").Replace("sUrl", "url");

            using (var client = new HttpClient())
            {
                string resourceAddress = "http://busyadmin.net:3000/insert";
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.PostAsync(resourceAddress, new StringContent(jsonString, Encoding.UTF8, "application/json"));
                await DisplayTextResult(response, ListBox1);
            }

        }

        private async Task DisplayTextResult(HttpResponseMessage response, ListBox output)
        {
            string responJsonText = await response.Content.ReadAsStringAsync();
            //GetJsonValue(responJsonText);

            var n = javaScriptSerializer.Deserialize<List<surl>>(responJsonText.Replace("purl", "sUrl").Replace("pstring", "sString"));

            foreach (var s in n)
            {
                output.Items.Add(s.sString + " " + s.sUrl);
            }

        }

        private async Task DisplayTextResult2(HttpResponseMessage response, ListBox output)
        {
            string responJsonText = await response.Content.ReadAsStringAsync();
            var n = responJsonText.Replace("\"", "").Replace("\\","").Split(' ');
            //GetJsonValue(responJsonText);

            foreach (var s in n)
            {
                output.Items.Add(s);
            }

        }


        protected async void Button2_Click(object sender, EventArgs e)
        {

            string[] sub = TextBox2.Text.Split(',');

            jsonString = javaScriptSerializer.Serialize(sub);

            using (var client = new HttpClient())
            {
                string resourceAddress = "http://busyadmin.net:3000/search";
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.PostAsync(resourceAddress, new StringContent(jsonString, Encoding.UTF8, "application/json"));
                await DisplayTextResult2(response, ListBox2);
            }

            //JSON POST/////////////////////////////////////////// 
            Response.Write(jsonString); //Post to PERL
            //JSON POST///////////////////////////////////////////

            //JSON GET////////////////////////////////////////////
            //string[] l = (string[])javaScriptSerializer.Deserialize(jsonString, typeof(string[])); //Get from PERL to jsonObject
            //JSON GET////////////////////////////////////////////

            //Appends array to list//////////////////////////////////////////
            //for (int t = 0; t < l.Length; t++)
            //{
            //    if (l.Contains(TextBox2.Text))
            //    {
            //        ListBox2.Items.Add(l[t]);
            //    }
            //}
            //Appends array to list//////////////////////////////////////////
        }

        protected async void Button3_Click(object sender, EventArgs e)
        {

            string sub =  "clear";

            jsonString = javaScriptSerializer.Serialize(sub);

            using (var client = new HttpClient())
            {
                string resourceAddress = "http://busyadmin.net:3000/clear";
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.PostAsync(resourceAddress, new StringContent(jsonString, Encoding.UTF8, "application/json"));
            }

            //JSON POST/////////////////////////////////////////// 
            Response.Write(jsonString); //Post to PERL
            //JSON POST///////////////////////////////////////////

            //JSON GET////////////////////////////////////////////
            //string[] l = (string[])javaScriptSerializer.Deserialize(jsonString, typeof(string[])); //Get from PERL to jsonObject
            //JSON GET////////////////////////////////////////////

            //Appends array to list//////////////////////////////////////////
            //for (int t = 0; t < l.Length; t++)
            //{
            //    if (l.Contains(TextBox2.Text))
            //    {
            //        ListBox2.Items.Add(l[t]);
            //    }
            //}
            //Appends array to list//////////////////////////////////////////
        }

    }
}

