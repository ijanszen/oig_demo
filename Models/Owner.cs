using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace dashboard.Models
{     
    public class Owner
    {
        public int ID { get; set; }
        [Required]
        [Display(Name = "Naam")]
        public string Name { get; set; }
        public virtual ICollection<Survey> Surveys { get; set; }
    }
}