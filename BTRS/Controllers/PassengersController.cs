using BTRS.DATA;
using BTRS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BTRS.Controllers
{
    public class PassengersController : Controller
    {

        private SystemDbContext _context;

        public PassengersController(SystemDbContext context)
        {
            this._context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignUp(Passengers passengers)
        {
            bool empty = checkEmpty(passengers);
            bool duplicat = checkUsername(passengers.Name);
            bool duplicate = checkUsername(passengers.Username);


            if (empty)
            {
                if (duplicat)
                {
                    _context.passengers.Add(passengers);
                    _context.SaveChanges();

                    TempData["Msg"] = "the data was saved";
                    return View();
                }
                else
                {
                    TempData["Msg"] = "Please Change the username";
                    return View();
                }
            }
            else
            {
                TempData["Msg"] = "Please fill all input ";
                return View();
            }



        }

        private bool CheckEmpty(Passengers passengers)
        {
            throw new NotImplementedException();
        }

        private bool checkUsername(string username)
        {

            Passengers passengers = _context.passengers.Where(u => u.Username.Equals(username)).FirstOrDefault();
            if (passengers != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool checkEmpty(Passengers passengers)
        {
            if (String.IsNullOrEmpty(passengers.Name)) return false;
            else if (String.IsNullOrEmpty(passengers.Username)) return false;
            else if (String.IsNullOrEmpty(passengers.Password)) return false;
            else if (String.IsNullOrEmpty(passengers.Email)) return false;
            else return true;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(Login passengerslogin)
        {
            if (ModelState.IsValid)
            {
                string username = passengerslogin.Username;
                string password = passengerslogin.Password;

                Passengers passengers = _context.passengers.Where(
                     p => p.Username.Equals(username) &&
                     p.Password.Equals(password)
                     ).FirstOrDefault();

                Admin admin = _context.admin.Where(
                    a => a.Username.Equals(username)
                    &&
                    a.Password.Equals(password)
                    ).FirstOrDefault();




                if (passengers != null)
                {
                    HttpContext.Session.SetInt32("PassengersId", passengers.Id);
                    return RedirectToAction("TripsList");
                }
                else if (admin != null)
                {

                    HttpContext.Session.SetInt32("adminID", admin.Id);

                    return RedirectToAction("Index", "Trips");
                }
                else
                {
                    TempData["Msg"] = "The user Not Found";
                }


            }
            else
            {

            }
            return View();
        }
        public IActionResult TripsList()
        {
            int PassengersId = (int)HttpContext.Session.GetInt32("PassengersId");

            List<int> lst=_context.pass_trips.Where(
                t => t.passengers.Id == PassengersId
                ).Select(s => s.trips.Id).ToList(); ;


            List<Trips> lst_trips=_context.trips.Where(
                t => lst.Contains(t.Id) == false
                ).ToList(); ;

            return View(lst_trips);
        }

        public IActionResult Booking(int id)
        {
            int PassengersId = (int)HttpContext.Session.GetInt32("PassengersId");

            Pass_Trips pass_Trips = new Pass_Trips();
            pass_Trips.passengers = _context.passengers.Find(PassengersId);
            pass_Trips.trips = _context.trips.Find(id);

            _context.pass_trips.Add(pass_Trips);
            _context.SaveChanges();

            return RedirectToAction("BookingList");
        }
        public IActionResult BookingList()
        {
            int PassengersId = (int)HttpContext.Session.GetInt32("PassengersId");

            List<int> lst_passengers = _context.pass_trips.Where(
                t => t.passengers.Id == PassengersId).Select(s => s.trips.Id).ToList();

            List<Trips> lst_trips = _context.trips.Where(
                t => lst_passengers.Contains(t.Id)
                ).ToList();

            return View(lst_trips);
        }

       public IActionResult Delete(int tripid)
        {
            int PassengersId = (int)HttpContext.Session.GetInt32("PassengersId");

             Pass_Trips pass_trips = _context.pass_trips.Where(
                t => t.passengers.Id == PassengersId && t.trips.Id==tripid
                ).FirstOrDefault();

            _context.pass_trips.Remove(pass_trips);
            _context.SaveChanges();
            return RedirectToAction("BookingList");
        }

    }
}
