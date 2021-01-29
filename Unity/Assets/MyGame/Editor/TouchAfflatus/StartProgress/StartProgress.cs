using ETModel;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;

public class StartProgress : Editor
{
   
    [MenuItem("Tools/web资源服务器")]
    private static void Open()
    {
      string  appName= EditorPrefs.GetString(("phpStudyPath"));
        if (string.IsNullOrEmpty(appName)) {
            Log.Error("无法启动phpStudy,请到Tools->TouchAfflatus->other");
            return; 
        }
      Process.Start(appName);
    }
    
}
