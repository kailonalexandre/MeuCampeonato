using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Data.Mapping
{
    public class GamesMap : IEntityTypeConfiguration<GamesEntity>
    {
        public void Configure(EntityTypeBuilder<GamesEntity> builder)
        {
            builder.ToTable("Games");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.FirstTeam)
                      .IsRequired()
                      .HasMaxLength(60);  
            builder.Property(u => u.SecondTeam)
                      .IsRequired()
                      .HasMaxLength(60);
            builder.Property(u => u.GoalsFistTeam).IsRequired();
            builder.Property(u => u.GoalsSecondTeam).IsRequired();
            builder.Property(u => u.TeamKey).IsRequired();
        }
    }
}
