using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace WebApplication1.BL
{
    public class User
    {
        private int id;
        private string name;
        private string email;
        private string password;
        private bool isAdmin = false;
        private bool isActive = true;

        private List<Course> myCourses = new List<Course>();

        private DBservices dbServices = new DBservices(); 

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }
        public bool IsAdmin { get => isAdmin; set => isAdmin = value; }
        public bool IsActive { get => isActive; set => isActive = value; }
        //public List<Course> MyCourses { get => myCourses; set => myCourses = value; }

        public User() { }

        public User(string name, string email, string password)
        {
            this.name = name;
            this.email = email;
            this.password = password;
            this.isAdmin = false;
            this.isActive = true;
        }

        public User(int id, string name, string email, string password, bool isAdmin, bool isActive)
        {
            this.id = id;
            this.name = name;
            this.email = email;
            this.password = password;
            this.isAdmin = isAdmin;
            this.isActive = isActive;
        }

        public static List<User> GetUsers()
        {
            return new DBservices().GetUsers();
        }

        public User GetUser(int userId)
        {
            return dbServices.GetUser(userId);
        }

        public List<Course> GetCourses()
        {
            return dbServices.GetUsersCourses(this.Id);
        }

        public bool Registration()
        {
            
            return dbServices.GetCourseByTitle("courseTitle");
        }

        public static User Login()
        {
            return new DBservices().Login();
        }

        public bool AddCourse(int courseId)
        {
            return dbServices.AddCourseToUser(this.id, courseId);
        }

        public void DeleteCourseById(int courseId)
        {
            dbServices.DeleteCourseFromUser(this.id, courseId);
        }

        public List<Course> GetByDurationRange(double fromDuration, double toDuration)
        {
            return dbServices.GetByDurationRangeForUser(this.id, fromDuration, toDuration);
        }

        public List<Course> GetByRatingRangeForCourses(double fromRating, double toRating)
        {
            return dbServices.GetByRatingRangeForUser(this.id, fromRating, toRating);
        }
    }
}
