using CET322Final.Data;
using CET322Final.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

public class StudentsController : Controller
{
    private readonly ApplicationDbContext _context;

    public StudentsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // Öğrenci listesi
    public IActionResult Index()
    {
        var students = _context.Students.ToList();
        return View(students);
    }

    // Detay sayfası
    public IActionResult Details(int id)
    {
        var student = _context.Students
            .Include(s => s.Enrollments)
            .ThenInclude(e => e.Course)
            .FirstOrDefault(s => s.Id == id);

        if (student == null) return NotFound();

        ViewBag.Enrollments = student.Enrollments; 
        return View(student);
    }

    // Yeni öğrenci formu
    public IActionResult Create()
    {
        return View();
    }

    // Yeni öğrenci oluşturma işlemi
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Student student)
    {
        if (ModelState.IsValid)
        {
            student.EnrollmentDate = DateTime.Now;
            _context.Students.Add(student);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        return View(student);
    }
    // Silme onay sayfası (GET)
    public IActionResult Delete(int id)
    {
        var student = _context.Students.FirstOrDefault(s => s.Id == id);
        if (student == null) return NotFound();

        return View(student);
    }

    // Silme işlemi (POST)
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        var student = _context.Students.FirstOrDefault(s => s.Id == id);
        if (student == null) return NotFound();

        _context.Students.Remove(student);
        _context.SaveChanges();

        return RedirectToAction("Index");
    }
}
