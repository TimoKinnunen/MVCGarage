namespace MVCGarage.Models
{
    public class VehicleType
    {
        public string Type { get; set; }

        //constructor
        public VehicleType(string type)
        {
            Type = type;
        }

        private string type; // backing field for Type
    }
}