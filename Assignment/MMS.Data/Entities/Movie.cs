
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic;

namespace MMS.Data.Entities;

    public class Movie
    {  
        public enum Genre
        {
            Action, Adventure, Comedy, Drama, Fantasy, Crime, 
            Horror, Romance, SciFi, Thriller, Other
        }       
        public int MovieId { get; set; }
        
        [Required][MaxLength (500)]
        public string Title { get; set; }

        [Required][Range (1920,2024)]//work out a way to check current year to make this dynamic
        public int Year { get; set;}

        [Required]
        [EnumDataType (typeof (Genre),ErrorMessage ="Invalid genre")]
        public Genre MovieGenre { get; set; }

        [Url]
        public String PhotoURL { get; set; }

        //Property to navigate reviews for the required movie (IList<Review>)
        public IList<Review> Reviews { get; set; } = new List<Review>();
        public double AvgReview { get; set; }

    }
