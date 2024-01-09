using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTRS.Models
{
	public class Bus
	{

        [Key]
		public int Id { get; set; }
		[Required(ErrorMessage ="Enter Bus Captain's name")]
		[StringLength(50)]
		public string CaptainName { get; set; }
		[Required(ErrorMessage = "Enter the number of seats in the bus")]
		public int NumOfSeats { get; set; }

        [ForeignKey("AdminID")]
		public Admin admin { get; set; }

	}
}
