using UnityEditor;

namespace RHFramework
{
    public class SimulationModelMenu
    {
        private const string kSimulationModeKey = "simulation mode";
        private const string kSimulationModePath = "RHFramework/Framework/ResKit/Simulation Mode";

        private static bool SimulationMode
        {
            get { return EditorPrefs.GetBool(kSimulationModeKey, true); }
            set { EditorPrefs.SetBool(kSimulationModeKey, value); }
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