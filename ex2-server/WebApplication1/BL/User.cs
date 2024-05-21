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
        
        static List<Course> myCourses = new List<Course>();
        static List<User> usersList = new List<User>();
        
        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }
        public bool IsAdmin { get => isAdmin; set => isAdmin = value; } 
        public bool IsActive { get => isActive; set => isActive = value; }
        public static List<Course> MyCourses { get => myCourses; set => myCourses = value; }
        public static List<User> UsersList { get => usersList; set => usersList = value; }

        public User() {
            if (usersList.Count == 0)
                usersList.Add(new User(Id = 1, Name = "admin", Email = "admin@admin.com", Password = "admin", IsAdmin = true, IsActive = true));
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

        public User(int id, string name, string email, string password, bool isAdmin, bool isActive)
        {
            this.id = id;
            this.name = name;
            this.email = email;
            this.password = password;
            this.isAdmin = isAdmin;
            this.isActive = isActive;
        }


        public List<User> getUsers()
        {
            return usersList;
        }
        public bool registration()
        {
                foreach (User user in usersList)
                {
                    if (user.Id == this.Id || user.Password.Equals(this.Password)) 
                            return false;

                }
               
                usersList.Add(this);
                return true;
                   
        }

        public bool login()
        {
            foreach (User user in usersList)
            {
                if (user.Id == this.Id && user.Password.Equals(this.Password))
                {
                    return true;
                }
            }
            return false;
        }   

    }
}
