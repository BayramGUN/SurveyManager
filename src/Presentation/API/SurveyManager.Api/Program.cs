using SurveyManager.Api;
using SurveyManager.Application;
using SurveyManager.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddPresentation()
                    .AddApplication()
                    .AddInfrastructure(builder.Configuration);
   
    
}

// Add services to the container.
var app = builder.Build();
{
    // Configure the HTTP request pipeline.
    
    app.UseHttpsRedirection();
    app.MapControllers();

    app.Run();

}