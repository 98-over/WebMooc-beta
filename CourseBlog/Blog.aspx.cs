using DAL;
using IDAL;
using Microsoft.AspNet.Identity;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Blog : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Session["u_id"] = 2;
            IBlog b = DataAccess.CreateBlog();
            System.Data.DataSet res = b.GetAllBlog();
            this.dl1.DataSource = res;
            this.dl1.DataBind();
            for (int i = 0; i < this.dl1.Items.Count; i++)
            {
                DataRow r = res.Tables[0].Rows[i];
                var b_no = r["b_no"];
                string path = Server.MapPath("../BlogImage/" + b_no.ToString() + "/");
                string[] imgFile = Directory.GetFiles(path);
                switch (imgFile.Length)
                {
                    case 0:
                        break;
                    case 1:
                        for (int j = 1; j <= imgFile.Length; j++)
                        {
                            Image img = (Image)this.dl1.Items[i].FindControl("img" + j.ToString());
                            img.Visible = true;
                            img.Width = 300;
                            img.ImageUrl = r["b_img"].ToString() + j.ToString() + imgFile[j - 1].Substring(imgFile[j - 1].IndexOf("."));
                        }
                        break;
                    case 2:
                        for (int j = 1; j <= imgFile.Length; j++)
                        {
                            Image img = (Image)this.dl1.Items[i].FindControl("img" + j.ToString());
                            img.Visible = true;
                            img.Width = 150;
                            img.Height = 150;
                            img.ImageUrl = r["b_img"].ToString() + j.ToString() + imgFile[j - 1].Substring(imgFile[j - 1].IndexOf("."));
                        }
                        break;
                    case 3:
                        for (int j = 1; j <= imgFile.Length; j++)
                        {
                            Image img = (Image)this.dl1.Items[i].FindControl("img" + j.ToString());
                            img.Visible = true;
                            img.Width = 100;
                            img.Height = 100;
                            img.ImageUrl = r["b_img"].ToString() + j.ToString() + imgFile[j - 1].Substring(imgFile[j - 1].IndexOf("."));
                        }
                        break;
                    case 4:
                        for (int j = 1; j <= imgFile.Length; j++)
                        {
                            if (j > 0 && j < 3)
                            {
                                Image img = (Image)this.dl1.Items[i].FindControl("img" + j.ToString());
                                img.Visible = true;
                                img.Width = 150;
                                img.Height = 150;
                                img.ImageUrl = r["b_img"].ToString() + j.ToString() + imgFile[j - 1].Substring(imgFile[j - 1].IndexOf("."));
                            }
                            else
                            {
                                Image img = (Image)this.dl1.Items[i].FindControl("img" + (j + 1).ToString());
                                img.Visible = true;
                                img.Width = 150;
                                img.Height = 150;
                                img.ImageUrl = r["b_img"].ToString() + j.ToString() + imgFile[j - 1].Substring(imgFile[j - 1].IndexOf("."));
                            }

                        }
                        break;
                    case 5:
                        for (int j = 1; j <= imgFile.Length; j++)
                        {
                            Image img = (Image)this.dl1.Items[i].FindControl("img" + j.ToString());
                            img.Visible = true;
                            img.Width = 100;
                            img.Height = 100;
                            img.ImageUrl = r["b_img"].ToString() + j.ToString() + imgFile[j - 1].Substring(imgFile[j - 1].IndexOf("."));
                        }
                        break;
                    case 6:
                        for (int j = 1; j <= imgFile.Length; j++)
                        {
                            Image img = (Image)this.dl1.Items[i].FindControl("img" + j.ToString());
                            img.Visible = true;
                            img.Width = 100;
                            img.Height = 100;
                            img.ImageUrl = r["b_img"].ToString() + j.ToString() + imgFile[j - 1].Substring(imgFile[j - 1].IndexOf("."));
                        }
                        break;
                    case 7:
                        for (int j = 1; j <= imgFile.Length; j++)
                        {
                            Image img = (Image)this.dl1.Items[i].FindControl("img" + j.ToString());
                            img.Visible = true;
                            img.Width = 100;
                            img.Height = 100;
                            img.ImageUrl = r["b_img"].ToString() + j.ToString() + imgFile[j - 1].Substring(imgFile[j - 1].IndexOf("."));
                        }
                        break;
                    case 8:
                        for (int j = 1; j <= imgFile.Length; j++)
                        {
                            Image img = (Image)this.dl1.Items[i].FindControl("img" + j.ToString());
                            img.Visible = true;
                            img.Width = 100;
                            img.Height = 100;
                            img.ImageUrl = r["b_img"].ToString() + j.ToString() + imgFile[j - 1].Substring(imgFile[j - 1].IndexOf("."));
                        }
                        break;
                    case 9:
                        for (int j = 1; j <= imgFile.Length; j++)
                        {
                            Image img = (Image)this.dl1.Items[i].FindControl("img" + j.ToString());
                            img.Visible = true;
                            img.Width = 100;
                            img.Height = 100;
                            img.ImageUrl = r["b_img"].ToString() + j.ToString() + imgFile[j - 1].Substring(imgFile[j - 1].IndexOf("."));
                        }
                        break;
                }
            }
            for (int i = 0; i < this.dl1.Items.Count; i++)
            {
                System.Data.DataSet dset = new DataSet();
                IRemark remark = DataAccess.CreateRemark();
                ImageButton btn = (ImageButton)this.dl1.Items[i].FindControl("zan");
                ImageButton btn_f = (ImageButton)this.dl1.Items[i].FindControl("btn_friend");
                DataList dt = (DataList)this.dl1.Items[i].FindControl("dl2");

                DataRow r = res.Tables[0].Rows[i];
                var b_no = r["b_no"];
                string f_id = r["u_id"].ToString();
                string uid = User.Identity.GetUserId();
                HiddenField hf = (HiddenField)dl1.Items[i].FindControl("u_name");
                string uname = hf.Value;
                if (IsFriend(uid, f_id) || uname == User.Identity.GetUserName())
                {
                    btn_f.Visible = false;
                }
                else
                {
                    btn_f.Visible = true;
                }
                if (b.IsFavour(b_no.ToString(), uid))
                {
                    btn.ImageUrl = "/image/r_zans.png";
                }
                else
                {
                    btn.ImageUrl = "/image/zans.png";
                }
                dset = remark.GetRemarkOfBlog("'" + b_no.ToString() + "'");
                dt.DataSource = dset;
                dt.DataBind();
            }
        }
    }

    protected void InsertBlog_Click(object sender, EventArgs e)
    {
        string b_no = GetRandomString(15);
        string path = Server.MapPath("../BlogImage/" + b_no + "/");
        Directory.CreateDirectory(path);
        if (this.uploadfile1.HasFile)
        {
            this.uploadfile1.PostedFile.SaveAs(path + "1" + this.uploadfile1.FileName.Substring(this.uploadfile1.FileName.IndexOf(".")));
        }
        if (this.uploadfile2.HasFile)
        {
            this.uploadfile2.PostedFile.SaveAs(path + "2" + this.uploadfile2.FileName.Substring(this.uploadfile2.FileName.IndexOf(".")));
        }
        if (this.uploadfile3.HasFile)
        {
            this.uploadfile3.PostedFile.SaveAs(path + "3" + this.uploadfile3.FileName.Substring(this.uploadfile3.FileName.IndexOf(".")));
        }
        if (this.uploadfile4.HasFile)
        {
            this.uploadfile4.PostedFile.SaveAs(path + "4" + this.uploadfile4.FileName.Substring(this.uploadfile4.FileName.IndexOf(".")));
        }
        if (this.uploadfile5.HasFile)
        {
            this.uploadfile5.PostedFile.SaveAs(path + "5" + this.uploadfile5.FileName.Substring(this.uploadfile5.FileName.IndexOf(".")));
        }
        if (this.uploadfile6.HasFile)
        {
            this.uploadfile6.PostedFile.SaveAs(path + "6" + this.uploadfile6.FileName.Substring(this.uploadfile6.FileName.IndexOf(".")));
        }
        if (this.uploadfile7.HasFile)
        {
            this.uploadfile7.PostedFile.SaveAs(path + "7" + this.uploadfile7.FileName.Substring(this.uploadfile7.FileName.IndexOf(".")));
        }
        if (this.uploadfile8.HasFile)
        {
            this.uploadfile8.PostedFile.SaveAs(path + "8" + this.uploadfile8.FileName.Substring(this.uploadfile8.FileName.IndexOf(".")));
        }
        if (this.uploadfile9.HasFile)
        {
            this.uploadfile9.PostedFile.SaveAs(path + "9" + this.uploadfile9.FileName.Substring(this.uploadfile9.FileName.IndexOf(".")));
        }

        IBlog impBlog = DataAccess.CreateBlog();
        Model.Blog b = new Model.Blog();
        b.B_no = b_no;
        b.B_text = this.b_text.Text;
        this.b_text.Text = "";
        b.B_time = DateTime.Now;
        b.U_id = User.Identity.GetUserId();
        b.B_img = "../BlogImage/" + b_no + "/";
        if (impBlog.insertBlog(b) != -1)
        {
            IBlog blog = DataAccess.CreateBlog();
            System.Data.DataSet res = blog.GetAllBlog();
            this.dl1.DataSource = res;
            this.dl1.DataBind();
            for (int i = 0; i < this.dl1.Items.Count; i++)
            {
                DataRow r = res.Tables[0].Rows[i];
                var bno = r["b_no"];
                string path1 = Server.MapPath("../BlogImage/" + bno.ToString() + "/");
                string[] imgFile = Directory.GetFiles(path1);
                switch (imgFile.Length)
                {
                    case 0:
                        break;
                    case 1:
                        for (int j = 1; j <= imgFile.Length; j++)
                        {
                            Image img = (Image)this.dl1.Items[i].FindControl("img" + j.ToString());
                            img.Visible = true;
                            img.Width = 300;
                            img.ImageUrl = r["b_img"].ToString() + j.ToString() + imgFile[j - 1].Substring(imgFile[j - 1].IndexOf("."));
                        }
                        break;
                    case 2:
                        for (int j = 1; j <= imgFile.Length; j++)
                        {
                            Image img = (Image)this.dl1.Items[i].FindControl("img" + j.ToString());
                            img.Visible = true;
                            img.Width = 150;
                            img.Height = 150;
                            img.ImageUrl = r["b_img"].ToString() + j.ToString() + imgFile[j - 1].Substring(imgFile[j - 1].IndexOf("."));
                        }
                        break;
                    case 3:
                        for (int j = 1; j <= imgFile.Length; j++)
                        {
                            Image img = (Image)this.dl1.Items[i].FindControl("img" + j.ToString());
                            img.Visible = true;
                            img.Width = 100;
                            img.Height = 100;
                            img.ImageUrl = r["b_img"].ToString() + j.ToString() + imgFile[j - 1].Substring(imgFile[j - 1].IndexOf("."));
                        }
                        break;
                    case 4:
                        for (int j = 1; j <= imgFile.Length; j++)
                        {
                            if (j > 0 && j < 3)
                            {
                                Image img = (Image)this.dl1.Items[i].FindControl("img" + j.ToString());
                                img.Visible = true;
                                img.Width = 150;
                                img.Height = 150;
                                img.ImageUrl = r["b_img"].ToString() + j.ToString() + imgFile[j - 1].Substring(imgFile[j - 1].IndexOf("."));
                            }
                            else
                            {
                                Image img = (Image)this.dl1.Items[i].FindControl("img" + (j + 1).ToString());
                                img.Visible = true;
                                img.Width = 150;
                                img.Height = 150;
                                img.ImageUrl = r["b_img"].ToString() + j.ToString() + imgFile[j - 1].Substring(imgFile[j - 1].IndexOf("."));
                            }

                        }
                        break;
                    case 5:
                        for (int j = 1; j <= imgFile.Length; j++)
                        {
                            Image img = (Image)this.dl1.Items[i].FindControl("img" + j.ToString());
                            img.Visible = true;
                            img.Width = 100;
                            img.Height = 100;
                            img.ImageUrl = r["b_img"].ToString() + j.ToString() + imgFile[j - 1].Substring(imgFile[j - 1].IndexOf("."));
                        }
                        break;
                    case 6:
                        for (int j = 1; j <= imgFile.Length; j++)
                        {
                            Image img = (Image)this.dl1.Items[i].FindControl("img" + j.ToString());
                            img.Visible = true;
                            img.Width = 100;
                            img.Height = 100;
                            img.ImageUrl = r["b_img"].ToString() + j.ToString() + imgFile[j - 1].Substring(imgFile[j - 1].IndexOf("."));
                        }
                        break;
                    case 7:
                        for (int j = 1; j <= imgFile.Length; j++)
                        {
                            Image img = (Image)this.dl1.Items[i].FindControl("img" + j.ToString());
                            img.Visible = true;
                            img.Width = 100;
                            img.Height = 100;
                            img.ImageUrl = r["b_img"].ToString() + j.ToString() + imgFile[j - 1].Substring(imgFile[j - 1].IndexOf("."));
                        }
                        break;
                    case 8:
                        for (int j = 1; j <= imgFile.Length; j++)
                        {
                            Image img = (Image)this.dl1.Items[i].FindControl("img" + j.ToString());
                            img.Visible = true;
                            img.Width = 100;
                            img.Height = 100;
                            img.ImageUrl = r["b_img"].ToString() + j.ToString() + imgFile[j - 1].Substring(imgFile[j - 1].IndexOf("."));
                        }
                        break;
                    case 9:
                        for (int j = 1; j <= imgFile.Length; j++)
                        {
                            Image img = (Image)this.dl1.Items[i].FindControl("img" + j.ToString());
                            img.Visible = true;
                            img.Width = 100;
                            img.Height = 100;
                            img.ImageUrl = r["b_img"].ToString() + j.ToString() + imgFile[j - 1].Substring(imgFile[j - 1].IndexOf("."));
                        }
                        break;
                }
            }
            for (int i = 0; i < this.dl1.Items.Count; i++)
            {
                System.Data.DataSet dset = new DataSet();
                IRemark remark = DataAccess.CreateRemark();
                DataList dt = (DataList)this.dl1.Items[i].FindControl("dl2");
                DataRow r = res.Tables[0].Rows[i];
                var bno = r["b_no"];
                dset = remark.GetRemarkOfBlog("'" + bno.ToString() + "'");
                dt.DataSource = dset;
                dt.DataBind();
            }
        }
    }

    protected string GetRandomString(int iLength)
    {
        string buffer = "0123456789abcdefghijklmnopqrstuvwxyz";// 随机字符中也可以为汉字（任何）
        StringBuilder sb = new StringBuilder();
        Random r = new Random();
        int range = buffer.Length;
        for (int i = 0; i < iLength; i++)
        {
            sb.Append(buffer.Substring(r.Next(range), 1));
        }
        return sb.ToString();
    }

    protected void dl1_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName == "InsertRemark")
        {
            IRemark impRemark = DataAccess.CreateRemark();
            Remark remark = new Remark();
            string b_no = e.CommandArgument.ToString();
            string r_no = GetRandomString(15);
            string text = string.Empty;
            TextBox textBox = (TextBox)e.Item.FindControl("remark");
            textBox.Visible = false;
            e.Item.FindControl("btn_remark").Visible = false;
            text = textBox.Text;
            TextBox uidBox = (TextBox)e.Item.FindControl("uid");
            remark.R_no = r_no;
            remark.B_no = b_no;
            remark.R_text = text;
            remark.R_time = DateTime.Now;
            remark.U_remark = User.Identity.GetUserId();
            remark.U_deremark = uidBox.Text;
            impRemark.insertRemark(remark);
            DataSet dset = new DataSet();
            dset = impRemark.GetRemarkOfBlog("'" + b_no.ToString() + "'");
            DataList dt = (DataList)e.Item.FindControl("dl2");
            dt.DataSource = dset;
            dt.DataBind();
        }
        else if (e.CommandName == "AddFriend")
        {
            string fd_id = e.CommandArgument.ToString();
            string my_id = User.Identity.GetUserId();
            string sql = "insert into friend values('" + my_id + "','" + fd_id + "',1)";
            if (!IsFriend(my_id, fd_id))
            {
                DataBase db = new DataBase();
                db.RunProc(sql);
                e.Item.FindControl("btn_friend").Visible = false;
                Response.Write("<script>alert('添加成功')</script>");
            }
            else
            {
                return;
            }
        }
        else if (e.CommandName == "Reply")
        {

        }
        else if (e.CommandName == "Favour")
        {
            IBlog b = DataAccess.CreateBlog();
            string b_no = e.CommandArgument.ToString();
            string u_id = User.Identity.GetUserId();
            //查询数据库该用户是否点赞
            if (!b.IsFavour(b_no, u_id))
            {
                b.AddFavour(b_no, u_id);
                Label lb = (Label)e.Item.FindControl("f_count");
                var text = lb.Text;
                int count = int.Parse(text) + 1;
                lb.Text = count.ToString();
                ImageButton btn = (ImageButton)e.Item.FindControl("zan");
                btn.ImageUrl = "/image/r_zans.png";
            }
            else
            {
                b.DelFavour(b_no, u_id);
                Label lb = (Label)e.Item.FindControl("f_count");
                var text = lb.Text;
                int count = int.Parse(text) - 1;
                lb.Text = count.ToString();
                ImageButton btn = (ImageButton)e.Item.FindControl("zan");
                btn.ImageUrl = "/image/zans.png";
            }
        }
        else if (e.CommandName == "ShowRemark")
        {
            e.Item.FindControl("remark").Visible = true;
            e.Item.FindControl("btn_remark").Visible = true;
        }
    }

    protected bool IsFriend(string my_id, string f_id)
    {
        DataBase db = new DataBase();
        SqlParameter[] prams =
            {
                db.MakeInParam("@owner_id",SqlDbType.VarChar,15,my_id),
                db.MakeInParam("@friend_id",SqlDbType.VarChar,255,f_id),
            };
        string sql = string.Empty;
        sql = "select * from friend where owner_id=@owner_id and friend_id=@friend_id";
        DataSet ds = db.RunProcReturn(sql, prams, "tb_friend");
        if ((ds == null) || (ds.Tables.Count == 0) || (ds.Tables.Count == 1 && ds.Tables[0].Rows.Count == 0))
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}