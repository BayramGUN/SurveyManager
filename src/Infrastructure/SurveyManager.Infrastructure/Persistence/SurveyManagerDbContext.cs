using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

using SurveyManager.Domain.Common.Models;
using SurveyManager.Domain.SurveyAggregate;
using SurveyManager.Domain.SurveyAggregate.Entities;
using SurveyManager.Domain.SurveyAnswerAggregate;
using SurveyManager.Infrastructure.Persistence.Interceptors;

namespace SurveyManager.Infrastructure.Persistence;

public class SurveyManagerDbContext : DbContext
{
    private readonly PublishDomainEventsInterceptor _publishDomainEventsIntercepotor;
    public SurveyManagerDbContext(
        DbContextOptions<SurveyManagerDbContext> options,
        PublishDomainEventsInterceptor publishDomainEventsIntercepotor)
        : base(options)
    {
        _publishDomainEventsIntercepotor = publishDomainEventsIntercepotor;
    }

    public DbSet<Survey> Surveys { get; set;} = null!;
    public DbSet<SurveyAnswer> SurveyAnswers { get; set;} = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Ignore<List<IDomainEvent>>()
            .ApplyConfigurationsFromAssembly(typeof(SurveyManagerDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_publishDomainEventsIntercepotor);

        base.OnConfiguring(optionsBuilder);
    }
    
}
/* internal class ChoicesConverter : ValueConverter<List<string>, string>
{
    public ChoicesConverter() : base(
        v => string.Join(",", v),
        v => v.Split(new[] { ',' }).ToList())
    {
    }
} */