using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCDemo.Models;

namespace MVCDemo.Controllers
{
    public class UserController : Controller
    {
        private readonly DELETEMEContext _context;

        public UserController(DELETEMEContext context)
        {
            _context = context;
            
        }

        // GET: User
        public async Task<IActionResult> Index()
        {
            var dELETEMEContext = _context.User.Include(u => u.Role);
            return View(await dELETEMEContext.ToListAsync());
        }

        // GET: User/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .Include(u => u.Role)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: User/Create
        public IActionResult Create()
        {
            ViewData["RoleID"] = new SelectList(SuperDAL.GetRoles(), "ID", "Name");
            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserName,Password,ConfirmPassword,Salt,RoleID")] User user)
        {
            if (ModelState.IsValid)
            {
                //_context.Add(user);
                //await _context.SaveChangesAsync();

                string oriPass = user.Password;
                string newSalt = Hasher.GenerateSalt();
                user.Password = Hasher.HashIt(oriPass, newSalt);
                user.dbAdd();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoleID"] = new SelectList(SuperDAL.GetRoles(), "ID", "Name",user.RoleID);
            return View(user);
        }

        // GET: User/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            ViewData["RoleID"] = new SelectList(SuperDAL.GetRoles(), "ID", "Name", user.RoleID);
            return View(user);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserName,Password,ConfirmPassword,Salt,RoleID,ID")] User user)
        {
            bool passwordChanged = false; // password changes, force a log out. 
            if (id != user.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // get new password
                    string newPass = user.Password;
                    // get user to see if password changed.
                    User userToCheck = SuperDAL.GetUser(user.ID);
                    // get hash for previous salt
                    string oldSaltHash = Hasher.HashIt(newPass, userToCheck.Salt);
                    // check if password changed.
                    if(oldSaltHash != userToCheck.Password) {
                        // password changed; update to a new password and salt.
                        string newSalt = Hasher.GenerateSalt();
                        user.Password = Hasher.HashIt(newPass, newSalt);
                        passwordChanged = true;
                    }
                    user.dbUpdate();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                if (passwordChanged) {
                    return RedirectToAction("Logout");
                } else {
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewData["RoleID"] = new SelectList(SuperDAL.GetRoles(), "ID", "Name", user.RoleID);
            return View(user);
        }

        // GET: User/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .Include(u => u.Role)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.User.FindAsync(id);
            _context.User.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.ID == id);
        }
        /// <summary>
        /// Handle Login request from user.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(string username, string password) {
            Models.User usr = SuperDAL.GetUser(username, password);
            //Messages mess = new Messages();
            if (usr != null) {
                // success
                Response.Cookies.Append("user", SuperDAL.GetCookie(usr));
                //HttpContext.Session.SetString("user",JsonConvert.SerializeObject(usr));
                SessionHelper.Set(HttpContext.Session, "user", usr);
                TempData["SuccessMessage"] = "Successful Login (TempData)";
                // use Temp Data because the next two will not live through a redirect.
                //ViewData["SuccessMessage"] = "Successful Login (ViewData)"; 
                //ViewBag.SuccessMessage = "Successful Login (ViewBag)";
            } else {
                // failed
                TempData["ErrorMessage"] = "Login Failed (TempData)";
                // use Temp Data because the next two will not live through a redirect.
                //ViewData["ErrorMessage"] = "Login Failed (ViewData)";
                //ViewBag.ErrorMessage = "Login Failed (ViewBag)";

            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout() {
            Response.Cookies.Delete("user");
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register() {

            //return View("Create",new { });
            ViewBag.Action = "Register";
            return RedirectToAction("Create");
        }

    }
}
