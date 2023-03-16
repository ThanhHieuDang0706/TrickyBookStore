using System.Collections.Generic;
using TrickyBookStore.Models;
using Microsoft.Extensions.DependencyInjection;

// KeepIt
namespace TrickyBookStore.Services.Books
{
    public interface IBookService
    {
        IList<Book> GetBooks(params long[] ids);
    }
}
