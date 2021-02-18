using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MilliganNathaniel413Bookstore.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        public string Title { get; set; }
        public string AuthorFirstName { get; set; }
        public string AuthorLastName { get; set; }
        public string Publisher { get; set; }
        [RegularExpression(@"^[0-9]{3}-[0-9]{10}$", ErrorMessage = "Invalid ISBN")]
        public string ISBN { get; set; }
        public string Classification { get; set; }
        public string Category { get; set; }
        public float Price { get; set; }

    }
}
