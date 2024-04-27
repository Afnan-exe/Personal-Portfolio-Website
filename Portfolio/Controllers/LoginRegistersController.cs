using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Portfolio.Models;

namespace Portfolio.Controllers
{
    public class LoginRegistersController : Controller
    {
        private readonly _context _context;

        public LoginRegistersController(_context context)
        {
            _context = context;
        }

        // GET: LoginRegisters
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("i") == null || HttpContext.Session.GetString("i") != "afnan@gmail.com")
            {
                return RedirectToAction("Dashboard", "Home");
            }

            return View(await _context.LoginRegister.ToListAsync());

        }

        // GET: LoginRegisters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetString("i") == null || HttpContext.Session.GetString("i") != "afnan@gmail.com")
            {
                return RedirectToAction("Dashboard", "Home");
            }

            if (id == null)
            {
                return NotFound();
            }

            var loginRegister = await _context.LoginRegister
                .FirstOrDefaultAsync(m => m.id == id);
            if (loginRegister == null)
            {
                return NotFound();
            }

            return View(loginRegister);
        }

        // GET: LoginRegisters/Create
        public IActionResult Create()
        {

			if (HttpContext.Session.GetString("i") == null || HttpContext.Session.GetString("i") != "afnan@gmail.com")
			{
				return RedirectToAction("Dashboard", "Home");
			}
			return View();
        }

        // POST: LoginRegisters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,UserName,Password,PasswordConfirmed,Email")] LoginRegister loginRegister)
        {
            if (ModelState.IsValid)
            {
                var x = _context.LoginRegister.FirstOrDefault(d=>d.Email==loginRegister.Email);

                if (x == null)
                {
                    if (loginRegister.Password == loginRegister.PasswordConfirmed)
                    {
                        _context.Add(loginRegister);
                        await _context.SaveChangesAsync();
                        return RedirectToAction("Index","Home");
                    }
                    else
                    {
                        ViewBag.c = "Password Not Confirmed";
				        return View();
                    }
                }
                else
                {
					ViewBag.c = "Already Have an Account using this Email";
					return View();
				}
            }
            return View(loginRegister);
        }

        // GET: LoginRegisters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetString("i") == null || HttpContext.Session.GetString("i") != "afnan@gmail.com")
            {
                return RedirectToAction("Dashboard", "Home");
            }

            if (id == null)
            {
                return NotFound();
            }

            var loginRegister = await _context.LoginRegister.FindAsync(id);
            if (loginRegister == null)
            {
                return NotFound();
            }
            return View(loginRegister);
        }

        // POST: LoginRegisters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,UserName,Password,PasswordConfirmed,Email")] LoginRegister loginRegister)
        {
            if (id != loginRegister.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loginRegister);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoginRegisterExists(loginRegister.id))
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
            return View(loginRegister);
        }

        // GET: LoginRegisters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetString("i") == null || HttpContext.Session.GetString("i") != "afnan@gmail.com")
            {
                return RedirectToAction("Dashboard", "Home");
            }

            if (id == null)
            {
                return NotFound();
            }

            var loginRegister = await _context.LoginRegister
                .FirstOrDefaultAsync(m => m.id == id);
            if (loginRegister == null)
            {
                return NotFound();
            }

            return View(loginRegister);
        }

        // POST: LoginRegisters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var loginRegister = await _context.LoginRegister.FindAsync(id);
            if (loginRegister != null)
            {
                _context.LoginRegister.Remove(loginRegister);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
		public async Task<IActionResult> ULogin()
		{
            if (HttpContext.Session.GetString("i") != null) { return RedirectToAction("Index", "Home"); }
            return  View();
		}

		[HttpPost]
        public async Task<IActionResult> ULogin(LoginRegister c)
        {
            var a =  _context.LoginRegister.Where(v => v.Email == c.Email && v.Password == c.Password);
            //var a = from v in _context.LoginRegister where v.Email.Equals(c.Email) && v.Password==c.Password select v;
            if (a.Any())
            {
				HttpContext.Session.SetString("i", c.Email);
				return RedirectToAction("Dashboard", "Home");
			}
			else
			{
				ViewBag.m = "Invalid Credentials";
				return View();
			}

		}
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");

        }
  //      public IActionResult RedirectO()
  //      {

		//	if (HttpContext.Session.GetString("i") == null)
		//	{
		//		return RedirectToAction("Index", "Home");
		//	}
  //          return View();
		//}


        private bool LoginRegisterExists(int id)
        {
            return _context.LoginRegister.Any(e => e.id == id);
        }
    }
}
