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

        private static List<Course> coursesList = new List<Course>();

        public Course() { }

        public Course(int id, string title, string url, double rating, int numberOfReviews, int instructorsId, string imageReference, string duration, string lastUpdate)
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
        public string Duration { get => duration; set => duration = value; }
        public string LastUpdate { get => lastUpdate; set => lastUpdate = value; }

        public static List<Course> CoursesList { get => coursesList; set => coursesList = value; }

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
            //User user = User.GetUser(userId);
            //if (user == null)
            //{
            //    Console.WriteLine($"User with ID {userId} not found.");
            //    return false;
            //}
            //if (!user.AddCourse(courseToAdd))
            //{
            //    Console.WriteLine($"Course {courseToAdd.title} is already added for user {userId}.");
            //    return false;
            //}
            //Console.WriteLine($"Course {courseToAdd.title} added for user {userId}.");
            //return true;
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
            if (db.GetByRatingRangeForUser(userId, fromRating, toRating).Count == 0)
            {
                return null;
            }
            else
            {
                return db.GetByRatingRangeForUser(userId, fromRating, toRating);
            }
        }


        public static List<Course> GetByDurationRangeForUser(int userId, double fromDuration, double toDuration)
        {
            DBservices db = new DBservices();
            if (db.GetByDurationRangeForUser(userId, fromDuration, toDuration).Count == 0)
            {
                return null;
            }
            else
            {
                return db.GetByDurationRangeForUser(userId, fromDuration, toDuration);
            }
           
        }

        public bool InsertNewCourse()
        {
            DBservices db = new DBservices();
            try { 
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

        public Course getCourseByTitle(string courseName)
        {
            foreach (Course course in coursesList)
            {
                if (course.Title == courseName) return course;
            }
            return null;
        }
    }
}
