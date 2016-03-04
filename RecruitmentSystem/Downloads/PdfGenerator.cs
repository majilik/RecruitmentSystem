
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using RecruitmentSystem.Models;
using RecruitmentSystem.Resources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;

namespace RecruitmentSystem.Downloads
{
    public class PdfGenerator
    {
        public static byte[] GenerateApplicationPdf(Application application)
        {
            //Create pdf document
            PdfDocument pdf = new PdfDocument();
            PdfPage page = pdf.AddPage();
            //Create pdf content
            Document doc = CreateDocument("Application", string.Format("{1}, {0}",application.Person.Name, application.Person.Surname));
            PopulateDocument(ref doc, application);
            //Create renderer for content
            DocumentRenderer renderer = new DocumentRenderer(doc);
            renderer.PrepareDocument();
            XRect A4 = new XRect(0, 0, XUnit.FromCentimeter(21).Point, XUnit.FromCentimeter(29.7).Point);
            XGraphics gfx = XGraphics.FromPdfPage(page);

            int pages = renderer.FormattedDocument.PageCount;
            for(int i = 0; i < pages; i++)
            {
                var container = gfx.BeginContainer(A4, A4, XGraphicsUnit.Point);
                gfx.DrawRectangle(XPens.LightGray, A4);
                renderer.RenderPage(gfx, (i + 1));
                gfx.EndContainer(container);
            }

            using (MemoryStream ms = new MemoryStream())
            {
                pdf.Save(ms, true);
                return ms.ToArray();
            }
        }

        private static Document CreateDocument(string title, string subject)
        {
            Document document = new Document();
            #region DocInfo
            document.Info.Title = title;
            document.Info.Subject = subject;
            Locales currentLocale = LocalesExtension.LocalesFromString(Thread.CurrentThread.CurrentUICulture.Name);
            document.Info.Author = "RecruitmentSystem";
            #endregion
            #region DocStyle
            //Set normal text styles
            Style style = document.Styles[StyleNames.Normal];
            style.Font.Name = "Arial";

            //Set header styles
            style = document.Styles[StyleNames.Header];
            style.ParagraphFormat.AddTabStop("16cm", TabAlignment.Right);

            //Set footer styles
            style = document.Styles[StyleNames.Footer];
            style.ParagraphFormat.AddTabStop("8cm", TabAlignment.Center);

            //Add table style
            style = document.Styles.AddStyle("Table", StyleNames.Normal);
            style.Font.Name = "Arial";
            style.Font.Size = 9;
            #endregion

            return document;
        }

