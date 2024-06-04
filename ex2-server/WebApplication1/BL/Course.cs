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
                return courseFlag;
            }
            //return courseFlag;
            return courseFlag && instructorFlag;

        }


        public bool Insert()
        {
            // Ensure instructorList is initialized
            if (coursesList == null)
            {
                throw new NullReferenceException("instructorList is not initialized.");
            }

            bool courseInList = false;

            // Check if the current instance already exists in the list
            foreach (var course in coursesList)
            {
                if (course.Id == this.Id && course.Title.Equals(this.Title))
                {
                    courseInList = true;
                    break;
                }
            }

            // Add the current instance to the list if it does not already exist
            if (!courseInList)
            {
                // Use a temporary list to avoid modifying the collection during enumeration
                var tempCourseList = new List<Course>(CoursesList);
                tempCourseList.Add(this);
                CoursesList = tempCourseList;
                return true;
            }
            return false;
        }





        //public bool Contains(List<Course> coursesList, Course course) // to be fixed \ deleted
        //{
        //    for(int i = 0; i < coursesList.Count; i++)
        //    {
        //        if (coursesList[i].Id == course.Id && coursesList[i].InstructorsId == course.InstructorsId)
        //        {
        //            return true; 
        //        }
        //    }
        //    return false; // Course not found
        //}

        public Course getCourseByTitle(string courseName)
        {
            foreach(Course course in coursesList)
            {
                if (course.Title == courseName) return course;
            }
            return null; //
        }

        public List<Course> GetByDurationRange(double fromDuration, double toDuration)
        {
            List<Course> selectedCourses = new List<Course>();
            foreach (Course course in coursesList)
            {
                string[] duration = course.Duration.Split(" ");
                string bit = duration[0];
                if (double.Parse(bit) >= fromDuration && double.Parse(bit) <= toDuration)
                    selectedCourses.Add(course);
            }
            return selectedCourses;

        }


        public List<Course> GetByRatingRange(double fromRating, double toRating)
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

        public static bool AddCourseToUser(int userId, Course courseToAdd) //
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
<<<<<<< HEAD
                return false; 
=======
                return false;
>>>>>>> main
            }
            Console.WriteLine($"Course {courseToAdd.title} added for user {userId}.");
            return true;
        }
    }
}