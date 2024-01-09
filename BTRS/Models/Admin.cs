using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BTRS.Models
{
    [Index(nameof(Admin.Username), IsUnique = true)]
    public class Admin
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter name")]

        public string FullName { get; set; }
        [Required(ErrorMessage = "Enter admin's username")]
        [StringLength(50)]
        public string Username { get; set; }
        [Required(ErrorMessage = "Enter admin's password")]
        [StringLength(50)]
        public string Password { get; set; }

        public ICollection<Trips> trips { get; set; }
        public ICollection<Bus> bus { get; set; }



    
    }
}














