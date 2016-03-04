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
      };
      Post["/books/delete"] = _ =>
      {
        Book.DeleteAll();
        List<Book> allBooks = Book.GetAll();
        return View["books.cshtml", allBooks];
      };
      Get["/books/{id}"] = parameters =>
      {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Book selectedBook = Book.Find(parameters.id);
        List<Author> bookAuthors = selectedBook.GetAuthor();
        List<Author> allAuthors = Author.GetAll();
        model.Add("book", selectedBook);
        model.Add("bookAuthors", bookAuthors);
        model.Add("allAuthors", allAuthors);
        return View["book.cshtml", model];
      };
      Post["/book/add_author"] = _ =>
      {
        Author author = Author.Find(Request.Form["author-id"]);
        Book book = Book.Find(Request.Form["book-id"]);
        book.AddAuthor(author);
        List<Book> allBooks = Book.GetAll();
        return View["books.cshtml", allBooks];
      };
      Get["/authors"] = _ =>
      {
        List<Author> allAuthors = Author.GetAll();
        return View["authors.cshtml", allAuthors];
      };
      Get["/authors/new"] = _ =>
      {
        return View["author_form.cshtml"];
      };
      Post["/authors/new"] = _ =>
      {
        Author newAuthor = new Author(Request.Form["author-name"]);
        newAuthor.Save();
        List<Author> allAuthors = Author.GetAll();
        return View["authors.cshtml", allAuthors];
      };
      Post["/authors/delete"] = _ =>
      {
        Author.DeleteAll();
        List<Author> AllAuthors = Author.GetAll();
        return View["authors.cshtml", AllAuthors];
      };
      Get["/authors/{id}"] = parameters =>
      {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Author selectedAuthor = Author.Find(parameters.id);
        List<Book> authorBook = selectedAuthor.GetBooks();
        List<Book> allBooks = Book.GetAll();
        model.Add("author", selectedAuthor);
        model.Add("authorBook", authorBook);
        model.Add("allBooks", allBooks);
        return View["author.cshtml", model];
      };
      Post["/authors/add_book"] = _ =>
      {
        Author author = Author.Find(Request.Form["author-id"]);
        Book book = Book.Find(Request.Form["book-id"]);
        author.AddBook(book);
        List<Author> allAuthors = Author.GetAll();
        return View["authors.cshtml", allAuthors];
      };
    }
  }
}
