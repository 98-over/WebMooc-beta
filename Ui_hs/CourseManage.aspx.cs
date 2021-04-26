using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using Model;

public partial class CourseManage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            var s = e.Row.Cells;
            ((LinkButton)e.Row.Cells[6].Controls[2]).Attributes.Add("onclick", "javascript:return confirm('你确认要删除：\"" + e.Row.Cells[1].Text + "\"吗?')");

        }

    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void Button1_Click1(object sender, EventArgs e)
    {
        Response.Redirect("AddCourse.aspx");
    }

    protected void Button1_Click2(object sender, EventArgs e)
    {
        CourseDAL courseDAL = new CourseDAL();
        string courseid = Guid.NewGuid().ToString();
        string imgName = DateTime.Now.ToString("yyyyMMddhhmmss");
        string imgPath = "/" + imgName + this.picture.PostedFile.FileName;
        string AbsolutePath = Server.MapPath("/Course_picture" + imgPath);
        this.picture.PostedFile.SaveAs(AbsolutePath);
        Course course = new Course();
        //Classes classes = new Classes();
        course.Id = courseid;
        course.CourseType = this.DropDownList1.SelectedValue;
        course.Coursepic = "/Course_picture/" + imgPath;
        course.Introduce = this.introduce.Value;
        course.Name = this.name.Text;
        //classes.Name = Request.Form["name"];
        //classes.Count = 1;
        //classes.TeacherId = 1;
        var httpFileCollection = this.course.PostedFiles;
        for (int i = 0; i < httpFileCollection.Count; i++)
        {

            FileModle fileModle = new FileModle();
            HttpPostedFile httpPostedFile1 = httpFileCollection[i];
            string path = Server.MapPath("/Course_source/" + httpPostedFile1.FileName);
            httpPostedFile1.SaveAs(path);
            fileModle.Name = httpPostedFile1.FileName;
            fileModle.Path = "/Course_source/" + httpPostedFile1.FileName;
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(path);
            fileModle.Size = System.Math.Ceiling(fileInfo.Length / (1024.0 * 1024.0)).ToString()+"M";
            fileModle.Time = DateTime.Now;
            fileModle.CourseID = courseid;
            courseDAL.AddFile(fileModle);
        }
        //courseDAL.AddClasses(classes);
        courseDAL.AddCourse(course);
        Response.Redirect("CourseManage.aspx");

    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("ClassManage.aspx");
    }
}