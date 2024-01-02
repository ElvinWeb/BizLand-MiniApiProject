using ApiProject.Core.Entites;
using ApiProject.Core.Repositories;
using ApiProject.Data.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiProject.Data.Repositories.Implementations
{
    public class FeatureRepository : GenericRepository<Feature>, IFeatureRepository
    {
        public FeatureRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
