using System;
using System.Linq;
using System.Collections.Generic;
using GarageLogic;

namespace ConsoleUI
{
    public class UserInterface
    {
        private Garage garage;
        private const int k_NumberOfServicesInGarage = 7;
        private const int k_Hour = 60;

        internal void GarageManager()
        {
            garage = new Garage();
            bool keepWorking = true;

            while (keepWorking)
            {
                int chosenOptionAsInt = 0;

                Console.WriteLine(Messages.sr_OpeningMessage);
                string chosenOption = Console.ReadLine();
                Utils.ValidateChosenOption(chosenOption, out chosenOptionAsInt, 0, k_NumberOfServicesInGarage);
                switch (chosenOptionAsInt)
                {
                    case 1:
                        AddNewVehicleToGarage();
                        break;
                    case 2:
                        DisplayLicenseNumbers();
                        break;
                    case 3:
                        ChangeVehicleStatus();
                        break;
                    case 4:
                        InflateVehicleWheels();
                        break;
                    case 5:
                        ReFuelVehicle();
                        break;
                    case 6:
                        ChargeElectricVehicle();
                        break;
                    case 7:
                        DisplayVehicleDetails();
                        break;
                    default:
                        Console.WriteLine(Messages.sr_GoodbyeMsg);
                        keepWorking = false;
                        break;
                }
            }
        }

        private void AddNewVehicleToGarage()
        {
            int chosenVehicle = 0;

            Console.WriteLine(Messages.sr_AskForName);
            string clientName = Console.ReadLine();
            Utils.ValidateClientName(ref clientName);
            Console.WriteLine(Messages.sr_AskForPhoneNumber);
            string phoneNumber = Console.ReadLine();
            Utils.ValidateClientPhoneNumber(ref phoneNumber);
            Console.WriteLine(Messages.sr_AskForVehicleType);

            foreach (eVehicleType vehicleType in Enum.GetValues(typeof(eVehicleType)))
            {
                Console.WriteLine((int)vehicleType + ". " + Utils.SplitCamelCase(vehicleType.ToString()));
            }

            string chosenVehicleType = Console.ReadLine();
            int numOfVehicleTypes = Enum.GetValues(typeof(eVehicleType)).Length;
            Utils.ValidateChosenOption(chosenVehicleType, out chosenVehicle, 1, numOfVehicleTypes);
            Vehicle newVehicleToAdd;
            Dictionary<string, string> vehicleSpecificParameters = VehicleFactory.InitializeVehicleProperties(chosenVehicle, out newVehicleToAdd);
            AskAndSetVehicleSpecificParameters(vehicleSpecificParameters, newVehicleToAdd);
            AskAndSetEngineSpecificParameters(newVehicleToAdd.Engine);
            AskAndSetWheelSpecificParameters(newVehicleToAdd.Wheels[0]);

            bool isVehicleExistsInGarage = garage.BuildAndAddVehicleToGarage(clientName, phoneNumber, vehicleSpecificParameters, newVehicleToAdd);

            if (isVehicleExistsInGarage)
            {
                Console.WriteLine(Messages.sr_VehicleAlreadyInGarage);
            }
            else
            {
                Console.WriteLine(Messages.sr_VehicleAddedToGarage);
            }
        }

        private void printRepairStatus()
        {
            foreach (Client.eRepairStatus repairStatus in Enum.GetValues(typeof(Client.eRepairStatus)))
            {
                Console.WriteLine((int)repairStatus + ". " + repairStatus);
            }
        }

        private void DisplayLicenseNumbers()
        {
            int userOptionAsInt = 0;

            Console.WriteLine(Messages.sr_AskForLicensePlateFilter);
            printRepairStatus();
            string userOption = Console.ReadLine();
            int.TryParse(userOption, out userOptionAsInt);

            switch (userOptionAsInt)
            {
                case (int)Client.eRepairStatus.InProgress:
                    printLicensePlate(garage.GetDictionaryByStatus(Client.eRepairStatus.InProgress));
                    break;
                case (int)Client.eRepairStatus.Repaired:
                    printLicensePlate(garage.GetDictionaryByStatus(Client.eRepairStatus.Repaired));
                    break;
                case (int)Client.eRepairStatus.Paid:
                    printLicensePlate(garage.GetDictionaryByStatus(Client.eRepairStatus.Paid));
                    break;
                default:
                    printAllLicensePlateInGarage(garage);
                    break;
            }
        }

        private void printAllLicensePlateInGarage(Garage garage)
        {
            printLicensePlate(garage.GetDictionaryByStatus(Client.eRepairStatus.InProgress));
            printLicensePlate(garage.GetDictionaryByStatus(Client.eRepairStatus.Repaired));
            printLicensePlate(garage.GetDictionaryByStatus(Client.eRepairStatus.Paid));
        }

