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
  }
}
