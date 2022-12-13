using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Data;

namespace Maistas.Controllers
{
    public class Users1Controller : Controller
    {
        private readonly FoodDbContext _context; 
        private readonly UserManager<MaistasUser> _userManager;

        public Users1Controller(FoodDbContext context, UserManager<MaistasUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        }

        [Authorize(Roles = "admin")]
        // GET: Users1
        public IActionResult Index()
        {
              return View();
        }

        //GET Users1/UserList
        public async Task<IActionResult> UserList()
        {
            var users = await _context.MaistasUser.ToListAsync();



            var query =
                from user in _context.MaistasUser
                join userRole in _context.UserRoles on user.Id equals userRole.UserId
                where (userRole.RoleId == 2)
                select user;
               
            var result = await query.ToListAsync();

           
/*
            
           var AvailableUsers = _context.MaistasUser.Select(x =>
            _userManager.IsInRoleAsync(x, "user").Result
     ).ToListAsync();*/

            return View(result);
            
        }
        //GET Users1/CourierList
        public async Task<IActionResult> CourierList()
        {
            //return View(await _context.MaistasUser.Select(u => u).Where(u => u.Role == "Courier").ToListAsync());
            return View(await _context.MaistasUser.ToListAsync());
        }


        // GET: Users1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MaistasUser == null)
            {
                return NotFound();
            }

            //var user = await _context.MaistasUser
                //.FirstOrDefaultAsync(m => m.Id == id);
            var user = await _context.MaistasUser.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users1/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Surname,Email,Username,Password,HelpQuestion,CardInfo,Address")] MaistasUser user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }
        // Users1/Login
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Username,Password")] MaistasUser user)
        {
           /* var test = await _context.MaistasUser.FirstAsync();

            if (test !=null)
            {
                if (user.Password == test.Password)
                {
                    //Session["UserID"] = test.Id;
                    

                    return RedirectToAction(nameof(Index));
                }
            }*/
            return NotFound();

        }

        // GET: Users1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MaistasUser == null)
            {
                return NotFound();
            }

            var user = await _context.MaistasUser.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Surname,Email,Username,Password,Role,HelpQuestion,CardInfo,Address")] MaistasUser user)
        {
           /* if (id != user.Id)
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
            }*/
            return View(user);
        }

        // GET: Users1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MaistasUser == null)
            {
                return NotFound();
            }

            //var user = await _context.MaistasUser.FirstOrDefaultAsync(m => m.Id == id);
            var user = await _context.MaistasUser
                .FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MaistasUser == null)
            {
                return Problem("Entity set 'FoodDbContext.User'  is null.");
            }
            var user = await _context.MaistasUser.FindAsync(id);
            if (user != null)
            {
                _context.MaistasUser.Remove(user);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
          //return _context.MaistasUser.Any(e => e.Id == id);
          return _context.MaistasUser.Any(e => e.Id == id);
        }
    }
}
