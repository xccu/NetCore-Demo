using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace User.ApplicationCore.Entities;

[Table("T_USER")]
public class User
{
    [Key]
    [Required]
    [Column("USER_ID")]
    public int id { get; set; }
    [Column("USER_NAME")]
    public String name { get; set; }
    [Column("PASSWORD")]
    public String password { get; set; }
    [Column("AGE")]
    public int age { get; set; }
    [Column("SEX")]
    public String sex { get; set; }
    [Column("RACE")]
    public String race { get; set; }

}