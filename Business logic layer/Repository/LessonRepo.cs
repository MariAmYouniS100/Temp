using Business_logic_layer.interfaces;
using Data_access_layer.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_logic_layer.Repository
{
   public  class LessonRepo :genericRepo<Lesson>,ILessonRepo
    {
        private readonly ApplicationDbContext context;

        public LessonRepo(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }
        public IQueryable<Lesson> searchCourseBytitle(string search)
        {
            return context.Lessons.Where(_context => _context.Title.ToLower().StartsWith(search));

        }

    }
}