        private static void PopulateDocument(ref Document document, Application application)
        {
            Section section = document.AddSection();
            var header = section.Headers.Primary.AddParagraph();
            header.AddText("Application");
            header.Format.Font.Size = 25;
            header.Format.Font.Color = Colors.DarkBlue;
            header.Format.Font.Bold = true;
            header.Format.Font.Italic = true;
            header.Format.Alignment = ParagraphAlignment.Center;

            #region Person
            var tablePerson = section.AddTable();
            tablePerson.Style = "Table";
            tablePerson.Borders.Color = Colors.Gray;
            tablePerson.Borders.Width = 0.25;
            tablePerson.Borders.Left.Width = 0.5;
            tablePerson.Borders.Right.Width = 0.5;
            tablePerson.Rows.LeftIndent = 0;

            var person = application.Person;

            var column = tablePerson.AddColumn("2cm");
            column.Format.Alignment = ParagraphAlignment.Center;
            column = tablePerson.AddColumn("5cm");
            column.Format.Alignment = ParagraphAlignment.Left;
            var row = tablePerson.AddRow();
            row.Cells[0].Format.Font.Bold = true;
            row.Cells[0].Format.Font.Size = 18;
            row.Cells[0].AddParagraph("Applicant");
            row.Cells[0].MergeRight = 1;
            row = tablePerson.AddRow();
            row.Cells[0].AddParagraph("Name");
            row.Cells[1].AddParagraph(string.Format("{1}, {0}", person.Name, person.Surname));
            row = tablePerson.AddRow();
            row.Cells[0].AddParagraph("Social Security Number");
            row.Cells[1].AddParagraph(person.Ssn);
            row = tablePerson.AddRow();
            row.Cells[0].AddParagraph("Email");
            row.Cells[1].AddParagraph(person.Email);
            tablePerson.Rows.Alignment = RowAlignment.Center;

            row = tablePerson.AddRow();
            row.Borders.Visible = false;

            tablePerson.SetEdge(0, 1, 2, 3, Edge.Box, BorderStyle.Single, 0.75, Color.Empty);
            #endregion
            
            #region CompetenceProfile
            var tableCompetence = section.AddTable();
            tableCompetence.Style = "Table";
            tableCompetence.Borders.Color = Colors.Gray;
            tableCompetence.Borders.Width = 0.25;
            tableCompetence.Borders.Left.Width = 0.5;
            tableCompetence.Borders.Right.Width = 0.5;
            tableCompetence.Rows.LeftIndent = 0;
            tableCompetence.Rows.Alignment = RowAlignment.Center;

            column = tableCompetence.AddColumn("6cm");
            column.Format.Alignment = ParagraphAlignment.Left;
            column = tableCompetence.AddColumn("3cm");
            column.Format.Alignment = ParagraphAlignment.Center;

            //add headers
            row = tableCompetence.AddRow();
            row.Cells[0].Format.Font.Bold = true;
            row.Cells[0].Format.Font.Size = 18;
            row.Cells[0].AddParagraph("Competence Profile");
            row.Cells[0].MergeRight = 1;
            row = tableCompetence.AddRow();
            row.Cells[0].Format.Font.Bold = true;
            row.Cells[0].AddParagraph("Competence");
            row.Cells[1].Format.Font.Bold = true;
            row.Cells[1].AddParagraph("Experience (Years)");

            foreach (CompetenceProfile cp in application.CompetenceProfiles)
            {
                row = tableCompetence.AddRow();
                row.Cells[0].AddParagraph(cp.Competence.DefaultName);
                row.Cells[1].AddParagraph(cp.YearsOfExperience.ToString());
            }
            row = tableCompetence.AddRow();
            row.Borders.Visible = false;
            #endregion

            #region Availabilites
            var tableAvail = section.AddTable();
            tableAvail.Style = "Table";
            tableAvail.Borders.Color = Colors.Gray;
            tableAvail.Borders.Width = 0.25;
            tableAvail.Borders.Left.Width = 0.5;
            tableAvail.Borders.Right.Width = 0.5;
            tableAvail.Rows.LeftIndent = 0;
            tableAvail.Rows.Alignment = RowAlignment.Center;

            column = tableAvail.AddColumn("3cm");
            column.Format.Alignment = ParagraphAlignment.Center;
            column = tableAvail.AddColumn("3cm");
            column.Format.Alignment = ParagraphAlignment.Center;

            //add headers
            row = tableAvail.AddRow();
            row.Cells[0].Format.Font.Bold = true;
            row.Cells[0].Format.Font.Size = 18;
            row.Cells[0].AddParagraph("Availabilities");
            row.Cells[0].MergeRight = 1;
            row = tableAvail.AddRow();
            row.Cells[0].Format.Font.Bold = true;
            row.Cells[0].AddParagraph("From");
            row.Cells[1].Format.Font.Bold = true;
            row.Cells[1].AddParagraph("To");

            foreach (Availability ab in application.Availabilities)
            {
                row = tableAvail.AddRow();
                row.Cells[0].AddParagraph(ab.FromDate.ToShortDateString());
                row.Cells[1].AddParagraph(ab.ToDate.ToShortDateString());
            }

            #endregion
        }
    }
}