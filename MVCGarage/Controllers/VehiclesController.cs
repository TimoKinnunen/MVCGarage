using MVCGarage.DAL;
using MVCGarage.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace MVCGarage.Controllers
{
    public class VehiclesController : Controller
    {
        private MVCGarageDbContext db = new MVCGarageDbContext();

        // GET: Vehicles
        public ActionResult Index()
        {
            var vehiclesToView = db.Vehicles
                .OrderBy(v => v.StartParkingTime)
                .Select(v => new VehicleOverview
                {
                    Id = v.Id,
                    RegNo = v.RegistrationNumber,
                    TypeName = v.VehicleType.Type,
                    CheckInTime = v.StartParkingTime,
                    CheckOutTime = v.EndParkingTime,
                    IsInGarage = (v.EndParkingTime == null)
                });

            return View(vehiclesToView.ToList());
        }

        // GET: Vehicles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = db.Vehicles.Find(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            return View(vehicle);

        }

        // GET: Vehicles/Create
        public ActionResult Create()
        {
            ViewBag.VehicleTypeId = new SelectList(db.VehicleTypes, "Id", "Type");
            return View();
        }

        // POST: Vehicles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,RegistrationNumber,VehicleTypeId,Color,StartParkingTime,EndParkingTime,ParkingTime,ParkingCostPerHour,ParkingCost,NumberOfWheels,BrandAndModel")] Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                // Time when checked in
                vehicle.StartParkingTime = DateTime.Now;
                // 60 SEK/hour
                vehicle.ParkingCostPerHour = 60;
                db.Vehicles.Add(vehicle);
                try
                {
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException)
                {
                    // Probably a violation of unique index for column RegistrationNumber
                    ModelState.AddModelError("RegistrationNumber", "Could not save to database. Probably because a vehicle with this registration number is in the garage.");
                }
            }
            ViewBag.VehicleTypeId = new SelectList(db.VehicleTypes, "Id", "Type", vehicle.VehicleTypeId);
            return View(vehicle);
        }

        // GET: Vehicles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = db.Vehicles.Find(id);
            db.Vehicles.Include(v => v.VehicleType).First(v => v.Id == id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            ViewBag.VehicleTypeId = new SelectList(db.VehicleTypes, "Id", "Type", vehicle.VehicleTypeId);
            return View(vehicle);
        }

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,RegistrationNumber,VehicleTypeId,Color,StartParkingTime,EndParkingTime,ParkingTime,ParkingCostPerHour,ParkingCost,NumberOfWheels,BrandAndModel")] Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vehicle).State = EntityState.Modified;
                try
                {
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException)
                {
                    // Probably a violation of unique index for column RegistrationNumber
                    ModelState.AddModelError("RegistrationNumber", "Could not save to database. Probably because a vehicle with this registration number is in the garage.");
                }
            }
            ViewBag.VehicleTypeId = new SelectList(db.VehicleTypes, "Id", "Type", vehicle.VehicleTypeId);
            return View(vehicle);
        }

        // GET: Vehicles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = db.Vehicles.Find(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            return View(vehicle);

        }

        // POST: VehicleTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Vehicle vehicle = db.Vehicles.Find(id);
            //db.Vehicles.Remove(vehicle);

            vehicle.EndParkingTime = DateTime.Now;
            vehicle.ParkingTime = vehicle.EndParkingTime - vehicle.StartParkingTime;
            vehicle.ParkingCost = vehicle.ParkingCostPerHour * (int)((TimeSpan)vehicle.ParkingTime).TotalMinutes / 60;
            db.Entry(vehicle).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Vehicles/Kvitto/5
        public ActionResult Kvitto(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = db.Vehicles.Find(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            return View(vehicle);

        }

        // GET: Vehicles/Search
        public ActionResult Search(string searchBy, string Search)
        {
            IQueryable<VehicleOverview> vehiclesToView = null;
            if (string.IsNullOrEmpty(searchBy) || string.IsNullOrEmpty(Search))
            {
                vehiclesToView = db.Vehicles
                               .OrderBy(v => v.StartParkingTime)
                               .Select(v => new VehicleOverview
                               {
                                   Id = v.Id,
                                   RegNo = v.RegistrationNumber,
                                   TypeName = v.VehicleType.Type,
                                   CheckInTime = v.StartParkingTime,
                                   CheckOutTime = v.EndParkingTime,
                                   IsInGarage = (v.EndParkingTime == null)
                               });
                ViewBag.ErrorMessage = "Select radio button 'Registration number' or 'Vehicle type' and a search string. Try again.";
            }

            if (searchBy == "Registration number")
            {
                vehiclesToView = db.Vehicles
                               .Where(v => v.RegistrationNumber.Contains(Search))
                               .OrderBy(v => v.StartParkingTime)
                               .Select(v => new VehicleOverview
                               {
                                   Id = v.Id,
                                   RegNo = v.RegistrationNumber,
                                   TypeName = v.VehicleType.Type,
                                   CheckInTime = v.StartParkingTime,
                                   CheckOutTime = v.EndParkingTime,
                                   IsInGarage = (v.EndParkingTime == null)
                               });
            }

            if (searchBy == "Vehicle type")
            {
                vehiclesToView = db.Vehicles
                               .Where(v => v.VehicleType.Type.Contains(Search))
                               .OrderBy(v => v.StartParkingTime)
                               .Select(v => new VehicleOverview
                               {
                                   Id = v.Id,
                                   RegNo = v.RegistrationNumber,
                                   TypeName = v.VehicleType.Type,
                                   CheckInTime = v.StartParkingTime,
                                   CheckOutTime = v.EndParkingTime,
                                   IsInGarage = (v.EndParkingTime == null)
                               });
            }
            if (vehiclesToView.Count() == 0)
            {
                ViewBag.SearchMessage = "No vehicle(s) found. Try again.";
            }
            else
            {
                ViewBag.SearchMessage = vehiclesToView.Count() + " vehicle(s) found.";
            }
            return View(vehiclesToView.ToList());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
