using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using DAL;
using Common;

public partial class ShowCourse : System.Web.UI.Page
{
    public static PagedDataSource pds = new PagedDataSource();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindDL(0);

        }
    }
    public void bindDL(int curPage)
    {

        pds.AllowPaging = true;
        pds.PageSize = 3;
        pds.CurrentPageIndex = curPage;

        //绑定数据源
        CourseDAL courseDAL = new CourseDAL();

        DataSet ds = SqlServerHelper.GetDataSet();

        pds.DataSource = ds.Tables[0].DefaultView;
        dlPhoto.DataSource = pds;
        dlPhoto.DataBind();

    }

    protected void dlPhoto_ItemCommand(object source, DataListCommandEventArgs e)
    {

        switch (e.CommandName)
        {
            case "first":
                if (pds.CurrentPageIndex != 0)
                {
                    pds.CurrentPageIndex = 0;
                }
                else
                {
                    Response.Write("<script language=javascript>" + "alert(\"已经是第一页\")" + "</script>");
                }
                bindDL(pds.CurrentPageIndex);
                break;
            case "pre":
                if (pds.CurrentPageIndex > 0)
                {
                    pds.CurrentPageIndex = pds.CurrentPageIndex - 1;
                    bindDL(pds.CurrentPageIndex);
                }
                else
                {
                    Response.Write("<script language=javascript>" + "alert(\"已经是第一页\")" + "</script>");
                }

                break;
            case "next":
                if (pds.CurrentPageIndex < pds.PageCount - 1)
                {
                    pds.CurrentPageIndex = pds.CurrentPageIndex + 1;
                    bindDL(pds.CurrentPageIndex);
                }
                else
                {
                    Response.Write("<script language=javascript>" + "alert(\"已经是最后一页\")" + "</script>");
                }

                break;
            case "last":
                if (pds.CurrentPageIndex != pds.PageCount - 1)
                {
                    pds.CurrentPageIndex = pds.PageCount - 1;

                }
                else
                {
                    Response.Write("<script language=javascript>" + "alert(\"已经是最后一页\")" + "</script>");
                }
                bindDL(pds.CurrentPageIndex);
                break;
            case "search":
                if (e.Item.ItemType == ListItemType.Footer)
                {
                    int pageCount = int.Parse(pds.PageCount.ToString());
                    TextBox txtPage = e.Item.FindControl("txtPage") as TextBox;
                    int myPage = 0;
                    if (txtPage.Text != null)
                    {
                        myPage = int.Parse(txtPage.Text.Trim().ToString());

                    }
                    if (myPage <= 0 || myPage > pageCount - 1)
                    {
                        Response.Write("<script>alert('请输入正确的页面数！')</script>");

                    }
                    bindDL(myPage);


                }
                break;

            case "detail":
                Response.Redirect("DetailCourse.aspx?id=" + e.CommandArgument);

                break;

        }
    }
}