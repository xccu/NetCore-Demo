using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace User.ApplicationCore.Entities;

[Table("T_USER_COURSE")]
public class UserCourse
{
    public UserCourse() 
    {
        this.Id = System.Guid.NewGuid().ToString();
    }

    [Key]
    [Required]
    [Column("ID")]
    public String Id { get; set; }
    [Column("COURSE_ID")]
    public String CourseId { get; set; }
    [Column("USER_ID")]
    public String UserId { get; set; }
    [Column("SCORE")]
    public float Score { get; set; }
}
