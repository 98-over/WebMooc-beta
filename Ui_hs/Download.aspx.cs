using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Download : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void download_Click(object sender, EventArgs e)
    {
        Button Btn = sender as Button;
        string path = Btn.CommandArgument.ToString();
        System.IO.FileInfo file = new System.IO.FileInfo(Server.MapPath("~/") + path);
        Response.Clear();
        Response.Charset = "GB2312";
        Response.ContentEncoding = System.Text.Encoding.UTF8;
        // 添加头信息，为"文件下载/另存为"对话框指定默认文件名
        Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(file.Name));
        // 添加头信息，指定文件大小，让浏览器能够显示下载进度
        Response.AddHeader("Content-Length", file.Length.ToString());
        // 指定返回的是一个不能被客户端读取的流，必须被下载
        Response.ContentType = "application/x-bittorrent";
        // 把文件流发送到客户端
        Response.WriteFile(file.FullName);
        // 停止页面的执行
        Response.End();
    }
}