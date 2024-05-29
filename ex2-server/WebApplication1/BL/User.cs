namespace WebApplication1.BL
{
    public class User
    {
        private int id;
        private string name;
        private string email;
        private string password;
        private bool isAdmin = false;
        private bool isActive = true;

        public List<Course> myCourses; //
        static List<User> usersList = new List<User>();
        private static int nextId = 2; // מתחיל ב-2 כי 1 שמור ל-admin

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }
        public bool IsAdmin { get => isAdmin; set => isAdmin = value; }
        public bool IsActive { get => isActive; set => isActive = value; }
        public  List<Course> MyCourses { get => myCourses; set => myCourses = value; } //
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

        public User(int id, string name, string email, string password)
        {
            this.id = id;
            this.name = name;
            this.email = email;
            this.password = password;
            this.isAdmin = false;
            this.isActive = true;
        }

        public User(string name, string email, string password)
        {
            this.id = nextId++;
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

        public  List<User> GetUsers() //
        {
            return usersList;
        }
        public static User GetUser(int userId)
        {
            return usersList.FirstOrDefault(u => u.Id == userId);
        }
        public List<Course> GetCourses() //
        {
            return myCourses;
        }


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

            myCourses.Add(course);
            return true;
        }

       



    }
}