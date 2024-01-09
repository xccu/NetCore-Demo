using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Device.ApplicationCore.Entities;

[Table("T_DEVICE")]
public class Device
{
    [Key]
    [Required]
    [Column("DEVICE_ID")]
    public int id { get; set; }
    [Column("DEVICE_NAME")]
    public String name { get; set; }
    [Column("DEVICE_DESCRIPTION")]
    public String description { get; set; }
    [Column("DEVICE_NO")]
    public String deviceNumber { get; set; }
    [Column("REGISTED_DATE")]
    public DateTime registedDate { get; set; }

}