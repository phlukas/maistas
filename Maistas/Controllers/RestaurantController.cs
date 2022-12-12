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

    public async Task<ActionResult> DishesList(Restaurant restaurant)
    {
        var dishes = await context.Dishes
            .Select(d=>d)
            .Where(d=> d.RestaurantId == restaurant.Id)
            .ToListAsync();
        
        DishListView dishListView = new DishListView();
        dishListView.Dishes = dishes;
        dishListView.Restaurant = restaurant;
        
        return View(dishListView);
    }
    
    [HttpPost]
    public async Task<ActionResult> DishesList(DishListView dishListView)
    {
        var filteredDishes = await context.Dishes
            .ToListAsync();

        if (dishListView.PriceFrom != null)
        {
            filteredDishes = filteredDishes.Where(d => d.Price >= dishListView.PriceFrom).ToList();
        }
        
        if (dishListView.PriceTo != null)
        {
            filteredDishes = filteredDishes.Where(d => d.Price <= dishListView.PriceTo).ToList();
        }
        
        if (dishListView.CaloriesFrom != null)
        {
            filteredDishes = filteredDishes.Where(d => d.Calories >= dishListView.CaloriesFrom).ToList();
        }
        
        if (dishListView.CaloriesTo != null)
        {
            filteredDishes = filteredDishes.Where(d => d.Calories <= dishListView.CaloriesTo).ToList();
        }
        
        if (dishListView.WeightFrom != null)
        {
            filteredDishes = filteredDishes.Where(d => d.WeightInGrams >= dishListView.WeightFrom).ToList();
        }
        
        if (dishListView.WeightTo != null)
        {
            filteredDishes = filteredDishes.Where(d => d.WeightInGrams <= dishListView.WeightTo).ToList();
        }
        
        if (dishListView.IsVegetarian != null)
        {
            filteredDishes = filteredDishes.Where(d => d.Vegan == dishListView.IsVegetarian).ToList();
        }
        
        dishListView.Dishes = filteredDishes;
        
        return View(dishListView);
    }
    
    public async Task<ActionResult> AddRestaurant()
    {
        var restaurant = new Restaurant();
        await restaurant.LoadAvailableDropdowns(context);
        
        return View(restaurant);
    }
    
    [HttpPost]
    public async Task<ActionResult> AddRestaurant(Restaurant restaurant)
    {
        if (restaurant.UserId == 0)
        {
            await restaurant.LoadAvailableDropdowns(context);
            return View(restaurant);
        }
        var user = await context.User.Select(u=> u).Where(u => u.Id == restaurant.UserId).FirstOrDefaultAsync();
        user.Role = "Restaurant";
        restaurant.User = user;
        
        #region User input validation
        ModelState["User.Name"].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;
        ModelState["User.Name"].Errors.Clear();
        ModelState["User.Email"].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;
        ModelState["User.Email"].Errors.Clear();
        ModelState["User.Surname"].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;
        ModelState["User.Surname"].Errors.Clear();
        ModelState["User.Role"].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;
        ModelState["User.Role"].Errors.Clear();
        ModelState["User.CardInfo"].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;
        ModelState["User.CardInfo"].Errors.Clear();
        ModelState["User.Password"].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;
        ModelState["User.Password"].Errors.Clear();
        ModelState["User.Username"].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;
        ModelState["User.Username"].Errors.Clear();
        ModelState["User.HelpQuestion"].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;
        ModelState["User.HelpQuestion"].Errors.Clear();
        #endregion
        
        ModelState["AvailableUsers"].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;
        ModelState["AvailableUsers"].Errors.Clear();
        
        
        if (ModelState.IsValid)
        {
            await context.Restaurants.AddAsync(restaurant);
            await context.SaveChangesAsync();
        
            return RedirectToAction("Index");
        }

        await restaurant.LoadAvailableDropdowns(context);
        return View(restaurant);
    }
    
    public IActionResult RemoveRestaurant(Restaurant restaurant)
    {
        return View(restaurant);
    }

    public async Task<ActionResult> EditRestaurant(Restaurant restaurant)
    {
        await restaurant.LoadAvailableDropdowns(context);
        return View(restaurant);
    }
    
    [HttpPost]
    public async Task<ActionResult> EditRestaurant(int id, Restaurant newRestaurant)
    {

        ModelState["AvailableUsers"].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;
        ModelState["AvailableUsers"].Errors.Clear();
        
        if (ModelState.IsValid)
        {
            var restaurant = await context.Restaurants.SingleAsync(x => x.Id == id);

            restaurant.Title = newRestaurant.Title;
            restaurant.Website = newRestaurant.Website;
            restaurant.PhoneNumber = newRestaurant.PhoneNumber;
            restaurant.WorkTime = newRestaurant.WorkTime;
            restaurant.MinimumOrderPrice = newRestaurant.MinimumOrderPrice;

            await context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        
        await newRestaurant.LoadAvailableDropdowns(context);
        return View(newRestaurant);
    }

    [HttpPost]
    public async Task<ActionResult> RemoveRestaurant(int id)
    {
        try
        {
            var restaurant = await context.Restaurants.SingleAsync(x => x.Id == id);
            var user = await context.User.SingleAsync(x => x.Id == restaurant.UserId);
            user.Role = "User";
            context.User.Update(user);
            context.Remove(restaurant);

            await context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        catch (Exception e)
        {
            ViewData["deletionNotPermitted"] = true;

            var restaurant = await context.Restaurants.SingleAsync(x => x.Id == id);
            // ReSharper disable once Mvc.ViewNotResolved
            return View(restaurant);
        }
    }

}