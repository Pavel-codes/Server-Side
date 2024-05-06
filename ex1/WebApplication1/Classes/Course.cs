namespace WebApplication1.Classes
{
    public class Course
    {
        private int id;
        private string title;
        private string url;
        private double rating;
        private int numberOfReviews;
        private int[] instructorsId;
        private string imageReference;
        private int duration;
        private DateTime lastUpdate;

        private static List<Course> coursesList = new List<Course>();

        public Course()
        {
        }

        public Course(int id, string title, string url, double rating, int numberOfReviews, int[] instructorsId, string imageReference, int duration, DateTime lastUpdate)
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
        public int[] InstructorsId
        {
            get => instructorsId;
            set
            {
                instructorsId = new int[value.Length];
                Array.Copy(value, 0, instructorsId, 0, value.Length);
            }
        }
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
    }
}
