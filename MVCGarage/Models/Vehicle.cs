using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCGarage.Models
{
    public class Vehicle
    {
        [Key]
        public int Id { get; set; }

        [Required, Index("IX_RegNoAndCheckOut", 1, IsUnique = true)]
        [StringLength(100)]
        [Display(Name = "Registration number")]
        public string RegistrationNumber { get; set; }

        public int VehicleTypeId { get; set; }

        [Display(Name = "Vehicle type")]
        virtual public VehicleType VehicleType { get; set; } //personbil, lastbil

        public string Color { get; set; }

        [DisplayFormat(DataFormatString = "{0:g}")] //g Default date & time 10/12/2002 10:11 PM
        [Display(Name = "Checked in")]
        public DateTime StartParkingTime { get; set; }

        [Index("IX_RegNoAndCheckOut", 2, IsUnique = true)]
        [DisplayFormat(NullDisplayText = "Not checked out yet", DataFormatString = "{0:g}")] //g Default date & time 10/12/2002 10:11 PM
        [Display(Name = "Checked out")]
        public DateTime? EndParkingTime { get; set; }

        [DisplayFormat(NullDisplayText = "Not checked out yet", DataFormatString = "{0:hh\\:mm}")]
        [Display(Name = "Parking time hh:mm")]
        public TimeSpan? ParkingTime { get; set; }

        [DisplayFormat(DataFormatString = "{0:g}")] //g Default date & time 10/12/2002 10:11 PM
        [Display(Name = "Parking cost per hour")]
        public int? ParkingCostPerHour { get; set; }

        [DisplayFormat(NullDisplayText = "Not checked out yet", DataFormatString = "{0:c}")]
        [Display(Name = "Parking cost")]
        public int? ParkingCost { get; set; }

        [Range(1, 10, ErrorMessage = "Value for number of wheels must be between 1 and 10.")]
        [Display(Name = "Number of wheels")]
        public int? NumberOfWheels { get; set; }

        [Display(Name = "Brand and model")]
        public string BrandAndModel { get; set; } //Saab 96,Volvo V70
    }
}