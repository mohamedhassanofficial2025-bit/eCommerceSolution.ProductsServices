using BusinessLogicLayer.Services;
using eCommerce.BusinessLogicLayer.Mappers;
using eCommerce.BusinessLogicLayer.ServiceContracts;
using eCommerce.BusinessLogicLayer.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace eCommerce.ProductsService.BusinessLogicLayer;

public static class DependencyInjection
{
  public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services)
  {
    //TO DO: Add Business Logic Layer services into the IoC container
    services.AddTransient<IProductsService, ProductService>();
    services.AddAutoMapper(typeof(ProductAddRequestToProductMappingProfile).Assembly);
    return services;
  }
}
