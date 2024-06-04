namespace WebApplication1.BL
{
    public class User
    {
<<<<<<< HEAD
        private int id = UsersList.Count + 1;
=======
        private int id = UsersList.Count+1;
>>>>>>> main
        private string name;
        private string email;
        private string password;
        private bool isAdmin = false;
        private bool isActive = true;

<<<<<<< HEAD
        public List<Course> myCourses; //
=======
        private List<Course> myCourses = new List<Course>();
>>>>>>> main
        static List<User> usersList = new List<User>();
        

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }
        public bool IsAdmin { get => isAdmin; set => isAdmin = value; }
        public bool IsActive { get => isActive; set => isActive = value; }
<<<<<<< HEAD
        public  List<Course> MyCourses { get => myCourses; set => myCourses = value; } //
=======
        public List<Course> MyCourses { get => myCourses; set => myCourses = value; }
>>>>>>> main
        public static List<User> UsersList { get => usersList; set => usersList = value; }

        static User()
        {
            // קונסטרקטור סטטי שדואג שהמנהלן יווצר פעם אחת בלבד
            if (usersList.Count == 0)
                usersList.Add(new User(1, "admin", "admin@admin.com", "admin", true, true));
        }

        public User()
        {
            MyCourses = new List<Course>();
        }

        public User(string name, string email, string password)
        {
<<<<<<< HEAD
            
            this.name = name;
            this.email = email;
            this.password = password;
            this.isAdmin = false;
            this.isActive = true;
        }

        public User(string name, string email, string password)
        {
            this.id = id;
=======
>>>>>>> main
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

<<<<<<< HEAD
        public  List<User> GetUsers() //
        {
            return usersList;
        }
=======

        public List<User> GetUsers() //
        {
            return usersList;
        }

>>>>>>> main
        public static User GetUser(int userId)
        {
            return usersList.FirstOrDefault(u => u.Id == userId);
        }
<<<<<<< HEAD
=======

>>>>>>> main
        public List<Course> GetCourses() //
        {
            return myCourses;
        }

<<<<<<< HEAD

=======
>>>>>>> main
        public bool registration()
        {
            foreach (User user in usersList)
            {
                if (user.Email == this.Email)
                    return false;
            }

            usersList.Add(this);
            return true;
        }

        public static User login(Login login)
        {
            foreach (User user in usersList)
            {
                if (user.Email == login.Email && user.Password.Equals(login.Password, StringComparison.OrdinalIgnoreCase))
                {
                    return user;
                }
            }
            return null;
        }

        public bool AddCourse(Course course) //
        {
            if (myCourses.Contains(course))
            {
                return false;
            }
<<<<<<< HEAD

            myCourses.Add(course);
            return true;
        }

       


=======
>>>>>>> main

            myCourses.Add(course);
            return true;
        }

        public void DeleteCourseById(int id)
        {
            Course courseToRemove = MyCourses.FirstOrDefault(course => course.Id == id);

            if (courseToRemove != null)
            {
                MyCourses.Remove(courseToRemove);
            }
            else
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
                    selectedCourses.Add(course);
            }
            return selectedCourses;
        }


        public List<Course> GetByRatingRange(double fromRating, double toRating)
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