using Microsoft.EntityFrameworkCore;
using StudentAPI.DataModels;
using System;
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


        public async Task<Student> GetStudentIdAsync(Guid studentId)
        {
            return await _context.Student.Include(nameof(Gender)).Include(nameof(Address)).FirstOrDefaultAsync(x => x.Id == studentId);
        }

        public async Task<List<Gender>> GetGendersAsync()
        {
            return await _context.Gender.ToListAsync();
        }

        public async Task<bool> Exists(Guid studentId)
        {
            return await _context.Student.AnyAsync(x => x.Id == studentId);
        }

        public async Task<Student> UpdateStudent(Guid studentId, Student request)
        {
            var existingStudent = await GetStudentIdAsync(studentId);
            if(existingStudent != null)
            {
                existingStudent.FirstName = request.FirstName;
                existingStudent.LastName = request.LastName;
                existingStudent.DateOfBirth = request.DateOfBirth;
                existingStudent.Email = request.Email;
                existingStudent.Mobile = request.Mobile;
                existingStudent.GenderId = request.GenderId;
                existingStudent.Address.PhysicalAddress = request.Address.PhysicalAddress;
                existingStudent.Address.PostalAddress = request.Address.PostalAddress;

                await _context.SaveChangesAsync();
                return existingStudent;
            }
            return null;
        }

        public async Task<Student> DeleteStudent(Guid studentId)
        {
            var student = await GetStudentIdAsync(studentId);
            if(student != null)
            {
                _context.Student.Remove(student);
                await _context.SaveChangesAsync();
                return student;
            }
            return null;
        }

        public async Task<Student> AddStudent(Student request)
        {
            var student = await _context.Student.AddAsync(request);
            await _context.SaveChangesAsync();
            return student.Entity;
        }

        public async Task<bool> UpdateProfileImage(Guid studentId, string profileImageUrl)
        {
            var student = await GetStudentIdAsync(studentId);

            if(student != null)
            {
                student.ProfileImageUrl = profileImageUrl;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
