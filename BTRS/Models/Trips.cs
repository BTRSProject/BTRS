using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTRS.Models
{
	public class Trips
	{
		[Key]
        [Required(ErrorMessage = "Enter the Trip Id")]
        public int Id { get; set; }
		[Required(ErrorMessage = "Enter the destination")]
		[StringLength(100)]
		public string Destination { get; set; }
		[Required(ErrorMessage = "Enter the bus number")]
		public int BusNum { get; set; }
		[Required(ErrorMessage = "Enter the starting day")]
		public DateTime StartDate { get; set;}
		[Required(ErrorMessage = "Enter the ending day")]
		public DateTime EndDate { get; set;}

        [ForeignKey("AdminID")]
        public Admin Admin { get; set; }


    }
}
