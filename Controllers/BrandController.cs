using Microsoft.AspNetCore.Mvc;
using System.Linq;

public class BrandController : Controller
{
    private readonly ApplicationDbContext _context;

    public BrandController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(string name)
    {
        if (_context.Brands.Any(b => b.Name == name))
        {
            ModelState.AddModelError(string.Empty, "Бренд с таким именем уже существует.");
            return View();
        }

        var brand = new Brand { Name = name };
        _context.Brands.Add(brand);
        _context.SaveChanges();

        return RedirectToAction("Index");
    }


    public IActionResult Delete(int id)
    {
        var brand = _context.Brands.Find(id);
        if (brand != null)
        {
            _context.Brands.Remove(brand);
            _context.SaveChanges();
        }
        return RedirectToAction("Index");
    }

    public IActionResult Index()
    {
        var brands = _context.Brands.ToList();
        return View(brands);
    }
}
