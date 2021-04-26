<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="CourseManage.aspx.cs" Inherits="CourseManage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" DataKeyNames="course_id" Style="width: 100%; height: auto" CssClass="gridtable" OnRowDataBound="GridView1_RowDataBound" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
        <Columns>
            <asp:BoundField DataField="course_id" HeaderText="course_id" SortExpression="course_id" Visible="False" />
            <asp:BoundField DataField="course_name" HeaderText="课程名" SortExpression="course_name" />
            <asp:BoundField DataField="course_type" HeaderText="课程类型" SortExpression="course_type" />
            <asp:BoundField DataField="course_introduce" HeaderText="课程介绍" SortExpression="course_introduce" />
            <asp:BoundField DataField="upload_id" HeaderText="upload_id" SortExpression="upload_id" Visible="False" />
            <asp:BoundField DataField="course_picpath" HeaderText="course_picpath" SortExpression="course_picpath" Visible="False" />
            <asp:CommandField HeaderText="操作" ShowDeleteButton="True" ShowEditButton="True" ShowHeader="True" />
            <asp:HyperLinkField runat="server" DataNavigateUrlFields="course_id" DataNavigateUrlFormatString='~/Ui_hs/FileManage.aspx?course_id={0}' Text="文件" HeaderText="文件管理"></asp:HyperLinkField>
        </Columns>

    </asp:GridView>

    <asp:SqlDataSource ID="SqlDataSource2" runat="server"></asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:SqlConnectionString %>" SelectCommand="SELECT * FROM [course]" DeleteCommand="delete  from course where course_id=@course_id" UpdateCommand="update course set course_name=@course_name,course_type=@course_type,course_introduce=@course_introduce where course_id=@course_id">
        <DeleteParameters>
            <asp:Parameter Name="course_id" Type="string" />
        </DeleteParameters>
        <UpdateParameters>
            <asp:Parameter Name="course_name" Type="String" />
            <asp:Parameter Name="course_type" Type="String" />
            <asp:Parameter Name="course_introduce" Type="String" />
            <asp:Parameter Name="course_id" Type="String" />

        </UpdateParameters>
    </asp:SqlDataSource>
    <asp:Label ID="Label1" runat="server" Text="课程类型"></asp:Label>
    <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:SqlConnectionString %>"
        SelectCommand="SELECT * FROM [classify]"></asp:SqlDataSource>
    <asp:DropDownList ID="DropDownList1" runat="server" DataTextField="classify_name" DataSourceID="SqlDataSource3"></asp:DropDownList><br />
    <asp:Label ID="Label2" runat="server" Text="课程名"></asp:Label>
    <asp:TextBox ID="name" runat="server"></asp:TextBox><br />
    <label runat="server">封面图片</label>
    <asp:FileUpload runat="server" name="picture" ID="picture"></asp:FileUpload><br />
    <label runat="server">课程简介</label>
    <textarea id="introduce" runat="server"></textarea><br />
    <label runat="server">上传课程文件</label>
    <asp:FileUpload runat="server" AllowMultiple="true" ID="course" name="source"></asp:FileUpload><label runat="server">如需选择多个文件请按住ctrl键</label><br />
    <asp:Button ID="Button1" runat="server" Text="添加新课程" OnClick="Button1_Click2" />
    <asp:Button ID="Button2" runat="server" Text="班级管理" OnClick="Button2_Click" />

</asp:Content>

