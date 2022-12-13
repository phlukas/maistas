using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Maistas.Controllers;
public class CategoryController : Controller
{
    private readonly FoodDbContext context;

    public CategoryController(FoodDbContext context)
    {
        this.context = context;
    }

    public async Task<IActionResult> IndexAsync()
    {
        var categories = await context.Categories.ToListAsync();

        return View(categories);
    }

    public async Task<ActionResult> Create()
    {
        var category = new Category();

        return View(category);
    }

    [HttpPost]
    public async Task<ActionResult> Create(Category category)
    {
        ModelState["Dishes"].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;
        ModelState["Dishes"].Errors.Clear();

        if (ModelState.IsValid)
        {
            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        return View(category);
    }

    public async Task<ActionResult> Delete(int id)
    {
        var category = await context.Categories.SingleAsync(x => x.Id == id);
        return View(category);
    }

    [HttpPost]
    public async Task<ActionResult> DeleteConfirm(int id)
    {
        try
        {
            var category = await context.Categories.SingleAsync(x => x.Id == id);
            context.Remove(category);

            await context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        catch (Exception e)
        {
            ViewData["deletionNotPermitted"] = true;

            var dish = await context.Categories.SingleAsync(x => x.Id == id);
            // ReSharper disable once Mvc.ViewNotResolved
            return View(dish);
        }
    }

}
