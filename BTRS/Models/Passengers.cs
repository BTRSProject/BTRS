using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BTRS.Models
{
	[Index(nameof(Passengers.Username), IsUnique = true)]
	[Index(nameof(Passengers.Email), IsUnique = true)]
	public class Passengers
	{
		[Key]
		public int Id { get; set; }
		[Required(ErrorMessage = "Enter passenger's name")]
		[StringLength(50)]
		public string Name { get; set; }
		[Required(ErrorMessage = "Enter passenger's username")]
		[StringLength(50)]
		public string Username { get; set; }
		[Required(ErrorMessage = "Enter passenger's password")]
		[StringLength(50)]
		public string Password { get; set; }
		[Required(ErrorMessage = "Enter passenger's E-mail")]
		[StringLength(50)]
		public string Email { get; set; }
		[Required(ErrorMessage = "Enter passenger's phone number")]
		public int PhoneNum { get; set; }
        [Required(ErrorMessage = "Enter passenger's Gender")]
        public string Gender { get; set; }

		public ICollection<Pass_Trips> Pass_Trips { get; set; }

	}
}
