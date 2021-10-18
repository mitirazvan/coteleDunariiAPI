using System;
using System.Collections.Generic;

namespace CoteleDunarii.Services.Mappers.Configuration
{
    public class MapperConfigurationBuilder : IMapperConfigurationBuilder
    {
        public HashSet<Type> ProfileTypes { get; } = new HashSet<Type>();

        public IMapperConfigurationBuilder UseCoteleDunariiMappingProfile()
        {
            ProfileTypes.Add(typeof(CoteleDunariiProfile));
            return this;
        }
    }
}
