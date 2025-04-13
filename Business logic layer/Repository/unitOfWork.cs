using Business_logic_layer.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Business_logic_layer.Repository.unitOfWork;

namespace Business_logic_layer.Repository
{
    public class unitOfWork : IunitofWork,IDisposable
    {
        private readonly ApplicationDbContext dbcontext;



        public unitOfWork(ApplicationDbContext dbcontext)
        {

            this.dbcontext = dbcontext;
        }

        public Task<int> Save()
        {
            return dbcontext.SaveChangesAsync();
        }

        public void Dispose()
        {
            dbcontext.Dispose();
        }

    }
}