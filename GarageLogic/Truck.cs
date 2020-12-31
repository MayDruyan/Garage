using System;
using System.Collections.Generic;
using System.Text;

namespace GarageLogic
{
    public class Truck : Vehicle
    {
        private readonly float r_MaxAirPressure = 28;
        private readonly int r_MaxFuelCapacity = 120;
        private readonly int r_NumOfWheels = 16;
        private readonly string r_DangerousMaterialsProperty = "if the truck contains dangerous materials, by \"yes\" or \"no\":";
        private readonly string r_TrunkSizeProperty = "trunk size:";
        private bool m_DangerousMaterials;
        private float m_TrunkSize;

        public Truck()
        {
            m_Engine = new FuelEngine(FuelEngine.eFuelType.Soler, r_MaxFuelCapacity);
            m_Wheels = new List<Wheel>(r_NumOfWheels);

            for (int i = 0; i < r_NumOfWheels; i++)
            {
                m_Wheels.Add(new Wheel(r_MaxAirPressure));
            }
        }

        internal override void AddProperties()
        {
            Properties.Add(r_DangerousMaterialsProperty);
            Properties.Add(r_TrunkSizeProperty);
        }

        internal override void BuildVehicle(Dictionary<string, string> i_Properties)
        {
            try
            {
                BuildBasicVehicle(i_Properties);
                m_DangerousMaterials = i_Properties[r_DangerousMaterialsProperty] == "yes";
                m_TrunkSize = int.Parse(i_Properties[r_TrunkSizeProperty]);
                InitializeWheels(r_NumOfWheels);
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

        public override bool ValidateAndSetParameters(string i_Property, string i_InputFromUser)
        {
            bool isInputValid = base.ValidateAndSetParameters(i_Property, i_InputFromUser);

            i_InputFromUser = i_InputFromUser.ToLower();

            if (i_Property.Equals(r_DangerousMaterialsProperty))
            {
                isInputValid = i_InputFromUser == "yes" || i_InputFromUser == "no";
                if (isInputValid)
                {
                    m_DangerousMaterials = i_InputFromUser == "yes";
                }
            }
            else if (i_Property.Equals(r_TrunkSizeProperty))
            {
                isInputValid = float.TryParse(i_InputFromUser, out m_TrunkSize);
            }

            return isInputValid;
        }

        internal override string GetSpecificData()
        {
            StringBuilder truckData = new StringBuilder();
            truckData.AppendLine(string.Format("The truck contains dangerous materials: {0}", m_DangerousMaterials.ToString()));
            truckData.AppendLine(string.Format("The truck's trunk size: {0}", m_TrunkSize));
            truckData.Append(base.GetSpecificData());

            return truckData.ToString();
        }
    }
}
