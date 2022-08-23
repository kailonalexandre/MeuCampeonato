using MeuCampeonato.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeuCampeonato.Data.Mapping
{
    public class MatchsMap : IEntityTypeConfiguration<MatchEntity>
    {
        public void Configure(EntityTypeBuilder<MatchEntity> builder)
        {
            builder.ToTable("Match");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.TeamA)
                      .IsRequired()
                      .HasMaxLength(60);  
            builder.Property(u => u.TeamB)
                      .IsRequired()
                      .HasMaxLength(60);
            builder.Property(u => u.ScoreTeamA).IsRequired();
            builder.Property(u => u.ScoreTeamB).IsRequired();
            builder.Property(u => u.Bracket).IsRequired();
        }
    }
}
