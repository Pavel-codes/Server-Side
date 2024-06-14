using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace WebApplication1.BL
{
    public class Course
    {
        private static DBservices dbServices = new DBservices();

        private int id;
        private string title;
        private string url;
        private double rating;
        private int numberOfReviews;
        private int instructorId;
        private string imageReference;
        private string duration;
        private DateTime lastUpdate;

        public int Id { get => id; set => id = value; }
        public string Title { get => title; set => title = value; }
        public string Url { get => url; set => url = value; }
        public double Rating { get => rating; set => rating = value; }
        public int NumberOfReviews { get => numberOfReviews; set => numberOfReviews = value; }
        public int InstructorId { get => instructorId; set => instructorId = value; }
        public string ImageReference { get => imageReference; set => imageReference = value; }
        public string Duration { get => duration; set => duration = value; }
        public DateTime LastUpdate { get => lastUpdate; set => lastUpdate = value; }

        public Course() { }

        public Course(int id, string title, string url, double rating, int numberOfReviews, int instructorId, string imageReference, string duration, DateTime lastUpdate)
        {
            this.id = id;
            this.title = title;
            this.url = url;
            this.rating = rating;
            this.numberOfReviews = numberOfReviews;
            this.instructorId = instructorId;
            this.imageReference = imageReference;
            this.duration = duration;
            this.lastUpdate = lastUpdate;
        }

        public List<Course> Read()
        {
            return dbServices.GetAllCourses();
        }

        public static bool AddCourseToUser(int userId, Course courseToAdd)
        {
            return dbServices.AddCourseToUser(userId, courseToAdd.Id) > 0;
        }

        public static bool DeleteCourse(int userId, int courseIdToDelete)
        {
            return dbServices.DeleteCourseFromUser(userId, courseIdToDelete) > 0;
        }

        public static List<Course> GetByRatingRangeForUser(int userId, double fromRating, double toRating)
        {
            return dbServices.GetByRatingRangeForUser(userId, fromRating, toRating);
        }

        public static List<Course> GetByDurationRangeForUser(int userId, double fromDuration, double toDuration)
        {
            return dbServices.GetByDurationRangeForUser(userId, fromDuration, toDuration);
        }

        public Course GetCourseByTitle(string courseTitle)
        {
            return dbServices.GetCourseByTitle(courseTitle);
        }

        public bool InsertNewCourse()
        {
            return dbServices.InsertCourse(this) > 0;
        }

        public bool UpdateCourse(int courseId, Course updatedCourse)
        {
            updatedCourse.Id = courseId;
            return dbServices.UpdateCourse(updatedCourse) > 0;
        }

        public static List<Course> GetCoursesByInstructor(int instructorId)
        {
            return dbServices.GetCoursesByInstructor(instructorId);
        }
    }
}
