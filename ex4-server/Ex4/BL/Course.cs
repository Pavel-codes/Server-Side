
namespace WebApplication1.BL
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
        private string duration;
        private string lastUpdate;
<<<<<<< Updated upstream
        private int numOfRegisters;
=======
>>>>>>> Stashed changes

        public Course() { }

        public Course(int id, string title, string url, double rating, int numberOfReviews, int instructorsId, string imageReference, string duration, string lastUpdate, int numOfRegisters)
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
            NumOfRegisters = numOfRegisters;
        }

        public int Id { get => id; set => id = value; }
        public string Title { get => title; set => title = value; }
        public string Url { get => url; set => url = value; }
        public double Rating { get => rating; set => rating = value; }
        public int NumberOfReviews { get => numberOfReviews; set => numberOfReviews = value; }
        public int InstructorsId { get => instructorsId; set => instructorsId = value; }
        public string ImageReference { get => imageReference; set => imageReference = value; }
        public string Duration { get => duration; set => duration = value; }
        public string LastUpdate { get => lastUpdate; set => lastUpdate = value; }

<<<<<<< Updated upstream
        public int NumOfRegisters { get => numOfRegisters; set => numOfRegisters = value; }




=======
>>>>>>> Stashed changes
        public List<Course> Read()
        {
            DBservices db = new DBservices();
            return db.ReadCourses();
        }

        public static bool AddCourseToUser(int userId, Course courseToAdd)
        {
            DBservices db = new DBservices();
            try
            {
                db.AddCourseToUser(userId, courseToAdd);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public static bool DeleteCourse(int userId, int courseidToDelete)
        {
            DBservices db = new DBservices();
            try
            {
                db.DeleteCourseFromUser(userId, courseidToDelete);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public static List<Course> GetByRatingRangeForUser(int userId, double fromRating, double toRating)
        {
            DBservices db = new DBservices();
            List<Course> courses = db.GetByRatingRangeForUser(userId, fromRating, toRating);
            if (courses.Count == 0)
            {
                return null;
            }
            else
            {
                return courses;
            }
        }


        public static List<Course> GetByDurationRangeForUser(int userId, double fromDuration, double toDuration)
        {
            DBservices db = new DBservices();
            List<Course> courses = db.GetByDurationRangeForUser(userId, fromDuration, toDuration);
            if (courses.Count == 0)
            {
                return null;
            }
            else
            {
                return courses;
            }
        }

        public bool InsertNewCourse()
        {
            DBservices db = new DBservices();
            try
            {
                db.InsertNewCourse(this);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool updateCourse(int id, Course updatedCourse)
        {
            DBservices db = new DBservices();
            try
            {
                db.EditCourse(id, updatedCourse);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

<<<<<<< Updated upstream
        public static List<Course> GetCoursesByUser(int userId)
        {
            DBservices db = new DBservices();
            List<Course> courses = db.GetCoursesOfUser(userId);
            if (courses.Any())
            {
                return courses;
            }
            else
            {
                return null;
            }
        }

=======
>>>>>>> Stashed changes
        public static List<Course> GetCoursesByInstructor(int instructorId)
        {
            DBservices db = new DBservices();
            List<Course> courses = db.GetCoursesByInstructor(instructorId);
            if (courses.Count == 0)
            {
                return null;
            }
            else
            {
                return courses;
            }
        }

        public static List<Course> GetTop5Courses()
        {
            DBservices db = new DBservices();
            List<Course> courses = db.GetTop5Courses();
            if (courses.Count == 0)
            {
                return null;
            }
            else
            {
                return courses;
            }
        }

    }
}
