using eCommerce.BusinessLogicLayer.Validators;
using eCommerce.ProductsMicroService.API.Middleware;
using eCommerce.ProductsService.BusinessLogicLayer;
using eCommerce.ProductsService.DataAccessLayer;
using FluentValidation;
using FluentValidation.AspNetCore;
using ProductsMicroService.API.APIEndPoints;

var builder = WebApplication.CreateBuilder(args);

//Add DAL and BLL services
builder.Services.AddDataAccessLayer(builder.Configuration);
builder.Services.AddBusinessLogicLayer();

builder.Services.AddControllers();
// Add cors services
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:4200")
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

//add json enum configuration
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(
        new System.Text.Json.Serialization.JsonStringEnumConverter());
});
//FluentValidations
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<ProductAddRequestValidator>();

//enable swagger and endpoint exploerer
//add end point exploerer service
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

app.UseExceptionHandlingMiddleware();


app.UseRouting();
app.UseCors();
//Auth
app.UseAuthentication();
app.UseAuthorization();
// Use Routing
app.UseSwagger();
app.UseSwaggerUI();

app.UseMinEndPoints();
//app.MapControllers();

app.Run();
