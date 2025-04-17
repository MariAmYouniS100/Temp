using Business_logic_layer.interfaces;
using Data_access_layer.model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_logic_layer.Repository
{
   public class RevisionRepo :genericRepo<Revision>, IRevisionRepo
    {
        private readonly ApplicationDbContext context;
        public RevisionRepo(ApplicationDbContext context) : base(context)
        {
        }
        public IQueryable<Revision> searchCourseBytitle(string search)
        {
            return context.Revisions.Where(c => c.Title.ToLower().StartsWith(search));

        }
        
    }
    
}
