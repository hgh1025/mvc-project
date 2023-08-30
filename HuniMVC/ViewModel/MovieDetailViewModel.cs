using HuniMVC.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HuniMVC.ViewModel
{
    public class MovieDetailsViewModel
    {
        public int Id { get; set; }

        public Movie Movie { get; set; }
        public IEnumerable<Message> Messages { get; set; }
        public string? Title { get; set; }
        public string? Genre { get; set; }
        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        [Range(1, 10000)]
        [DataType(DataType.Currency)] // \ 표시
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z0-9""'\s-]*$")]
        [StringLength(5)]
        [Required]
        public string? Rating { get; set; }
        
    }

}
