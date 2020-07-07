using Microsoft.AspNetCore.Identity;
using Soccer.Common.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Soccer.Web.Data.Entities
{
    public class UserEntity : IdentityUser
    {
        [Display(Name = "Document")]
        [MaxLength(20, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Document { get; set; }

        [Display(Name = "Nombre(s)")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string FirstName { get; set; }

        [Display(Name = "Apellido")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string LastName { get; set; }

        [MaxLength(300, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        public string Address { get; set; }

        [Display(Name = "Imagen")]
        public string PicturePath { get; set; }

        [Display(Name = "Tipo de Usuario")]
        public UserType UserType { get; set; }

        [Display(Name = "Equipo Favorito")]
        public TeamEntity Team { get; set; }

        [Display(Name = "Nombre")]
        public string FullName => $"{FirstName} {LastName}";

        [Display(Name = "Usuario")]
        public string FullNameWithDocument => $"{FirstName} {LastName} - {Document}";

        public ICollection<PredictionEntity> Predictions { get; set; }

    }
}
