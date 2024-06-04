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

        public bool Insert()
        {
            // Ensure instructorList is initialized
            if (instructorList == null)
            {
                throw new NullReferenceException("instructorList is not initialized.");
            }

            bool instructorInList = false;

            // Check if the current instance already exists in the list
            foreach (var instructor in instructorList)
            {
                if (instructor.Id == this.Id && instructor.Title.Equals(this.Title))
                {
                    instructorInList = true;
                    break;
                }
            }

            // Add the current instance to the list if it does not already exist
            if (!instructorInList)
            {
                // Use a temporary list to avoid modifying the collection during enumeration
                var tempInstructorList = new List<Instructor>(instructorList);
                tempInstructorList.Add(this);
                instructorList = tempInstructorList;
                return true;
            }

            return false;
        }

        public List<Instructor> Read()
        {
            return instructorList;
        }
    }
}
