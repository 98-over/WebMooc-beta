using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DetailCourse : System.Web.UI.Page
{
    public static string id;
    protected void Page_Load(object sender, EventArgs e)
    {
        id = Request.QueryString["id"];
        DropDownList1.DataBind();
        DropDownList1.Items.Insert(0, new ListItem("请选择", "0"));
    }
}