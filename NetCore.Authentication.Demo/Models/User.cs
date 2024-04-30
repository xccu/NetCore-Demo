namespace Security.Model;

public class User
{
    public string Name { get; set; } = "user1";
    public DateTime BirthDate { get; set; } = DateTime.Now.Date.AddYears(-20);
}
