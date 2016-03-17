using RecruitmentSystem.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;


namespace RecruitmentSystem.Models.ViewModel
{
    /// <summary>
    /// View model for application views
    /// </summary>
    public class ApplicationView
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ApplicationView() : this(new QueryService<Competence>().GetAll())
        {
        }

        /// <summary>
        /// Constructor. Takes a list of competences.
        /// </summary>
        /// <param name="competences"></param>
        public ApplicationView(IList<Competence> competences)
        {
            _competences = competences;
            SelectedCompetences = new Dictionary<int, decimal>();
            SelectedAvailabilities = new Dictionary<DateTime, DateTime>();
        }

        /// <summary>
        /// GET/SET
        /// </summary>
        [DisplayFormat(ApplyFormatInEditMode = true)]
        public decimal SelectedYearsOfExperience { get; set; }

        /// <summary>
        /// GET/SET. Returns a IEnumerable list of competences from _competences
        /// </summary>
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

        /// <summary>
        /// GET/SET
        /// </summary>
        public Dictionary<int, decimal> SelectedCompetences { get; set; }

        /// <summary>
        /// GET/SET
        /// </summary>
        private IEnumerable<Competence> _competences { get; set; }

        /// <summary>
        /// GET/SET
        /// </summary>
        public Dictionary<DateTime, DateTime> SelectedAvailabilities { get; set; }
    }
}