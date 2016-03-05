using RecruitmentSystem.Controllers.Base;
using RecruitmentSystem.Downloads;
using RecruitmentSystem.Models;
using System.Web.Mvc;

namespace RecruitmentSystem.Controllers
{
    public class DownloadController : BaseController
    {
        /// <summary>
        /// Downloads a PDF generated from an Application.
        /// </summary>
        /// <param name="application">Application to generate PDF with.</param>
        /// <returns>Adds a binary to the response header.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PdfDownload(Application application)
        {
            byte[] fileBinary = PdfGenerator.GenerateApplicationPdf(application);
            string fileName = string.Format("application_{1}_{0}_{2}", application.Person.Name, application.Person.Surname, application.ApplicationDate.ToShortDateString());
            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", string.Format("attachment;filename={0}.pdf", fileName));
            Response.BinaryWrite(fileBinary);
            Response.End();

            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}