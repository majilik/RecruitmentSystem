using RecruitmentSystem.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace RecruitmentSystem.Models.ViewModel
{
    public class ApplicationView
    {
        public ApplicationView() : this(new QueryService<Competence>().GetAll())
        {
        }

        public ApplicationView(IList<Competence> competences)
        {
            _competences = competences;
            SelectedCompetences = SelectedCompetences ?? new Dictionary<Competence, decimal>();
            Competences = new List<SelectListItem>();
            SelectedAvailabilities = SelectedAvailabilities ?? new Dictionary<DateTime, DateTime>();
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
                foreach (Competence competence in _competences)
                {
                    competences.Add(new SelectListItem() { Value = competence.Id.ToString(), Text = competence.LocalizedName });
                }

                return competences;
            }
            set
            {
            }
        }

        public Dictionary<Competence, decimal> SelectedCompetences { get; set; }

        private IEnumerable<Competence> _competences { get; set; }

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
            Competence key = _competences.Single(c => c.Id == SelectedCompetence);
            SelectedCompetences[key] = SelectedYearsOfExperience;
            key = null;
        }

        public void RemoveCompetence(Competence competence)
        {
            //SelectedCompetences.Remove(competence);
        }

        public void AddAvailability()
        {
            SelectedAvailabilities[SelectedFromDate] = SelectedToDate;
        }
    }
}