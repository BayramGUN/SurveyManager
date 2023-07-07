using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SurveyManager.Domain.HostAggregate.ValueObjects;
using SurveyManager.Domain.SurveyAggregate;
using SurveyManager.Domain.SurveyAggregate.ValueObjects;

namespace SurveyManager.Infrastructure.Persistence.Configurations;

public class SurveyConfigurations : IEntityTypeConfiguration<Survey>
{
    public void Configure(EntityTypeBuilder<Survey> builder)
    {
        ConfigureSurveysTable(builder);
        ConfigureQuestionsTable(builder);
    }

    private void ConfigureQuestionsTable(EntityTypeBuilder<Survey> builder)
    {
        builder.OwnsMany(survey => survey.Questions, qb => 
        {
            qb.ToTable("Questions");

            qb.WithOwner().HasForeignKey("SurveyId");
            
            qb.HasKey("Id", "SurveyId");

            qb.Property(question => question.Id)
                .HasColumnName("QuestionId")
                .HasConversion(
                    id => id.Value,
                    value => QuestionId.Create(value)
                );

            qb.Property(question => question.Name)
                .HasMaxLength(20);

            qb.Property(question => question.Title)
                .HasMaxLength(250);

            qb.Property(question => question.Type)
                .HasMaxLength(20);

            qb.Property(question => question.Choices)
                .HasColumnName("Choices").HasConversion(
                    choices => string.Join(";", choices!),
                    choicesString => choicesString.Split(';', StringSplitOptions.RemoveEmptyEntries).ToList()
                );
            //qb.Navigation(question => question).Metadata.SetField("_questions");
        });
        builder.Metadata.FindNavigation(nameof(Survey.Questions))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private void ConfigureSurveysTable(EntityTypeBuilder<Survey> builder)
    {
        builder.ToTable("Surveys");

        builder.HasKey(survey => survey.Id);

        builder.Property(survey => survey.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => SurveyId.Create(value));

        builder.Property(survey => survey.Title)
            .HasMaxLength(150);

        builder.Property(survey => survey.Description)
            .HasMaxLength(250);
        
        builder.Property(survey => survey.HostId)
            .HasConversion(
                id => id.Value,
                value => HostId.Create(value));
    }
}