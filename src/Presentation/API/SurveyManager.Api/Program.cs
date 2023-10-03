using Hangfire;

using SurveyManager.Api;
using SurveyManager.Application;
using SurveyManager.Application.Common.Services;
using SurveyManager.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddPresentation()
                    .AddApplication()
                    .AddInfrastructure(builder.Configuration);   
}

var _mySpecs = "_mySpecs";
var app = builder.Build();
{
    //app.UseHangfireServer();
    app.UseHangfireDashboard();
    app.UseCors(_mySpecs);
    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
    RecurringJob.AddOrUpdate<IServiceManagement>("updateJob", (service) => service.UpdateDatabase(), "*/30 * * * *");
    app.Run();

}