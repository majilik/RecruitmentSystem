using RecruitmentSystem.Controllers.Base;
using RecruitmentSystem.DAL.Query;
using RecruitmentSystem.Downloads;
using RecruitmentSystem.Models;
using System.Web.Mvc;

namespace RecruitmentSystem.Controllers
{
    /// <summary>
    /// Represents a controller that handles user actions through an
    /// ASP.NET MVC Web application and responds to this action.
    /// Controller handles requests for downloading files.
    /// </summary>
    public class DownloadController : BaseController
    {
        /// <summary>
        /// Downloads a PDF generated from an Application.
        /// </summary>
        /// <param name="id">Id of application to get for generation of PDF.</param>
        /// <returns>Adds a binary file to the response header.</returns>
        public ActionResult PdfDownload(int id)
        {
            Application application = GetApplication.Invoke(id);

            byte[] fileBinary = PdfGenerator.GenerateApplicationPdf(application);
            string fileName = string.Format("application_{1}_{0}_{2}", application.Person.Name,
                application.Person.Surname, application.ApplicationDate.ToShortDateString());
            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", string.Format("attachment;filename={0}.pdf", fileName));
            Response.BinaryWrite(fileBinary);
            Response.End();

            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}