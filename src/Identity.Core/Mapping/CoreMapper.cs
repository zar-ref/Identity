using AutoMapper;
using Identity.Core.Mapping.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Core.Mapping
{
    public static class CoreMapper
    {

        public static readonly Lazy<IMapper> LazyMapper = new Lazy<IMapper>(() =>
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                // This line ensures that internal properties are also mapped over.
                cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
                cfg.AddProfile<UserProfile>();
            });
            var mapper = configuration.CreateMapper();

            return mapper;

        });

        public static IMapper Mapper => LazyMapper.Value;
    }
}
