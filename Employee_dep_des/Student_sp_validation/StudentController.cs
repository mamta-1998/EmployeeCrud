using FinalMVC.Models;
using FinalMVC.Repository;
using Microsoft.AspNetCore.Mvc;

namespace FinalMVC.Controllers
{
 
       public class StudentController : Controller
    {
        private readonly IStudentRepository _repository;

        public StudentController(IStudentRepository repository)
        {
            _repository = repository;
        }

        #region Student List

        public IActionResult Index()
        {
            List<Student> students = _repository.GetStudents();

            return View(students);
        }

        #endregion

        #region Create

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Student model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Marks Validation

            if (model.PhysicsMarks < 0 || model.PhysicsMarks > 100)
            {
                ModelState.AddModelError("", "Physics Marks must be between 0 and 100");
                return View(model);
            }

            if (model.ChemistryMarks < 0 || model.ChemistryMarks > 100)
            {
                ModelState.AddModelError("", "Chemistry Marks must be between 0 and 100");
                return View(model);
            }

            if (model.MathMarks < 0 || model.MathMarks > 100)
            {
                ModelState.AddModelError("", "Math Marks must be between 0 and 100");
                return View(model);
            }

            // Business Logic

            model.TotalMarks =
                model.PhysicsMarks +
                model.ChemistryMarks +
                model.MathMarks;

            model.Percentage = Math.Round(model.TotalMarks / 3.0m, 2);

            // Admission Eligibility Logic

            if (model.Percentage >= 75
                && model.PhysicsMarks >= 60
                && model.ChemistryMarks >= 60
                && model.MathMarks >= 60)
            {
                model.AdmissionStatus = "Eligible";
            }
            else
            {
                model.AdmissionStatus = "Not Eligible";
            }

            _repository.AddStudent(model);

            TempData["Success"] = "Student Admission Saved Successfully";

            return RedirectToAction("Index");
        }

        #endregion

        #region Edit

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Student student = _repository.GetStudentById(id);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }       
        [HttpPost]
        public IActionResult Edit(Student model) 
        {
            if (!ModelState.IsValid)
            {
                return View(model);  
            }

            // Recalculate

            model.TotalMarks =
                model.PhysicsMarks +
                model.ChemistryMarks +
                model.MathMarks;

            model.Percentage =
                Math.Round(model.TotalMarks / 3.0m, 2);

            if (model.Percentage >= 75
                && model.PhysicsMarks >= 60
                && model.ChemistryMarks >= 60
                && model.MathMarks >= 60)
            {
                model.AdmissionStatus = "Eligible";
            }
            else
            {
                model.AdmissionStatus = "Not Eligible";
            }
            var id = model.StudentId;
            _repository.UpdateStudent(model);

            TempData["Success"] = "Student Updated Successfully";

            return RedirectToAction("Index");
        }

        #endregion

        #region Details

        public IActionResult Details(int id)
        {
            Student student = _repository.GetStudentById(id);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        #endregion

        #region Delete
        //[HttpPost]
        public IActionResult Delete(int id)
        {
            //Student student = _repository.GetStudentById(id);
            _repository.DeleteStudent(id);
            return RedirectToAction("Index");
        }

    
        #endregion
    }
}
