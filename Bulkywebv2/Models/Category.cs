using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Bulkywebv2.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        [DisplayName("Category Name")]
        public string? Name { get; set; }
        [DisplayName("Display Order")]
        [Range(1,100, ErrorMessage ="Please enter a Number within the Range of 1-100")]
        public int DisplayOrder { get; set; }


    }
}
