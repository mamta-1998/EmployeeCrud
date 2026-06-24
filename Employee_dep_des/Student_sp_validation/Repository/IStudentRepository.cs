using FinalMVC.Models;

namespace FinalMVC.Repository
{
    public interface IStudentRepository
    {
        List<Student> GetStudents();
        Student GetStudentById(int id);
        void AddStudent(Student student);
        void UpdateStudent(Student student);
        void DeleteStudent(int id);
    }
}
