using Student_Hostel.DTO;

public interface IStudentService
{
    Task CreateStudent(CreateStudentDTO dto);

    Task UpdateRoomNo(UpdateRoomDTO dto);

    Task DeleteStudent(int id);

    Task<List<StudentDTO>> GetAllStudents();

    Task<List<StudentDTO>> GetStudentsInHostel();
}