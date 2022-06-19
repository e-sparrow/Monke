namespace Game.Settings.Interfaces
{
    public interface IGameplaySettings
    {
        /// <summary>
        /// Tilt of device sensitivity used to correct imaginary gravity
        /// </summary>
        float TiltSensitivity
        {
            get;
        }

        /// <summary>
        /// Max distance when player can grab nearest joint
        /// </summary>
        float MaxGrabDistance
        {
            get;
        }
    }
}