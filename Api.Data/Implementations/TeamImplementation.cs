using MeuCampeonato.Data.Context;
using MeuCampeonato.Data.Repository;
using MeuCampeonato.Domain.Entities;
using MeuCampeonato.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeuCampeonato.Data.Implementations
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
