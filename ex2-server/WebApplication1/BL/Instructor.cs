namespace WebApplication1.BL
{
    public class Instructor
    {
        private int id;
        private string title;
        private string name;
        private string image;
        private string jobTitle;

        private static List<Instructor> instructorList = new List<Instructor>();

        public Instructor() { }

        public Instructor(int id, string title, string name, string image, string jobTitle)
        {
            Id = id;
            Title = title;
            Name = name;
            Image = image;
            JobTitle = jobTitle;
        }

        public int Id { get => id; set => id = value; }
        public string Title { get => title; set => title = value; }
        public string Name { get => name; set => name = value; }
        public string Image { get => image; set => image = value; }
        public string JobTitle { get => jobTitle; set => jobTitle = value; }
        public static List<Instructor> InstructorList { get => instructorList; set => instructorList = value; }

        public bool Insert() // fixed
        {
            bool isPresent = instructorList.Contains(this);
            if (isPresent)
            {
                return false;
            }
            else
            {
                instructorList.Add(this);
            }
            return true;
        }

        public List<Instructor> Read()
        {
            return instructorList;
        }
    }
}
