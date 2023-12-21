using AutoMapper;
using Demo.AutoMapper.Dtos;
using Demo.AutoMapper.Models;

namespace Demo.AutoMapper;

public class NodeProfile : Profile
{
    public NodeProfile() 
    {
        CreateMap<NodeDto, NodeModel>();
        ReplaceMemberName("Name", "NodeName");        
    }
}
