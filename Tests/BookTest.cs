using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Library
{
  public class BookTest : IDisposable
  {
    public BookTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=library_test;Integrated Security=SSPI;";
    }
    public void Dispose()
    {
      Book.DeleteAll();
      Author.DeleteAll();
    }

    [Fact]
    public void Test_EqualOverrideTrueForSameName()
    {
      Book firstBook = new Book("C# for dummies");
      Book secondBook = new Book("C# for dummies");

      Assert.Equal(firstBook, secondBook);
    }

    [Fact]
    public void Test_Save()
    {
      Book testBook = new Book("C# for dummies");
      testBook.Save();

      List<Book> result = Book.GetAll();
      List<Book> testList = new List<Book>{testBook};

      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_SaveAssignsIdToObject()
    {
      Book testBook = new Book("C# for dummies");
      testBook.Save();

      Book savedBook = Book.GetAll()[0];

      int result = savedBook.GetId();
      int testId = testBook.GetId();

      Assert.Equal(testId, result);
    }

    [Fact]
    public void Test_FindFindsBook()
    {
      Book testBook = new Book("C# for dummies");
      testBook.Save();

      Book foundBook = Book.Find(testBook.GetId());

      Assert.Equal(testBook, foundBook);
    }

    [Fact]
    public void Test_Delete_DeletesAuthorAssociationsFromDatabase()
    {
      Author testAuthor = new Author("Taylor");
      testAuthor.Save();

      Book testBook = new Book("Nathan Sucks");
      testBook.Save();

      testBook.AddAuthor(testAuthor);
      testBook.Delete();

      List<Book> resultAuthorBooks = testAuthor.GetBooks();
      List<Book> testAuthorBooks = new List<Book> {};

      Assert.Equal(testAuthorBooks, resultAuthorBooks);
    }

    [Fact]
    public void Test_update()
    {
      Book testBook = new Book("Nathan's Yard");
      testBook.Save();
      testBook.Update("Taylor's Yard");

      Book newBook = new Book ("Taylor's Yard");
      newBook.Save();
      Assert.Equal(newBook.GetTitle(), testBook.GetTitle());
    }

    [Fact]
    public void Test_GetAuthor()
    {
      Author newAuthor = new Author("Taylor", 1);
      newAuthor.Save();

      Book newBook = new Book("C#", 1);
      newBook.Save();

      newBook.AddAuthor(newAuthor);

      List<Author> authors = newBook.GetAuthor();
      Assert.Equal(newAuthor.GetName(), authors[0].GetName());
    }

    [Fact]
    public void Test_SearchBook()
    {
      Book testBook = new Book("Tay");
      Book testBook2 = new Book("Taylor");
      Book testBook3 = new Book("Taylore");
      testBook.Save();
      testBook2.Save();
      testBook3.Save();

      List<Book> results = Book.SearchBook("tay");
      foreach(Book i in results)
      {
        Console.WriteLine("Book Search: Title: " + i.GetTitle() + ", ID: " + i.GetId());
      }
      List<Book> testList = new List<Book> {testBook, testBook2, testBook3};

      Assert.Equal(testList, results);

    }
  }
}
