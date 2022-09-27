using AutoMapper;
using GeekShopping.Product.API.Data.ValueObjects;
using GeekShopping.Product.API.Model;

namespace GeekShopping.Product.API.Config
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(c=> 
            {
                c.CreateMap<ProductVO, Model.Product>();
                c.CreateMap<Model.Product, ProductVO>();
            });
            return mappingConfig;
        }
    }
}
