<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="upload.aspx.cs" Inherits="kfc_fall2020.upload" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
           

          <p style="margin:0px">
        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="Smaller" style="font-size: large"></asp:Label>
        <br />
        <div style="background-color:#F7F6F3; width:487px; height: 231px; margin-left: 0px; ">
     
      
                    <table  class="nav-justified" style="width:100%;">  
                        <tr> 

                            <td  style="padding-bottom:40px; width: 179px;"></td>
                        </tr>
                        <tr>  
                            <td style="padding-bottom:20px; width: 179px;height: 26px;font-weight:bold;font-size:16px">  
                                &nbsp;&nbsp; File Name:</td>  
                            <td  style="padding-bottom:20px; width: 179px;">  
                                <asp:TextBox ID="TextBox1" runat="server" Width="124px"></asp:TextBox>  
                            </td>  
                            <td>  
                                 </td>  
                        </tr>  
                        <tr>  
                            <td style=" padding-bottom:20px; width: 179px; height: 26px;font-weight:bold;font-size:16px">  
                                &nbsp;&nbsp;  
                                Upload Your file:</td>  
                            <td style="padding-bottom:20px; width: 179px;">  
                                <asp:FileUpload ID="FileUpload1" runat="server" Width="209px" />  
                            </td>  
                           
                        </tr>  
                     
                            <td  style="padding-top:10px; padding-bottom:20px ;padding-left:20px">
                                <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Upload" />  
                            </td>  
                           
                                   
                        </tr>  
                    </table>  

         </div>
 
       </p>


</asp:Content>
