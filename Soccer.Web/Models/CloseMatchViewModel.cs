using Soccer.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Soccer.Web.Models
{
    public class CloseMatchViewModel
    {
        public int MatchId { get; set; }

        public int GroupId { get; set; }

        public int LocalId { get; set; }

        public int VisitorId { get; set; }

        [Display(Name = "Goles Local")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public int? GoalsLocal { get; set; }

        [Display(Name = "Goales Visitante")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public int? GoalsVisitor { get; set; }

        public GroupEntity Group { get; set; }

        public TeamEntity Local { get; set; }

        public TeamEntity Visitor { get; set; }

    }
}
