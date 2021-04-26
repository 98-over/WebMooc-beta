<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ShowCourse.aspx.cs" Inherits="ShowCourse " %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:DataList ID="dlPhoto" runat="server" onitemcommand="dlPhoto_ItemCommand" RepeatDirection="Horizontal"  >         
            <ItemTemplate>


       <asp:ImageButton id="ProductImage" ImageUrl='<%# DataBinder.Eval(Container.DataItem, "course_picpath")%>' Height="100px" Width ="200px" runat="server" CommandName="detail" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "course_id") %>'/>
               
                  <br />
               <asp:Label ID="Label2" runat="server" width="10px" Height="10px"><%# DataBinder.Eval(Container.DataItem, "course_name") %></asp:Label>
                
               
            </ItemTemplate>
             <FooterTemplate>
                <asp:LinkButton ID="LinkButton1" CommandName="first" runat="server" >首页</asp:LinkButton>
                <asp:LinkButton ID="LinkButton2" CommandName="pre" runat="server">上一页</asp:LinkButton>
                <asp:LinkButton ID="LinkButton3" CommandName="next" runat="server">下一页</asp:LinkButton>
                <asp:LinkButton ID="LinkButton4"  CommandName="last" runat="server">末页</asp:LinkButton>
                  <asp:Label ID="Label3" runat="server"  ><%=pds.CurrentPageIndex %>/</asp:Label>
                  <asp:Label ID="Label4" runat="server"  ><%=pds.PageCount-1 %></asp:Label>
                <asp:TextBox ID="txtPage" runat="server" Height="18px" Width="34px"></asp:TextBox>
                <asp:LinkButton ID="LinkButton5"  CommandName="search" runat="server">Go</asp:LinkButton>
            </FooterTemplate>
        </asp:DataList>
    <a href="CourseManage.aspx">课程管理</a>
</asp:Content>

