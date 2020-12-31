using System;
using System.Linq;
using System.Text.RegularExpressions;
using GarageLogic;

namespace ConsoleUI
{
    public static class Utils
    {
        internal static void ValidateChosenOption(string io_ChosenOption, out int io_Option, int i_StartIndex, int i_EndIndex)
        {
            io_Option = 0;
            bool isOptionValid = int.TryParse(io_ChosenOption, out io_Option); // If the conversion failed int.TryParse assigns 0 in out parameter

            while (!isOptionValid || !ValidateOptionInRange(io_Option, i_StartIndex, i_EndIndex))
            {
                Console.WriteLine(Messages.sr_InvalidOptionChosen);
                io_ChosenOption = Console.ReadLine();
                isOptionValid = int.TryParse(io_ChosenOption, out io_Option);
                if (isOptionValid)
                {
                    isOptionValid = ValidateOptionInRange(io_Option, i_StartIndex, i_EndIndex);
                }
            }
        }

        internal static bool ValidateOptionInRange(int i_Option, int i_StartRange, int i_EndRange)
        {
            bool isInRange = true;

            if (i_Option < i_StartRange || i_Option > i_EndRange)
            {
                isInRange = false;    
            }

            return isInRange;
        }

        internal static void ValidateClientName(ref string i_Name)
        {
            bool isNameValid = i_Name.Trim().Length != 0 && i_Name.All(char.IsLetter);

            while (!isNameValid)
            {
                Console.WriteLine(Messages.sr_InvalidName);
                i_Name = Console.ReadLine();
                isNameValid = i_Name.Trim().Length != 0 && i_Name.All(char.IsLetter);
            }
        }

        internal static void ValidateClientPhoneNumber(ref string i_PhoneNumber)
        {
            bool isPhoneNumberValid = i_PhoneNumber.Trim().Length != 0 && i_PhoneNumber.Length == 10 && i_PhoneNumber.All(char.IsDigit);

            while (!isPhoneNumberValid)
            {
                Console.WriteLine(Messages.sr_InvalidPhoneNumber);
                i_PhoneNumber = Console.ReadLine();
                isPhoneNumberValid = i_PhoneNumber.Trim().Length != 0 && i_PhoneNumber.Length == 10 && i_PhoneNumber.All(char.IsDigit);
            }
        }

        internal static void ValidateLicensePlateValidity(string i_LicensePlate)
        {
            bool isLicensePlateValid = i_LicensePlate.Trim().Length != 0;

            while (!isLicensePlateValid)
            {
                Console.WriteLine(Messages.sr_InvalidLicensePlate);
                i_LicensePlate = Console.ReadLine();
                isLicensePlateValid = i_LicensePlate.Trim().Length != 0;
            }
        }

        internal static bool ValidateVehicleIsElectricBased(Client clientToCheckMaxEnergy)
        {
            return clientToCheckMaxEnergy.Vehicle.Engine is ElectricEngine;
        }

        internal static bool ValidateVehicleIsFuelBased(Client clientToCheckMaxEnergy)
        {
            return clientToCheckMaxEnergy.Vehicle.Engine is FuelEngine;
        }

        internal static string SplitCamelCase(string input)
        {
            return Regex.Replace(input, "([A-Z])", " $1", RegexOptions.Compiled).Trim();
        }
    }
}