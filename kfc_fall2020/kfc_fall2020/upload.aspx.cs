using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using KFC.App_Code;
using commonapp.App_Code;

namespace kfc_fall2020
{
    public partial class upload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                HttpPostedFile filev = FileUpload1.PostedFile;

            
                int iFileSize = filev.ContentLength;


                if (iFileSize > 15728640)  // 15MB
                {
                    Label1.Text = "The file size is too big   ";
                    Label1.ForeColor = System.Drawing.Color.Red;
                    // File exceeds the file maximum size
                
                }
                else
                {
                    string str = FileUpload1.FileName;
                    FileUpload1.PostedFile.SaveAs(Server.MapPath("~/Upload2/" + str));
                    string file = "~/Upload2/" + str.ToString();
                    string name = TextBox1.Text;
                    CRUD myCrud = new CRUD();
                    string mySql = @"INSERT upload(name,file_)
                                 VALUES(@name,@file)";
                    Dictionary<string, object> myPara = new Dictionary<string, object>();
                    myPara.Add("@name", name);
                    myPara.Add("@file", file);
                    common mycommon = new common();
                    int rtn = myCrud.InsertUpdateDelete(mySql, myPara);
                    Label1.Text = mycommon.sucsessmessage(rtn);
                    if (rtn == 1) { Label1.ForeColor = System.Drawing.Color.Green; }
                    else { Label1.ForeColor = System.Drawing.Color.Red; }
                }


            }

            else
            {
                Label1.Text = "Please Upload your file";
                Label1.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}