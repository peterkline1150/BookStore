using BookStore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class GenreDetail
    {
        public int GenreId { get; set; }
        public string GenreName { get; set; }

        public List<string> BooksTitlesInGenre { get; set; } = new List<string>();
    }
}
