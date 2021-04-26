using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ToPdf : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string name = Request.QueryString["name"];
        Goto(Request.ApplicationPath + "/Course_source/" + name, Request.ApplicationPath + "Course_source/temp.pdf");
    }
    public void Goto(string sourceDoc, string saveDoc)
    {
        string s_sourceDoc = Server.MapPath(sourceDoc);

        string s_saveDoc = Server.MapPath(saveDoc);

        string docExtendName = System.IO.Path.GetExtension(s_sourceDoc).ToLower();

        switch (docExtendName)
        {

            case ".doc":
            case ".docx":
                Aspose.Words.Document doc = new Aspose.Words.Document(s_sourceDoc);
                doc.Save(s_saveDoc, Aspose.Words.SaveFormat.Pdf);
                Response.Redirect(saveDoc);
                break;
            case ".xls":
            case ".xlsx":
                Aspose.Cells.Workbook workbook = new Aspose.Cells.Workbook(s_sourceDoc);
                workbook.Save(s_saveDoc, Aspose.Cells.SaveFormat.Pdf);
                Response.Redirect(saveDoc);
                break;

            case ".ppt":
            case ".pptx":
                Aspose.Slides.Pptx.PresentationEx pres = new Aspose.Slides.Pptx.PresentationEx(s_sourceDoc);
                pres.Save(s_saveDoc, Aspose.Slides.Export.SaveFormat.Pdf);
                Response.Redirect(saveDoc);
                break;
            case ".pdf":
                Aspose.Pdf.Document pdf = new Aspose.Pdf.Document(s_sourceDoc);
                pdf.Save(s_saveDoc, Aspose.Pdf.SaveFormat.Pdf);
                Response.Redirect(saveDoc);
                break;
            case ".txt":
                string content = File.ReadAllText(s_sourceDoc, Encoding.UTF8);
                File.WriteAllText(s_saveDoc.Replace("pdf", "txt"), content, Encoding.UTF8);
                Response.Redirect(saveDoc);
                break;
        }
    }
}