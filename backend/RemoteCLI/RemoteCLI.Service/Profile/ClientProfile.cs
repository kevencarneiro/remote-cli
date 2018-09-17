using RemoteCLI.Common.Models;
using RemoteCLI.Domain.Models;

namespace RemoteCLI.Application.Profile
{
    public class ClientProfile : AutoMapper.Profile
    {
        public ClientProfile()
        {
            CreateMap<MachineInfo, Client>()
                .ReverseMap()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id.AsString));
        }
    }
}