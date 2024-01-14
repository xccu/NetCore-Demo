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
        this.Id = System.Guid.NewGuid().ToString();
    }

    [Key]
    [Required]
    [Column("USER_ID")]
    public String Id { get; set; }
    [Column("USER_NAME")]
    public String Name { get; set; }
    [Column("PASSWORD")]
    public String Password { get; set; }
    [Column("AGE")]
    public int Age { get; set; }
    [Column("GENDER")]
    public String Gender { get; set; }
    [Column("RACE")]
    public String Race { get; set; }

    [ConcurrencyCheck]
    [Column("VERSION_NO")]
    public int VersionNo { get; set; }
}