<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="FileManage.aspx.cs" Inherits="FileManage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="fId" DataSourceID="SqlDataSource1" CssClass="gridtable" style="height:auto;width:100%">
            <Columns>
                <asp:BoundField DataField="fId" HeaderText="fId" InsertVisible="False" ReadOnly="True" SortExpression="fId" Visible="False" />
                <asp:BoundField DataField="fName" HeaderText="文件名" SortExpression="fName" />
                <asp:BoundField DataField="fpath" HeaderText="fpath" SortExpression="fpath" Visible="False" />
                <asp:BoundField DataField="uploadtime" HeaderText="上传时间" SortExpression="uploadtime" />
                <asp:BoundField DataField="uploadid" HeaderText="uploadid" SortExpression="uploadid" Visible="False" />
                <asp:BoundField DataField="course_id" HeaderText="course_id" SortExpression="course_id" Visible="False" />
                <asp:BoundField DataField="file_size" HeaderText="文件大小" SortExpression="file_size"  />
                <asp:CommandField  HeaderText="操作" ShowDeleteButton="true"/>
            </Columns>
        </asp:GridView>
        <asp:FileUpload ID="FileUpload1" runat="server"  AllowMultiple="true"/>
        <asp:Button ID="Button1" runat="server" Text="添加新文件" OnClick="Button1_Click" />


        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:SqlConnectionString %>" SelectCommand="SELECT * FROM [course_file] WHERE ([course_id] = @course_id)" DeleteCommand="delete from course_file where fId=@fId">
            <SelectParameters>
                <asp:QueryStringParameter Name="course_id" QueryStringField="course_id" Type="String" />
            </SelectParameters>
            <DeleteParameters>

                <asp:Parameter Name="fId" Type="Int32"/>
            </DeleteParameters>
        </asp:SqlDataSource>
</asp:Content>

