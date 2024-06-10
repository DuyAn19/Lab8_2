using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Students
    {
        [Key]
        public Guid ID { get; set; }
        [Required]
        [StringLength(30, ErrorMessage ="Ten Khoong qua 30 ky tu")]
        public string Ten { get; set; }
        public int Tuoi { get; set; }
        public int Role { get; set; }
        [EmailAddress (ErrorMessage ="Sai dinh dang Email")]
        public string Email { get; set; }

        public int Luong { get; set; }
        public bool TrangThai {  get; set; }
    }
}
