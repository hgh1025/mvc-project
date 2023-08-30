using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace HuniMVC.Models
{
    public class MovieGenreViewModel
    {
        
       
        public List<Movie>? Movies { get; set; }
      
        public SelectList? Genres { get; set; }   
       public string? MovieGenre { get; set; }
        //public string? SearchString { get; set; }
        //public string? Rating { get; set; }


    }
}