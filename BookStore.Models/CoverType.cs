﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models
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
