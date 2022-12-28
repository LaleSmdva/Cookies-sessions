using Core.Entities;
using DataAccess.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ShippingItemController : Controller
    {
        private AppDbContext _context;
        public ShippingItemController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.ShippingItems);
        }

        public async Task<IActionResult> Details(int id)
		{
			var model=await _context.ShippingItems.FindAsync(id);
			return View(model);
		}
		#region Create
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ActionName("Create")] //set,get metodlarin adlarinin eyni olmagini istemiremse yaziram
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CreatePost(ShippingItem item)
		{
			if (!ModelState.IsValid) return View(); //annotationlara uygun modelin geldiyini yoxluyur
			await _context.ShippingItems.AddAsync(item);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}
		#endregion

		#region Update
		public IActionResult Update(int id)
		{
            var model=_context.ShippingItems.Find(id);
            if(model==null) return NotFound();
			return View(model);
		}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id,ShippingItem item)
        {
            if (id != item.Id) return BadRequest();
            if(!ModelState.IsValid) return View(item);
            var model = _context.ShippingItems.Find(id);
            if (model == null) return NotFound();
            model.Description = item.Description;
            model.Title = item.Title;
            model.Image = item.Image;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
		#endregion
		#region Delete
		public async Task<IActionResult> Delete(int id)
		{
			var model = await _context.ShippingItems.FindAsync(id);
			if (model == null) return NotFound();
			return View(model);
		}
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")] //rootdan delete'a delete adinda post gelirse bunu isledirik
		public async Task<IActionResult> DeleteItem(int id)
		{
			var model = await _context.ShippingItems.FindAsync(id);
			if (model == null) return NotFound();
            _context.ShippingItems.Remove(model);
            await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}
		#endregion
	
	}
}
