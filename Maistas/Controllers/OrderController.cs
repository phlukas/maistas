//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace Maistas.Controllers;

//public class OrderController : Controller
//{
//	private readonly FoodDbContext context;

//	public OrderController(FoodDbContext context)
//	{
//		this.context = context;
//	}

//	//public async Task<ActionResult> Index()
//	//{
//	//	var orders = await context.Orders.Include(x => x.Restaurant).Include(x => x.OrderedDish).ToListAsync();

//	//	return View(orders);
//	//}

//	public async Task<ActionResult> Create()
//	{
//		var order = new Order();
//		await order.LoadAvailableDropdowns(context);

//		return View(order);
//	}

//	[HttpPost]
//	public async Task<ActionResult> Create(Order order)
//	{

//		order.TotalCost = 0;
//		order.Distance = 0;
//		order.Status = "Nepateiktas";
//		order.Duration = "30:00";
//		order.DeliveryTime = DateTime.Now;
//		order.OrderTime = DateTime.Now;
//		order.Date = DateTime.Today;

//		//await context.Orders.AddAsync(order);
//		await context.SaveChangesAsync();

//		return RedirectToAction("Index");
//	}

//	public IActionResult AddDish()
//	{

//		return View();
//	}

//	public async Task<ActionResult> AddOrderedDish()
//	{
//		var orderDish = new OrderedDish();
//		await orderDish.LoadAvailableDropdowns(context);

//		return View(orderDish);
//	}

//	public async Task<ActionResult> Delete(int id)
//	{
//		//var order = await context.Orders.SingleAsync(x => x.Id == id);
//		return View(order);
//	}

//	[HttpPost]
//	public async Task<ActionResult> DeleteConfirm(int id)
//	{
//		try
//		{
//			var order = await context.Orders.SingleAsync(x => x.Id == id);
//			context.Remove(order);

//			await context.SaveChangesAsync();

//			return RedirectToAction("Index");
//		}
//		catch (Exception e)
//		{
//			ViewData["deletionNotPermitted"] = true;

//			//var order = await context.Orders.SingleAsync(x => x.Id == id);
//			//// ReSharper disable once Mvc.ViewNotResolved
//			//return View(order);
//		}
//	}
//}
