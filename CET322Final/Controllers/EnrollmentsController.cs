﻿using CET322Final.Data;
using CET322Final.Models;
using CET322Final.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class EnrollmentsController : Controller
{
    private readonly ApplicationDbContext _context;

    public EnrollmentsController(ApplicationDbContext context)
    {
        _context = context;
    }

    //  Ders atama formu
    public IActionResult Create()
    {
        var viewModel = new StudentEnrollmentViewModel
        {
            Students = _context.Students.ToList(),
            Courses = _context.Courses.ToList()
        };

        return View(viewModel);
    }

    // Ders atama işlemi
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(StudentEnrollmentViewModel model)
    {
        if (ModelState.IsValid)
        {
            var enrollment = new Enrollment
            {
                StudentId = model.StudentId,
                CourseId = model.CourseId
            };

            _context.Enrollments.Add(enrollment);
            _context.SaveChanges();

            return RedirectToAction("Index", "Students");
        }

        // Hatalıysa 
        model.Students = _context.Students.ToList();
        model.Courses = _context.Courses.ToList();

        return View(model);
    }

    //Not Güncelleme Sayfası
    public IActionResult Edit(int id)
    {
        var enrollment = _context.Enrollments
            .Include(e => e.Student)
            .Include(e => e.Course)
            .FirstOrDefault(e => e.Id == id);

        if (enrollment == null)
            return NotFound();

        return View(enrollment);
    }
    //Not Güncelleme İşlemi
    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, decimal? grade)
    {
        var enrollment = _context.Enrollments.FirstOrDefault(e => e.Id == id);
        if (enrollment == null)
            return NotFound();

        enrollment.Grade = grade;

        try
        {
            _context.SaveChanges();
            return RedirectToAction("Details", "Students", new { id = enrollment.StudentId });
        }
        catch
        {
            return View(enrollment);
        }
    }

    //Dersten çıkarma 
    public IActionResult Delete(int id)
    {
        var enrollment = _context.Enrollments
            .Include(e => e.Course)
            .Include(e => e.Student)
            .FirstOrDefault(e => e.Id == id);

        if (enrollment == null) return NotFound();

        return View(enrollment);
    }

    //Dersten çıkarma işlemi
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        var enrollment = _context.Enrollments.Find(id);
        if (enrollment != null)
        {
            int studentId = enrollment.StudentId;
            _context.Enrollments.Remove(enrollment);
            _context.SaveChanges();

            return RedirectToAction("Details", "Students", new { id = studentId });
        }

        return NotFound();
    }
}
