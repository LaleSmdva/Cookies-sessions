using Core.Entities;
using DataAccess.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.IO;
using WebUI.Areas.Admin.ViewModels.Slider;
using WebUI.Utilities;

namespace WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SlideItemController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public SlideItemController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index()
        {
            return View(_context.SlideItems);
        }
        public async Task<IActionResult> Details(int id)
        {
            var slide = await _context.SlideItems.FindAsync(id);
            if (slide == null) return NotFound();
            return View(slide);
        }
        #region Create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SlideCreateVM slide)
        {
            if (!ModelState.IsValid) return View(slide);
            if (slide.Photo == null) ///enctype-i front yazir(file secmesek enctype error verir)
			{
                ModelState.AddModelError("Photo", "Select a file");
                return View(slide);
            }
            if (!slide.Photo.CheckFileSize(100))
            {
                ModelState.AddModelError("Photo", "File size must be less than 3 kilobyte");
                return View(slide);
            }
            if (!slide.Photo.CheckImage("image/")) //yalniz image fayllari secilsin
            {
                ModelState.AddModelError("Photo", "Please choose an image file");
                return View(slide);
            }
            /* metod-1*/
            //var filename = Guid.NewGuid().ToString() + slide.Photo.FileName;
            //var path = Path.Combine(@"C:\Users\User\Desktop\Copy of CRUD(errorlu sinif)\Sinif-ASP.NET-CRUD-\WebUI\wwwroot\assets\images\website-images", filename);
            //using (FileStream stream = new FileStream(path, FileMode.Create))
            //{
            //    await slide.Photo.CopyToAsync(stream);
            //}

            /* metod-2*/
            //var wwwroot = _env.WebRootPath;
            //var filename = Guid.NewGuid().ToString() + slide.Photo.FileName;
            //var resultpath = Path.Combine(wwwroot, "assets", "images", "website-images", filename);

            //using (FileStream stream = new FileStream(resultpath, FileMode.Create))
            //{
            //    await slide.Photo.CopyToAsync(stream);
            //}
            var filename = string.Empty;
            try
            {
                 filename = await slide.Photo.CopyFileAsync(_env.WebRootPath, "assets", "images", "website-images");

            }
            catch (Exception)
            {

                return View(slide);
            }
            SlideItem slideItem = new SlideItem()
            {
                Title = slide.Title,
                Offer = slide.Offer,
                Description = slide.Description,
                Photo = filename
            };
            await _context.SlideItems.AddAsync(slideItem);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        #endregion
        #region Delete
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _context.SlideItems.FindAsync(id);
            if (model == null) return NotFound();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var model = await _context.SlideItems.FindAsync(id);
            if (model == null) return NotFound();
            //db-dan silmezden evvel folder-da silmeliyem
            var path = Path.Combine(_env.WebRootPath, "assets", "images", "website-images",model.Photo);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            _context.SlideItems.Remove(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion
        #region Update
        public IActionResult Update(int id)
        {
            var model = _context.SlideItems.Find(id);
            if (model == null) return NotFound();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, SlideCreateVM slide)
        {
            //if (id != item.Id) return BadRequest();
            //if (!ModelState.IsValid) return View(item);
            //var model = _context.SlideItems.Find(id);
            //if (model == null) return NotFound();
            //_context.Update(item);
            //_context.Update(file);
            //model.Description = item.Description;
            //model.Title = item.Title;
            //model.Image = item.Image;



        //    var wwwroot = _env.WebRootPath;
        //    var filename = Guid.NewGuid().ToString() + slide.Photo.FileName;
        //    var resultpath = Path.Combine(wwwroot, "assets", "images", "website-images", filename);

        //    using (FileStream stream = new FileStream(resultpath, FileMode.Create))
        //    {
        //        await slide.Photo.CopyToAsync(stream);
        //    }
        //    var model = _context.SlideItems.Find(id);


        //    SlideItem slideItem = new SlideItem()
        //    {
        //        //Title = slide.Title,
        //        //Offer = slide.Offer,
        //        //Description = slide.Description,
        //        //Photo = filename
        //    model.Description = slide.Description,
        //    model.Title = slide.Title,
        //    model.Photo = slide.filename
        //};
    

        //_context.Update(slide);
        //    await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

            #endregion
        }
    }
}
