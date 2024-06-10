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
            return coursesList;
        }

        public static bool AddCourseToUser(int userId, Course courseToAdd)
        {
            User user = User.GetUser(userId);
            if (user == null)
            {
                Console.WriteLine($"User with ID {userId} not found.");
                return false;
            }
            if (!user.AddCourse(courseToAdd))
            {
                Console.WriteLine($"Course {courseToAdd.title} is already added for user {userId}.");
                return false;
            }
            Console.WriteLine($"Course {courseToAdd.title} added for user {userId}.");
            return true;
        }

        public static bool DeleteCourse(int userId, int courseidToDelete)
        {
            User user = User.GetUser(userId);
            if (user == null)
            {
                Console.WriteLine($"User with ID {userId} not found.");
                return false;
            }
            else
            {
                user.DeleteCourseById(courseidToDelete);
                return true;
            }
        }

        public static List<Course> GetByRatingRangeForUser(int userId, double fromRating, double toRating)
        {
            User user = User.GetUser(userId);
            if (user == null)
            {
                throw new Exception($"User with ID {userId} not found.");
            }

            return user.GetByRatingRangeForCourses(fromRating, toRating);
        }


        public static List<Course> GetByDurationRangeForUser(int userId, double fromDuration, double toDuration)
        {
            User user = User.GetUser(userId);
            if (user == null)
            {
                throw new Exception($"User with ID {userId} not found.");
            }

            return user.GetByDurationRange(fromDuration, toDuration);
        }

        public bool InsertNewCourse()
        {
            bool courseFlag = true;
            bool instructorFlag = false;
            if (coursesList.Count == 0)
            {
                courseFlag = true;
            }
            else
            {
                foreach (Course c in coursesList)
                {
                    if (c.Id == Id || c.Title.Equals(Title))
                    {
                        courseFlag = false;
                        break;
                    }
                }
            }
            foreach (Instructor instructor in Instructor.InstructorList)
            {
                if (instructor.Id == InstructorsId)
                {
                    instructorFlag = true;
                    break;
                }
            }

            if (courseFlag && instructorFlag)
            {
                coursesList.Add(this);
                return true;
            }
            return false;
        }

        public bool Insert()
        {
            if (coursesList == null)
            {
                throw new NullReferenceException("coursesList is not initialized.");
            }

            bool courseInList = false;

            foreach (var course in coursesList)
            {
                if (course.Id == this.Id && course.Title.Equals(this.Title))
                {
                    courseInList = true;
                    break;
                }
            }

            if (!courseInList)
            {
                var tempCourseList = new List<Course>(CoursesList);
                tempCourseList.Add(this);
                CoursesList = tempCourseList;
                return true;
            }
            return false;
        }

        public bool updateCourse(int id, Course updatedCourse)
        {
            // Find the course with the given id
            Course courseToUpdate = CoursesList.FirstOrDefault(c => c.Id == id);

            // If the course is not found, return NotFound
            if (courseToUpdate == null)
            {
                return false;
            }

            // Update the course properties with the new data
            courseToUpdate.Title = updatedCourse.Title;
            courseToUpdate.Url = updatedCourse.Url;
            courseToUpdate.Rating = updatedCourse.Rating;
            courseToUpdate.NumberOfReviews = updatedCourse.NumberOfReviews;
            courseToUpdate.InstructorsId = updatedCourse.InstructorsId;
            if (string.IsNullOrEmpty(updatedCourse.ImageReference))
            {
                updatedCourse.ImageReference = "https://www.clio.com/wp-content/uploads/2024/03/Journal-Entry-Accounting-1-750x422.png";
            }
            courseToUpdate.ImageReference = updatedCourse.ImageReference;
            courseToUpdate.Duration = updatedCourse.Duration;
            courseToUpdate.LastUpdate = updatedCourse.LastUpdate;

            // Return Ok with a success message
            return true;
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
