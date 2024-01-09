using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BTRS.DATA;
using BTRS.Models;

namespace BTRS.Controllers
{
    public class TripsController : Controller
    {
        private readonly SystemDbContext _context;

        public TripsController(SystemDbContext context)
        {
            _context = context;
        }

        // GET: Trips
        public ActionResult Index()
        {
            return View(_context.trips.ToList());
        }

        // GET: TripsContoller/Details/5
        public ActionResult Details(int id)
        {
            Trips trips = _context.trips.Find(id);

            return View(trips);
        }

        // GET: TripsContoller/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TripsContoller/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Trips trips)
        {
            try
            {

                int adminid = (int)HttpContext.Session.GetInt32("adminID");

                Admin admin = _context.admin.Where(
                  a => a.Id == adminid
                  ).FirstOrDefault();

                trips.Admin = admin;

                _context.trips.Add(trips);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TripsContoller/Edit/5
        public ActionResult Edit(int id)
        {
            Trips trips = _context.trips.Find(id);

            return View(trips);
        }

        // POST: TripsContoller/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Trips trips)
        {
            try
            {

                _context.trips.Update(trips);

                _context.SaveChanges();

                return RedirectToAction(nameof(Index));

            }
            catch
            {
                return View();
            }
        }

        // GET: TripsContoller/Delete/5
        public ActionResult Delete(int id)
        {
            Trips trips = _context.trips.Find(id);
            _context.trips.Remove(trips);
            _context.SaveChanges();

            return RedirectToAction("index");
        }

        // POST: TripsContoller/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Trips trips)
        {
            try
            {
                _context.trips.Remove(trips);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}