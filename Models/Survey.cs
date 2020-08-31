using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using dashboard.Common;

namespace dashboard.Models
{
    public enum Status {Concept, Gepland, Lopend, Afgerond}
    public class Survey : IValidatableObject
    {
        public int ID { get; set; }
        [Required]
        [Display(Name = "Naam")]
        public String Name { get; set; }
        public Status Status { get; set;  }
        [DataType(DataType.DateTime), Display(Name = "Startdatum"), BiggerThanCurrentDate()]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        [DataType(DataType.DateTime), Display(Prompt = "yyyy-MM-dd HH:mm", Name = "Eindatum")] 
        [BiggerThanCurrentDate()]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }
        public int OwnerID { get; set; }
        public virtual Owner Owner { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (EndDate.CompareTo(StartDate) <= 0 )
            {
                yield return new ValidationResult(
                   "De eindatum van een onderzoek moet minstens een uur later zijn dan de begindatum.",
                   new[] { nameof(EndDate), nameof(StartDate) });
            }
        }
    }
}