using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
		var dishes = await context.Dishes.Include(x => x.Category).Include(x => x.Restaurant).ToListAsync();

        return View(dishes);
	}

	public async Task<ActionResult> Create()
	{
		var dish = new Dish();
		await dish.LoadAvailableDropdowns(context);

		return View(dish);
	}

	[HttpPost]
	public async Task<ActionResult> Create(Dish dish)
	{
		ModelState["DishIngredients"].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;
        ModelState["AvailableCategories"].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;
        ModelState["AvailableRestaurants"].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;
        ModelState["Category"].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;
        ModelState["Category"].Errors.Clear();
        ModelState["Restaurant"].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;
        ModelState["Restaurant"].Errors.Clear();

        if (ModelState.IsValid)
		{
			await context.Dishes.AddAsync(dish);
			await context.SaveChangesAsync();

			return RedirectToAction("Index");
		}

        await dish.LoadAvailableDropdowns(context);
        return View(dish);
	}

	public async Task<ActionResult> Edit(int id)
	{
		var dish = await context.Dishes.SingleAsync(x => x.Id == id);
        await dish.LoadAvailableDropdowns(context);
        return View(dish);
	}

	[HttpPost]
	public async Task<ActionResult> Edit(int id, Dish newDish)
	{
        ModelState["DishIngredients"].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;
        ModelState["AvailableCategories"].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;
        ModelState["AvailableRestaurants"].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;
        ModelState["Category"].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;
        ModelState["Category"].Errors.Clear();
        ModelState["Restaurant"].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;
        ModelState["Restaurant"].Errors.Clear();

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

		await newDish.LoadAvailableDropdowns(context);

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
			// ReSharper disable once Mvc.ViewNotResolved
			return View(dish);
		}
	}
	public async Task<ActionResult> Recommended()
    {
		var topDishesGroup = from orderedDish in context.Set<OrderedDish>()
                                group orderedDish by orderedDish.DishId
								into g
                                select new { g.Key, Count = g.Count()};

		var myTopDishesGroup = from orderedDish in context.Set<OrderedDish>()
								where orderedDish.Order.UserId == 1
								group orderedDish by orderedDish.DishId
								into g
								select new { g.Key, Count = g.Count()};

        var topDishesIds = await topDishesGroup.OrderByDescending(x => x.Count).Take(10).Select(x => x.Key).ToListAsync();
        var myTopDishesIds = await myTopDishesGroup.OrderByDescending(x => x.Count).Take(3).Select(x => x.Key).ToListAsync();

		var topDishes = await context.Dishes.Where(x => topDishesIds.Contains(x.Id)).ToListAsync();
        var myTopDishes = await context.Dishes.Where(x => myTopDishesIds.Contains(x.Id)).ToListAsync();


        List<Dish> recommendedDishes = new List<Dish>(3);

		foreach (var myTopDish in myTopDishes)
		{
			var bestDish = topDishes[0];

            foreach (var topDish in topDishes)
			{
				if (CalculateSimilarity(bestDish.Name, myTopDish.Name) < CalculateSimilarity(topDish.Name, myTopDish.Name))
				{
					bestDish = topDish;
                }
			}

            recommendedDishes.Add(bestDish);

            topDishes.Remove(bestDish);
		}

        return View(recommendedDishes);
    }

    private double CalculateSimilarity(string source, string target)
    {
        if ((source == null) || (target == null)) return 0.0;
        if ((source.Length == 0) || (target.Length == 0)) return 0.0;
        if (source == target) return 1.0;

        int stepsToSame = ComputeLevenshteinDistance(source, target);
        return (1.0 - ((double)stepsToSame / (double)Math.Max(source.Length, target.Length)));
    }

    private int ComputeLevenshteinDistance(string source, string target)
    {
        if ((source == null) || (target == null)) return 0;
        if ((source.Length == 0) || (target.Length == 0)) return 0;
        if (source == target) return source.Length;

        int sourceWordCount = source.Length;
        int targetWordCount = target.Length;

        // Step 1
        if (sourceWordCount == 0)
            return targetWordCount;

        if (targetWordCount == 0)
            return sourceWordCount;

        int[,] distance = new int[sourceWordCount + 1, targetWordCount + 1];

        // Step 2
        for (int i = 0; i <= sourceWordCount; distance[i, 0] = i++) ;
        for (int j = 0; j <= targetWordCount; distance[0, j] = j++) ;

        for (int i = 1; i <= sourceWordCount; i++)
        {
            for (int j = 1; j <= targetWordCount; j++)
            {
                // Step 3
                int cost = (target[j - 1] == source[i - 1]) ? 0 : 1;

                // Step 4
                distance[i, j] = Math.Min(Math.Min(distance[i - 1, j] + 1, distance[i, j - 1] + 1), distance[i - 1, j - 1] + cost);
            }
        }

        return distance[sourceWordCount, targetWordCount];
    }
}
