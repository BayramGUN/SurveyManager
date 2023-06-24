
using Microsoft.AspNetCore.Mvc.Infrastructure;
using SurveyManager.Api.Common.Errors;
using SurveyManager.Application;
using SurveyManager.Infrastructure;

//var MyAllowSpecificOrigins = "_myAllowSpecificOrigins"; 

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddApplication()
                    .AddInfrastructure(builder.Configuration);
   
    builder.Services.AddControllers();
    /* builder.Services.AddCors(options =>
    {
    options.AddPolicy(MyAllowSpecificOrigins,
                          policy =>
                          {
                              policy.AllowAnyOrigin()
                                    .AllowAnyHeader()
                                    .AllowAnyMethod();
                          });
    }); */
    builder.Services.AddSingleton<ProblemDetailsFactory, SurveyManagerProblemDetailsFactory>();
}

// Add services to the container.


var app = builder.Build();
{
    // Configure the HTTP request pipeline.
    
    app.UseHttpsRedirection();
    app.MapControllers();

    app.Run();

}