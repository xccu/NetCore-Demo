using Microsoft.VisualBasic;

namespace Models
{
    public class User 
    { 
        public string Id { get; set; }
        public string name { get; set; }
        public string gender { get; set; }
        public int age { get; set; }
        public string race { get; set; }

        public User()
        {
            this.Id = System.Guid.NewGuid().ToString();
        }
    }

}