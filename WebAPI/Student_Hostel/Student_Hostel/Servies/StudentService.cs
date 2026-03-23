using Microsoft.EntityFrameworkCore;
using Student_Hostel.DTO;
using Student_Hostel.Models;

public class StudentService : IStudentService
{
    private readonly StudentDbContext _context;

    public StudentService(StudentDbContext context)
    {
        _context = context;
    }

    public async Task CreateStudent(CreateStudentDTO dto)
    {
        var student = new Student
        {
            StudentName = dto.StudentName,
            Gender = dto.Gender,
            Age = dto.Age,
            Course = dto.Course
        };

        await _context.Students.AddAsync(student);
        await _context.SaveChangesAsync();

        var hostel = new Hostel
        {
            RoomNumber = dto.RoomNumber,
            StudentId = student.StudentId
        };

        await _context.Hostels.AddAsync(hostel);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateRoomNo(UpdateRoomDTO dto)
    {
        var hostel = await _context.Hostels
            .FirstOrDefaultAsync(x => x.StudentId == dto.StudentId);

        if (hostel == null)
            throw new Exception("Student not found in hostel");

        hostel.RoomNumber = dto.RoomNumber;

        await _context.SaveChangesAsync();
    }

    public async Task DeleteStudent(int id)
    {
        var student = await _context.Students
            .Include(x => x.Hostel)
            .FirstOrDefaultAsync(x => x.StudentId == id);

        if (student == null)
            throw new Exception("Student not found");

        if (student.Hostel != null)
            _context.Hostels.Remove(student.Hostel);

        _context.Students.Remove(student);

        await _context.SaveChangesAsync();
    }

    public async Task<List<StudentDTO>> GetAllStudents()
    {
        return await _context.Students
            .Select(x => new StudentDTO
            {
                StudentId = x.StudentId,
                StudentName = x.StudentName,
                Age = (int)x.Age
            }).ToListAsync();
    }

    public async Task<List<StudentDTO>> GetStudentsInHostel()
    {
        return await _context.Hostels
            .Include(x => x.Student)
            .Select(x => new StudentDTO
            {
                StudentId = x.Student.StudentId,
                StudentName = x.Student.StudentName,
                Age = (int)x.Student.Age,
                Course = x.Student.Course,
                RoomNumber = x.RoomNumber
            }).ToListAsync();
    }
}