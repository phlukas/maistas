using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Maistas.Controllers
{
    public class UsersController : Controller
    {
        private readonly FoodDbContext _context;


        public UsersController(FoodDbContext context)
        {
            _context = context;
        }
        public List<User> users = new();
        User user2 = new User
        {
            Id = 1,
            Name = "Tomas",
            Surname = "Tomauskas",
            Username = "Tomelis",
            Email = "Tomelis@gmail.com",
            Password = "Slaptas",
            Role = "Klientas",
            HelpQuestion = "Klausimelis",
            CardInfo = "VISA",
            Address = "Vilnius"
        };
        User user1 = new User
        {
            Id = 0,
            Name = "Petras",
            Surname = "Petrauskas",
            Username = "Petrelis",
            Email = "petrelis@gmail.com",
            Password = "Slaptas",
            Role = "Kurjeris",
            HelpQuestion = "Klausimelis",
            CardInfo = "VISA",
            Address = "Kaunas"
        };

        // GET: Users
        public async Task<IActionResult> Index()
        {
            //_context.User.Add(user1);
           //_context.User.Add(user);
            
            users.Add(user1);
            users.Add(user2);

            return View(users);
        }

        // GET: Users/Details/5

        //public async Task<IActionResult> Details(int? id)
        public async Task<IActionResult> Details(int id)
        {
            users.Add(user1);
            users.Add(user2);
            //if (id == null || _context.User == null)
            if (id == -1 || users[id] == null || users == null)
            {
                return NotFound();
            }

           /* var user = await _context.User
                .FirstOrDefaultAsync(m => m.Id == id); 
            if (user == null)
            {
                return NotFound();
            }*/

            return View(users[id]);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }
        // GET: Users/Login
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Username,Password")] User user)
        {
            if (ModelState.IsValid)
            {
                //_context.Add(user);
                //await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //return View(user);
            
            return RedirectPermanent("~/Home");
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Surname,Email,Username,Password,Role,HelpQuestion,CardInfo,Address")] User user)
        {
            if (ModelState.IsValid)
            {
                //_context.Add(user);
                //await _context.SaveChangesAsync();
                return RedirectPermanent("~/Home");
            }
            return View(user);
        }

        // GET: Users/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        public async Task<IActionResult> Edit(int id)
        {
            users.Add(user1);
            users.Add(user2);
            Console.WriteLine(users);
            if (id == -1 || users[id] == null || users == null)
            {
                return NotFound();
            }

            var user = users[id];
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Surname,Email,Username,Password,Role,HelpQuestion,CardInfo,Address")] User user)
        {
            users.Add(user1);
            users.Add(user2);
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            users.Add(user1);
            users.Add(user2);
            if (id == null || _context.User == null)
            {
                return NotFound();
            }
            
            var user = await _context.User
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            users.Add(user1);
            users.Add(user2);
            if (_context.User == null)
            {
                return Problem("Entity set 'FoodDbContext.User'  is null.");
            }
            var user = await _context.User.FindAsync(id);
            if (user != null)
            {
                _context.User.Remove(user);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            users.Add(user1);
            users.Add(user2);
            //return _context.User.Any(e => e.Id == id);
            if (users[id] != null)
                return true;
            else
                return false;
        }
    }
}
