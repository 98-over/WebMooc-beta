<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="DetailCourse.aspx.cs" Inherits="DetailCourse " %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <div style="position: absolute; left: 80px; width: 280px; height: 400px">

        <asp:DataList ID="DataList1" runat="server" DataSourceID="SqlDataSource2">
            <ItemTemplate>
                课程名称:
                 <asp:Label ID="course_nameLabel" runat="server" Text='<%# Eval("course_name") %>' />
                <br />
                课程类型:
                 <asp:Label ID="course_typeLabel" runat="server" Text='<%# Eval("course_type") %>' />
                <br />
                课程简介:
                 <asp:Label ID="course_introduceLabel" runat="server" Text='<%# Eval("course_introduce") %>' />
                <br />
                <br />
            </ItemTemplate>
        </asp:DataList>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:SqlConnectionString %>" SelectCommand="SELECT [course_name], [course_type], [course_introduce] FROM [course] WHERE ([course_id] = @course_id)">
            <SelectParameters>
                <asp:QueryStringParameter Name="course_id" QueryStringField="id" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
    </div>
    <center>
            <video id="video" width="60%" height="60%"  preload="metadata" autoplay="autoplay" controls="controls" type="video/mp4" autobuffer></video>
            <p>
                选择播放速率：
                <select id="selRate">
                    <option value="0.25">0.25</option>
                    <option value="0.5">0.5</option>
                    <option value="1" selected="selected">1.0</option>
                    <option value="1.25">1.25</option>
                    <option value="1.5">1.5</option>
                    <option value="1.75">1.75</option>
                    <option value="2">2.0</option>
                </select>
            </p>
    
        <div>
            <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="SqlDataSource1" DataTextField="fpath" DataValueField="fpath">
            </asp:DropDownList>
            <a href="Download.aspx?course_id=<%=id%>" target="_blank">下载课程资源</a>
            <a href="../js/question/questionnaire.html?id=<%=id%>">提交测试</a>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:SqlConnectionString %>" SelectCommand="SELECT [fpath] FROM [course_file] WHERE ([course_id] = @course_id)"><SelectParameters>
                    <asp:QueryStringParameter QueryStringField="id" Name="course_id" Type="String" ></asp:QueryStringParameter>
                    </SelectParameters>
                </asp:SqlDataSource>
        </div>
                </center>
    <script type="text/javascript">

         var dl = window.document.getElementById("DropDownList1").getElementsByTagName("option");

         for (var i = 0; i < dl.length; i++) {
             var str = dl[i].innerHTML
             var index = str.lastIndexOf("/")
             str = str.substring(index + 1, str.length);

             dl[i].innerHTML = str


         }

    </script>
</asp:Content>

