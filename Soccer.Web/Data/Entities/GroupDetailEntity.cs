using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Soccer.Web.Data.Entities
{
    public class GroupDetailEntity
    {
        public int Id { get; set; }

        public TeamEntity Team { get; set; }

        [Display(Name = "Partidos Jugados")]
        public int MatchesPlayed { get; set; }

        [Display(Name = "Partidos Ganados")]
        public int MatchesWon { get; set; }

        [Display(Name = "Partidos Empatados")]
        public int MatchesTied { get; set; }

        [Display(Name = "Partidos Perdidos")]
        public int MatchesLost { get; set; }

        [Display(Name = "Puntos")]
        public int Points => MatchesWon * 3 + MatchesTied;

        [Display(Name = "Goles a Favor")]
        public int GoalsFor { get; set; }

        [Display(Name = "Goles en Contra")]
        public int GoalsAgainst { get; set; }

        [Display(Name = "Diferencia de Goles")]
        public int GoalDifference => GoalsFor - GoalsAgainst;

        public GroupEntity Group { get; set; }
    }

}
