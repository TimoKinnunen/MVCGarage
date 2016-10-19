using MVCGarage.DAL;
using MVCGarage.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MVCGarage.Services
{
    public static class VehicleTypeService
    {
        private static MVCGarageDbContext db = new MVCGarageDbContext();

        public static IEnumerable<VehicleType> List()
        {
            return db.VehicleTypes.ToList().OrderBy(vt => vt.Type);
        }

        public static IEnumerable<SelectListItem> SelectList(int selectedId)
        {
            var items = db.VehicleTypes
                .OrderBy(vt => vt.Type)
                .Select(vt => new SelectListItem
                {
                    Value = vt.Id.ToString(),
                    Text = vt.Type,
                    Selected = vt.Id.Equals(selectedId)
                });
            return items;
        }
    }
}