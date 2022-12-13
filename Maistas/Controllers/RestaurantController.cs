using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Mail;


namespace Maistas.Controllers;

public class RestaurantController : Controller
{
    private readonly FoodDbContext context;
    private readonly UserManager<MaistasUser> _userManager;

    public RestaurantController(FoodDbContext context, UserManager<MaistasUser> userManager)
    {
        this.context = context;
        _userManager = userManager;
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
        await dishListView.LoadAvailableDropdowns(context);
        dishListView.Dishes = dishes;
        dishListView.Restaurant = restaurant;
        
        return View(dishListView);
    }
    
    [HttpPost]
    public async Task<ActionResult> DishesList(DishListView dishListView)
    {
        var filteredDishes = await context.Dishes
            .ToListAsync();
        await dishListView.LoadAvailableDropdowns(context);

        if (dishListView.CategoryId != null && dishListView.CategoryId != 0)
        {
            filteredDishes = filteredDishes
                .Where(d => d.CategoryId == dishListView.CategoryId)
                .ToList();
        }

        if (dishListView.PriceFrom != null)
        {
            filteredDishes = filteredDishes
                .Where(d => d.Price >= dishListView.PriceFrom)
                .ToList();
        }
        
        if (dishListView.PriceTo != null)
        {
            filteredDishes = filteredDishes
                .Where(d => d.Price <= dishListView.PriceTo)
                .ToList();
        }
        
        if (dishListView.CaloriesFrom != null)
        {
            filteredDishes = filteredDishes
                .Where(d => d.Calories >= dishListView.CaloriesFrom)
                .ToList();
        }
        
        if (dishListView.CaloriesTo != null)
        {
            filteredDishes = filteredDishes
                .Where(d => d.Calories <= dishListView.CaloriesTo)
                .ToList();
        }
        
        if (dishListView.WeightFrom != null)
        {
            filteredDishes = filteredDishes
                .Where(d => d.WeightInGrams >= dishListView.WeightFrom)
                .ToList();
        }
        
        if (dishListView.WeightTo != null)
        {
            filteredDishes = filteredDishes
                .Where(d => d.WeightInGrams <= dishListView.WeightTo)
                .ToList();
        }
        
        if (dishListView.IsVegetarian != null)
        {
            filteredDishes = filteredDishes
                .Where(d => d.Vegan == dishListView.IsVegetarian)
                .ToList();
        }
        
        dishListView.Dishes = filteredDishes;
        
        return View(dishListView);
    }
    
    public async Task<ActionResult> AddRestaurant()
    {
        var restaurant = new Restaurant();
        await restaurant.LoadAvailableDropdowns(context, _userManager);
        
        return View(restaurant);
    }
    
    [HttpPost]
    public async Task<ActionResult> AddRestaurant(Restaurant restaurant)
    {
        if (restaurant.UserId == 0)
        {
            await restaurant.LoadAvailableDropdowns(context, _userManager);
            return View(restaurant);
        }
        var user = await context.MaistasUser.Select(u=>u).Where(u => u.Id == restaurant.UserId).FirstOrDefaultAsync();
        _userManager.RemoveFromRoleAsync(user, "user").Wait();
        _userManager.AddToRoleAsync(user, "restaurant").Wait();
        restaurant.User = user;
        
        #region User input validation
        ModelState["User.Name"].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;
        ModelState["User.Name"].Errors.Clear();
        ModelState["User.Surname"].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;
        ModelState["User.Surname"].Errors.Clear();
        ModelState["User.CardInfo"].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;
        ModelState["User.CardInfo"].Errors.Clear();
        ModelState["User.Address"].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;
        ModelState["User.Address"].Errors.Clear();
        #endregion

        ModelState["AvailableUsers"].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;
        ModelState["AvailableUsers"].Errors.Clear();
        
        
        if (ModelState.IsValid)
        {
            await context.Restaurants.AddAsync(restaurant);
            await context.SaveChangesAsync();
        
            return RedirectToAction("Index");
        }

        await restaurant.LoadAvailableDropdowns(context, _userManager);
        return View(restaurant);
    }
    
    public IActionResult RemoveRestaurant(Restaurant restaurant)
    {
        return View(restaurant);
    }

    public async Task<ActionResult> EditRestaurant(Restaurant restaurant)
    {
        await restaurant.LoadAvailableDropdowns(context, _userManager);
        return View(restaurant);
    }
    
    [HttpPost]
    public async Task<ActionResult> EditRestaurant(int id, Restaurant newRestaurant)
    {
        ModelState["Title"].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;
        ModelState["Title"].Errors.Clear();
        ModelState["Website"].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;
        ModelState["Website"].Errors.Clear();
        ModelState["PhoneNumber"].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;
        ModelState["PhoneNumber"].Errors.Clear();
        ModelState["AvailableUsers"].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;
        ModelState["AvailableUsers"].Errors.Clear();
        ModelState["WorkTime"].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;
        ModelState["WorkTime"].Errors.Clear();
        ModelState["User"].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;
        ModelState["User"].Errors.Clear();

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
        
        await newRestaurant.LoadAvailableDropdowns(context, _userManager);
        return View(newRestaurant);
    }

    [HttpPost]
    public async Task<ActionResult> RemoveRestaurant(int id)
    {
        try
        {
            var restaurant = await context.Restaurants.SingleAsync(x => x.Id == id);
            var user = await context.MaistasUser.SingleAsync(x => x.Id == restaurant.UserId);
            var result = await _userManager.AddToRoleAsync(user, "restaurant");
            context.MaistasUser.Update(user);
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