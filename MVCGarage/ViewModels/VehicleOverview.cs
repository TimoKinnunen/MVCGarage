using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCGarage.Models
{
    public class VehicleOverview
    {

        public int Id { get; set; }

        [Display(Name = "Registration number")]
        public string RegNo { get; set; }

        [Display(Name = "Type of vehicle")]
        public string TypeName { get; set; }

        [DisplayFormat(DataFormatString = "{0:g}")] //g Default date & time 10/12/2002 10:11 PM
        [Display(Name = "Checked in")]
        public DateTime? CheckInTime { get; set; }

        [DisplayFormat(DataFormatString = "{0:g}")] //g Default date & time 10/12/2002 10:11 PM
        [Display(Name = "Checked out")]
        public DateTime? CheckOutTime { get; set; }

        [Display(Name = "Is still in garage")]
        public bool IsInGarage { get; set; }
    }
}