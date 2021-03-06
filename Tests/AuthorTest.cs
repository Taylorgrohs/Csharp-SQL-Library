using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Library
{
  public class AuthorTest : IDisposable
  {
    public AuthorTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=library_test;Integrated Security=SSPI;";
    }
    public void Dispose()
    {
      Author.DeleteAll();
      Book.DeleteAll();
    }

    [Fact]
    public void Test_EqualOverrideTrueForSameName()
    {
      Author firstAuthor = new Author("Taylor");
      Author secondAuthor = new Author("Taylor");

      Assert.Equal(firstAuthor, secondAuthor);
    }

    [Fact]
    public void Test_Save()
    {
      Author testAuthor = new Author("Taylor");
      testAuthor.Save();

      List<Author> result = Author.GetAll();
      List<Author> testList = new List<Author>{testAuthor};

      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_SaveAssignsIdToObject()
    {
      Author testAuthor = new Author("C# for dummies");
      testAuthor.Save();

      Author savedAuthor = Author.GetAll()[0];

      int result = savedAuthor.GetId();
      int testId = testAuthor.GetId();

      Assert.Equal(testId, result);
    }

    [Fact]
    public void Test_FindFindsAuthor()
    {
      Author testAuthor = new Author("Taylor");
      testAuthor.Save();

      Author foundAuthor = Author.Find(testAuthor.GetId());

      Assert.Equal(testAuthor, foundAuthor);
    }

    [Fact]
    public void Test_Delete_DeletesStudentFromDatabase()
    {
      Author testAuthor = new Author("Taylor");
      testAuthor.Save();

      Book testBook = new Book("Nathan Sucks");
      testBook.Save();

      testAuthor.AddBook(testBook);
      testAuthor.Delete();

      List<Book> resultAuthorBooks = testAuthor.GetBooks();
      List<Book> testAuthorBooks = new List<Book> {};

      Assert.Equal(testAuthorBooks, resultAuthorBooks);
    }

    [Fact]
    public void Test_update()
    {
      Author testAuthor = new Author("Taylor");
      testAuthor.Save();
      testAuthor.Update("Nathan");


      Author newAuthor = new Author("Nathan");
      newAuthor.Save();

      Assert.Equal(newAuthor.GetName(), testAuthor.GetName());
    }
    [Fact]
    public void Test_GetBook()
    {
      Book newBook = new Book("C#", 1);

      newBook.Save();

      Author newAuthor = new Author("Taylor", 1);
      newAuthor.Save();

      newAuthor.AddBook(newBook);

      List<Book> books = newAuthor.GetBooks();
      Assert.Equal(newBook.GetTitle(), books[0].GetTitle());
    }

    [Fact]
    public void Test_SearchAuthor()
    {
      Author testAuthor = new Author("Tay");
      Author testAuthor2 = new Author("Taylor");
      Author testAuthor3 = new Author("Taylore");
      testAuthor.Save();
      testAuthor2.Save();
      testAuthor3.Save();

      List<Author> results = Author.SearchAuthor("tay");
      foreach(Author i in results)
      {
        Console.WriteLine("Author Search: Name: " + i.GetName() + ", ID: " + i.GetId());
      }
      List<Author> testList = new List<Author>{testAuthor, testAuthor2, testAuthor3};

      Assert.Equal(testList, results);
    }
  }
}
