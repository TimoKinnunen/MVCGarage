using MVCGarage.DAL;
using MVCGarage.Models;
using System;
using System.Data.Entity;
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
            var vehiclesToView = from v in db.Vehicles
                                 select new VehicleOverview
                                 {
                                     Id = v.Id,
                                     RegNo = v.RegistrationNumber,
                                     TypeName = v.VehicleType.Type,
                                     CheckInTime = v.StartParkingTime,
                                     CheckOutTime = v.EndParkingTime,
                                     IsInGarage = (v.EndParkingTime == null)
                                 };
            vehiclesToView = vehiclesToView.OrderBy(v => v.CheckInTime);
            //var vehiclesToView = db.Vehicles;
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
            return View();
        }

        // POST: Vehicles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,RegistrationNumber,Color,StartParkingTime,EndParkingTime,ParkingTime,ParkingCostPerHour,ParkingCost,NumberOfWheels,BrandAndModel")] Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                // Find which vehicle type was selected in form
                int vtId = 0;
                int.TryParse(Request["VehicleType"], out vtId);
                vehicle.VehicleType = db.VehicleTypes.FirstOrDefault(vt => vt.Id == vtId);
                // Time when checked in
                vehicle.StartParkingTime = DateTime.Now.AddHours(-1);
                // 60 SEK/hour
                vehicle.ParkingCostPerHour = 60;
                db.Vehicles.Add(vehicle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

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
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            return View(vehicle);
        }

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,RegistrationNumber,Color,StartParkingTime,EndParkingTime,ParkingTime,ParkingCostPerHour,ParkingCost,NumberOfWheels,BrandAndModel")] Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vehicle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
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
