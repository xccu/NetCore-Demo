using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.AutoMapper;

public class NodeModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public NodeModel Child { get; set; } = new();
}
