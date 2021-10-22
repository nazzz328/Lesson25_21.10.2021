using System;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using System.Linq;
using System.Collections.Generic;
using ConsoleApp.Models;

namespace ConsoleApp
{
    public class Program
    {
        static string con = "Data source = localhost; Initial catalog=HomeWork23_16.10.2021; Integrated security=true";
        static void Main(string[] args)
        {
            // Create
            CreateStudent(new Student { First_Name = "Aziz", Second_Name = "Azizov", City = "Dushanbe" });

            // Update
            UpdateStudent(1, new Student { First_Name = "Lolo", Second_Name = "Koko", City = "Moscow" });

            // Delete
            DeleteStudent(5);

            // Read
            var students = GetStudents();
        }


        public static List<Student> GetStudents()
        {
            var students = new List<Student>();
            using (IDbConnection db = new SqlConnection(con))
            {
                students = db.Query<Student>("SELECT * FROM STUDENTS").ToList();
            }
            return students;
        }

        public static int? CreateStudent(Student student)
        {
            using (IDbConnection db = new SqlConnection(con))
            {
                var sqlquery = $"INSERT INTO Students(First_Name, Second_Name, City) VALUES('{student.First_Name}', '{student.Second_Name}', '{student.City}'); SELECT CAST(SCOPE_IDENTITY() as int)";
                int? studentId = db.Query<int>(sqlquery, student).FirstOrDefault();
                return studentId;
            }
            
        }

        public static int UpdateStudent(int id, Student student)
        {
            using (IDbConnection db = new SqlConnection(con))
            {
                var sqlquery = $"UPDATE Students SET First_Name = '{student.First_Name}', Second_Name = '{student.Second_Name}', City = '{student.City}' WHERE Id = {id}";
                int result = db.Query<int>(sqlquery).FirstOrDefault();
                return result;
            }
            
        }

        public static int DeleteStudent (int id)
        {
            using (IDbConnection db = new SqlConnection(con))
            {
                var sqlquery = $"DELETE FROM Students WHERE Id = {id}";
                int result = db.Query<int>(sqlquery).FirstOrDefault();
                return result;
            }
        }
    }
}
