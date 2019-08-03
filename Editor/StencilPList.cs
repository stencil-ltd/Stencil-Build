using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using System.IO;

#if STENCIL_ADS
using Ads;
#endif

[InitializeOnLoad]
public class StencilPlist
{
    static StencilPlist()
    {}

    [PostProcessBuild]
    public static void ChangeXcodePlist(BuildTarget buildTarget, string pathToBuiltProject) {

        if (buildTarget == BuildTarget.iOS) {
   
            // Get plist
            string plistPath = pathToBuiltProject + "/Info.plist";
            PlistDocument plist = new PlistDocument();
            plist.ReadFromString(File.ReadAllText(plistPath));
   
            // Get root
            PlistElementDict rootDict = plist.root;
   
            // Encryption
            rootDict.SetBoolean("ITSAppUsesNonExemptEncryption", false);
            
            // File Sharing
            rootDict.SetString("NSCameraUsageDescription", "This app wants to save a photo to your device.");
            rootDict.SetString("NSPhotoLibraryUsageDescription", "This app wants to save a photo to your device.");
            rootDict.SetString("NSPhotoLibraryAddUsageDescription", "This app wants to save a photo to your device.");
            rootDict.SetString("NSLocationWhenInUseUsageDescription", "This app uses your location to make better recommendations.");
            
            // Push Notifications & Background
//            rootDict.SetBoolean("UIApplicationExitsOnSuspend", false);
            rootDict.values.Remove("UIApplicationExitsOnSuspend");
            rootDict.CreateArray("UIBackgroundModes").AddString("remote-notification");
            
            #if STENCIL_ADS
            rootDict.SetString("GADApplicationIdentifier", AdSettings.Instance.AppId.Ios);
            #endif
            
            // Write to file
            File.WriteAllText(plistPath, plist.WriteToString());
        }
    }
}