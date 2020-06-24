using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Soccer.Web.Data.Entities
{
    public class GroupEntity
    {
        public int Id { get; set; }

        [MaxLength(30, ErrorMessage = "El campo {0} debe tener un máximo de {1}.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; }

        public TournamentEntity Tournament { get; set; }

        public ICollection<GroupDetailEntity> GroupDetails { get; set; }

        public ICollection<MatchEntity> Matches { get; set; }

        [Display(Name = "Activo")]
        public bool Active { get; set; }

    }

}
