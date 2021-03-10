using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MilliganNathaniel413Bookstore.Models
{
    public class Cart
    {
        public List<CartLine> Lines { get; set; } = new List<CartLine>();

        public virtual void AddItem (Book bk, int qty)
        {
            CartLine line = Lines
                .Where(b => b.Book.BookId == bk.BookId)
                .FirstOrDefault();

            if (line == null)
            {
                //adds new CartLine for new book being added to cart
                Lines.Add(new CartLine
                {
                    Book = bk,
                    Quantity = qty
                });
            }
            else
            {
                //adds additional quantity to CartLine for book previously added to cart
                line.Quantity += qty;
            }
        }

        //removes one line
        public virtual void RemoveLine(Book bk) =>
            Lines.RemoveAll(line => line.Book.BookId == bk.BookId);

        //clears whole cart
        public virtual void Clear() => Lines.Clear();

        //returns each line's quantity by book price
        public decimal ComputeTotalSum() => Lines.Sum(e => ((decimal)e.Book.Price * e.Quantity)); 

        public class CartLine
        {
            public int CartLineId { get; set; }
            public Book Book { get; set; }
            public int Quantity { get; set; }
        }

    }
}
