using RecruitmentSystem.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace RecruitmentSystem.Models.ViewModel
{
    public class RecruiterView
    {
        
        public RecruiterView()
        {
            Competences = new List<SelectListItem>();
            FromDate = new List<SelectListItem>();
            ToDate = new List<SelectListItem>();
            DatesOfApplication = new List<SelectListItem>();
            Names = new List<SelectListItem>();
            Result = new List<Application>();
            _Applications = new QueryService<Application>().GetAll();
            _Competences = new QueryService<Competence>().GetAll();
            _Availabilities = new QueryService<Availability>().GetAll();
        }

        
        private IEnumerable<Competence> _Competences { get; set; }

        
        private IEnumerable<Availability> _Availabilities { get; set; }
        private IEnumerable<Application> _Applications { get; set; }
        private List<SelectListItem> _Names = new List<SelectListItem>();
        public IEnumerable<SelectListItem> DatesOfApplication
        {
            get
            {
                List<SelectListItem> datesOfApplication = new List<SelectListItem>();
                datesOfApplication.Add(new SelectListItem() { Text = "Any" });
                _Names.Add(new SelectListItem() { Text = "Any" });
                foreach (Application a in _Applications)
                {
                    _Names.Add(new SelectListItem() { Value = a.Person.Id.ToString(), Text = a.Person.Name });
                    datesOfApplication.Add(new SelectListItem() { Value = a.Id.ToString(), Text = a.ApplicationDate.ToString() });
                }

                return datesOfApplication;
            }
            set
            {
            }
        }

        [DisplayName("Names")]
        public IEnumerable<SelectListItem> Names
        {
            get
            {
                return _Names;
            }
            set
            {
            }
        }

        [DisplayName("Competence")]
        public IEnumerable<SelectListItem> Competences
        {
            get
            {
                List<SelectListItem> competences = new List<SelectListItem>();
                competences.Add(new SelectListItem() { Text = "Any" });
                foreach (Competence competence in _Competences)
                {
                    competences.Add(new SelectListItem() { Value = competence.Id.ToString(), Text = competence.LocalizedName });
                }

                return competences;
            }
            set
            {
            }
        }

        [DisplayName("Available From")]
        public IEnumerable<SelectListItem> FromDate
        {
            get
            {
                List<SelectListItem> fromDate = new List<SelectListItem>();
                fromDate.Add(new SelectListItem() { Text = "Any" });
                foreach (Availability a in _Availabilities)
                {
                    fromDate.Add(new SelectListItem() { Value = a.Id.ToString(), Text = a.FromDate.ToString() });
                }
                return fromDate;
            }
            set { }
        }

        [DisplayName("Available To")]
        public IEnumerable<SelectListItem> ToDate
        {
            get
            {
                List<SelectListItem> toDate = new List<SelectListItem>();
                toDate.Add(new SelectListItem() { Text = "Any" });
                foreach (Availability a in _Availabilities)
                {
                    toDate.Add(new SelectListItem() { Value = a.Id.ToString(), Text = a.ToDate.ToString() });
                }
                return toDate;
            }
            set { }
        }

        

        [DisplayName("Selected Competence")]
        public Competence SelectedCompetence { get; set; }
        public DateTime SelectedFromDate { get; set; }
        public DateTime SelectedToDate { get; set; }
        public DateTime SelectedDateOfRegistration { get; set; }
        public string SelectedName { get; set; }
        public List<Application> Result { get; set; }

        
    }




}