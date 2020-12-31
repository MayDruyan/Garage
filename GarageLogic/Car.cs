using System;
using System.Collections.Generic;
using System.Text;

namespace GarageLogic
{
    public class Car : Vehicle
    {
        private enum eColors
        {
            Red,
            White,
            Black,
            Silver
        }

        private enum eNumOfDoors
        {
            A = 2,
            B = 3,
            C = 4,
            D = 5
        }

        private eColors m_Color;
        private eNumOfDoors m_NumOfDoors;
        private readonly float r_MaxWheelsPressure = 32;
        private readonly int r_NumOfWheels = 4;
        private readonly int r_FuelTankSize = 60;
        private readonly float r_ElectricTankSize = 2.1F;
        private readonly string r_ColorProperty = "color (Red, White, Black, Silver):";
        private readonly string r_NumOfDoorProperty = "number of doors [2-5]:";

        public Car(eEngineType i_EngineType)
        {
            m_Engine = i_EngineType == eEngineType.Fuel ? (Engine) new FuelEngine(FuelEngine.eFuelType.Octan96, this.r_FuelTankSize) : new ElectricEngine(this.r_ElectricTankSize);
            m_Wheels = new List<Wheel>(this.r_NumOfWheels);
            for (int i = 0; i < this.r_NumOfWheels; i++)
            {
                m_Wheels.Add(new Wheel(this.r_MaxWheelsPressure));
            }
        }

        internal override void BuildVehicle(Dictionary<string, string> i_Properties)
        {
            try
            {
                BuildBasicVehicle(i_Properties);
                InitializeWheels(this.r_NumOfWheels);
            }
            catch (FormatException)
            {
                Console.WriteLine("The type you entered is invalid.");
            }
            catch (ArgumentException)
            {
                Console.WriteLine("The value you entered is invalid.");
            }
        }

        internal override void AddProperties()
        {
            Properties.Add(this.r_ColorProperty);
            Properties.Add(this.r_NumOfDoorProperty);
        }

        public override bool ValidateAndSetParameters(string i_Property, string i_InputFromUser)
        {
            bool isInputValid = base.ValidateAndSetParameters(i_Property, i_InputFromUser);

            if (i_Property.Equals(this.r_ColorProperty))
            {
                isInputValid = Enum.TryParse(i_InputFromUser, out this.m_Color);
            }
            else if (i_Property.Equals(this.r_NumOfDoorProperty))
            {
                int inputNumOfDoors;
                isInputValid = int.TryParse(i_InputFromUser, out inputNumOfDoors);
                if (isInputValid && inputNumOfDoors >= (int) eNumOfDoors.A && inputNumOfDoors <= (int)eNumOfDoors.D)
                {
                    this.m_NumOfDoors = (eNumOfDoors) inputNumOfDoors;
                }
                else
                {
                    isInputValid = false;
                }
            }

            return isInputValid;
        }

        internal override string GetSpecificData()
        {
            StringBuilder carData = new StringBuilder();
            carData.AppendLine(string.Format("The car's color is: {0}", this.m_Color.ToString()));
            carData.AppendLine(string.Format("The car's number of doors is: {0}", (int)this.m_NumOfDoors));
            carData.Append(base.GetSpecificData());

            return carData.ToString();
        }
    }
}