        private void printLicensePlate(Dictionary<string, Client> i_DictionaryToPrint)
        {
            foreach (string licensePlate in i_DictionaryToPrint.Keys)
            {
                Console.WriteLine(licensePlate);
            }
        }

        private void ChangeVehicleStatus()
        {
            int chosenStatus = 0;
            bool isGarageEmpty = false;
            string licensePlate = getLicensePlateFromUser(out isGarageEmpty);

            if (!isGarageEmpty)
            {
                Console.WriteLine(Messages.sr_AskForRepairStatus);
                printRepairStatus();
                bool isChosenStatusValid = int.TryParse(Console.ReadLine(), out chosenStatus);

                while (!isChosenStatusValid)
                {
                    Console.WriteLine(Messages.sr_InvalidChosenStatus);
                    isChosenStatusValid = int.TryParse(Console.ReadLine(), out chosenStatus);
                    if (isChosenStatusValid)
                    {
                        isChosenStatusValid = Utils.ValidateOptionInRange(chosenStatus, 1, Enum.GetValues(typeof(Client.eRepairStatus)).Length);
                    }
                }

                Client.eRepairStatus chosenStatusAsEnum = (Client.eRepairStatus)chosenStatus;
                garage.ChangeRepairStatus(licensePlate, chosenStatusAsEnum);
                Console.WriteLine(string.Format(Messages.sr_StatusChanged, chosenStatus.ToString()));
            }
        }

        private string getLicensePlateFromUser(out bool o_IsGarageEmpty)
        {
            o_IsGarageEmpty = garage.IsGarageEmpty();
            string licensePlate = string.Empty;

            if (o_IsGarageEmpty)
            {
                Console.WriteLine(Messages.sr_InvalidGarageIsEmpty);
            }
            else
            {
                Console.WriteLine(Messages.sr_AskForLicensePlate);
                licensePlate = Console.ReadLine();

                Utils.ValidateLicensePlateValidity(licensePlate);
                bool isVehicleInGarage = garage.CheckIfVehicleInGarage(licensePlate);
                while (!isVehicleInGarage)
                {
                    Console.WriteLine(Messages.sr_InvalidVehicleNotInGarage);
                    licensePlate = Console.ReadLine();
                    isVehicleInGarage = garage.CheckIfVehicleInGarage(licensePlate);
                }
            }

            return licensePlate;
        }

        private void DisplayVehicleDetails()
        {
            bool isGarageEmpty = false;
            string licensePlate = getLicensePlateFromUser(out isGarageEmpty);

            if (!isGarageEmpty)
            {
                Client clientToPrint = garage.GetClientByLicensePlate(licensePlate);
                string clientsData = clientToPrint.GetClientData();
                Console.WriteLine(clientsData);
            }
        }

        private void ChargeElectricVehicle()
        {
            bool isGarageEmpty = false;
            string licensePlate = getLicensePlateFromUser(out isGarageEmpty);
            if (!isGarageEmpty)
            {
                Client clientToCheckMaxEnergy = garage.GetClientByLicensePlate(licensePlate);

                if (Utils.ValidateVehicleIsElectricBased(clientToCheckMaxEnergy))
                {
                    Console.WriteLine(Messages.sr_AskForNumOfMinutesToAdd);
                    float amountToAdd = getAmountToAdd(clientToCheckMaxEnergy.Vehicle.Engine.MaxEnergy * k_Hour, clientToCheckMaxEnergy.Vehicle.Engine.CurrentEnergy * k_Hour);
                    garage.ReCharge(licensePlate, amountToAdd / k_Hour);
                    Console.WriteLine(Messages.sr_VehicleIsCharged);
                }
                else
                {
                    Console.WriteLine(Messages.sr_InvalidVehicleTypeToCharge);
                }
            }
        }

        private float getAmountToAdd(float i_MaxEnergy, float i_CurrentEnergy)
        {
            string amountToAdd = Console.ReadLine();
            float amountToAddAsFloat;
            bool isAmountFloat = float.TryParse(amountToAdd, out amountToAddAsFloat);

            if (isAmountFloat)
            {
                isAmountFloat = isAmountFloat && amountToAddAsFloat <= i_MaxEnergy && i_CurrentEnergy + amountToAddAsFloat <= i_MaxEnergy && amountToAddAsFloat >= 0;
            }

            while (!isAmountFloat)
            {
                Console.WriteLine(string.Format(Messages.sr_InvalidAmountOfEnergy, i_MaxEnergy, i_MaxEnergy - i_CurrentEnergy));
                amountToAdd = Console.ReadLine();
                isAmountFloat = float.TryParse(amountToAdd, out amountToAddAsFloat);
                if (isAmountFloat)
                {
                    isAmountFloat = isAmountFloat && amountToAddAsFloat <= i_MaxEnergy && i_CurrentEnergy + amountToAddAsFloat <= i_MaxEnergy && amountToAddAsFloat >= 0;
                }
            }

            return amountToAddAsFloat;
        }

