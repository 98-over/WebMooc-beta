using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.UI;
using WebMooc_beta;
using Model;
using DAL;
using IDAL;

public partial class Account_Register : Page
{
    protected void CreateUser_Click(object sender, EventArgs e)
    {
        var manager = new UserManager();
        var user = new ApplicationUser() { UserName = UserName.Text };
        IdentityResult result = manager.Create(user, Password.Text);
        if (result.Succeeded)
        {
            IUser_Student imp_stu = DataAccess.CreateStudent();
            User_Student stu = new User_Student();
            stu.StuId1 = user.Id;
            string sql = string.Empty;
            sql = "insert into user_id values('" + user.Id + "')";
            DataBase db = new DataBase();
            db.RunProc(sql);
            stu.StuName1 = UserName.Text;
            imp_stu.InsertStudent(stu);
            manager.AddToRole(user.Id, "student");
            IdentityHelper.SignIn(manager, user, isPersistent: false);
            IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
        }
        else
        {
            ErrorMessage.Text = result.Errors.FirstOrDefault();
        }
    }
}