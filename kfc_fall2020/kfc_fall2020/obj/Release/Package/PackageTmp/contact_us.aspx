<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="contact_us.aspx.cs" Inherits="kfc_fall2020.contact_us" ValidateRequest="false"%>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <br />

     <p style="margin:0px">
           <strong>
           <asp:Label ID="lblMsg" runat="server" style="font-size: large"></asp:Label>
           </strong>
           <br />
           <div style="background-color:#F7F6F3; width:669px; height: 370px; margin-left: 0px; ">
    <table class="nav-justified" style="width: 751px; height: 283px">
        <tr><td>    </td></tr>
        <tr>
            <td style="height: 26px;font-weight:bold;font-size:16px" >&nbsp;&nbsp; To-Email</td>
            <td style="height: 26px;padding-bottom:10px; " >
                <asp:TextBox ID="txtSenderEmail" runat="server"  Width="450px"   ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="height: 26px;font-weight:bold;font-size:16px">&nbsp;&nbsp; Subject
            </td>
            <td style="height: 26px;padding-bottom:10px;">
                <asp:TextBox ID="txtSubject" runat="server" Width="450px " ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="font-weight:bold;font-size:16px">&nbsp;&nbsp;&nbsp; File Attachments:</td>
            <td style="padding-bottom:10px;">
               <asp:FileUpload ID="fuAttachment" runat="server"   Enabled="true"  
                   AllowMultiple="true" Width="449px" /></td>
        </tr>
        <tr>
            <td valign style="font-weight:bold;font-size:16px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Message
            </td>
            <td style="padding-bottom:10px;">
                <asp:TextBox ID="txtBody" runat="server" TextMode="MultiLine" Height="103px" 
                    Width="450px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td valign="top">&nbsp;</td>
            <td style="padding-bottom:10px;">
             
                <asp:Button ID="btnSendMailViaMailMgr" runat="server" 
                    Text="Send " OnClick="SendEmail" />
             
            </td>
        </tr>
    </table>
               <p style="color: #CC0000"><strong>&nbsp;&nbsp;&nbsp;&nbsp; Note : &quot; To-Email &quot; for test the sending only </strong></p>
               </div>
     </p>
</asp:Content>
