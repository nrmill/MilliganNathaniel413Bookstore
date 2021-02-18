using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MilliganNathaniel413Bookstore.Models
{
    public class BookstoreDbContext : DbContext
    {
        //default context constructor
        public BookstoreDbContext (DbContextOptions<BookstoreDbContext> options) : base (options)
        {

        }

        //class attribute that is the actual database of Book objects
        public DbSet<Book> Books { get; set; }
    }
}
