using System;
using System.Collections.Generic;

namespace CoteleDunarii.Services.Mappers.Configuration
{
    public interface IMapperConfigurationBuilder
    {
        HashSet<Type> ProfileTypes { get; }

        IMapperConfigurationBuilder UseCoteleDunariiMappingProfile();
    }
}
