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

        public bool Insert() // need to check it
        {
            try
            {
                if (instructorList.Count > 0)
                {
                    foreach (Instructor instructor in instructorList)
                    {
                        if (instructor.Id == Id && instructor.Title.Equals(Title))
                        {
                            return false;
                        }
                    }
                    instructorList.Add(this);
                    return true;
                }
                else
                {
                    instructorList.Add(this);
                    return true;
                }
            } 
            catch { 
                return false;
            }
        }

        public List<Instructor> Read()
        {
            return instructorList;
        }
    }
}
