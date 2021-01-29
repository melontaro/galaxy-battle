using System;
using UnityEditor;
using UnityEngine;
using System.IO;
using BDFramework.Editor.Asset;
using BDFramework.Editor.BuildPackage;
using BDFramework.Editor.EditorLife;
using Code.BDFramework.Core.Tools;
using Code.BDFramework.Editor;
using UnityEditor.SceneManagement;

namespace BDFramework.Editor
{
    static public class EditorBuildPackage
    {

        public enum BuildMode
        {
            Debug = 0,
            Release,
        }
        static string SCENEPATH="Assets/Scenes/BDFrame.unity";
        static string[] SceneConfigs =
        {
            "Assets/Scenes/Config/Debug.json",
            "Assets/Scenes/Config/Release.json",
        };


        static EditorBuildPackage()
        {
            //初始化框架编辑器下
            BDFrameEditorLife.InitBDEditorLife();
        }
        

        [MenuItem("BDFrameWork工具箱/打包/BuildAPK(使用当前配置 )")]
        public static void EditorBuildAPK_Empty()
        {
            LoadConfig();
            BuildAPK_Empty();
        }

        [MenuItem("BDFrameWork工具箱/打包/BuildAPK(Debug-StreamingAsset)")]
        public static void EditorBuildAPK_Debug()
        {
            if (EditorUtility.DisplayDialog("提示", "此操作会重新编译资源,是否继续？", "OK", "Cancel"))
            {
                BuildAPK_Debug();
            }
          
        }

        [MenuItem("BDFrameWork工具箱/打包/BuildAPK(Release-Persistent)")]
        public static void EditorBuildAPK()
        {
            if (EditorUtility.DisplayDialog("提示", "此操作会重新编译资源,是否继续？", "OK", "Cancel"))
            {
                BuildAPK_Release();
            }
        }
        
        [MenuItem("BDFrameWork工具箱/打包/导出XCode工程(ipa暂未实现)")]
        public static void EditorBuildIpa()
        {
            BuildIpa();
        }





        
        /// <summary>
        /// 加载场景配置
        /// </summary>
        /// <param name="mode"></param>
       
        static public void LoadConfig(BuildMode? mode=null)
        {
            var  scene=  EditorSceneManager.OpenScene(SCENEPATH);
            TextAsset textContent = null;
            if (mode != null)
            {
                string path = SceneConfigs[(int)mode];
                textContent  = AssetDatabase.LoadAssetAtPath<TextAsset>(path);
                var config = GameObject.Find("BDFrame").GetComponent<BDLauncher>();
                config.ConfigContent = textContent;
            }
            EditorSceneManager.SaveScene(scene);
        }




        #region Android

        /// <summary>
        /// 构建包体，
        /// </summary>
        static public void BuildAPK_Empty()
        {
            LoadConfig();
            BuildAPK();
        }

        /// <summary>
        /// 构建Debug包体
        /// </summary>
        static public void BuildAPK_Debug()
        {
            LoadConfig(BuildMode.Debug);
            EditorWindow_OnekeyBuildAsset.GenAllAssets(Application.streamingAssetsPath,RuntimePlatform.Android, BuildTarget.Android);
            BuildAPK();
        }
        
        /// <summary>
        /// 构建Release包体
        /// </summary>
        static public void BuildAPK_Release()
        {
            LoadConfig(BuildMode.Release);
            EditorWindow_OnekeyBuildAsset.GenAllAssets(Application.streamingAssetsPath,RuntimePlatform.Android, BuildTarget.Android);
            BuildAPK();
        }

        
        /// <summary>
        /// 打包APK
        /// </summary>
        static public void BuildAPK()
        {

            if (!BDFrameEditorConfigHelper.EditorConfig.IsSetConfig())
            {
                BDebug.LogError("请注意设置apk keystore账号密码");
                return;
            }

            var absroot = Application.dataPath.Replace("Assets", "");
            PlayerSettings.Android.keystoreName =absroot  + BDFrameEditorConfigHelper.EditorConfig.Android.keystoreName;
            PlayerSettings.keystorePass =  BDFrameEditorConfigHelper.EditorConfig.Android.keystorePass;
            PlayerSettings.Android.keyaliasName= BDFrameEditorConfigHelper.EditorConfig.Android.keyaliasName;
            PlayerSettings.keyaliasPass =  BDFrameEditorConfigHelper.EditorConfig.Android.keyaliasPass;
            //
            var outdir = BDApplication.ProjectRoot + "/Build";
            var outputPath = IPath.Combine(  outdir,  Application.productName+".apk");
            //文件夹处理
            if (!Directory.Exists(outdir)) Directory.CreateDirectory(outdir);
            if(File.Exists(outputPath)) File.Delete(outputPath);
            //清空StreamingAsset
            var ios = IPath.Combine(Application.streamingAssetsPath, "iOS");
            if (Directory.Exists(ios))
            {
                Directory.Delete(ios, true);
            }
            var win = IPath.Combine(Application.streamingAssetsPath, "Windows");
            if (Directory.Exists(win)) 
            {
                Directory.Delete(win, true);
            }
            //开始项目一键打包
            string[] scenes = { SCENEPATH };
            UnityEditor.BuildPipeline.BuildPlayer(scenes, outputPath, BuildTarget.Android,BuildOptions.None);
            if (File.Exists(outputPath))
            {
                Debug.Log( "Build Success :" + outputPath);
            }
            else
            {

                Debug.LogException(new Exception("Build Fail! Please Check the log! "));
            }
        }
        
        
        #endregion
        
        #region iOS
        /// <summary>
        /// build Ipa
        /// </summary>
        static public void BuildIpa()
        {
            //
            var outdir = BDApplication.ProjectRoot + "/Build";
            //文件夹处理
            if (!Directory.Exists(outdir))
            {
                Directory.CreateDirectory(outdir);
            }
            //清空StreamingAsset
            var android = IPath.Combine(Application.streamingAssetsPath, "Android");
            if (Directory.Exists(android))
            {
                Directory.Delete(android, true);
            }
            var win = IPath.Combine(Application.streamingAssetsPath, "Windows");
            if (Directory.Exists(win)) 
            {
                Directory.Delete(win, true);
            }
            //开始构建XCode
            string[] scenes = { SCENEPATH };
            BuildPipeline.BuildPlayer(scenes, outdir, BuildTarget.iOS,BuildOptions.None);
        }
        
        static public void BuildIpa_Empty()
        {
            BuildIpa();
        }
        static public void BuildIpa_Debug()
        {
           
        }
        
        static public void BuildIpa_Release()
        {
           
        }
        #endregion
    }
}