using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryAppIMFinal.Models
{
    public class Media
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public string ISBN { get; set; }
        public bool PartOfSeries { get; set; }
        public string? Series {  get; set; }
        public DateTime YearReleased { get; set; }
        public DateTime DateCreated { get ; set; }

        public Media() { }
        public Media (int Id, string Title, string Author, string Genre, string ISBN,  DateTime YearReleased, DateTime DateCreated, string? Series)
        {
            this.Id = Id;
            this.Title = Title;
            this.Author = Author;
            this.Genre = Genre;
            this.ISBN = ISBN;
            this.YearReleased = YearReleased;
            this.DateCreated = DateCreated;
            this.Series = Series;
        }
    }
}
