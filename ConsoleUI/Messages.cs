namespace ConsoleUI
{
    public static class Messages
    {
        internal static readonly string sr_AskForVehicleType = "Please enter the required number for the vehicle type:";
        internal static readonly string sr_OpeningMessage = @"
Welcome to our garage! Please enter the number of the required service:
1 - Insert a new vehicle to the garage
2 - Display a list of license numbers currently in the garage
3 - Change vehicle's repair status
4 - Infalte vehicle's wheels to the maximum
5 - Refuel a fuel-based vehicle
6 - Charge an electric-based vehicle
7 - Display details on a vehicle by license plate
To exit, enter 0.";

        internal static readonly string sr_GoodbyeMsg = "Thank you for visiting our garage. See you next time!";

        // Ask for input
        internal static readonly string sr_AskForName = "Please enter your name:";
        internal static readonly string sr_AskForPhoneNumber = "Please enter your phone number:";
        internal static readonly string sr_AskForLicensePlateFilter = "Please choose the number of the required status. To view all, enter any other number";
        internal static readonly string sr_AskForLicensePlate = "Please enter a license plate:";
        internal static readonly string sr_AskForRepairStatus = "Please choose the number of the required status:";
        internal static readonly string sr_AskForAmountOfFuelToAdd = "Please enter amount of fuel to add in liters:";
        internal static readonly string sr_AskForNumOfMinutesToAdd = "Please enter the number of minutes to charge";
        internal static readonly string sr_AskForFuelTypeToAdd = "Please enter a fuel type:";

        // Status messages
        internal static readonly string sr_VehicleAlreadyInGarage = "The vehicle is already in the garage. Vehicle's status has changed to \"In progress\"";
        internal static readonly string sr_VehicleAddedToGarage = "The vehicle has been added to the garage successfully.";
        internal static readonly string sr_StatusChanged = "Vehicle's status changed successfully to {0}";
        internal static readonly string sr_WheelsInflated = "The vehicle's wheels are infalted!";
        internal static readonly string sr_VehicleIsCharged = "The vehicle was charged successfully!";
        internal static readonly string sr_VehicleIsFueled = "The vehicle was fueled successfully!";

        // Invalid messages
        internal static readonly string sr_InvalidName = "The name you entered is invalid. Please enter a name with letters only.";
        internal static readonly string sr_InvalidChosenStatus = "The status you chosen is invalid. Please enter a valid status.";
        internal static readonly string sr_InvalidVehicleNotInGarage = "The license plate you entered is not associated with any vehicle in the garage.\nPlease enter a license plate of a vehicle in the garage.";
        internal static readonly string sr_InvalidLicensePlate = "The license plate you entered is invalid. Plase enter a valid license plate.";
        internal static readonly string sr_InvalidAmountOfEnergy = "The amount of energy entered is invalid. The maximum is {0}. Please enter a valid amount of energy, up to {1}";
        internal static readonly string sr_InvalidFuelType = "The fuel type entered {0} is not compatible to the vehicle. Please enter the right fuel type {1}.";
        internal static readonly string sr_InvalidOptionChosen = "The option you chose is not valid. Please choose a valid option number.";
        internal static readonly string sr_InvalidArgument = "The argument you entered is invalid. Please enter again.";
        internal static readonly string sr_InvalidPhoneNumber = "The number you entered is invalid, it should be only digits and in length 10.";
        internal static readonly string sr_InvalidVehicleTypeToCharge = "The vehicle you chose has fuel based engine.";
        internal static readonly string sr_InvalidVehicleTypeToFuel = "The vehicle you chose has electric based engine.";
        internal static readonly string sr_InvalidGarageIsEmpty = "The garage is empty. Please add a vehicle before choosing this operation again.";
    }
}
