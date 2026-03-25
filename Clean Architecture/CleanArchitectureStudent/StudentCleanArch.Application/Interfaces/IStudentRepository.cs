using StudentCleanArch.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentCleanArch.Application.Interfaces
{
    public interface IStudentRepository
    {
        Task<Student> AddStudent(Student student);
        Task<List<Student>> GetAllStudents();
        Task<Student> GetStudentById(int id);
    }
}
