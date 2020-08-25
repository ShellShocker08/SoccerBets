using System.ComponentModel.DataAnnotations;

namespace Soccer.Common.Request
{
    public class EmailRequest
    {

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string CultureInfo { get; set; }


    }
}
