using System;
using System.Collections.Generic;

namespace GarageLogic
{
    public class FuelEngine : Engine
    {
        private readonly string r_CurrentFuelProperty = "current fuel in liters:";

        public enum eFuelType
        {
            Octan95,
            Octan96,
            Octan98,
            Soler
        }

        private eFuelType m_FuelType;

        public FuelEngine(eFuelType i_FuelType, float i_MaxEnergy)
        {
            m_FuelType = i_FuelType;
            MaxEnergy = i_MaxEnergy;
        }

        public eFuelType FuelType
        {
            get
            {
                return m_FuelType;
            }
        }

        internal void AddEnergy(eFuelType i_FuelType, float i_FuelToAdd)
        {
            if (!i_FuelType.Equals(m_FuelType))
            {
                throw new ArgumentException(string.Format("The fuel type you entered {0} does not fit this vehicle's fuel type: {1}", i_FuelType, m_FuelType));
            }

            base.AddEnergy(i_FuelToAdd);
        }

        public override void AddProperties(List<string> i_List)
        {
            i_List.Add(r_CurrentFuelProperty);
        }
    }
}
