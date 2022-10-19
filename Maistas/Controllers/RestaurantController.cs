using Maistas.Models;
using Microsoft.AspNetCore.Mvc;


namespace Maistas.Controllers;

public class RestaurantController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult RestaurantList()
    {
        return View();
    }
    
    public IActionResult DishesList()
    {
        return View();
    }
    
    public IActionResult AddRestaurant()
    {
        return View();
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