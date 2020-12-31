using System.Collections.Generic;

namespace GarageLogic
{
    public enum eVehicleType
    {
        FuelCar = 1,
        ElectricCar,
        FuelMotorcycle,
        ElectricMotorcycle,
        Truck
    }
    
    public static class VehicleFactory
    {
        public static Vehicle BuildVehicle(Dictionary<string, string> i_VehicleParameters, Vehicle i_NewVehicleToAdd)
        {
            i_NewVehicleToAdd.BuildVehicle(i_VehicleParameters);
            i_NewVehicleToAdd.SetEnergyMeter();
            return i_NewVehicleToAdd;
        }

        public static Dictionary<string, string> InitializeVehicleProperties(int i_ChosenVehicleType, out Vehicle io_Vehicle)
        {
            Dictionary<string, string> vehicleProperties = new Dictionary<string, string>();
            io_Vehicle = null;
            switch (i_ChosenVehicleType)
            {
                case (int)eVehicleType.FuelCar:
                    io_Vehicle = new Car(eEngineType.Fuel);
                    break;
                case (int)eVehicleType.ElectricCar:
                    io_Vehicle = new Car(eEngineType.Electric);
                    break;
                case (int)eVehicleType.FuelMotorcycle:
                    io_Vehicle = new Motorcycle(eEngineType.Fuel);
                    break;
                case (int)eVehicleType.ElectricMotorcycle:
                    io_Vehicle = new Motorcycle(eEngineType.Electric);
                    break;
                case (int)eVehicleType.Truck:
                    io_Vehicle = new Truck();
                    break;
            }

            io_Vehicle.AddProperties();

            foreach (string property in io_Vehicle.Properties)
            {
                vehicleProperties.Add(property, string.Empty);
            }

            return vehicleProperties;
        }
    }
}
