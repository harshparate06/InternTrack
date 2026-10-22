
using InternTrack.Data;
using InternTrack.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InternTrack.Controllers;

public class InternshipApplicationsController : Controller
{
    private readonly ApplicationDbContext _context;

    public InternshipApplicationsController(
        ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var applications = await _context.Applications
            .OrderByDescending(a => a.ApplicationDate)
            .ToListAsync();

        return View(applications);
    }

    
    public async Task<IActionResult> Dashboard()
    {
        ViewBag.TotalApplications =
            await _context.Applications.CountAsync();

        ViewBag.AppliedCount =
            await _context.Applications
                .CountAsync(a => a.Status == "Applied");

        ViewBag.InterviewCount =
            await _context.Applications
                .CountAsync(a => a.Status == "Interview");

        ViewBag.SelectedCount =
            await _context.Applications
                .CountAsync(a => a.Status == "Selected");

        return View();
    }


    [HttpGet]
    public IActionResult Create()
    {
        return View(new InternshipApplication());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        InternshipApplication application)
    {
        if (!ModelState.IsValid)
        {
            return View(application);
        }

        application.CreatedAt = DateTime.UtcNow;
        application.UpdatedAt = DateTime.UtcNow;

        _context.Applications.Add(application);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var application = await _context.Applications
            .FindAsync(id);

        if (application == null)
        {
            return NotFound();
        }

        return View(application);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        int id, InternshipApplication application)
    {
        if (id != application.ApplicationId)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return View(application);
        }

        var existingApplication = await _context.Applications
            .FindAsync(id);

        if (existingApplication == null)
        {
            return NotFound();
        }

        existingApplication.CompanyName = application.CompanyName;
        existingApplication.JobRole = application.JobRole;
        existingApplication.ApplicationUrl = application.ApplicationUrl;
        existingApplication.Location = application.Location;
        existingApplication.ApplicationDate = application.ApplicationDate;
        existingApplication.Deadline = application.Deadline;
        existingApplication.Status = application.Status;
        existingApplication.WorkMode = application.WorkMode;
        existingApplication.Stipend = application.Stipend;
        existingApplication.Notes = application.Notes;
        existingApplication.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var application = await _context.Applications
            .FindAsync(id);

        if (application == null)
        {
            return NotFound();
        }

        _context.Applications.Remove(application);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }


}
