using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.UI;
using commonapp.App_Code;
using System.Web.UI.WebControls;
using KFC.App_Code;
using System.IO;
using System.Web;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.Threading;

namespace kfc_fall2020
{
    public partial class Cruderege : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                populateGv();
                populateDepCombo();
                populateDepCombo1();
                popdepddl();
                popgenderddl();
            }
        }
        //edit mode on 
        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            populateGv();

        }
        //grid view deleting row button 
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            CRUD myCrud = new CRUD();
            common mycommon = new common();
            string mySql = @"DELETE FROM  patient_info WHERE p_id=@p_id;";
            Dictionary<string, object> myPara = new Dictionary<string, object>();
            myPara.Add("@p_id", GridView1.DataKeys[e.RowIndex].Value);
            int rtn = myCrud.InsertUpdateDelete(mySql, myPara);
            inout.Text = mycommon.sucsessmessage(rtn);
            if (rtn == 1) { inout.ForeColor = System.Drawing.Color.Green; }
            else { inout.ForeColor = System.Drawing.Color.Red; }
            populateGv();
        }
        //cancel update for row 
        protected void OnRowCancelingEdit(object sender, EventArgs e)
        {
            GridView1.EditIndex = -1;
            populateGv();
        }
        //bind data while edting row 
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {   //gender dropdownlist 
                    DropDownList drop = (DropDownList)e.Row.FindControl("DropDownListGdr") as DropDownList;
                    CRUD myCrud = new CRUD();
                    string mySql2 = @"select * from gendertb";
                    using (SqlDataReader dr = myCrud.getDrPassSql(mySql2))
                    {
                        drop.DataSource = dr;
                        drop.DataValueField = "p_gender_id";
                        drop.DataTextField = "p_gender";
                        drop.DataBind();
                        drop.SelectedValue = DataBinder.Eval(e.Row.DataItem, "p_gender_id").ToString();
                    }

                    //department dropdownlist
                    CRUD myCrud1 = new CRUD();
                    DropDownList dep = (DropDownList)e.Row.FindControl("DropDownListDep") as DropDownList;
                    string mySql3 = @"select * from department1";
                    using (SqlDataReader dr2 = myCrud1.getDrPassSql(mySql3))
                    {
                        dep.DataSource = dr2;
                        dep.DataValueField = "p_dep_id";
                        dep.DataTextField = "p_dep";
                        dep.DataBind();
                        dep.SelectedValue = DataBinder.Eval(e.Row.DataItem, "p_dep_id").ToString();
                    }



                }
            }
        }
        //this method for updating in gridview rows 
        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            DropDownList drop = (DropDownList)GridView1.Rows[e.RowIndex].FindControl("DropDownListGdr");
            DropDownList dropdep = (DropDownList)GridView1.Rows[e.RowIndex].FindControl("DropDownListDep");
            //update when click in row update button 
            CRUD myCrud = new CRUD();
            string mySql = @"update patient_info set p_name =@p_name, p_age=@age , p_nationalid_iqama=@national_id,
                               p_gender_id=@genderid,p_dep_id = @depid 
                               where p_id=@p_id";

            Dictionary<string, object> myPara = new Dictionary<string, object>();
            myPara.Add("@p_id", GridView1.DataKeys[e.RowIndex].Value);
            myPara.Add("@p_name", ((TextBox)(GridView1.Rows[e.RowIndex].Cells[1].Controls[0])).Text);
            myPara.Add("@age", ((TextBox)(GridView1.Rows[e.RowIndex].Cells[2].Controls[0])).Text);
            myPara.Add("@national_id", ((TextBox)(GridView1.Rows[e.RowIndex].Cells[3].Controls[0])).Text);
            myPara.Add("@genderid", int.Parse(drop.SelectedValue));
            myPara.Add("@depid", int.Parse(dropdep.SelectedValue));
            common mycommon = new common();
            int rtn = myCrud.InsertUpdateDelete(mySql, myPara);
            inout.Text = mycommon.sucsessmessage(rtn);
            if (rtn == 1) { inout.ForeColor = System.Drawing.Color.Green; }
            else { inout.ForeColor = System.Drawing.Color.Red; }
            GridView1.EditIndex = -1;
            populateGv();
        }

        //ADD Button 
        protected void addbtn_Click(object sender, EventArgs e)
        {
            common mycommon = new common();
            if (mycommon.checkredundcy(Tbxid.Text) != 1)
            {
                inout.Text = mycommon.checkfield(Tbxnm.Text, Tbxage.Text, Tbxid.Text, rdbtn1.SelectedIndex, ddlst.SelectedIndex);
                if (!String.Equals(inout.Text, "")) { inout.ForeColor = System.Drawing.Color.Red;
                    return;
                }
                else
                {
                    // ------
                    int rtn = mycommon.add(Tbxnm.Text, Tbxage.Text, Tbxid.Text, int.Parse(rdbtn1.SelectedValue), int.Parse(ddlst.SelectedValue));
                    inout.Text = mycommon.sucsessmessage(rtn);
                    if (rtn == 1) { inout.ForeColor = System.Drawing.Color.Green; }
                    else { inout.ForeColor = System.Drawing.Color.Red; }
                    clearinput();
                    populateGv();

                }

            }
            else {
                inout.Text = "The National ID / IQama is already exsist  ...!";
                inout.ForeColor = System.Drawing.Color.Red;
            }

        }
        // Bind  Data in Gridview 
        protected void populateGv()
        {
            CRUD myCrud = new CRUD();
            string mySql = @"
             SELECT p.p_id, p.p_name,p.p_nationalid_iqama,p.p_age,g.p_gender,g.p_gender_id,d.p_dep,d.p_dep_id
              FROM patient_info p
              INNER JOIN department1 d ON p.p_dep_id = d.p_dep_id 
              INNER JOIN gendertb g ON g.p_gender_id = p.p_gender_id";
            using (SqlDataReader dr = myCrud.getDrPassSql(mySql))
            {
                GridView1.DataSource = dr;
                GridView1.DataBind();
            }
        }
        //Bind Data with DropDownList Button(Department)
        protected void populateDepCombo()
        {
            CRUD myCrud = new CRUD();
            string mySql = @"select p_dep_id, p_dep from department1";
            using (SqlDataReader dr = myCrud.getDrPassSql(mySql))
            {

                ddlst.DataTextField = "p_dep";
                ddlst.DataValueField = "p_dep_id";
                ddlst.DataSource = dr;
                ddlst.DataBind();
                ddlst.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "NA"));
            }
        }
        //  Bind Data with Radio Button ( Gender)
        protected void populateDepCombo1()
        {
            CRUD myCrud = new CRUD();
            string mySql = @"select p_gender_id, p_gender from gendertb";
            using (SqlDataReader dr = myCrud.getDrPassSql(mySql))
            {
                rdbtn1.DataTextField = "p_gender";
                rdbtn1.DataValueField = "p_gender_id";
                rdbtn1.DataSource = dr;
                rdbtn1.DataBind();
            }
        }


        //this method for puting data in text box when user click #fileno
        protected void populateForm_Click(object sender, EventArgs e)
        {
            string mySql = @"select p_id,p_name,p_nationalid_iqama,p_age,p_gender_id,p_dep_id
					 from patient_info where p_id=@p_id";
            Dictionary<string, object> myPara = new Dictionary<string, object>();
            myPara.Add("@p_id", int.Parse((sender as LinkButton).CommandArgument));
            CRUD myCrud = new CRUD();
            using (SqlDataReader dr = myCrud.getDrPassSql(mySql, myPara)){
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Tbxfile.Text = dr["p_id"].ToString();
                        Tbxnm.Text = dr["p_name"].ToString();
                        Tbxage.Text = dr["p_age"].ToString();
                        Tbxid.Text = dr["p_nationalid_iqama"].ToString();
                        rdbtn1.SelectedValue = dr["p_gender_id"].ToString();
                        ddlst.SelectedValue = dr["p_dep_id"].ToString();

                    }
                }
            }
        }
        //update when click 
        protected void upbtn_Click(object sender, EventArgs e)
        {
            //update >> 

            if (!String.IsNullOrEmpty(Tbxfile.Text))
            {
                common mycommon = new common();

                inout.Text = mycommon.checkfield(Tbxnm.Text, Tbxage.Text, Tbxid.Text, rdbtn1.SelectedIndex, ddlst.SelectedIndex);
                if (!String.Equals(inout.Text, ""))
                {
                    inout.ForeColor = System.Drawing.Color.Red;
                    return;
                }
                else
                {

                    string mySql = @"update patient_info set p_name =@p_name, p_age=@age , p_nationalid_iqama=@national_id,
                        p_gender_id=@genderid,p_dep_id = @depid 
                        where p_id=@p_id";
                    Dictionary<string, object> myPara = new Dictionary<string, object>();
                    myPara.Add("@p_id", int.Parse(Tbxfile.Text));
                    myPara.Add("@p_name", Tbxnm.Text);
                    myPara.Add("@age", Tbxage.Text);
                    myPara.Add("@national_id", Tbxid.Text);
                    myPara.Add("@genderid", int.Parse(rdbtn1.SelectedValue));
                    myPara.Add("@depid", int.Parse(ddlst.SelectedValue));
                    CRUD myCrud = new CRUD();
                    int rtn = myCrud.InsertUpdateDelete(mySql, myPara);
                    inout.Text = mycommon.sucsessmessage(rtn);
                    if (rtn == 1) { inout.ForeColor = System.Drawing.Color.Green; }
                    else { inout.ForeColor = System.Drawing.Color.Red; }
                    clearinput();
                    populateGv();
                }
            }
            else
            {
                inout.Text = "Please Click on a FileNo to Modify it ...!";
                inout.ForeColor = System.Drawing.Color.Red;

            }

        }
        //Delete Button 
        protected void delbtn_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Tbxfile.Text))
            {
                common mycommon = new common();
                CRUD myCrud = new CRUD();
                string mySql = @"DELETE FROM  patient_info WHERE p_id=@p_id;";
                Dictionary<string, object> myPara = new Dictionary<string, object>();
                myPara.Add("@p_id", Tbxfile.Text);
                int rtn = myCrud.InsertUpdateDelete(mySql, myPara);
                inout.Text = mycommon.sucsessmessage(rtn);
                if (rtn == 1) { inout.ForeColor = System.Drawing.Color.Green; }
                else { inout.ForeColor = System.Drawing.Color.Red; }
                clearinput();
                populateGv();
            }

            else
            {
                inout.Text = "Please Click on a FileNo to Modify it ...!";
                inout.ForeColor = System.Drawing.Color.Red;

            }

        }
        /*  --- Select Button based on what textbox or dropdownlist is filled 
             if more than one textbox is filled it will choos by pirioty 
          -- Pirioty :
            1- Name 
            2- Age 
            3- Id 
            4- Gender 
            5- Department 
        */
        protected void slbtn_Click(object sender, EventArgs e)

        {
            selectlabel.Text = "Instruction:" + "<br>" + "To Select Record based on what textbox or dropdownlist  "
                               + "<br>" + " make sure all other field is empty or it will choos by pirioty "
                               + "<br>" + "  -- Pirioty " + "<br>" + "1-Name" + "<br>" + "2-Age" + "<br>" + "3-ID" + "<br>"
                               + "4-Gender" + "<br>" + "5-Department";
            common mycommon = new common();
            CRUD myCrud = new CRUD();
            SqlDataReader dr;
            Dictionary<string, object> myPara = new Dictionary<string, object>();
            if (!string.IsNullOrEmpty(Tbxnm.Text)) {
                myPara.Add("@p_name", Tbxnm.Text);
                dr = myCrud.getDrPassSql(mycommon.selectbytype(1), myPara);
                GridView1.DataSource = dr;
                GridView1.DataBind();
                inout.Text = "";
            }
            else if (!string.IsNullOrEmpty(Tbxage.Text))
            {
                myPara.Add("@p_age", Tbxage.Text);
                dr = myCrud.getDrPassSql(mycommon.selectbytype(2), myPara);
                GridView1.DataSource = dr;
                GridView1.DataBind();
                inout.Text = "";
            }
            else if (!string.IsNullOrEmpty(Tbxid.Text))
            {
                myPara.Add("@p_nationalid", Tbxid.Text);
                dr = myCrud.getDrPassSql(mycommon.selectbytype(3), myPara);
                GridView1.DataSource = dr;
                GridView1.DataBind();
                inout.Text = "";
            }

            else if (rdbtn1.SelectedIndex != -1)
            {
                myPara.Add("@p_gender_id", int.Parse(rdbtn1.SelectedValue));
                dr = myCrud.getDrPassSql(mycommon.selectbytype(4), myPara);
                GridView1.DataSource = dr;
                GridView1.DataBind();
                inout.Text = "";
            }
            else if (ddlst.SelectedIndex != 0)
            {
                myPara.Add("@p_dep_id", int.Parse(ddlst.SelectedValue));
                dr = myCrud.getDrPassSql(mycommon.selectbytype(5), myPara);
                GridView1.DataSource = dr;
                GridView1.DataBind();
                inout.Text = "";
            }
            else {
                inout.Text = "Enter Select type  !";
                inout.ForeColor = System.Drawing.Color.Red;
            }




        }

        //code for the add button above gridview 
        protected void gvaddbtn_Click(object sender, EventArgs e)
        {
            common mycommon = new common();
            if (mycommon.checkredundcy(idadd.Text) != 1)
            {
                inout.Text = mycommon.checkfield(nameadd.Text, ageadd.Text, idadd.Text, ddlgdradd.SelectedIndex, ddldepadd.SelectedIndex);
                if (!String.Equals(inout.Text, ""))
                {
                    inout.ForeColor = System.Drawing.Color.Red;
                    return;
                }
                else
                {
                    // ------
                    int rtn = mycommon.add(nameadd.Text, ageadd.Text, idadd.Text, int.Parse(ddlgdradd.SelectedValue), int.Parse(ddldepadd.SelectedValue));
                    inout.Text = mycommon.sucsessmessage(rtn);
                    if (rtn == 1) { inout.ForeColor = System.Drawing.Color.Green; }
                    else { inout.ForeColor = System.Drawing.Color.Red; }
                    clearinput();
                    populateGv();

                }

            }
            else
            {
                inout.Text = "The National ID / IQama is already exsist  ...!";
                inout.ForeColor = System.Drawing.Color.Red;
            }
        }

        //bind ddl above the gridview 

        protected void popdepddl()
        {
            CRUD myCrud = new CRUD();
            string mySql = @"select p_dep_id, p_dep from department1";
            using (SqlDataReader dr = myCrud.getDrPassSql(mySql))
            {

                ddldepadd.DataTextField = "p_dep";
                ddldepadd.DataValueField = "p_dep_id";
                ddldepadd.DataSource = dr;
                ddldepadd.DataBind();
                ddldepadd.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Dep", "NA"));
            }
        }
        //  Bind Data with Radio Button ( Gender)
        protected void popgenderddl()
        {
            CRUD myCrud = new CRUD();
            string mySql = @"select p_gender_id, p_gender from gendertb";
            using (SqlDataReader dr = myCrud.getDrPassSql(mySql))
            {
                ddlgdradd.DataTextField = "p_gender";
                ddlgdradd.DataValueField = "p_gender_id";
                ddlgdradd.DataSource = dr;
                ddlgdradd.DataBind();
                ddlgdradd.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Gender", "NA"));
            }
        }

        //reset all input and display gridview 
        protected void resetbtn_Click(object sender, EventArgs e)
        {
            inout.Text = "";
            clearinput();
            populateGv();
        }

        //method to clear input
        protected void clearinput()
        {
            Tbxfile.Text = "";
            Tbxnm.Text = "";
            Tbxage.Text = "";
            Tbxid.Text = "";
            nameadd.Text = "";
            ageadd.Text = "";
            idadd.Text = "";
            toemailtxt.Text = "";
            selectlabel.Text = "";
            rdbtn1.SelectedIndex = -1;
            ddlst.SelectedIndex = -1;
            ddlgdradd.SelectedIndex = -1;
            ddldepadd.SelectedIndex = -1;


        }
        protected void export_Click(object sender, EventArgs e)
        {
            switch (ddlexport.SelectedValue)
            {
                case "Word":
                    ExportGridToword();
                    break;

                case "Excel":
                    ExportGridToExcel();
                    break;
                case "PDF":
                    ExportGridToPDF();
                    break;

                default:
                    inout.Text = "  Select Export Type !";
                    inout.ForeColor = System.Drawing.Color.Red;
                    break;

            }


        }

        public void ExportGridToExcel()
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            string FileName = "Patient info " + DateTime.Now + ".xls";
            using (StringWriter strwritter = new StringWriter())
            {
                using (HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter))
                {
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.ContentType = "application/vnd.ms-excel";
                    Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
                    GridView1.GridLines = GridLines.Both;
                    GridView1.HeaderStyle.Font.Bold = true;
                    GridView1.RenderControl(htmltextwrtter);
                    Response.Write(strwritter.ToString());
                    Response.End();
                }
            }
        }
        public void ExportGridToword()
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            string FileName = "Patient info" + DateTime.Now + ".doc";
            using (StringWriter strwritter = new StringWriter())
            {
                using (HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter))
                {
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.ContentType = "application/msword";
                    Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
                    GridView1.GridLines = GridLines.Both;
                    GridView1.HeaderStyle.Font.Bold = true;
                    GridView1.RenderControl(htmltextwrtter);
                    Response.Write(strwritter.ToString());
                    Response.End();
                }
            }

        }
        //export to pdf with background color 

        public void ExportGridToPDF()
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    //To Export all pages
                    GridView1.AllowPaging = false;
                  

                    GridView1.RenderControl(hw);
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A2, 10f, 10f, 10f, 0f);
                    HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                    PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    htmlparser.Parse(sr);
                    pdfDoc.Close();

                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=Patient_info.pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Write(pdfDoc);
                    Response.End();
                }
            }
        }
        protected void emailgv_Click(object sender, EventArgs e)
        {
               //email info to send 
                String fromemail = "kfmcteest@gmail.com";
                String myps = "Test123456!";//password
                String toemail = toemailtxt.Text;

                if (!common.IsValidEmail(toemail))
                {
                    inout.Text = "Please enter email with correct fromat . youremail@example.com ";
                    inout.ForeColor = System.Drawing.Color.Red;
                    return;
                }
                MailMessage mail = new MailMessage(fromemail, toemail);//create MailMessage class object
                if (string.IsNullOrEmpty(toemailtxt.Text) )                {
                    inout.Text = "Please Enter To-email to send GV  ";
                    inout.ForeColor = System.Drawing.Color.Red;
                    return;
                }
                
                mail.Subject = "GridView Content ";
                mail.Body = getHTML(GridView1); // gridview content in the body of message
                mail.IsBodyHtml = false;
                SmtpClient smtp = new SmtpClient();//Creating Smtp class objec
                smtp.Host = "smtp.gmail.com";
                smtp.UseDefaultCredentials = false;
                smtp.EnableSsl = true;
                smtp.Port = 587;

           
                //try to send email and validate if there is error in sending 
                try
                {   
                    NetworkCredential NetworkCred = new NetworkCredential(fromemail, myps);//set Network Credential
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;
                    smtp.Send(mail);

                    inout.Text = "Sucsess";
                    inout.ForeColor = System.Drawing.Color.Green;
                }
                catch (SmtpFailedRecipientException ex)
                {
                    SmtpStatusCode statusCode = ex.StatusCode;
                    if (statusCode == SmtpStatusCode.MailboxBusy || statusCode == SmtpStatusCode.MailboxUnavailable || statusCode == SmtpStatusCode.TransactionFailed)
                    {   // wait 5 seconds, try a second time
                        Thread.Sleep(5000);
                        smtp.Send(mail);
                        inout.Text = ex.Message.ToString();
                        inout.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (Exception ex)
                {
                    inout.Text = ex.ToString();
                    inout.ForeColor = System.Drawing.Color.Red;
                }
                finally
                {
                    mail.Dispose();
                }
                clearinput();
            }
          

        // make gridview as a string  (HTML TAG)
        public  string getHTML(GridView gv)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter textwriter = new StringWriter(sb);
            HtmlTextWriter htmlwriter = new HtmlTextWriter(textwriter);
            gv.RenderControl(htmlwriter);
            htmlwriter.Flush();
            textwriter.Flush();
            htmlwriter.Dispose();
            textwriter.Dispose();
            return sb.ToString();
        }


        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }

       

       
    }
}