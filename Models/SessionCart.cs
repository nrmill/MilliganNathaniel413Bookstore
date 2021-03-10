using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using MilliganNathaniel413Bookstore.Infrastructure;

namespace MilliganNathaniel413Bookstore.Models
{
    public class SessionCart : Cart
    {
        public static Cart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?
                .HttpContext.Session;
            SessionCart cart = session?.GetJson<SessionCart>("cart")
                ?? new SessionCart();
            cart.Session = session;
            return cart;
        }

        [JsonIgnore]
        public ISession Session { get; set; }
        //add an item to session storage version of cart
        public override void AddItem(Book book, int quantity)
        {
            base.AddItem(book, quantity);
            Session.SetJson("cart", this);
        }

        //remove an item from session storage version of cart
        public override void RemoveLine(Book book)
        {
            base.RemoveLine(book);
            Session.SetJson("cart", this);
        }

        //clear items in session storage version of cart
        public override void Clear()
        {
            base.Clear();
            Session.Remove("cart");
        }
    }
}
