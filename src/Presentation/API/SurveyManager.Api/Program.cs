using SurveyManager.Api;
using SurveyManager.Application;
using SurveyManager.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddPresentation()
                    .AddApplication()
                    .AddInfrastructure(builder.Configuration);
   
    
}

var app = builder.Build();
{
    // Configure the HTTP request pipeline.
    app.UseCors("_mySpecs");
    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();

}