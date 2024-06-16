namespace WebApplication1.BL
{
    public class User
    {
        private int id = UsersList.Count + 1;
        private string name;
        private string email;
        private string password;
        private bool isAdmin = false;
        private bool isActive = true;

        private List<Course> myCourses = new List<Course>();
        static List<User> usersList = new List<User>();

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }
        public bool IsAdmin { get => isAdmin; set => isAdmin = value; }
        public bool IsActive { get => isActive; set => isActive = value; }
        public List<Course> MyCourses { get => myCourses; set => myCourses = value; }
        public static List<User> UsersList { get => usersList; set => usersList = value; }

        public User()
        {
            if (usersList.Count == 0)
                usersList.Add(new User(Id = 1, Name = "admin", Email = "admin@admin.com", Password = "admin", IsAdmin = true, IsActive = true));
        }

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

        public List<User> GetUsers()
        {
            return usersList;
        }

        public static User GetUser(int userId)
        {
            return usersList.FirstOrDefault(u => u.Id == userId);
        }

        public List<Course> GetCourses()
        {
            return myCourses;
        }

        public bool registration(User user)
        {
            DBservices db = new DBservices();
            try
            {
                registration(user);
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
            
        }

        public User login(Login login)
        {
            DBservices db = new DBservices();
            User user = db.Login(login);
            if (user != null)
            {
                return user;
            }
          
          return null;
            
            
        }

        public bool AddCourse(Course course)
        {
            if (myCourses.Any(c => c.Id == course.Id))
            {
                return false;
            }

            if (myCourses.Any(c => c.Title == course.Title))
            {
                return false;
            }
            myCourses.Add(course);
            return true;
        }

        public void DeleteCourseById(int courseid)
        {
            bool found = false;
            foreach (Course course in MyCourses)
            {
                if (course.Id == courseid)
                {
                    found = true;
                    MyCourses.Remove(course);
                    break;
                }
            }
            if (!found)
            {
                throw new Exception("Course Not Found");
            }
        }

        public List<Course> GetByDurationRange(double fromDuration, double toDuration)
        {
            List<Course> selectedCourses = new List<Course>();
            foreach (Course course in MyCourses)
            {
                string[] duration = course.Duration.Split(" ");
                string bit = duration[0];
                if (double.Parse(bit) >= fromDuration && double.Parse(bit) <= toDuration)
                {
                    selectedCourses.Add(course);
                }
            }
            return selectedCourses;
        }

        public List<Course> GetByRatingRangeForCourses(double fromRating, double toRating)
        {
            List<Course> selectedCourses = new List<Course>();
            foreach (Course course in MyCourses)
            {
                if (course.Rating >= fromRating && course.Rating <= toRating)
                    selectedCourses.Add(course);
            }
            return selectedCourses;
        }

    }
}