using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Model
{
    public class Media
    {
        [Key]
        public int BookID { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? Genre { get; set; }
        public string? ISBN { get; set; }
        public string? Series { get; set; }
        public int NumOfCopies { get; set; }
        public int Pages { get; set; }
        public DateTime YearReleased { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
