using System.ComponentModel.DataAnnotations;

namespace DataAccess;

public class User
{
    [Required]
    public string Id { get; set; }
    [Required]
    public string Name { get; set; }
    public string Gender { get; set; }
    [Range(0, 200)]
    public int Age { get; set; }
    public string Race { get; set; }
    [DataType(DataType.Date)]
    public DateTime RegisterDate { get; set; }

    public User()
    {
        this.Id = System.Guid.NewGuid().ToString();
    }
}