using UnityEngine.InputSystem;
using Zenject;

namespace Game.Calibration
{
    public class AccelerometerCalibrator
    {
        [Inject]
        private void Initialize()
        {
            if (Accelerometer.current != null)
            {
                InputSystem.EnableDevice(Accelerometer.current);
            } 
        }
    }
}