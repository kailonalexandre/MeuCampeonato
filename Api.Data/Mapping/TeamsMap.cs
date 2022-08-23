using MeuCampeonato.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeuCampeonato.Data.Mapping
{
    public class TeamsMap : IEntityTypeConfiguration<TeamsEntity>
    {
        public void Configure(EntityTypeBuilder<TeamsEntity> builder)
        {
            builder.ToTable("Teams");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Name)
                      .IsRequired()
                      .HasMaxLength(60);
        }
    }
}
