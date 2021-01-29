using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace BDFramework.Editor.TouchAfflatus
{
    internal class OthersWindow
    {

        [LabelText("PHPStudy路径")]
        [FilePath(AbsolutePath = true)]
        public string phpStudyPath = "";
        //
        [LabelText("ExcelToCS路径")]
        [FilePath(AbsolutePath = true)]
        public string excelToCsPath = IPath.Combine(Application.dataPath, "ClickFish/Game@hotfix/Table");

        [LabelText("PrefabToCS路径")]
        [FilePath(AbsolutePath = true)]
        public string prefabToCsPath = IPath.Combine(Application.dataPath, "ClickFish/Game@hotfix/UI");

        [LabelText("FTP IP 地址")]
        public string ftpAddress = "";
        [LabelText("FTP用户名称")]
        public string ftpUserName = "";
        [LabelText("FTP密码")]
        public string ftpPassword = "";
        [LabelText("FTP本地路径")]
        [FolderPath(AbsolutePath = true)]
        public string localFolderPath = "";
      
        [Button("保存")]
        private void save()
        {
            EditorPrefs.SetString(nameof(PrefsNames.phpStudyPath), this.phpStudyPath);
            EditorPrefs.SetString(nameof(PrefsNames.excelToCsPath), this.excelToCsPath);
            EditorPrefs.SetString(nameof(PrefsNames.UIPrefabToCsPath), this.prefabToCsPath);

            //FTP
            EditorPrefs.SetString(nameof(PrefsNames.FTPAddress), this.ftpAddress);
            EditorPrefs.SetString(nameof(PrefsNames.FTPUserName), this.ftpUserName);
            EditorPrefs.SetString(nameof(PrefsNames.FTPPassword), this.ftpPassword);
            EditorPrefs.SetString(nameof(PrefsNames.LocalFolderPath), this.localFolderPath);


        }
        public OthersWindow()
        {
            string path = EditorPrefs.GetString(nameof(PrefsNames.phpStudyPath));
            this.phpStudyPath = path == null ? "" : path;
            string ftpAddress = EditorPrefs.GetString(nameof(PrefsNames.FTPAddress));
            this.ftpAddress = ftpAddress == null ? "" : ftpAddress;
            string ftpUserName = EditorPrefs.GetString(nameof(PrefsNames.FTPUserName));
            this.ftpUserName = ftpUserName == null ? "" : ftpUserName;
            string fptPassword = EditorPrefs.GetString(nameof(PrefsNames.FTPPassword));
            this.ftpPassword = fptPassword == null ? "" : fptPassword;
            string localFolderPath = EditorPrefs.GetString(nameof(PrefsNames.LocalFolderPath));
            this.localFolderPath = localFolderPath == null ? "" : localFolderPath;
        }
        /*
        public GameObject obj;

        [TypeFilter("GetFilteredTypeList")]
        public List<GameObject> Array = new List<GameObject>();

        public IEnumerable<Type> GetFilteredTypeList()
        {
            if (obj == null) return null;
            var q = typeof(GameObject).Assembly.GetTypes()

            .Where(x => typeof(GameObject).Name != obj.name);                 // 排除不从BaseClass继承的类 

            // Adds various C1<T> type variants.
            // q = q.AppendWith(typeof(C1<>).MakeGenericType(typeof(GameObject))); //添加C1泛型为GameObject 的value
            // q = q.AppendWith(typeof(C1<>).MakeGenericType(typeof(AnimationCurve)));//添加C1泛型为AnimationCurve 的value
            //q = q.AppendWith(typeof(C1<>).MakeGenericType(typeof(List<float>)));//添加C1泛型为List<float> 的value

            return q;




        }
        */
    }
}