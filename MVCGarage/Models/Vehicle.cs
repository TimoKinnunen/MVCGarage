using System;
using System.ComponentModel.DataAnnotations;

namespace MVCGarage.Models
{
    public class Vehicle
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Registration number")]
        public string RegistrationNumber { get; set; }

        [Display(Name = "Vehicle type")]
        public VehicleType VehicleType { get; set; } //personbil, lastbil

        public string Color { get; set; }

        [Display(Name = "Checked in")]
        public DateTime? StartParkingTime { get; set; }

        [Display(Name = "Checked out")]
        public DateTime? EndParkingTime { get; set; }

        [Display(Name = "Parking time")]
        public DateTimeOffset? ParkingTime { get; set; }

        [Display(Name = "Number of wheels")]

        public int? NumberOfWheels { get; set; }
        [Display(Name = "Brand and model")]
        public string BrandAndModel { get; set; } //Saab 96,Volvo V70
    }
}