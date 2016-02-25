using RecruitmentSystem.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

namespace RecruitmentSystem.Models.ViewModel
{
    public class ApplicationView
    {
        public ApplicationView()
        {
            _Competences = new QueryService<Competence>().GetAll();
            Competences = new List<SelectListItem>();
            SelectedCompetences = new Dictionary<Competence, decimal>();
            SelectedAvailabilities = new Dictionary<DateTime, DateTime>();
        }

        [DisplayName("Competence")]
        public int SelectedCompetence { get; set; }

        [DisplayName("Years of Experience")]
        [DisplayFormat(ApplyFormatInEditMode = true)]
        public decimal SelectedYearsOfExperience { get; set; }

        [DisplayName("Available Competences")]
        public IEnumerable<SelectListItem> Competences
        {
            get
            {
                List<SelectListItem> competences = new List<SelectListItem>();
                competences.Add(new SelectListItem() { Text = "Select competence..." });
                foreach (Competence competence in _Competences)
                {
                    competences.Add(new SelectListItem() { Value = competence.Id.ToString(), Text = competence.Name });
                }

                return competences;
            }
            set
            {
            }
        }

        public Dictionary<Competence, decimal> SelectedCompetences { get; set; }

        private IEnumerable<Competence> _Competences { get; set; }

        [DisplayName("Available From")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime SelectedFromDate { get; set; }

        [DisplayName("Available To")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime SelectedToDate { get; set; }

        public Dictionary<DateTime, DateTime> SelectedAvailabilities { get; set; }

        public void AddCompetence()
        {
            Competence key = _Competences.Single(c => c.Id == SelectedCompetence);
            SelectedCompetences[key] = SelectedYearsOfExperience;
        }

        public void RemoveCompetence(Competence competence)
        {
            SelectedCompetences.Remove(competence);
        }

        public void AddAvailability()
        {
            SelectedAvailabilities[SelectedFromDate] = SelectedToDate;
        }
    }
}