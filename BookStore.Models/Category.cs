using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.JavaScript;

namespace Bookstore.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} is verplicht veld.")]
        [MaxLength(50, ErrorMessage = "{0} mag maximaal {1} karakters lang zijn.")]
        [Display(Name = "Naam")]
        public string? Name  { get; set; }
        [Display(Name = "Volgnummer")]
        [Range(minimum:1,maximum:100,ErrorMessage = "{0} moet tussen {1} en {2} liggen.")]
        public int DisplayOrder { get; set; }
        [Display(Name = "Aanmaakdatum")]
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
        
    }
}
