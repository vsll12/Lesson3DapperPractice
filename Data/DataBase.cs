using Dapper;
using Lesson3DapperPractice.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Lesson3DapperPractice.Data
{
    internal class DataBase
    {
        private readonly SqlConnection _sqlConnection;

        public DataBase(string connectionString)
        {
            _sqlConnection = new SqlConnection(connectionString);
        }

        public Student? Login(int studentId)
        {
            var query = "SELECT * FROM Students Where Id = @studentId";
            var student =  _sqlConnection.QuerySingleOrDefault<Student>(query,param : new { studentId = studentId });
            return student;
        }

        public List<Book> GetAllBooks()
        {
            var query = "SELECT * FROM Books Where Quantity > 0";
            var books = _sqlConnection.Query<Book>(query).ToList();
            return books;
        }

        public void TakeBook(int bookId,int idStudent)
        {
            var query = "INSERT INTO S_Cards(Id,Id_Student,Id_Book,DateOut,Id_Lib) VALUES(@Id,@IdStudent,@IdBook,@DateOut,@Idlib)";
            var query2 = "SELECT MAX(Id) FROM S_Cards";
            var value = _sqlConnection.ExecuteScalar<int>(query2);
            _sqlConnection.Execute(query,param : new {Id = value + 1,IdStudent = idStudent,IdBook = bookId,DateOut = DateTime.Now,IdLib = 1});
        }

        public List<Book> TakenBooks(int studentId)
        {
            var query = "SELECT Books.Name,Books.Id,Books.Quantity FROM S_Cards JOIN Students ON S_Cards.Id_Student = Students.Id JOIN Books ON Books.Id = S_Cards.Id_Book Where Id_Student = @studentId AND S_Cards.DateIn IS NULL";
            var books = _sqlConnection.Query<Book>(query,param : new {studentId = studentId}).ToList();
            return books;
        }


        public void ReturnBook(int bookId,int studentId)
        {
            var command = "UPDATE S_Cards SET DateIn = @dateTime WHERE Id_Book = @bookId and Id_Student = @idStudent";
            _sqlConnection.Execute(command,param : new {bookId = bookId,idStudent = studentId,dateTime = DateTime.Now});
        }
    }
}
