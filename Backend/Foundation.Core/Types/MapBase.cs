using AutoMapper;
using AutoMapper.QueryableExtensions;
using Foundation.Core.Interfaces;

namespace Foundation.Core.Types
{
    public abstract class MapBase<TFrom, TTo>: IMap<TFrom, TTo>
    {

        // ReSharper disable once MemberCanBePrivate.Global
        protected readonly IMapper Mapper;
        
        protected MapBase()
        {
            var configuration = new MapperConfiguration(cfg => { cfg.CreateMap<TFrom, TTo>(); });
            Mapper = configuration.CreateMapper();
        }

        protected MapBase(Action<IMapperConfigurationExpression> expression)
        {
            Mapper = new MapperConfiguration(cfg =>
            {
                cfg.DisableConstructorMapping();
                expression(cfg);
            }).CreateMapper();
        }
        
        protected MapBase(MapperConfiguration configuration)
        {
            Mapper = configuration.CreateMapper();
        }

        public TTo Map(TFrom from) => Mapper.Map<TFrom, TTo>(from);

        public IEnumerable<TTo> Map(IEnumerable<TFrom> from) =>
            from is IQueryable<TFrom> fromQuery
                ? fromQuery.ProjectTo<TTo>(Mapper.ConfigurationProvider)
                : from.Select(q => Mapper.Map<TTo>(q));

    }
}
