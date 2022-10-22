using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Maistas.Controllers;

public class DishController : Controller
{
    private readonly FoodDbContext context;

    public DishController(FoodDbContext context)
    {
        this.context = context;
    }

    public async Task<ActionResult> Index()
	{
		var dishes = await context.Dishes.Include(x => x.Category).ToListAsync();

        return View(dishes);
	}

	public async Task<ActionResult> Create()
	{
		var dish = new Dish();
		await dish.LoadAvailableCategories(context);

		return View(dish);
	}

	[HttpPost]
	public async Task<ActionResult> Create(Dish dish)
	{
		ModelState["DishIngredients"].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;
        ModelState["AvailableCategories"].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;
        ModelState["Category"].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;
        ModelState["Category"].Errors.Clear();

        if (ModelState.IsValid)
		{
			await context.Dishes.AddAsync(dish);
			await context.SaveChangesAsync();

			return RedirectToAction("Index");
		}

		return View(dish);
	}

	public async Task<ActionResult> Edit(int id)
	{
		var dish = await context.Dishes.SingleAsync(x => x.Id == id);
        await dish.LoadAvailableCategories(context);
        return View(dish);
	}

	[HttpPost]
	public async Task<ActionResult> Edit(int id, Dish newDish)
	{
        ModelState["DishIngredients"].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;
        ModelState["AvailableCategories"].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;
        ModelState["Category"].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;
		ModelState["Category"].Errors.Clear();

        if (ModelState.IsValid)
		{
			var dish = await context.Dishes.SingleAsync(x => x.Id == newDish.Id);

			dish.Name = newDish.Name;
			dish.Price = newDish.Price;
			dish.Description = newDish.Description;
			dish.Calories = newDish.Calories;
			dish.CategoryId = newDish.CategoryId;
			dish.Remainder = newDish.Remainder;
			dish.Vegan = newDish.Vegan;
			dish.WeightInGrams = newDish.WeightInGrams;

			await context.SaveChangesAsync();

			return RedirectToAction("Index");
		}

		await newDish.LoadAvailableCategories(context);

        return View(newDish);
	}

	public async Task<ActionResult> Delete(int id)
	{
		var dish = await context.Dishes.SingleAsync(x => x.Id == id);
		return View(dish);
	}

	[HttpPost]
	public async Task<ActionResult> DeleteConfirm(int id)
	{
		try
		{
			var dish = await context.Dishes.SingleAsync(x => x.Id == id);
			context.Remove(dish);

			await context.SaveChangesAsync();

			return RedirectToAction("Index");
		}
		catch (Exception e)
		{
			ViewData["deletionNotPermitted"] = true;

            var dish = await context.Dishes.SingleAsync(x => x.Id == id);
            return View(dish);
        }
	}
}
