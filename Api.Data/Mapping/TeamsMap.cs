﻿using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Data.Mapping
{
    public class TeamsMap : IEntityTypeConfiguration<TeamsEntity>
    {
        public void Configure(EntityTypeBuilder<TeamsEntity> builder)
        {
            builder.ToTable("Teams");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.TeamName)
                      .IsRequired()
                      .HasMaxLength(60);
        }
    }
}