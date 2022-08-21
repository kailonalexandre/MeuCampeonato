using Api.Data.Context;
using Api.Data.Repository;
using Api.Domain.Entities;
using Api.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Data.Implementations
{
    public class TeamImplementation : BaseRepository<TeamsEntity>, ITeamsRepository
    {
        private DbSet<TeamsEntity> _dataset;

        public TeamImplementation(MyContext context) : base(context)
        {
            _dataset = context.Set<TeamsEntity>();
        }

    }
}
