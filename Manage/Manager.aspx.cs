using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using IDAL;
using InterfaceImplement;
using DAL;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using WebMooc_beta;

public partial class Manager : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new IdentityDbContext()));
            var roles = roleManager.Roles.ToList();
            this.RoleDL.DataSource = roles;
            this.RoleDL.DataBind();
            IUser_Teacher it = DataAccess.CreateTeacher();
            var teachers = it.GetAllTeacher();
            if ((teachers == null) || (teachers.Tables.Count == 0) || (teachers.Tables.Count == 1 && teachers.Tables[0].Rows.Count == 0))
            {
                this.HaveTeaUser.Visible = true;
            }
            else
            {
                this.TeacherDL.DataSource = teachers;
                this.TeacherDL.DataBind();
            }
            
            IUser_Student istu = DataAccess.CreateStudent();
            var stus = istu.GetAllStudent();
            if ((stus == null) || (stus.Tables.Count == 0) || (stus.Tables.Count == 1 && stus.Tables[0].Rows.Count == 0))
            {
                this.HaveStuUser.Visible = true;
            }
            else
            {
                this.StudentDL.DataSource = stus;
                this.StudentDL.DataBind();
            }
            //this.dl_rolefun.DataSource = tb_fun;
            //this.dl_rolefun.DataBind();
        }
    }
    protected void RoleDL_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if(e.CommandName == "showRoleDetail")
        {
            this.ShowNewRoleFun.Visible = false;
            this.ShowRemoveRoleFun.Visible = false;
            this.showEditRoleFun.Visible = false;
            this.readyRoleFun.Text = "";
            this.lb_NewRoleFun.Text = "";
            this.lb_RvRoleFun.Text = "";
            this.lb_roleName.Text = e.CommandArgument.ToString();
            string sql = string.Empty;
            sql = "select fun_name from tb_rolefun where role_name='" + e.CommandArgument.ToString()+"'";
            DataBase db = new DataBase();
            var res = db.GetDataSet(sql, "tb_roleFun");
            foreach (DataTable dt in res.Tables)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    this.readyRoleFun.Text += dr.ItemArray[0].ToString() + "\t";
                }
            }
        }
    }

    protected void CreateRole_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(this.role_fun.Text))
        {
            Response.Write("<script>alert('请选择角色权限!')</script>");
            return;
        }
        var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new IdentityDbContext()));
        if (!roleManager.RoleExists(this.createRoleName.Text))
        {
            roleManager.Create(new IdentityRole(this.createRoleName.Text));
        }
        else
        {
            Response.Write("<script>alert('该角色已存在!')</script>");
            return;
        }
        var roles = roleManager.Roles.ToList();
        this.RoleDL.DataSource = roles;
        this.RoleDL.DataBind();
        string[] list = Regex.Split(this.role_fun.Text, "\t");
        string sql = string.Empty;
        for(int i = 0; i < list.Length; i++)
        {
            DataBase db = new DataBase();
            SqlParameter[] prams =
            {
                db.MakeInParam("@role_name",SqlDbType.VarChar,40,this.createRoleName.Text),
                db.MakeInParam("@fun_name",SqlDbType.VarChar,40,list[i])
            };
            sql = "insert into tb_rolefun values(@role_name,@fun_name)";
            db.RunProc(sql, prams);
        }
        Response.Write("<script>alert('创建成功!')</script>");
        DataBase db1 = new DataBase();
        string sql1 = string.Empty;
        sql1 = "select * from tb_fun";
        var tb_fun = db1.GetDataSet(sql1, "fun_tb");
        this.dl_fun.DataSource = tb_fun;
        this.dl_fun.DataBind();
        this.createRoleName.Text = "";
        this.role_fun.Text = "";
        this.no_fun.Visible = true;
    }

    protected void dl_fun_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if(e.CommandName == "add_role")
        {
            
            if (this.role_fun.Text.Contains(e.CommandArgument.ToString()))
            {
                this.role_fun.Text = this.role_fun.Text.Replace(e.CommandArgument.ToString() + "\t", "");
                if(this.role_fun.Text == "")
                {
                    this.no_fun.Visible = true;
                }
                Button bt = (Button)e.Item.FindControl("fun_select");
                bt.BackColor = System.Drawing.Color.White;
            }
            else
            {
                this.role_fun.Text += e.CommandArgument.ToString() + "\t";
                Button bt = (Button)e.Item.FindControl("fun_select");
                bt.BackColor = System.Drawing.Color.LightGray;
                this.no_fun.Visible = false;
            }
        }
    }

    protected void DeleteRole_Click(object sender, EventArgs e)
    {
        var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new IdentityDbContext()));
        if (roleManager.RoleExists(this.lb_roleName.Text))
        {
            roleManager.Delete(roleManager.FindByName(this.lb_roleName.Text));
            var roles = roleManager.Roles.ToList();
            this.RoleDL.DataSource = roles;
            this.RoleDL.DataBind();
            
            DataBase db = new DataBase();
            string sql = string.Empty;
            SqlParameter[] prams =
            {
                db.MakeInParam("@role_name",SqlDbType.VarChar,40,this.lb_roleName.Text),
            };
            sql = "delete from tb_rolefun where role_name=@role_name";
            db.RunProc(sql, prams);
            this.lb_roleName.Text = "";
        }
    }

    protected void dl_rolefun_ItemCommand(object source, DataListCommandEventArgs e)
    {

    }

    protected void EditRoleFun_Click(object sender, EventArgs e)
    {
        this.btn_DeleteRoleFun.Visible = false;
        this.btn_EditRoleFun.Visible = false;
        this.Submit.Visible = true;
        this.Cancel.Visible = true;
        this.showEditRoleFun.Visible = true;
        this.ShowNewRoleFun.Visible = true;
        this.ShowRemoveRoleFun.Visible = true;
        DataBase db = new DataBase();
        string sql = string.Empty;
        sql = "select * from tb_fun";
        var tb_fun = db.GetDataSet(sql, "fun_tb");
        this.DL_editRoleFun.DataSource = tb_fun;
        this.DL_editRoleFun.DataBind();
        foreach(DataListItem item in this.DL_editRoleFun.Items)
        {
            Button bt = (Button)item.FindControl("roleAddFun_select");
            if (this.readyRoleFun.Text.Contains(bt.Text))
            {
                bt.BackColor = System.Drawing.Color.LightGray;
            }
        }
    }
    protected void ShowCreateRole_Click(object sender, EventArgs e)
    {
        this.ShowCreateRole.Visible = true;
        DataBase db = new DataBase();
        string sql = string.Empty;
        sql = "select * from tb_fun";
        var tb_fun = db.GetDataSet(sql, "fun_tb");
        this.dl_fun.DataSource = tb_fun;
        this.dl_fun.DataBind();
    }

    protected void HiddenCreateRole_Click(object sender, EventArgs e)
    {
        this.createRoleName.Text = "";
        this.role_fun.Text = "";
        this.ShowCreateRole.Visible = false;
    }

    protected void DL_editRoleFun_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if(e.CommandName == "Add_FunRole")
        {
            if (this.readyRoleFun.Text.Contains(e.CommandArgument.ToString()) && !this.lb_RvRoleFun.Text.Contains(e.CommandArgument.ToString()))
            {
                this.lb_RvRoleFun.Text += e.CommandArgument.ToString() + "\t";
                Button bt = (Button)e.Item.FindControl("roleAddFun_select");
                bt.BackColor = System.Drawing.Color.Red;
            }
            else if(this.lb_NewRoleFun.Text.Contains(e.CommandArgument.ToString()))
            {
                this.lb_NewRoleFun.Text = this.lb_NewRoleFun.Text.Replace(e.CommandArgument.ToString() + "\t", "");
                Button bt = (Button)e.Item.FindControl("roleAddFun_select");
                bt.BackColor = System.Drawing.Color.White;
            }else if(this.readyRoleFun.Text.Contains(e.CommandArgument.ToString()) && this.lb_RvRoleFun.Text.Contains(e.CommandArgument.ToString()))
            {
                this.lb_RvRoleFun.Text = this.lb_RvRoleFun.Text.Replace(e.CommandArgument.ToString() + "\t", "");
                Button bt = (Button)e.Item.FindControl("roleAddFun_select");
                bt.BackColor = System.Drawing.Color.LightGray;
            }else if (!this.readyRoleFun.Text.Contains(e.CommandArgument.ToString()) && !this.lb_NewRoleFun.Text.Contains(e.CommandArgument.ToString()))
            {
                this.lb_NewRoleFun.Text += e.CommandArgument.ToString() + "\t";
                Button bt = (Button)e.Item.FindControl("roleAddFun_select");
                bt.BackColor = System.Drawing.Color.LightGray;
            }
        }
    }

    protected void Submit_Click(object sender, EventArgs e)
    {
        string roleName = this.lb_roleName.Text;
        string[] newFun = Regex.Split(this.lb_NewRoleFun.Text, "\t");
        string[] delFun = Regex.Split(this.lb_RvRoleFun.Text, "\t");
        string addSql = string.Empty;
        string delSql = string.Empty;
        addSql = "insert into tb_rolefun values(@role_name,@fun_name)";
        delSql = "delete from tb_rolefun where role_name=@role_name and fun_name=@fun_name";
        DataBase db = new DataBase();
        for(int i = 0; i < newFun.Length; i++)
        {
            if (string.IsNullOrEmpty(newFun[i]))
            {
                continue;
            }
            SqlParameter[] prams =
            {
                db.MakeInParam("@role_name",SqlDbType.VarChar,40,roleName),
                db.MakeInParam("@fun_name",SqlDbType.VarChar,40,newFun[i]),
            };
            db.RunProc(addSql, prams);
        }
        for(int i = 0; i < delFun.Length; i++)
        {
            if (string.IsNullOrEmpty(delFun[i]))
            {
                continue;
            }
            SqlParameter[] prams =
            {
                db.MakeInParam("@role_name",SqlDbType.VarChar,40,roleName),
                db.MakeInParam("@fun_name",SqlDbType.VarChar,40,delFun[i]),
            };
            db.RunProc(delSql, prams);
        }
        this.Submit.Visible = false;
        this.Cancel.Visible = false;
        this.lb_NewRoleFun.Text = "";
        this.lb_RvRoleFun.Text = "";
        this.ShowNewRoleFun.Visible = false;
        this.ShowRemoveRoleFun.Visible = false;
        this.showEditRoleFun.Visible = false;
        this.btn_DeleteRoleFun.Visible = true;
        this.btn_EditRoleFun.Visible = true;
        string sql = string.Empty;
        sql = "select fun_name from tb_rolefun where role_name='" + roleName + "'";
        var res = db.GetDataSet(sql, "tb_roleFun");
        this.readyRoleFun.Text = "";
        foreach (DataTable dt in res.Tables)
        {
            foreach (DataRow dr in dt.Rows)
            {
                this.readyRoleFun.Text += dr.ItemArray[0].ToString() + "\t";
            }
        }
        Response.Write("<script>alert('修改成功!')</script>");
    }

    protected void Cancel_Click(object sender, EventArgs e)
    {
        this.Submit.Visible = false;
        this.Cancel.Visible = false;
        this.lb_NewRoleFun.Text = "";
        this.lb_RvRoleFun.Text = "";
        this.ShowNewRoleFun.Visible = false;
        this.ShowRemoveRoleFun.Visible = false;
        this.showEditRoleFun.Visible = false;
        this.btn_DeleteRoleFun.Visible = true;
        this.btn_EditRoleFun.Visible = true;
    }

    protected void EditTeaRole_Click(object sender, EventArgs e)
    {
        this.btn_EditTeaRole.Visible = false;
        this.tea_Submit.Visible = true;
        this.tea_Cancel.Visible = true;
        this.showTeaEditRole.Visible = true;
        this.ShowNewTeaRole.Visible = true;
        this.ShowRemoveTeaRole.Visible = true;
        var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new IdentityDbContext()));
        var roles = roleManager.Roles.ToList();
        this.DL_EditTeaRole.DataSource = roles;
        this.DL_EditTeaRole.DataBind();
        foreach (DataListItem item in this.DL_EditTeaRole.Items)
        {
            Button bt = (Button)item.FindControl("TeaAddRole_select");
            if (this.readyTeaRole.Text.Contains(bt.Text))
            {
                bt.BackColor = System.Drawing.Color.LightGray;
            }
        }
    }

    protected void Tea_Submit_Click(object sender, EventArgs e)
    {
        string teaName = this.lb_TeaName.Text;
        string[] newRole = Regex.Split(this.lb_NewTeaRole.Text, "\t");
        string[] delRole = Regex.Split(this.lb_RvTeaRole.Text, "\t");
        for (int i = 0; i < newRole.Length; i++)
        {
            if (string.IsNullOrEmpty(newRole[i]))
            {
                continue;
            }
            var manager = new UserManager();
            var user = manager.FindByName(teaName);
            manager.AddToRole(user.Id, newRole[i]);
        }
        for (int i = 0; i < delRole.Length; i++)
        {
            if (string.IsNullOrEmpty(delRole[i]))
            {
                continue;
            }
            var manager = new UserManager();
            var user = manager.FindByName(teaName);
            manager.RemoveFromRole(user.Id, delRole[i]);
        }
        this.tea_Submit.Visible = false;
        this.tea_Cancel.Visible = false;
        this.lb_NewTeaRole.Text = "";
        this.lb_RvTeaRole.Text = "";
        this.ShowNewTeaRole.Visible = false;
        this.ShowRemoveTeaRole.Visible = false;
        this.showTeaEditRole.Visible = false;
        this.btn_EditTeaRole.Visible = true;
        var m = new UserManager();
        ApplicationUser u = m.FindByName(teaName);
        var roles = u.Roles.ToList();
        var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new IdentityDbContext()));
        this.readyTeaRole.Text = "";
        foreach (IdentityUserRole r in roles)
        {
            var role = roleManager.FindById(r.RoleId);
            this.readyTeaRole.Text += role.Name + "\t";
        }
        Response.Write("<script>alert('修改成功!')</script>");
    }

    protected void Tea_Cancel_Click(object sender, EventArgs e)
    {
        this.tea_Submit.Visible = false;
        this.tea_Cancel.Visible = false;
        this.lb_NewTeaRole.Text = "";
        this.lb_RvTeaRole.Text = "";
        this.ShowNewTeaRole.Visible = false;
        this.ShowRemoveTeaRole.Visible = false;
        this.showTeaEditRole.Visible = false;
        this.btn_EditTeaRole.Visible = true;
    }

    protected void DL_EditTeaRole_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if(e.CommandName == "Add_TeaRole")
        {
            if (this.readyTeaRole.Text.Contains(e.CommandArgument.ToString()) && !this.lb_RvTeaRole.Text.Contains(e.CommandArgument.ToString()))
            {
                this.lb_RvTeaRole.Text += e.CommandArgument.ToString() + "\t";
                Button bt = (Button)e.Item.FindControl("TeaAddRole_select");
                bt.BackColor = System.Drawing.Color.Red;
            }
            else if (this.lb_NewTeaRole.Text.Contains(e.CommandArgument.ToString()))
            {
                this.lb_NewTeaRole.Text = this.lb_NewTeaRole.Text.Replace(e.CommandArgument.ToString() + "\t", "");
                Button bt = (Button)e.Item.FindControl("TeaAddRole_select");
                bt.BackColor = System.Drawing.Color.White;
            }
            else if (this.readyTeaRole.Text.Contains(e.CommandArgument.ToString()) && this.lb_RvTeaRole.Text.Contains(e.CommandArgument.ToString()))
            {
                this.lb_RvTeaRole.Text = this.lb_RvTeaRole.Text.Replace(e.CommandArgument.ToString() + "\t", "");
                Button bt = (Button)e.Item.FindControl("TeaAddRole_select");
                bt.BackColor = System.Drawing.Color.LightGray;
            }
            else if (!this.readyTeaRole.Text.Contains(e.CommandArgument.ToString()) && !this.lb_NewTeaRole.Text.Contains(e.CommandArgument.ToString()))
            {
                this.lb_NewTeaRole.Text += e.CommandArgument.ToString() + "\t";
                Button bt = (Button)e.Item.FindControl("TeaAddRole_select");
                bt.BackColor = System.Drawing.Color.LightGray;
            }
        }
    }

    protected void DL_EditStuRole_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if(e.CommandName == "Add_StuRole")
        {
            if (this.readyStuRole.Text.Contains(e.CommandArgument.ToString()) && !this.lb_RvStuRole.Text.Contains(e.CommandArgument.ToString()))
            {
                this.lb_RvStuRole.Text += e.CommandArgument.ToString() + "\t";
                Button bt = (Button)e.Item.FindControl("StuAddRole_select");
                bt.BackColor = System.Drawing.Color.Red;
            }
            else if (this.lb_NewStuRole.Text.Contains(e.CommandArgument.ToString()))
            {
                this.lb_NewStuRole.Text = this.lb_NewStuRole.Text.Replace(e.CommandArgument.ToString() + "\t", "");
                Button bt = (Button)e.Item.FindControl("StuAddRole_select");
                bt.BackColor = System.Drawing.Color.White;
            }
            else if (this.readyStuRole.Text.Contains(e.CommandArgument.ToString()) && this.lb_RvStuRole.Text.Contains(e.CommandArgument.ToString()))
            {
                this.lb_RvStuRole.Text = this.lb_RvStuRole.Text.Replace(e.CommandArgument.ToString() + "\t", "");
                Button bt = (Button)e.Item.FindControl("StuAddRole_select");
                bt.BackColor = System.Drawing.Color.LightGray;
            }
            else if (!this.readyStuRole.Text.Contains(e.CommandArgument.ToString()) && !this.lb_NewStuRole.Text.Contains(e.CommandArgument.ToString()))
            {
                this.lb_NewStuRole.Text += e.CommandArgument.ToString() + "\t";
                Button bt = (Button)e.Item.FindControl("StuAddRole_select");
                bt.BackColor = System.Drawing.Color.LightGray;
            }
        }
    }

    protected void Stu_Submit_Click(object sender, EventArgs e)
    {
        string stuName = this.lb_StuName.Text;
        string[] newRole = Regex.Split(this.lb_NewStuRole.Text, "\t");
        string[] delRole = Regex.Split(this.lb_RvStuRole.Text, "\t");
        for (int i = 0; i < newRole.Length; i++)
        {
            if (string.IsNullOrEmpty(newRole[i]))
            {
                continue;
            }
            var manager = new UserManager();
            var user = manager.FindByName(stuName);
            manager.AddToRole(user.Id, newRole[i]);
        }
        for (int i = 0; i < delRole.Length; i++)
        {
            if (string.IsNullOrEmpty(delRole[i]))
            {
                continue;
            }
            var manager = new UserManager();
            var user = manager.FindByName(stuName);
            manager.RemoveFromRole(user.Id, delRole[i]);
        }
        this.stu_Submit.Visible = false;
        this.stu_Cancel.Visible = false;
        this.lb_NewStuRole.Text = "";
        this.lb_RvStuRole.Text = "";
        this.ShowNewStuRole.Visible = false;
        this.ShowRemoveStuRole.Visible = false;
        this.showStuEditRole.Visible = false;
        this.btn_EditStuRole.Visible = true;
        var m = new UserManager();
        ApplicationUser u = m.FindByName(stuName);
        var roles = u.Roles.ToList();
        var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new IdentityDbContext()));
        this.readyStuRole.Text = "";
        foreach (IdentityUserRole r in roles)
        {
            var role = roleManager.FindById(r.RoleId);
            this.readyStuRole.Text += role.Name + "\t";
        }
        Response.Write("<script>alert('修改成功!')</script>");
    }

    protected void Stu_Cancel_Click(object sender, EventArgs e)
    {
        this.stu_Submit.Visible = false;
        this.stu_Cancel.Visible = false;
        this.lb_NewStuRole.Text = "";
        this.lb_RvStuRole.Text = "";
        this.ShowNewStuRole.Visible = false;
        this.ShowRemoveStuRole.Visible = false;
        this.showStuEditRole.Visible = false;
        this.btn_EditStuRole.Visible = true;
    }

    protected void EditStuRole_Click(object sender, EventArgs e)
    {
        this.btn_EditStuRole.Visible = false;
        this.stu_Submit.Visible = true;
        this.stu_Cancel.Visible = true;
        this.showStuEditRole.Visible = true;
        this.ShowNewStuRole.Visible = true;
        this.ShowRemoveStuRole.Visible = true;
        var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new IdentityDbContext()));
        var roles = roleManager.Roles.ToList();
        this.DL_EditStuRole.DataSource = roles;
        this.DL_EditStuRole.DataBind();
        foreach (DataListItem item in this.DL_EditStuRole.Items)
        {
            Button bt = (Button)item.FindControl("StuAddRole_select");
            if (this.readyStuRole.Text.Contains(bt.Text))
            {
                bt.BackColor = System.Drawing.Color.LightGray;
            }
        }
    }

    protected void SelectAllFun(object sender, EventArgs e)
    {
        if (this.CreateSelectAllFun.Checked)
        {
            this.no_fun.Visible = false;
            this.role_fun.Text = "";
            foreach (DataListItem item in this.dl_fun.Items)
            {
                Button bt = (Button)item.FindControl("fun_select");
                bt.BackColor = System.Drawing.Color.LightGray;
                this.role_fun.Text += bt.Text + "\t";
            }
        }
        else
        {
            this.no_fun.Visible = true;
            this.role_fun.Text = "";
            foreach (DataListItem item in this.dl_fun.Items)
            {
                Button bt = (Button)item.FindControl("fun_select");
                bt.BackColor = System.Drawing.Color.White;
            }
        }
    }

    protected void TeacherDL_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if(e.CommandName == "showTeaDetail")
        {
            this.lb_TeaName.Text = e.CommandArgument.ToString();
            HiddenField hf = (HiddenField)e.Item.FindControl("tea_id");
            string tea_id = hf.Value;
            var manager = new UserManager();
            ApplicationUser user = manager.FindById(tea_id);
            var roles = user.Roles.ToList();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new IdentityDbContext()));
            this.readyTeaRole.Text = "";
            foreach (IdentityUserRole r in roles)
            {
                var role = roleManager.FindById(r.RoleId);
                this.readyTeaRole.Text += role.Name + "\t";
            }
        }
    }

    protected void StudentDL_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if(e.CommandName == "showStuDetail")
        {
            this.lb_StuName.Text = e.CommandArgument.ToString();
            HiddenField hf = (HiddenField)e.Item.FindControl("stu_id");
            string stu_id = hf.Value;
            var manager = new UserManager();
            ApplicationUser user = manager.FindById(stu_id);
            var roles = user.Roles.ToList();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new IdentityDbContext()));
            this.readyStuRole.Text = "";
            foreach (IdentityUserRole r in roles)
            {
                var role = roleManager.FindById(r.RoleId);
                this.readyStuRole.Text += role.Name + "\t";
            }
        }
    }
}