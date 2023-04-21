using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess
{
    public class User 
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string name { get; set; }
        public string gender { get; set; }
        [Range(0,200)]
        public int age { get; set; }
        public string race { get; set; }

        public User()
        {
            this.Id = System.Guid.NewGuid().ToString();
        }
    }

}