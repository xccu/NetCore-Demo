using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace User.ApplicationCore.Entities;

[Table("T_USER_COURSE")]
public class UserCourse
{
    [Key]
    [Required]
    [Column("COURSE_ID")]
    public string courseId { get; set; }
    [Column("USER_ID")]
    public string userId { get; set; }
    [Column("SCORE")]
    public float score { get; set; }
}
