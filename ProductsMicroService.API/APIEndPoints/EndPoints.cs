using eCommerce.BusinessLogicLayer.DTO;
using eCommerce.BusinessLogicLayer.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace ProductsMicroService.API.APIEndPoints;

public static class EndPoints
{
    public static IEndpointRouteBuilder UseMinEndPoints(this IEndpointRouteBuilder app)
    {
        // GET: /api/Products
        app.MapGet("/api/Products",
            async (IProductsService productsService) =>
            {
                var products = await productsService.GetProducts();
                return Results.Ok(products);
            });

        // GET: /api/Products/{productId}
        app.MapGet("/api/Products/{productId:guid}",
            async (Guid productId, IProductsService productsService) =>
            {
                var product = await productsService
                    .GetProductByCondition(p => p.ProductID == productId);

                return product is null
                    ? Results.NotFound()
                    : Results.Ok(product);
            });

        // POST: /api/Products
        app.MapPost("/api/Products/",
            async ([FromBody]
                ProductAddRequest request,
                   IProductsService productsService) =>
            {
                var product = await productsService.AddProduct(request);

                return product is null
                    ? Results.Problem("Problem in Create Product")
                    : Results.Created(
                        $"/api/Products/{product.ProductID}",
                        product);
            });
        // GET /api/products/search/xxxxxxxxxxxxxxxx

        app.MapGet("/api/products/search/{SearchString}", async
            (IProductsService productsService, string SearchString) =>
        {
            List<ProductResponse?> productsByProductName = await
                productsService.GetProductsByCondition(temp =>
                    temp.ProductName != null &&
                    temp.ProductName.Contains(
                        SearchString,
                        StringComparison.OrdinalIgnoreCase));

            List<ProductResponse?> productsByCategory = await
                productsService.GetProductsByCondition(temp =>
                    temp.Category != null &&
                    temp.Category.Contains(
                        SearchString,
                        StringComparison.OrdinalIgnoreCase));

            var products = productsByProductName.Union(productsByCategory);

            return Results.Ok(products);
        });
        // PUT: /api/Products/
        app.MapPut("/api/Products/",
            async ([FromBody]
                   ProductUpdateRequest request,
                   IProductsService productsService) =>
            { 
                var product = await productsService.UpdateProduct(request);

                return product is null
                    ? Results.Problem("Problem in Update Product")
                    : Results.Ok(product);
            });

        // DELETE: /api/Products/{productId}
        app.MapDelete("/api/Products/{productId:guid}",
            async (Guid productId,
                   IProductsService productsService) =>
            {
                var deleted = await productsService.DeleteProduct(productId);
                if(deleted)
                    return Results.Ok("Product Deleted Successflly!");
                return Results.Problem("Problem in delete Product!");
            });

        return app;
    }
}