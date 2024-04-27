using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Portfolio.Models;
using Microsoft.AspNetCore.Hosting;
using System.Linq;
using System;
using Microsoft.Extensions.Hosting;

namespace Portfolio.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly _context _context;
        private readonly IWebHostEnvironment _environment;

        public ProjectsController(_context context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("i") == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(await _context.Project.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetString("i") == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (!id.HasValue)
                return NotFound();

            var project = await _context.Project
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            return project == null ? NotFound() : View(project);
        }

        public IActionResult Create()
        {

            if (HttpContext.Session.GetString("i") == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
       
        public async Task<IActionResult> Create(Projects project, IFormFile image)
        {
            //if (ModelState.IsValid)
            //{
            //    _context.Add(products);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}
            //ViewData["CategoryId"] = new SelectList(_context.categories, "CategoryId", "CatName", products.CategoryId);
            //return View(products);

            if (image != null)
            {
                string extention = Path.GetExtension(image.FileName);

                switch (extention)
                {
                    case ".jpg":
                    case ".gif":
                    case ".pdf":
                    case ".png":

                        string p = Path.Combine(_environment.WebRootPath, "photos");
                        var filename = Path.GetFileName(image.FileName);
                        string ipath = Path.Combine(p, filename);

                        using (var fs = new FileStream(ipath, FileMode.Create))
                        {
                            await image.CopyToAsync(fs);
                        }
                        project.Image = @"\photos\" + filename;
                        _context.Add(project);
                        await _context.SaveChangesAsync();
                        return RedirectToAction("Index");

                        break;

                    default:
                        ViewBag.m = "Wrong Picture Format";
                        break;
                }


            }
            else
            {
                ViewBag.m = "Must Add Picture";

            }
            return View();
        }
        //public async Task<IActionResult> Create([Bind("ProjectName,ProjectDescription,ProjectLink")] Projects project, IFormFile image)
        //{
        //    if (image == null)
        //    {
        //        ViewBag.m = "Must add a picture.";
        //        return View(project);
        //    }
        //    string extension = Path.GetExtension(image.FileName).ToLower();
        //    if (extension != ".jpg" && extension != ".gif" && extension != ".pdf")
        //    {
        //        ViewBag.m = "Unsupported picture format.";
        //        return View(project);
        //    }
        //    await Saveimage(project, image);

        //    _context.Add(project);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //public async Task<IActionResult> Saveimage(Projects project, IFormFile image)
        //{

        //    string path = Path.Combine(_environment.WebRootPath, "photos", Path.GetFileName(image.FileName));
        //    project.Image = @"\photos\" + Path.GetFileName(image.FileName);

        //    using (var stream = new FileStream(path, FileMode.Create))
        //    {
        //        await image.CopyToAsync(stream);
        //    }
        //    return View();
        //}

        public async Task<IActionResult> Edit(int? id)
        {

            if (HttpContext.Session.GetString("i") == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (!id.HasValue)
                return NotFound();

            var project = await _context.Project.FindAsync(id);
            return project == null ? NotFound() : View(project);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProjectName,ProjectDescription,Image,ProjectLink")] Projects project, IFormFile image)
        {



            //if (image == null)
            //{
            //    ViewBag.m = "Must add a picture.";
            //    return View(project);
            //}
            //string extension = Path.GetExtension(image.FileName).ToLower();
            //if (extension != ".jpg" && extension != ".gif" && extension != ".pdf" && extension!=".png")
            //{
            //    ViewBag.m = "Unsupported picture format.";
            //    return View(project);
            //}
            //await Saveimage(project, image);


            

            // Only updates the state to Modified if the entity exists
            _context.Update(project);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {

            if (HttpContext.Session.GetString("i") == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (!id.HasValue)
                return NotFound();

            var project = await _context.Project
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
            return project == null ? NotFound() : View(project);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var project = await _context.Project.FindAsync(id);
            if (project == null)
                return NotFound();

            _context.Project.Remove(project);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectExists(int id)
        {

            return _context.Project.Any(e => e.Id == id);
        }
        //public async Task<IActionResult> PicSave(IFormFile image, Projects project)
        //{
        //    if (image == null)
        //    {
        //        ViewBag.m = "Must add a picture.";
        //        return View(project);
        //    }

        //    string extension = Path.GetExtension(image.FileName).ToLower();
        //    if (extension != ".jpg" && extension != ".gif" && extension != ".pdf")
        //    {
        //        ViewBag.Message = "Unsupported picture format.";
        //        return View(project);
        //    }

        //    string path = Path.Combine(_environment.WebRootPath, "photos", Path.GetFileName(image.FileName));
        //    project.Image = @"\photos\" + Path.GetFileName(image.FileName);

        //    using (var stream = new FileStream(path, FileMode.Create))
        //    {
        //        await image.CopyToAsync(stream);
        //    }
        //    return View();
        //}

        //public IActionResult Create() => View();

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("ProjectName,ProjectDescription,ProjectLink")] Projects p, IFormFile file)
        //{


        //    await PicSave(file, p);
        //    if (p.Image != null)
        //    {
        //        _context.Add(p);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewBag.m = "Must add a picture.";
        //    return View(p);

        //}

    }
}
