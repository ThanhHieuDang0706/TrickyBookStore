using System.Collections.Generic;
using System.Linq;
using TrickyBookStore.Models;

namespace TrickyBookStore.Services.Books
{
    public class BookService : IBookService
    {
        public IList<Book> GetBooks(params long[] ids)
        {
            List<Book> books = Store.Books.Data.Where(book => ids.Contains(book.Id)).ToList();
            return books;
        }
    }
}
