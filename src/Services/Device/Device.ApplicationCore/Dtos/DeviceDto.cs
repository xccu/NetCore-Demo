using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Device.ApplicationCore.Dtos;

public class DeviceDto
{
    public int id { get; set; }
    public String name { get; set; }
    public String description { get; set; }
    public String Number { get; set; }
    public DateTime date { get; set; }
}
