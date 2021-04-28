using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APICoreDemo.Models
{
    public class CustomerDataModel
    {
        public string ID { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Max length is 50 characters.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Max length is 50 characters.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Max length is 100 characters.")]
        public string Occupation { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Max length is 100 characters.")]
        public string City { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Max length is 100 characters.")]
        public string State { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Max length is 100 characters.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Max length is 100 characters.")]
        [DataType(DataType.Url)]
        [Display(Name = "Image URL")]
        public string ImageURL { get; set; }
    }
}
