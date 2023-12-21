using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.AutoMapper.Dtos;

public class NodeDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public List<NodeDto> SubNodes { get; set; } = new();
}
