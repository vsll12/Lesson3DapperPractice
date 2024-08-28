using Lesson3DapperPractice.Data;
using Lesson3DapperPractice.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson3DapperPractice.Services
{
    internal class DataBaseService
    {
        private static string connectionString = "Data Source=DESKTOP-7F74UDB;Initial Catalog=Library;Integrated Security=true";

        DataBase db = new DataBase(connectionString);

        private List<Book> Books = new List<Book>();

        private Student? Student;

        public void Start()
        {
            while (true)
            {
                int input;
                int id;
                Console.Write("Enter your ID :");
                id = int.Parse(Console.ReadLine());
                Student = db.Login(id);
                Books = db.GetAllBooks();
                Console.WriteLine($"{Student.FirstName}");
                Console.WriteLine("1.All books");
                Console.WriteLine("2.My books");
                Console.Write("Enter your choice : ");
                input = int.Parse(Console.ReadLine());
                if(input == 1)
                {
                    foreach(Book book in Books)
                    {
                        Console.WriteLine(book.Id + " " + book.Name);
                    }

                    int idBook;
                    Console.WriteLine("Enter Id of book you want to take : ");
                    idBook = int.Parse(Console.ReadLine());

                    db.TakeBook(idBook, Student.Id);

                    Console.WriteLine("You took the book");

                    Thread.Sleep(2000);

                    Console.Clear();

                }
                else if(input == 2)
                {
                    var myBooks = db.TakenBooks(Student.Id);

                    foreach(Book book in myBooks)
                    {
                        Console.WriteLine(book.Id + " " + book.Name);
                    }

                    int idBook;
                    Console.WriteLine("Enter Id of book you want to return : ");
                    idBook = int.Parse(Console.ReadLine());

                    db.ReturnBook(idBook, Student.Id);

                    Console.WriteLine("You returned the book");

                    Thread.Sleep(2000);

                    Console.Clear();

                }
            }
        }
    }
}
