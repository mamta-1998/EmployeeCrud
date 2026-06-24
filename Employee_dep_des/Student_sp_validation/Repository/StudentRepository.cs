using System.Data;
using FinalMVC.Data;
using FinalMVC.Models;
using Microsoft.Data.SqlClient;

namespace FinalMVC.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly DBHelper _dbHelper;
        public StudentRepository(DBHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public void AddStudent(Student student)
        {
            SqlParameter[] param =
            {
                new SqlParameter("@StudentName",student.StudentName),
                new SqlParameter("@FatherName",student.FatherName),
                new SqlParameter("@Gender",student.Gender),
                new SqlParameter("@DOB",student.DOB),
                new SqlParameter("@Mobile",student.Mobile),
                new SqlParameter("@Email",student.Email),
                new SqlParameter("@PhysicsMarks",student.PhysicsMarks),
                new SqlParameter("@ChemistryMarks",student.ChemistryMarks),
                new SqlParameter("@MathMarks",student.MathMarks),
                new SqlParameter("@TotalMarks",student.TotalMarks),
                new SqlParameter("@Percentage",student.Percentage),
                new SqlParameter("@AdmissionStatus",student.AdmissionStatus)
            };

            _dbHelper.ExecuteNonQuery( "SP_InsertStudent", param);
        }


        public void DeleteStudent(int id)
        {
            SqlParameter[] param =
            {
                new SqlParameter("@StudentId",id)
            };

            _dbHelper.ExecuteNonQuery( "SP_DeleteStudent", param);
        }
    

        public Student GetStudentById(int id)
        {
            Student student = new Student();

            SqlParameter[] param =
            {
                new SqlParameter("@StudentId",id)
            };
            //string query = "";

            //DataTable dt = _dbHelper.ExecuteQuery("SELECT * FROM Students WHERE StudentId = @StudentId", mode:"",parameters: param  );
            DataTable dt = _dbHelper.ExecuteQuery( "SP_GetStudentById", param, "sp");

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];

                student.StudentId = Convert.ToInt32(dr["StudentId"]); 
                student.StudentName = dr["StudentName"].ToString(); 
                student.FatherName = dr["FatherName"].ToString(); 
                student.Gender = dr["Gender"].ToString(); 
                student.DOB = Convert.ToDateTime(dr["DOB"]); 
                student.Mobile = dr["Mobile"].ToString(); 
                student.Email = dr["Email"].ToString(); 
                student.PhysicsMarks = Convert.ToInt32(dr["PhysicsMarks"]);  
                student.ChemistryMarks = Convert.ToInt32(dr["ChemistryMarks"]); 
                student.MathMarks = Convert.ToInt32(dr["MathMarks"]); 
                student.TotalMarks = Convert.ToInt32(dr["TotalMarks"]); 
                student.Percentage = Convert.ToDecimal(dr["Percentage"]);  
                student.AdmissionStatus = dr["AdmissionStatus"].ToString();
            }

            return student;
        }


        public List<Student> GetStudents()
        {
            List<Student> students = new List<Student>();

            DataTable dt = _dbHelper.ExecuteQuery( "SP_GetStudents");

            foreach (DataRow dr in dt.Rows)
            {
                students.Add(new Student
                {
                    StudentId = Convert.ToInt32(dr["StudentId"]),
                    StudentName = dr["StudentName"].ToString(),
                    FatherName = dr["FatherName"].ToString(),
                    Gender = dr["Gender"].ToString(),
                    DOB = Convert.ToDateTime(dr["DOB"]),
                    Mobile = dr["Mobile"].ToString(),
                    Email = dr["Email"].ToString(),
                    PhysicsMarks = Convert.ToInt32(dr["PhysicsMarks"]),
                    ChemistryMarks = Convert.ToInt32(dr["ChemistryMarks"]),
                    MathMarks = Convert.ToInt32(dr["MathMarks"]),
                    TotalMarks = Convert.ToInt32(dr["TotalMarks"]),
                    Percentage = Convert.ToDecimal(dr["Percentage"]),
                    AdmissionStatus = dr["AdmissionStatus"].ToString()
                });
            }

            return students;
        }

        public void UpdateStudent(Student student)
        {
            SqlParameter[] param =
            {
                new SqlParameter("@StudentId",student.StudentId),
                new SqlParameter("@StudentName",student.StudentName),
                new SqlParameter("@FatherName",student.FatherName),
                new SqlParameter("@Gender",student.Gender),
                new SqlParameter("@DOB",student.DOB),
                new SqlParameter("@Mobile",student.Mobile),
                new SqlParameter("@Email",student.Email),
                new SqlParameter("@PhysicsMarks",student.PhysicsMarks),
                new SqlParameter("@ChemistryMarks",student.ChemistryMarks),
                new SqlParameter("@MathMarks",student.MathMarks),
                new SqlParameter("@TotalMarks",student.TotalMarks),
                new SqlParameter("@Percentage",student.Percentage),
                new SqlParameter("@AdmissionStatus",student.AdmissionStatus)
            };

            _dbHelper.ExecuteNonQuery( "SP_UpdateStudent", param);
        }
    }
}
