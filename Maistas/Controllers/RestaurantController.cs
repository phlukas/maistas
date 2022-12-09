using Maistas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Maistas.Controllers;

public class RestaurantController : Controller
{
    private readonly FoodDbContext context;
    
    public RestaurantController(FoodDbContext context)
    {
        this.context = context;
    }
    
    public async Task<ActionResult> Index()
    {
        var restaurants = await context.Restaurants.ToListAsync();
        
        return View(restaurants);
    }

    public IActionResult DishesList()
    {
        return View();
    }
    
    public async Task<ActionResult> AddRestaurant()
    {
        var restaurant = new Restaurant();
        // await restaurant.LoadAvailableDropdowns(context);
        
        return View(restaurant);
    }
    
    [HttpPost]
    public async Task<ActionResult> AddRestaurant(Restaurant restaurant)
    {
        ModelState["User"].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;
        ModelState["User"].Errors.Clear();
        
        if (ModelState.IsValid)
        {

            await context.Restaurants.AddAsync(restaurant);
            await context.SaveChangesAsync();
        
            return RedirectToAction("Index");
        }

        // await restaurant.LoadAvailableDropdowns(context);
        return View(restaurant);
    }
    
    public IActionResult RemoveRestaurant()
    {
        return View();
    }

    public IActionResult EditRestaurant()
    {
        return View();
    }

    public IActionResult FilteredDishList()
    {
        return View();
    }
}