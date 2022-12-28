using DataAccess.Contexts;
using Microsoft.AspNetCore.Mvc;
using WebUI.ViewModels;

namespace WebUI.Controllers;

public class HomeController : Controller
{
    private AppDbContext _context;
    public HomeController(AppDbContext context)
    {
        _context = context;
    }
    
    public IActionResult Index()
    {
        HomeViewModel homeVM = new()
        {
            SlideItems= _context.SlideItems,
            ShippingItems= _context.ShippingItems

        };
        return View(homeVM);
        #region ViewBag,Tempdata,Redirect
        //ViewBag.Name = "Metin";
        //ViewData["Surname"] = "Iskenderov";
        //TempData["Age"] = 5;
        //return RedirectToAction(nameof(Test));
        //return RedirectToAction("Test");
        //return Test();
        //return View(nameof(Test));
        #endregion
    }
}
