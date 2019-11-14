using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using System.IO;
using JetBrains.Annotations;
using UnityEditor.iOS.Xcode;

public class EntitlementsPostProcess
{
    private const string entitlements = @"
    <?xml version=""1.0"" encoding=""UTF-8\""?>
    <!DOCTYPE plist PUBLIC ""-//Apple//DTD PLIST 1.0//EN"" ""http://www.apple.com/DTDs/PropertyList-1.0.dtd"">
    <plist version=""1.0"">
        <dict>
            <key>aps-environment</key>
            <string>development</string>
        </dict>
    </plist>";
    
    [PostProcessBuild, UsedImplicitly]
    public static void OnPostProcess(BuildTarget buildTarget, string buildPath)
    {
        if (buildTarget != BuildTarget.iOS) return;

        var file_name = "unity.entitlements";
        var proj_path = PBXProject.GetPBXProjectPath(buildPath);
        var proj = new PBXProject();
        proj.ReadFromFile(proj_path);

        // target_name = "Unity-iPhone"
        string target_guid;
        #if UNITY_2019_3_OR_NEWER
        var target_name = "Unity-iPhone";
        target_guid = proj.GetUnityMainTargetGuid();
        #else
        var target_name = PBXProject.GetUnityTargetName();
        target_guid = proj.TargetGuidByName(target_name);
        #endif        
        var dst = buildPath + "/" + target_name + "/" + file_name;
        try
        {
            File.WriteAllText(dst, entitlements);
            proj.AddFile(target_name + "/" + file_name, file_name);
            
            // Entitlements
            proj.AddBuildProperty(target_guid, "CODE_SIGN_ENTITLEMENTS", target_name + "/" + file_name);
            
            // Frameworks
            proj.AddFrameworkToProject(target_guid, "UserNotifications.framework", false);
            
            #if STENCIL_TENJIN || STENCIL_IRONSRC
            proj.AddFrameworkToProject(target_guid, "AdSupport.framework", false);
            proj.AddFrameworkToProject(target_guid, "iAd.framework", false);
            #endif
            
            proj.WriteToFile(proj_path);
        }
        catch (IOException e)
        {
            Debug.LogWarning($"Could not copy entitlements. Probably already exists. ({e})");
        }
    }
}
