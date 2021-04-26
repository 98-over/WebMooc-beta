using DAL;
using IDAL;
using Microsoft.AspNet.Identity;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ChatRoom : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataBase db = new DataBase();
            string sql = string.Empty;
            string my_id = User.Identity.GetUserId();
            sql = "select * from [user],friend where [user].id = friend.friend_id and owner_id = '" + my_id + "'";
            DataSet f_ds = db.GetDataSet(sql, "f_tb");
            this.f_dl.DataSource = f_ds;
            this.f_dl.DataBind();
            this.user_id.Value = User.Identity.GetUserId();
        }
    }

    protected void click_friend(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName == "chat")
        {
            HiddenField hf = (HiddenField)e.Item.FindControl("f_name");
            string f_id = e.CommandArgument.ToString();
            this.fd_name.Text = hf.Value;
            this.recv_id.Value = f_id;
            string my_id = User.Identity.GetUserId();
            IMessage m = DataAccess.CreateMessage();
            DataSet meg_ds = m.GetMessageOfUser(my_id, f_id);
            this.meg_dl.DataSource = meg_ds;
            this.meg_dl.DataBind();
        }
    }

    protected void Send_Click(object sender, EventArgs e)
    {
        Message meg = new Message();
        meg.Meg = this.meg_box.Text;
        this.meg_box.Text = "";
        meg.SendId = User.Identity.GetUserId();
        meg.RecvId = this.recv_id.Value;
        meg.Time = DateTime.Now;
        meg.MegType = 2;
        IMessage m = DataAccess.CreateMessage();
        m.InsertMessage(meg);
        MessageBind();
    }

    public void MessageBind()
    {
        string my_id = User.Identity.GetUserId();
        string f_id = this.recv_id.Value;
        IMessage m = DataAccess.CreateMessage();
        this.meg_dl.DataSource = m.GetMessageOfUser(my_id, f_id);
        this.meg_dl.DataBind();
    }
}