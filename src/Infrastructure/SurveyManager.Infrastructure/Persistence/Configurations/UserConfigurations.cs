using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SurveyManager.Domain.HostAggregate.ValueObjects;
using SurveyManager.Domain.SurveyAggregate;
using SurveyManager.Domain.SurveyAggregate.ValueObjects;
using SurveyManager.Domain.UserAggregate;

namespace SurveyManager.Infrastructure.Persistence.Configurations;

public class UserConfigurations : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        ConfigureUsersTable(builder);
    }

    private void ConfigureUsersTable(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(user => user.Id);

        builder.Property(user => user.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => SurveyId.Create(value));

        builder.Property(user => user.Email)
            .HasMaxLength(150);

        builder.Property(user => user.Firstname)
            .HasMaxLength(25);
        
        builder.Property(user => user.Lastname)
            .HasMaxLength(25);
            
        builder.Property(user => user.Password);
    }
}