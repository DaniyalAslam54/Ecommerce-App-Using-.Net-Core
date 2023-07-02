using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BulkyWeb.Models
{
    [Table("Categories")]
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
         
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        [DisplayName("Name")]
        public string Name { get; set; }
        [Required]
        [DisplayName("Display Order")]

        [Range(1, 100)]
        //[Range(1, 100, ErrorMessage  = "")]
        public int DisplayOrder { get; set; }


        
    }
}
