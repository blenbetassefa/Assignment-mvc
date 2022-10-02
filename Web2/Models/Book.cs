using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web2.Models
{
    public partial class Book
    {
        [Key]
        public int BookTblId { get; set; }
        
        [Required(ErrorMessage = "This field is required"), Display(Name = "Book ID")]
        public string BookId { get; set; }
        
        [Required(ErrorMessage = "This field is required"), Display(Name = "Book Title")]
        public string BookTitle { get; set; }
        
        [Required(ErrorMessage = "This field is required"), Display(Name = "Book Category")]
        public string BookCategory { get; set; }
        
        [Required(ErrorMessage = "This field is required"), Display(Name = "Book Author")]
        public string BookAuthor { get; set; }
        
        [Required(ErrorMessage = "This field is required"), Display(Name = "Book Publisher")]
        public string BookPublisher { get; set; }
        
        [Required(ErrorMessage = "This field is required"), MaxLength(13), Display(Name = "Book ISBN")]
        public string BookIsbn { get; set; }
        
        [Required(ErrorMessage = "This field is required"), Display(Name = "Book Copyright")]
        public string BookCopyright { get; set; }
        
        [Required(ErrorMessage = "This field is required"), DataType(DataType.Date), Display(Name = "Date Book Acquired")]
        public DateTime? BookDateAdded { get; set; }
        
        [Required(ErrorMessage = "This field is required"), Display(Name = "Book Status")]
        public string BookStatus { get; set; }
        
        [Display(Name = "Book Image")]
        public string BookImg { get; set; }

        [NotMapped]
        public IFormFile Img { get; set; }

        [NotMapped]
        public string Error { get; set; }




    }
}
