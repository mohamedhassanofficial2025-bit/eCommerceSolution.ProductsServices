using AutoMapper;
using eCommerce.BusinessLogicLayer.DTO;
using eCommerce.BusinessLogicLayer.ServiceContracts;
using eCommerce.DataAccessLayer.Entities;
using eCommerce.DataAccessLayer.RepositoryContracts;
using FluentValidation;
using FluentValidation.Results;
using System.Linq.Expressions;

namespace BusinessLogicLayer.Services;

public class ProductService : IProductsService
{
    private readonly IValidator<ProductAddRequest> _productAddRequestValidator;
    private readonly IValidator<ProductUpdateRequest> _productUpdateRequestValidator;
    private readonly IMapper _mapper;
    private readonly IProductsRepository _productsRepository;


    public ProductService(IValidator<ProductAddRequest> productAddRequestValidator, IValidator<ProductUpdateRequest> productUpdateRequestValidator, IMapper mapper, IProductsRepository productsRepository)
    {
        _productAddRequestValidator = productAddRequestValidator;
        _productUpdateRequestValidator = productUpdateRequestValidator;
        _mapper = mapper;
        _productsRepository = productsRepository;
    }


    public async Task<ProductResponse?> AddProduct(ProductAddRequest productAddRequest)
    {
        if (productAddRequest == null)
        {
            throw new ArgumentNullException(nameof(productAddRequest));
        }

        //Validate the product using Fluent Validation
        ValidationResult validationResult = await _productAddRequestValidator.ValidateAsync(productAddRequest);

        // Check the validation result
        if (!validationResult.IsValid)
        {
            string errors = string.Join(", ", validationResult.Errors.Select(temp => temp.ErrorMessage)); //Error1, Error2, ...
            throw new ArgumentException(errors);
        }


        //Attempt to add product
        Product productInput = _mapper.Map<Product>(productAddRequest); //Map productAddRequest into 'Product' type (it invokes ProductAddRequestToProductMappingProfile)
        Product? addedProduct = await _productsRepository.AddProduct(productInput);

        if (addedProduct == null)
        {
            return null;
        }

        ProductResponse addedProductResponse = _mapper.Map<ProductResponse>(addedProduct); //Map addedProduct into 'ProductRepsonse' type (it invokes ProductToProductResponseMappingProfile)

        return addedProductResponse;
    }


    public async Task<bool> DeleteProduct(Guid productID)
    {
        var result = await _productsRepository.DeleteProduct(productID);
        if (!result)
            return false;
        return true;
    }

    public async Task<ProductResponse?> GetProductByCondition(Expression<Func<Product, bool>> conditionExpression)
    {
        if(conditionExpression == null) return null;
        var result= await _productsRepository.GetProductByCondition(conditionExpression);

        if(result == null)
            return null;
        var ProductResponse= _mapper.Map<ProductResponse>(result);
        return ProductResponse;
    }

    public async Task<List<ProductResponse?>> GetProducts()
    {
        var Products =await _productsRepository.GetProducts();
        if (Products == null)
            return null;
        List<ProductResponse?> productsResponse = new List<ProductResponse?>();
        foreach (var product in Products)
        {
            productsResponse.Add(_mapper.Map<ProductResponse>(product));
        }
        return productsResponse;
    }

    public async Task<List<ProductResponse?>> GetProductsByCondition(Expression<Func<Product, bool>> conditionExpression)
    {
        if (conditionExpression == null) return null;
        var products=await _productsRepository.GetProductsByCondition(conditionExpression);
        if (products == null)
            return null;
        List<ProductResponse?> productsResponse = new List<ProductResponse?>();
        foreach (var product in products)
        {
            productsResponse.Add(_mapper.Map<ProductResponse>(product));
        }
        return productsResponse;
    }

    public async Task<ProductResponse?> UpdateProduct(ProductUpdateRequest productUpdateRequest)
    {
        if (productUpdateRequest == null)
            return null;

        // Validate the product using Fluent Validation
        ValidationResult validationResult =
            await _productUpdateRequestValidator.ValidateAsync(productUpdateRequest);

        // Check the validation result
        if (!validationResult.IsValid)
        {
            string errors = string.Join(", ",
                validationResult.Errors.Select(temp => temp.ErrorMessage));

            throw new ArgumentException(errors);
        }

        Product product = _mapper.Map<Product>(productUpdateRequest);

        Product? result = await _productsRepository.UpdateProduct(product);

        if (result == null)
            return null;

        ProductResponse productResponse =
            _mapper.Map<ProductResponse>(result);

        return productResponse;
    }
}