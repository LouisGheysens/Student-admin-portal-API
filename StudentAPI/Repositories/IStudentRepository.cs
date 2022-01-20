using StudentAPI.DataModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentAPI.Repositories
{
    public interface IStudentRepository
    {
        Task<List<Student>> GetStudentsAsync();

    }
}
