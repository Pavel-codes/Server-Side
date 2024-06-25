

namespace WebApplication1.BL
{
    public class Instructor
    {
        private int id;
        private string title;
        private string name;
        private string image;
        private string jobTitle;


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

        public List<Instructor> Read()
        {
            DBservices db = new DBservices();
            return db.ReadInstructors();

        }

        public string instructorNameById(int id)
        {
            DBservices db = new DBservices();
            return db.InstructorName(id);
        }
    }
}
