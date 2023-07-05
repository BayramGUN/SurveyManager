using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SurveyManager.Domain.HostAggregate.ValueObjects;
using SurveyManager.Domain.SurveyAggregate.ValueObjects;
using SurveyManager.Domain.SurveyAnswerAggregate;
using SurveyManager.Domain.SurveyAnswerAggregate.ValueObjects;

namespace SurveyManager.Infrastructure.Persistence.Configurations;

public class SurveyAnswerConfigurations : IEntityTypeConfiguration<SurveyAnswer>
{
    public void Configure(EntityTypeBuilder<SurveyAnswer> builder)
    {
        ConfigureSurveyAnswersTable(builder);
        ConfigureQuestionsTable(builder);
    }

    private void ConfigureQuestionsTable(EntityTypeBuilder<SurveyAnswer> builder)
    {
        builder.OwnsMany(surveyAnswer => surveyAnswer.Answers, ab => 
        {
            ab.ToTable("Answers");

            ab.WithOwner().HasForeignKey("SurveyAnswerId");
            
            ab.HasKey("Id", "SurveyAnswerId");

            ab.Property(answer => answer.Id)
                .HasColumnName("AnswerId")
                .HasConversion(
                    id => id.Value,
                    value => AnswerId.Create(value)
                );

            ab.Property(answer => answer.QuestionName)
                .HasMaxLength(200);

            ab.Property(answer => answer.Type)
                .HasMaxLength(20);

            ab.Property(answer => answer.Choices)
                .HasColumnName("Choices").HasConversion(
                    choices => string.Join(";", choices!),
                    choicesString => choicesString.Split(';', StringSplitOptions.RemoveEmptyEntries).ToList()
                );
            //ab.Navigation(question => question).Metadata.SetField("_questions");
        });
        builder.Metadata.FindNavigation(nameof(SurveyAnswer.Answers))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private void ConfigureSurveyAnswersTable(EntityTypeBuilder<SurveyAnswer> builder)
    {
        builder.ToTable("SurveyAnswers");

        builder.HasKey(surveyAnswer => surveyAnswer.Id);

        builder.Property(surveyAnswer => surveyAnswer.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => SurveyAnswerId.Create(value));
        
        builder.Property(surveyAnswer => surveyAnswer.HostId)
            .HasConversion(
                id => id.Value,
                value => HostId.Create(value));

        builder.Property(surveyAnswer => surveyAnswer.SurveyId)
            .HasConversion(
                id => id.Value,
                value => SurveyId.Create(value));
    }
}