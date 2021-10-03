<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Cruderege.aspx.cs" Inherits="kfc_fall2020.Cruderege"
    EnableEventValidation="false"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server"   >
    <p style="margin:0px">
        <asp:Label ID="inout" runat="server" Font-Bold="True" Font-Size="Smaller" style="font-size: large;margin-left: -100px;" ></asp:Label>
        <br />
       
       <table style="width:100%">
           
           <tr>
               <td> <div style="background-color:#F7F6F3; width:383px; height: 309px; margin-left: -100px; ">
       <table   style="width: 368px; height: 250px ;">


            <tr>
             <td style="width: 227px; height: 27px;font-weight:bold;font-size:16px"  >&nbsp;&nbsp; FileNO:</td>
             <td style="padding-bottom:10px; width: 179px;"><asp:TextBox ID="Tbxfile" runat="server"  ReadOnly="True"></asp:TextBox></td>
            </tr>
            <tr>
             <td style="width: 227px ;font-weight:bold;font-size:16px">&nbsp;&nbsp; Name:</td>
           <td style="padding-bottom:10px; width: 179px;"> <asp:TextBox ID="Tbxnm" runat="server" MaxLength="50"  ></asp:TextBox></td>
            </tr>
           
             <tr>
           <td style="width: 227px  ;font-weight:bold;font-size:16px">&nbsp;&nbsp; Age:</td>
          <td style="padding-bottom:10px; width: 179px;">  <asp:TextBox ID="Tbxage" runat="server" MaxLength="3"  ></asp:TextBox></td>
            </tr>
              <tr>
                  <td style="width: 227px  ;font-weight:bold;font-size:16px">&nbsp;&nbsp; NationalId/Iqama:</td>
            <td style="padding-bottom:10px; width: 179px;"> <asp:TextBox ID="Tbxid" runat="server" MaxLength="10"  ></asp:TextBox></td>
            </tr>
             <tr>
   
            <td style="width: 227px;font-weight:bold;font-size:16px; height: 30px;">&nbsp;&nbsp; Gender:</td>
                 <td style="padding-bottom:10px; width: 179px; height: 30px;"><asp:RadioButtonList ID="rdbtn1" runat="server"  TextAlign="Left" RepeatDirection="Horizontal"  Width="179px"  Font-Size="14px" ></asp:RadioButtonList></td>
                 <br />
            </tr>
                <tr>
           <td style="width: 227px;font-weight:bold;font-size:16px; height: 32px;">&nbsp;&nbsp; Department:</td>
                  <td style="padding-bottom:10px; width: 179px; height: 32px;">  
               <asp:DropDownList ID="ddlst" runat="server"  Width="114px"   >
                </asp:DropDownList></td>  
            </tr> 
           <tr>
                       <td  colspan="2" style="padding-top:10px; padding-bottom:20px ;padding-left:20px">
                 <asp:Button ID="addbtn" runat="server" Text="ADD" OnClick="addbtn_Click" />
                 <asp:Button ID="upbtn" runat="server" Text="UPDATE" OnClick="upbtn_Click" />
                 <asp:Button ID="delbtn" runat="server" Text="DELETE" OnClick="delbtn_Click" />
                 <asp:Button ID="slbtn" runat="server"  Text="SELECT" OnClick="slbtn_Click" />
                 <asp:Button ID="resetbtn" runat="server"  Text="Reset" OnClick="resetbtn_Click"  />
                        
                      </td></tr>

           <tr><td> <p style="padding-top:10px;width:200px">
              <asp:Label ID="selectlabel" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="#627A96" ></asp:Label></p></td></tr>

            
        
        </table>
            </div>
               </td>

          <td>
              <br />
              <br />
             <table  style="width:100%">   
              
                  <tr style="background-color:#F7F6F3"><td style="padding-left:270px;padding-bottom:5px"> 


                      <asp:TextBox ID="toemailtxt" runat="server"  MaxLength="50" Width="450px"  placeholder="TO-EMAIL"></asp:TextBox>
                     <span style="padding-left:14px">
                  <asp:Button ID="emailgv" runat="server" Text="Email GV Content" Width="126px" OnClick="emailgv_Click"   />
                         </span>
                        

                                                       </td></tr>


                  <tr style="background-color:#F7F6F3"><td style="padding-left:410px;padding-bottom:5px"> 
                       <asp:DropDownList ID="ddlexport" runat="server"  Width="140px"   >
                           <asp:ListItem>Select Type</asp:ListItem>
                           <asp:ListItem>Word</asp:ListItem>
                           <asp:ListItem>Excel</asp:ListItem>
                           <asp:ListItem>PDF</asp:ListItem>
                           
                           </asp:DropDownList>
                      <span style="padding-left:14px">
                      <asp:Button ID="export" runat="server" Text="Export" Width="126px" OnClick="export_Click"  />   </span></td></tr>
                       
                  <tr style="background-color:#F7F6F3">
                       
                      <td style="padding-left:80px">
                         
                          <asp:TextBox ID="nameadd" runat="server"  MaxLength="50" Width="110px"  placeholder="Name"></asp:TextBox>
                          <asp:TextBox ID="ageadd" runat="server" MaxLength="3" Width="40px" placeholder="Age" ></asp:TextBox>
                          <asp:TextBox ID="idadd" runat="server" MaxLength="10" Width="100px"  placeholder="ID Number" ></asp:TextBox>
                           <asp:DropDownList ID="ddlgdradd" runat="server"  Width="90px"  >
                           </asp:DropDownList>
                          <asp:DropDownList ID="ddldepadd" runat="server"  Width="114px"   >
                           </asp:DropDownList>
                              <span style="padding-left:15px">
                           <asp:Button ID="gvaddbtn" runat="server" Text="ADD Record" Width="126px" OnClick="gvaddbtn_Click"  />
                                  </span>
                        
                      </td> 
                      
                  </tr>


                  <tr ><td >
               <asp:GridView ID="GridView1" runat="server" CssClass="center" AutoGenerateColumns="False" 
                                  DataKeyNames ="p_id" 
                                   OnRowDataBound="GridView1_RowDataBound" 
                                   OnRowDeleting="GridView1_RowDeleting"
                                   OnRowEditing="GridView1_RowEditing"
                                   OnRowUpdating="GridView1_RowUpdating"
                                   OnRowCancelingEdit="OnRowCancelingEdit" 
                                   EnableTheming="True" GridLines="None" CellPadding="10" ForeColor="#333333" Width="742px" Height="250px"   >
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:TemplateField  HeaderText="#FileNO" >
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkupdate" runat="server"  
                                                CommandArgument='<%# Bind("p_id") %>' OnClick="populateForm_Click"
                                                Text='<%# Eval("p_id")  %>'></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle Font-Bold="True" HorizontalAlign="Center" />

                                    </asp:TemplateField>
                                    <asp:BoundField DataField="p_name" HeaderText="Name"  >      
                                    <ControlStyle CssClass="widde" />
                                    <ItemStyle Font-Bold="True" HorizontalAlign="Center" />
                                 
                                    </asp:BoundField>
                                    <asp:BoundField DataField="p_age" HeaderText="Age"  >
                         
                                       <ControlStyle CssClass="wide" />
                                    <ItemStyle Font-Bold="True" HorizontalAlign="Center" />
                         
                                       </asp:BoundField>
                                    <asp:BoundField DataField="p_nationalid_iqama" HeaderText="ID Number"  >
                                   
                                    <ControlStyle CssClass="wide" />
                                    <ItemStyle Font-Bold="True" HorizontalAlign="Center" />
                                   
                                  </asp:BoundField>
                                  <%--  this the ddlgender --%>
                               <asp:TemplateField HeaderText="GENDER" >
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="labelgender" Text='<%#Eval("p_gender") %>'></asp:Label>  
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="DropDownListGdr"
                                        AppendDataBoundItems="True"  runat="server"  >
                                    </asp:DropDownList>
                         
                                </EditItemTemplate>
                                   <ControlStyle CssClass="wide" />
                                   <ItemStyle Font-Bold="True" HorizontalAlign="Center" />
                                 </asp:TemplateField>

                                     <%--  this the ddlDepartment --%>
                                    <asp:TemplateField HeaderText="Department">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%#Eval("p_dep") %>'></asp:Label>  
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="DropDownListDep" AppendDataBoundItems="True" runat="server">
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                        <ControlStyle CssClass="wide" />
                                        <ItemStyle Font-Bold="True" HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                     <%-- EDIT BUTTON - DELETE BUTTON --%>
                                    <asp:CommandField ShowEditButton="True" ButtonType="Button" >
                                      <ControlStyle CssClass="wide" />
                                    </asp:CommandField>
                                      <asp:commandfield showdeletebutton="true" ButtonType="Button" >
                                    <ControlStyle CssClass="wide" />
                                    </asp:commandfield>


       
                                    </Columns>

                                <EditRowStyle BackColor="#999999" />
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />

                                <HeaderStyle CssClass="center" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />

                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                       </asp:GridView>
                  </td> </tr>
             </table>



       </td>
    </tr>
    </table>
        
     </p>
 
</asp:Content>
