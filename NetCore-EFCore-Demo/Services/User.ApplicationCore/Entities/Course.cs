using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace User.ApplicationCore.Entities;


[Table("T_COURSE")]
public class Course
{
    public Course()
    {
        this.Id = System.Guid.NewGuid().ToString();
    }

    [Key]
    [Required]
    [Column("COURSE_ID")]
    public String Id { get; set; }
    [Column("COURSE_NAME")]
    public String CourseName { get; set; }
    [Column("credit")]
    public float Credit { get; set; }

}
