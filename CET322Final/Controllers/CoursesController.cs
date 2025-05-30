using CET322Final.Data;
using CET322Final.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

public class CoursesController : Controller
{
    private readonly ApplicationDbContext _context;

    public CoursesController(ApplicationDbContext context)
    {
        _context = context;
    }

    // Herkes
    //Dersleri anasayfaya çek
    public IActionResult Index()
    {
        var courses = _context.Courses.ToList();
        return View(courses);
    }

    // Herkes
    //Gelen id kontrol varsa göster
    public IActionResult Details(int id)
    {
        var course = _context.Courses.FirstOrDefault(c => c.Id == id);
        if (course == null) return NotFound();
        return View(course);
    }

   //Ders ekleme GET
    // ADMIN
    [Authorize(Roles = "Admin")]
    public IActionResult Create()
    {
        return View();
    }

    //POST 
    // ADMIN
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public IActionResult Create(Course course)
    {
        if (ModelState.IsValid)
        {
            _context.Courses.Add(course);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        return View(course);
    }

    //ADMIN
    //İdden dersi bul forma gönder GET
    [Authorize(Roles = "Admin")]
    public IActionResult Edit(int id)
    {
        var course = _context.Courses.FirstOrDefault(c => c.Id == id);
        if (course == null) return NotFound();
        return View(course);
    }

    // SADECE ADMIN
    // Gelen dersi düzenle POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public IActionResult Edit(Course course)
    {
        if (ModelState.IsValid)
        {
            _context.Courses.Update(course);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        return View(course);
    }

    // SADECE ADMIN
    // Silme onayı GET
    [Authorize(Roles = "Admin")]
    public IActionResult Delete(int id)
    {
        var course = _context.Courses.FirstOrDefault(c => c.Id == id);
        if (course == null) return NotFound();

        return View(course);
    }
    //Silme işlemi onaylandı POST
    // SADECE ADMIN
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public IActionResult DeleteConfirmed(int id)
    {
        var course = _context.Courses.Find(id);
        if (course != null)
        {
            _context.Courses.Remove(course);
            _context.SaveChanges();
        }

        return RedirectToAction("Index");
    }
}
