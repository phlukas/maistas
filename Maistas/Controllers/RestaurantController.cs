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
        
        return View(restaurant);
    }
    
    [HttpPost]
    public async Task<ActionResult> AddRestaurant(Restaurant restaurant)
    {
        Console.WriteLine("AddRestaurant");
        Console.WriteLine(ModelState.ErrorCount);
        restaurant.User = new User();
        if (ModelState.IsValid)
        {
            Console.WriteLine("Model state is valid");
            await context.Restaurants.AddAsync(restaurant);
            await context.SaveChangesAsync();
        
            return RedirectToAction("Index");
        }

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
}