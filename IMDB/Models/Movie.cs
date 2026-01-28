using IMDB.Data.Base;
using IMDB.Data.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace IMDB.Models
{
    public class Movie : IEntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string ImageURL { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public MovieCategory MovieCategory { get; set; }
        // Relationships
        // Cinema
        public int CinemaId { get; set; }
        [ForeignKey("CinemaId")]
        //[ValidateNever]
        public Cinema Cinema { get; set; }
        // Producer
        public int ProducerId { get; set; }
        [ForeignKey("ProducerId")]
        //[ValidateNever]
        public Producer Producer { get; set; }
        // Actors
        //[ValidateNever]
        public List<Actor_Movie> Actors_Movies { get; set; }
    }
}
