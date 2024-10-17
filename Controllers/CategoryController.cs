using Microsoft.AspNetCore.Mvc;
using System.Linq;

public class CategoryController : Controller
{
    private readonly ApplicationDbContext _context;

    public CategoryController(ApplicationDbContext context)
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
        if (_context.Categories.Any(c => c.Name == name))
        {
            ModelState.AddModelError(string.Empty, "Категория с таким именем уже существует.");
            return View();
        }

        var category = new Category { Name = name };
        _context.Categories.Add(category);
        _context.SaveChanges();

        return RedirectToAction("Index");
    }

    public IActionResult Delete(int id)
    {
        var category = _context.Categories.Find(id);
        if (category != null)
        {
            _context.Categories.Remove(category);
            _context.SaveChanges();
        }
        return RedirectToAction("Index");
    }

    public IActionResult Index()
    {
        var categories = _context.Categories.ToList();
        return View(categories);
    }
}
