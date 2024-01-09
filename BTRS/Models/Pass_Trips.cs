using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTRS.Models
{
	public class Pass_Trips
	{
        [Key]
        public int Id { get; set; }
        [ForeignKey("PassengerID")]
		public Passengers passengers { set; get; }
		[ForeignKey("TripID")]
		public Trips trips { set; get; }
	}
}
