using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Identity.Domain.Profiles;

namespace Identity.Domain
{
    public static class DomainBootsrapperMapper
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