        private void ReFuelVehicle()
        {
            bool isGarageEmpty = false;
            string licensePlate = getLicensePlateFromUser(out isGarageEmpty);

            if (!isGarageEmpty)
            {
                Client clientToCheckMaxEnergy = garage.GetClientByLicensePlate(licensePlate);

                if (Utils.ValidateVehicleIsFuelBased(clientToCheckMaxEnergy))
                {
                    FuelEngine.eFuelType fuelType = getFuelTypeFromUser(clientToCheckMaxEnergy);
                    Console.WriteLine(Messages.sr_AskForAmountOfFuelToAdd);
                    float amountToAdd = getAmountToAdd(clientToCheckMaxEnergy.Vehicle.Engine.MaxEnergy, clientToCheckMaxEnergy.Vehicle.Engine.CurrentEnergy);
                    garage.ReFuel(licensePlate, amountToAdd, fuelType);
                    Console.WriteLine(Messages.sr_VehicleIsFueled);
                }
                else
                {
                    Console.WriteLine(Messages.sr_InvalidVehicleTypeToFuel);
                }
            }
        }

        private FuelEngine.eFuelType getFuelTypeFromUser(Client i_ClientToCheck)
        {
            Console.WriteLine(Messages.sr_AskForFuelTypeToAdd);
            string fuelTypeFromUser = Console.ReadLine();
            FuelEngine fuelEngineVehicle = (FuelEngine)i_ClientToCheck.Vehicle.Engine;
            FuelEngine.eFuelType fuelType;
            bool isValidFuelType = Enum.TryParse(fuelTypeFromUser, out fuelType);

            while (!isValidFuelType || !fuelType.Equals(fuelEngineVehicle.FuelType))
            {
                Console.WriteLine(string.Format(Messages.sr_InvalidFuelType, fuelTypeFromUser, fuelEngineVehicle.FuelType));
                fuelTypeFromUser = Console.ReadLine();
                isValidFuelType = Enum.TryParse(fuelTypeFromUser, out fuelType);
            }

            return fuelType;
        }

        private void InflateVehicleWheels()
        {
            bool isGarageEmpty = false;
            string licensePlate = getLicensePlateFromUser(out isGarageEmpty);

            if (!isGarageEmpty)
            {
                garage.InflateVehicleWheelsToMaximum(licensePlate);
                Console.WriteLine(Messages.sr_WheelsInflated);
            }
        }

        private void AskAndSetVehicleSpecificParameters(Dictionary<string, string> i_VehicleSpecificParameters, Vehicle i_Vehicle)
        {
            foreach (string property in i_VehicleSpecificParameters.Keys.ToList())
            {
                Console.WriteLine("Please enter " + property);
                string inputFromUser = Console.ReadLine();
                bool isInputValid = i_Vehicle.ValidateAndSetParameters(property, inputFromUser);
                while (!isInputValid)
                {
                    Console.WriteLine(Messages.sr_InvalidArgument);
                    inputFromUser = Console.ReadLine();
                    isInputValid = i_Vehicle.ValidateAndSetParameters(property, inputFromUser);
                }

                i_VehicleSpecificParameters[property] = inputFromUser;
            }
        }

        private void AskAndSetEngineSpecificParameters(Engine i_Engine)
        {
            List<string> engineProperties = new List<string>();

            i_Engine.AddProperties(engineProperties);
            foreach (string property in engineProperties)
            {
                Console.WriteLine("Please enter " + property);
                string inputFromUser = Console.ReadLine();
                bool isInputValid = i_Engine.ValidateAndSetParameters(property, inputFromUser);
                while (!isInputValid)
                {
                    Console.WriteLine(Messages.sr_InvalidArgument);
                    inputFromUser = Console.ReadLine();
                    isInputValid = i_Engine.ValidateAndSetParameters(property, inputFromUser);
                }
            }
        }

        private void AskAndSetWheelSpecificParameters(Wheel i_Wheel)
        {
            List<string> wheelProperties = new List<string>();
            
            i_Wheel.AddProperties(wheelProperties);
            foreach (string property in wheelProperties)
            {
                Console.WriteLine("Please enter " + property);
                string inputFromUser = Console.ReadLine();
                bool isInputValid = i_Wheel.ValidateAndSetParameters(property, inputFromUser);
                while (!isInputValid)
                {
                    Console.WriteLine(Messages.sr_InvalidArgument);
                    inputFromUser = Console.ReadLine();
                    isInputValid = i_Wheel.ValidateAndSetParameters(property, inputFromUser);
                }
            }
        }
    }
}
