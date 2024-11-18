using System.ComponentModel.DataAnnotations;

namespace Bookstore.Models
{
	public class CoverType
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[MaxLength(50)]
		[Display(Name = "Soort kaft")]
		public string Name { get; set; }
	}
}
