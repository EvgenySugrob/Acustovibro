namespace Vmaya.Scene3D
{
    public class Settings
    {
        public static float mouseSens = 0.5f;

        public static float calcSens(float value, float maxValue)
        {
            float v = mouseSens - 0.5f;
            return value + maxValue * v * (v < 0 ? 0.9f : 2f);
        }
    }
}