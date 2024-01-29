using Base.ApplicationCore.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace User.ApplicationCore.Entities;

[Table("T_USER")]
public class User : IVersion
{
    public User()
    {
        this.id = Guid.NewGuid().ToString();
    }

    [Key]
    [Required]
    [Column("USER_ID")]
    public string id { get; set; }
    [Column("USER_NAME")]
    public String name { get; set; }
    [Column("PASSWORD")]
    public String password { get; set; }
    [Column("AGE")]
    public int age { get; set; }
    [Column("GENDER")]
    public String gender { get; set; }
    [Column("RACE")]
    public String race { get; set; }

    [ConcurrencyCheck]
    [Column("VERSION_NO")]
    public int VersionNo { get; set; }
}