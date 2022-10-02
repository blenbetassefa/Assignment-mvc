using System.ComponentModel.DataAnnotations;

namespace Web2.Models
{
    public class comment
    {
        public int Name { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string subject { get; set; }
        [Required]
        public string Message { get; set; }
        
   
    }
}
