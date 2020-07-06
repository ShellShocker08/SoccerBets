using System.ComponentModel.DataAnnotations;

namespace Soccer.Web.Data.Entities
{
    public class PredictionEntity
    {
        public int Id { get; set; }

        public MatchEntity Match { get; set; }

        public UserEntity User { get; set; }

        [Display(Name = "Goles Local")]
        public int? GoalsLocal { get; set; }

        [Display(Name = "Goles Visitante")]
        public int? GoalsVisitor { get; set; }

        [Display(Name = "Puntos")]
        public int Points { get; set; }
    }
}
