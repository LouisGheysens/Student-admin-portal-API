using Microsoft.EntityFrameworkCore;
using StudentAPI.DataModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentAPI.Repositories
{
    public class SqlStudentRepository : IStudentRepository
    {

        private readonly StudentAdminContext _context;

        public SqlStudentRepository(StudentAdminContext context)
        {
            this._context = context;
        }

        public async Task<List<Student>> GetStudentsAsync()
        {
           return await _context.Student.Include(nameof(Gender)).Include(nameof(Address)).ToListAsync();
        }
    }
}
