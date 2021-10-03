using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading;
using KFC.App_Code;
using commonapp.App_Code;

namespace kfc_fall2020
{
    public partial class contact_us : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        { }
            protected void clearinput()
        {
            txtSenderEmail.Text = "";
            fuAttachment.Dispose();
            txtSubject.Text = "";
            txtBody.Text = "";

        }
            protected void SendEmail(object sender, EventArgs e)
            {   //email info to send 
                String fromemail = "kfmcteest@gmail.com";
                String myps         = "Test123456!";//password
                String toemail = txtSenderEmail.Text;

            if (!common.IsValidEmail(toemail))
            {
                lblMsg.Text = "Please enter email with correct fromat . youremail@example.com ";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                return;
            }




            MailMessage mail = new MailMessage(fromemail,toemail );//create MailMessage class object
                if (string.IsNullOrEmpty(txtSubject.Text) || string.IsNullOrEmpty(txtBody.Text) ||
                string.IsNullOrEmpty(txtSenderEmail.Text))
                 {
                lblMsg.Text = "Please fill Subject & email body & from email ";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                return;
                }
            //taking input from user 
            //  mail.Subject = txtSubject.Text+ ""+"   Sender email : "+txtSenderEmail.Text;
                mail.Subject = txtSubject.Text;
                mail.Body = txtBody.Text;
                mail.IsBodyHtml = false;
                SmtpClient smtp = new SmtpClient();//Creating Smtp class objec
                smtp.Host = "smtp.gmail.com";
                smtp.UseDefaultCredentials = false;
            smtp.EnableSsl = true;
                smtp.Port = 587;

            HttpPostedFile filev = fuAttachment.PostedFile;
            int iFileSize = filev.ContentLength;
            //try to send email and validate if there is error in sending 
            try
            {   // if user attach a file attach it to the email message
                if (fuAttachment.HasFile)
                {
                    if (iFileSize < 15728640)
                    {

                        foreach (HttpPostedFile file in fuAttachment.PostedFiles)
                        {
                            string FileName = Path.GetFileName(fuAttachment.PostedFile.FileName);
                            mail.Attachments.Add(new Attachment(fuAttachment.PostedFile.InputStream, FileName));
                        }
                    }

                    else
                    {
                        lblMsg.Text = "The file size is too big   ";
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                        return;
                    }
                }
                
                NetworkCredential NetworkCred = new NetworkCredential(fromemail, myps);//set Network Credential
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Send(mail);
       
                lblMsg.Text = "Sucsess";
                lblMsg.ForeColor = System.Drawing.Color.Green;
            }
            catch (SmtpFailedRecipientException ex)
            {
                SmtpStatusCode statusCode = ex.StatusCode;
                if (statusCode == SmtpStatusCode.MailboxBusy || statusCode == SmtpStatusCode.MailboxUnavailable || statusCode == SmtpStatusCode.TransactionFailed)
                {   // wait 5 seconds, try a second time
                    Thread.Sleep(5000);
                    smtp.Send(mail);

                    lblMsg.Text= ex.Message.ToString();
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text= ex.ToString();
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
            finally
            {
                mail.Dispose();
            }
            clearinput();
           
        }
        }
    }
