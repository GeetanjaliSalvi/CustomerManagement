using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerManagementSystemAPI
{
    public static class MapperHelper
    {
        static void MappingHelper()
        {
            CreateMaps();
        }
        public static void Initialize()
        {
        }
        private static void CreateMaps()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Models.Customer, CustomerManagementDAL.Customer>());
        }

        /// <summary>
        /// Map objects from a source to a destination instance that is provided.
        /// </summary>
        /// <typeparam name="TSource">source type</typeparam>
        /// <typeparam name="TDestination">destination type</typeparam>
        /// <param name="source">source object</param>
        /// <param name="destination">destination object</param>
        /// <returns>a reference to the destination instance</returns>
        public static TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            var config = new MapperConfiguration(cfg =>
                cfg.CreateMap<Models.Customer, CustomerManagementDAL.Customer>()
            );
            var mapper = new Mapper(config);
            return mapper.Map(source, destination);
        }

        /// <summary>
        /// Map objects
        /// </summary>
        /// <typeparam name="TSource">source type</typeparam>
        /// <typeparam name="TDestination">destination type</typeparam>
        /// <param name="obj">source object(s)</param>
        /// <returns>destination objects(s)</returns>
        public static TDestination ModelToDomainMap<TSource, TDestination>(TSource obj)
        {
            var config = new MapperConfiguration(cfg =>
                cfg.CreateMap<Models.Customer, CustomerManagementDAL.Customer>()
            );

            var mapper = new Mapper(config);
            return mapper.Map<TSource, TDestination>(obj);
        }

        public static TDestination DomainToModelMap<TSource, TDestination>(TSource obj)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<CustomerManagementDAL.Customer, Models.Customer>()
            );

            var mapper = new Mapper(config);
            return mapper.Map<TSource, TDestination>(obj);
        }
    }
}