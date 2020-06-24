using System;
using System.ComponentModel.DataAnnotations;

namespace Soccer.Web.Data.Entities
{
    public class MatchEntity
    {
        public int Id { get; set; }

        [Display(Name = "Fecha")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm}", ApplyFormatInEditMode = false)]
        public DateTime Date { get; set; }

        [Display(Name = "Fecha Local")]
        public DateTime DateLocal => Date.ToLocalTime();

        public TeamEntity Local { get; set; }

        [Display(Name = "Visitante")]
        public TeamEntity Visitor { get; set; }

        [Display(Name = "Goles Local")]
        public int GoalsLocal { get; set; }

        [Display(Name = "Goles Visitantes")]
        public int GoalsVisitor { get; set; }

        [Display(Name = "Cerrado")]
        public bool IsClosed { get; set; }

        [Display(Name = "Activo")]
        public bool Active { get; set; }

        public GroupEntity Group { get; set; }
    }
}
