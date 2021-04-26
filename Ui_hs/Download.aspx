<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Download.aspx.cs" Inherits="Download" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:GridView ID="GridView1" runat="server" CssClass="=gridtable" style="width:100%;height:auto" AutoGenerateColumns="False" DataKeyNames="fId" DataSourceID="SqlDataSource1" OnRowDataBound="GridView1_RowDataBound" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" >
            <Columns>
                <asp:BoundField DataField="fId" HeaderText="fId" InsertVisible="False" ReadOnly="True" SortExpression="fId" Visible="False" />
                <asp:BoundField DataField="fName" HeaderText="文件名" SortExpression="fName" />
                <asp:BoundField DataField="fpath" HeaderText="fpath" SortExpression="fpath" Visible="False" />
                <asp:BoundField DataField="uploadtime" HeaderText="上传时间" SortExpression="uploadtime" />
                <asp:BoundField DataField="uploadid" HeaderText="uploadid" SortExpression="uploadid" Visible="False" />
                <asp:BoundField DataField="course_id" HeaderText="course_id" SortExpression="course_id" Visible="False" />
                <asp:BoundField DataField="file_size" HeaderText="文件大小" SortExpression="file_size" />
                <asp:TemplateField HeaderText="操作">
                    <ItemTemplate>
                         <asp:Button runat="server" Text="下载"  OnClick="download_Click" ID="download" CommandArgument='<%#Eval("fpath") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
               
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:SqlConnectionString %>" SelectCommand="SELECT * FROM [course_file] WHERE ([course_id] = @course_id)">
            <SelectParameters>
                <asp:QueryStringParameter Name="course_id" QueryStringField="course_id" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
</asp:Content>

