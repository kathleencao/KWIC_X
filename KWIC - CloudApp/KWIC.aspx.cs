using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;

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

        protected void Page_Load(object sender, EventArgs e)
        {
         sub = new string[] { };
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            txt = TextBox1.Text;
            //array of each line
            lst = txt.Split(new Char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries); 

            for (int i = 0; i < lst.Length; i++)
            {
                //Find Substring/////////////////////////////////////////////////////////////
                sub = lst[i].Split(default(string[]), StringSplitOptions.RemoveEmptyEntries);
                sub = lst[i].Split((string[])null, StringSplitOptions.RemoveEmptyEntries);
                sub = lst[i].Split(null as string[], StringSplitOptions.RemoveEmptyEntries);
                //Find Substring/////////////////////////////////////////////////////////////

                //JSON POST/////////////////////////////////////////// 
                jsonString = javaScriptSerializer.Serialize(sub);
                Response.Write(jsonString); //Post to PERL
                //JSON POST///////////////////////////////////////////

                //JSON GET////////////////////////////////////////////
                string[] l = (string[])javaScriptSerializer.Deserialize(jsonString, typeof(string[])); //Get from PERL to jsonObject
                //JSON GET////////////////////////////////////////////

                //Appends array to list//////////////////////////////////////////
                for (int t = 0; t < l.Length; t++)
                {
                    if (l.Contains(TextBox2.Text))
                    {
                        ListBox2.Items.Add(l[t]);
                    }
                }
                //Appends array to list//////////////////////////////////////////


            }
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            //JSON POST/////////////////////////////////////////// 
            jsonString = javaScriptSerializer.Serialize(sub);
            Response.Write(jsonString); //Post to PERL
            //JSON POST///////////////////////////////////////////

            //JSON GET////////////////////////////////////////////
            string[] l = (string[])javaScriptSerializer.Deserialize(jsonString, typeof(string[])); //Get from PERL to jsonObject
            //JSON GET////////////////////////////////////////////

            //Appends array to list//////////////////////////////////////////
            for (int t = 0; t < l.Length; t++)
            {
                if (l.Contains(TextBox2.Text))
                {
                    ListBox2.Items.Add(l[t]);
                }
            }
            //Appends array to list//////////////////////////////////////////
        }

    }
}

