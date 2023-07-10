using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Bulky.Models
{
    [Table("Products")]
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }

        [Required]
         
        public string Title { get; set; }


        [Required]

        public string Description { get; set; }

        [Required]
        public string ISBN { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        [Display(Name = "List Price")]
        public float ListPrice { get; set; }



        [Required]
        [Display(Name = "Price for 1-50")]
        public float Price { get; set; }

        [Required]
        [Display(Name = "Price for 50+")]
        public float Price50 { get; set; }
        [Required]
        [Display(Name = "Price for 100+")]
        public float Price100 { get; set; }


        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category category { get; set; }

        [ValidateNever]
        public string ImageUrl { get; set; }
    }
}
