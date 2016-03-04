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
            SelectedCompetences = new Dictionary<int, decimal>();
            SelectedAvailabilities = new Dictionary<DateTime, DateTime>();
        }

        [DisplayFormat(ApplyFormatInEditMode = true)]
        public decimal SelectedYearsOfExperience { get; set; }

        public IEnumerable<SelectListItem> Competences
        {
            get
            {
                return _competences.Aggregate(new List<SelectListItem>(), (accumulator, entry) =>
                    {
                        accumulator.Add(new SelectListItem()
                        {
                            Value = entry.Id.ToString(),
                            Text = entry.LocalizedName
                        });
                        return accumulator;
                    });
            }
            set {}
        }

        public Dictionary<int, decimal> SelectedCompetences { get; set; }

        private IEnumerable<Competence> _competences { get; set; }

        public Dictionary<DateTime, DateTime> SelectedAvailabilities { get; set; }
    }
}