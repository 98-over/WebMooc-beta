using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using Model;

public partial class FileManage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string course_id = Request.QueryString["course_id"].ToString();
            ViewState["course_id"] = course_id;

        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        CourseDAL courseDAL = new CourseDAL();
        HttpFileCollection httpFileCollection = Request.Files;
        for (int i = 0; i < httpFileCollection.Count; i++)
        {

            FileModle fileModle = new FileModle();
            HttpPostedFile httpPostedFile1 = httpFileCollection[i];
            string path = Server.MapPath("/Course_source/" + httpPostedFile1.FileName);
            httpPostedFile1.SaveAs(path);
            fileModle.Name = httpPostedFile1.FileName;
            fileModle.Path = "/Course_source/" + httpPostedFile1.FileName;
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(path);
            fileModle.Size = System.Math.Ceiling(fileInfo.Length / (1024.0 * 1024.0)).ToString() + "M";
            fileModle.Time = DateTime.Now;
            fileModle.CourseID = ViewState["course_id"].ToString();
            courseDAL.AddFile(fileModle);
        }
        Response.Redirect("FileManage.aspx?course_id=" + ViewState["course_id"]);
    }
}