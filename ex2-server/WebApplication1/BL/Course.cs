﻿namespace WebApplication1.BL
{
    public class Course
    {
        private int id;
        private string title;
        private string url;
        private double rating;
        private int numberOfReviews;
        private int instructorsId;
        private string imageReference;
        private int duration;
        private DateTime lastUpdate;

        private static List<Course> coursesList = new List<Course>();

        public Course() { }

        public Course(int id, string title, string url, double rating, int numberOfReviews, int instructorsId, string imageReference, int duration, DateTime lastUpdate)
        {
            Id = id;
            Title = title;
            Url = url;
            Rating = rating;
            NumberOfReviews = numberOfReviews;
            InstructorsId = instructorsId;
            ImageReference = imageReference;
            Duration = duration;
            LastUpdate = lastUpdate;
        }

        public int Id { get => id; set => id = value; }
        public string Title { get => title; set => title = value; }
        public string Url { get => url; set => url = value; }
        public double Rating { get => rating; set => rating = value; }
        public int NumberOfReviews { get => numberOfReviews; set => numberOfReviews = value; }
        public int InstructorsId { get => instructorsId; set => instructorsId = value; }
        public string ImageReference { get => imageReference; set => imageReference = value; }
        public int Duration { get => duration; set => duration = value; }
        public DateTime LastUpdate { get => lastUpdate; set => lastUpdate = value; }
        public static List<Course> CoursesList { get => coursesList; set => coursesList = value; }
        

        public List<Course> Read()
        {
            return coursesList;
        }

        public bool Insert()
        {
            foreach (Course course in coursesList)
            {
                if (course.Id == Id || course.Title.Equals(Title)) return false;
            }
            coursesList.Add(this);
            return true;
        }

        public List<Course> GetByDurationRange(int fromDuration, int toDuration)
        {
            List<Course> selectedCourses = new List<Course>();
            foreach (Course course in coursesList)
            {
                if (course.Duration >= fromDuration && course.Duration <= toDuration)
                    selectedCourses.Add(course);
            }
            return selectedCourses;

        }
        public List<Course> GetByRatingRange(int fromRating, int toRating)
        {
            List<Course> selectedCourses = new List<Course>();
            foreach (Course course in coursesList)
            {
                if (course.Rating >= fromRating && course.Rating <= toRating)
                    selectedCourses.Add(course);
            }
            return selectedCourses;
        }

        public void DeleteById(int id)
        {
            bool found = false;
            foreach (Course course in coursesList)
            {
                if (course.Id == id)
                {
                    found = true;
                    coursesList.Remove(course);
                }
            }
            if (!found)
            {
                throw new Exception("Course Not Found");
            }
        }
    }
}
