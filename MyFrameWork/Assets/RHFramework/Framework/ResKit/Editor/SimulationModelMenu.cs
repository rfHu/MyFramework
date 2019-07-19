using UnityEditor;

namespace RHFramework
{
    public class SimulationModelMenu
    {
        private const string kSimulationModePath = "RHFramework/Framework/ResKit/Simulation Mode";

        private static bool SimulationMode
        {
            get { return ResMgr.SimulationMode; }
            set { ResMgr.SimulationMode = value; }
        }

        [MenuItem(kSimulationModePath)]
        private static void ToggleSimulationMode()
        {
            SimulationMode = !SimulationMode;
        }

        [MenuItem(kSimulationModePath, true)]
        public static bool ToggleSimulationModeValidate()
        {
            Menu.SetChecked(kSimulationModePath, SimulationMode);
            return true;
        }
    }
}