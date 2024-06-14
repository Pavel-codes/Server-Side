using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace WebApplication1.BL
{
    public class Instructor
    {
        private int id;
        private string title;
        private string name;
        private string image;
        private string jobTitle;

        public int Id { get => id; set => id = value; }
        public string Title { get => title; set => title = value; }
        public string Name { get => name; set => name = value; }
        public string Image { get => image; set => image = value; }
        public string JobTitle { get => jobTitle; set => jobTitle = value; }

        // Method to insert a new instructor into the database
        public bool InsertInstructor()
        {
            return new DBservices().InsertInstructor(this) > 0;
        }

        public static List<Course> GetCoursesByInstructor(int instructorId)
        {
            return new DBservices().GetCoursesByInstructor(instructorId);
        }
    }
}