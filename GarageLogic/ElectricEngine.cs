using System.Collections.Generic;

namespace GarageLogic
{
    public class ElectricEngine : Engine
    {
        private readonly string r_CurrentEnergyProperty = "current energy left in hours:";

        public ElectricEngine(float i_MaxEnergy)
        {
            MaxEnergy = i_MaxEnergy;
        }

        public override void AddProperties(List<string> i_List)
        {
            i_List.Add(r_CurrentEnergyProperty);
        }
    }
}
