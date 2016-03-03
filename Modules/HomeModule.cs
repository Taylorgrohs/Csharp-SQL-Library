using Nancy;
using Library;
using System.Collections.Generic;
using System;

namespace Library
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ =>
      {
        return View["index.cshtml"];
      };
      Get["/books"] = _ =>
      {
        List<Book> allBooks = Book.GetAll();
        return View["books.cshtml", allBooks];
      };
      Get["/books/new"] = _ =>
      {
        List<Author> allAuthors = Author.GetAll();
        return View["book_form.cshtml", allAuthors];
      };
      Post["/books/new"] = _ =>
      {
        Book newBook = new Book(Request.Form["book-title"]);
        newBook.Save();
        Author newAuthor = Author.Find(Request.Form["author-id"]);
        newBook.AddAuthor(newAuthor);
        List<Book> allBooks = Book.GetAll();
        return View["books.cshtml", allBooks];
      }
    }
  }
}
