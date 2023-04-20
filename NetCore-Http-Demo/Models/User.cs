using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class User 
    {
        [Required]
        public string Id { get; set; }
        [Required]
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