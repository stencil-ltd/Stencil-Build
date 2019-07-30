using UnityEditor;

namespace Standard
{
    [InitializeOnLoad]
    public class SplashScreenFuckOff {
        
        static SplashScreenFuckOff()
        {
            PlayerSettings.SplashScreen.showUnityLogo = false;
        }
    }
}
