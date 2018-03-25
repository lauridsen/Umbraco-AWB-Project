using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AarhusWebDevCoop.ViewModels
{
	public class ContactForm
	{
		[Required(ErrorMessage = "Please enter your name")]
		[StringLength(200, MinimumLength = 2)]
		public string Name { get; set; }

		[Required]
		[StringLength(200, MinimumLength = 2)]
		[EmailAddress(ErrorMessage = "Please enter a valid email address.")]
		public string Email { get; set; }

		[Required]
		[StringLength(200, MinimumLength = 2)]
		public string Subject { get; set; }

		[Required]
		[Display(Name = "Enter your message")]
		[StringLength(200, MinimumLength = 2)]
		public string Message { get; set; }
	}
}