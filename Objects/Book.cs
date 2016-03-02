using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace Library
{
  public class Book
  {
    private int _id;
    private string _title;

    public Book(string Title, int Id = 0)
    {
      _id = Id;
      _title = Title;
    }
    public int GetId()
    {
      return _name;
    }
    public string GetTitle()
    {
      return _title;
    }
    public void SetTitle(string newTitle)
    {
      _title = newTitle;
    }
    public override bool Equals(System.Object otherTitle)
    {
      if(!(otherBook is Book))
      {
        return false;
      }
      else
      {
        Book newBook = (Book) otherBook;
        bool idEquality = this.GetId() == newBook.GetId();
        bool titleEquality = this.GetTitle() == newBook.GetTitle();
        return (idEquality && titleEquality);
      }
    }
    public static List<Book> GetAll()
    {
      List<Book> allBooks = new List<Book>{};

      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM books;", conn);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int bookId = rdr.GetInt32(0);
        string bookName = rdr.GetString(1);
        Book newBook = new Book(bookName, bookId);
        allBooks.Add(newBook);
      }
      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
      return allBooks;
    }
    public void Save()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO books(title) OUTPUT INSERTED.id VALUES(@BookTitle);", conn);

      SqlParameters titleParameter = new SqlParameter();
      titleParameter.ParameterName = "@BookTitle";
      titleParameter.Value = this.GetTitle();

      cmd.Parameters.Add(titleParameter);

      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
    }
    pu
  }
}
