using System;
using System.Diagnostics;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using ETModel;
using System.Collections.Generic;
using System.Collections;
using BDFramework.UFlux.Extension;

namespace BDFramework.Editor.TouchAfflatus
{
    internal class GeneralWindowCode
    {
        public GeneralWindowCode()
        {
            SetWindowTemplate();
            GeneralToFolderPath = EditorPrefs.GetString(nameof(PrefsNames.UIPrefabToCsPath));
        }

        private void SetWindowTemplate()
        {
            Dictionary<string, string> windowTemplates = GeneralWindowHelper.GetWindowTemplates();
            ValueDropdownList<string> csHtmlTemplatesTmp = csHtmlTemplates as ValueDropdownList<string>;
            foreach (var item in windowTemplates)
            {
                csHtmlTemplatesTmp.Add(item.Key, item.Value);
            }
        }

        //Window_xxx
        [VerticalGroup("Window")]
        [Title("Window 设置")]
        [SceneObjectsOnly]
        [OnValueChanged("onWindowObjChange")]
        [LabelText("选择场景中的WindowUI")]
        public GameObject WindowGameObject;
        [VerticalGroup("Window")]
        [LabelText("删除不需要生成代码的子对象")]
        public List<GameObject> childGameObjects = new List<GameObject>();

        [VerticalGroup("Window")]
        [PropertySpace(SpaceBefore = 30)]
        [LabelText("选择要生成到的文件夹")]
        [InfoBox("默认路径可到Other里边配置,然后重新打开生效")]
        [FolderPath]
        public String GeneralToFolderPath = EditorPrefs.GetString(nameof(PrefsNames.UIPrefabToCsPath));


        IEnumerable csHtmlTemplates = new ValueDropdownList<string>();
        [PropertySpace(SpaceBefore = 20)]
        [VerticalGroup("Window")]
        [LabelText("选择代码模板")]
        [ValueDropdown("csHtmlTemplates", SortDropdownItems = true)]
        public string codeTemplate = "下拉选择";

        [VerticalGroup("Window")]
        [PropertySpace(SpaceBefore = 30)]
        [Sirenix.OdinInspector.Button("生成Window代码", ButtonSizes.Large, ButtonStyle.FoldoutButton), GUIColor(0.3f, 0.8f, 1)]
        void GeneralCode()
        {
            if (string.IsNullOrEmpty(codeTemplate) || WindowGameObject == null || string.IsNullOrEmpty(GeneralToFolderPath))
            {
                UnityEngine.Debug.Log("配置不能为空");
                return;
            }
            GeneralWindowHelper.GenerateWindowCode(codeTemplate, WindowGameObject, childGameObjects, GeneralToFolderPath);
            AssetDatabase.Refresh();

        }

        [PropertySpace(SpaceBefore = 30)]
        [VerticalGroup("Item")]
        [Title("Item 设置(Item的设置不需要完全依赖上边Window设置，但需要提供上边Window设置中的文件名称)")]
        [LabelText("Item")]
        //[Toggle("Enabled")]
        public MyTogItem togItem = new MyTogItem();
        [Serializable]
        public class MyTogItem
        {

            public MyTogItem()
            {
                Dictionary<string, string> itemTemplates = GeneralWindowHelper.GetItemTemplates();
                ValueDropdownList<string> csHtmlTemplatesTmp = csHtmlTemplates as ValueDropdownList<string>;
                foreach (var item in itemTemplates)
                {
                    csHtmlTemplatesTmp.Add(item.Key, item.Value);
                }
            }
            // public bool Enabled = false;
            [LabelText("Item物体")]
            [OnValueChanged("OnTogItemChange")]
            public GameObject ItemObject;
            [LabelText("删除不需要生成代码的Item子物体")]
            public List<GameObject> subItemObjects = new List<GameObject>();

            [LabelText("文件名称")]
            public String FileName;


            IEnumerable csHtmlTemplates = new ValueDropdownList<string>();
            [LabelText("选择代码模板")]
            [ValueDropdown("csHtmlTemplates", SortDropdownItems = true)]
            public string codeTemplate = "下拉选择";
            public void OnTogItemChange()
            {
                subItemObjects.Clear();

                if (ItemObject != null)
                {
                    UnityEngine.Debug.Log(ItemObject.name);
                    foreach (Transform child in ItemObject.GetComponentsInChildren<Transform>(true))
                    {
                        if (child == ItemObject.transform)
                        {
                            continue;
                        }
                        subItemObjects.Add(child.gameObject);
                    }

                }

            }
        }



        [VerticalGroup("Item")]
        [PropertySpace(SpaceBefore = 30)]
        [Sirenix.OdinInspector.Button("生成Item代码", ButtonSizes.Large, ButtonStyle.FoldoutButton), GUIColor(0.3f, 0.8f, 1)]
        void ItemBtnClick()
        {
            UnityEngine.Debug.Log(childGameObjects.Count);
        }



        List<string> m_listPath = new List<string>();

        public string GetChildPath(Transform trans, Transform child)
        {
            m_listPath.Clear();
            FindChildGameObject(trans.gameObject, child);
            string path = "";
            for (int i = 1; i < m_listPath.Count; i++)
            {
                path += m_listPath[i];
                if (i != m_listPath.Count - 1)
                    path += "/";
            }
            return path;
        }


        public GameObject FindChildGameObject(GameObject parent, Transform child)
        {
            m_listPath.Add(parent.name);
            if (parent.transform == child)
            {

                return parent;
            }
            if (parent.transform.childCount < 1)
            {
                return null;
            }
            GameObject obj = null;
            for (int i = 0; i < parent.transform.childCount; i++)
            {
                GameObject go = parent.transform.GetChild(i).gameObject;
                obj = FindChildGameObject(go, child);
                if (obj != null)
                {

                    break;
                }
            }

            return obj;
        }
        public void onWindowObjChange()
        {
            childGameObjects.Clear();
            if (WindowGameObject != null)
            {

                foreach (Transform child in WindowGameObject.GetComponentsInChildren<Transform>(true))
                {
                    if (child == WindowGameObject.transform || string.Equals(child.name, "Text"))
                    {
                        continue;
                    }
                    childGameObjects.Add(child.gameObject);
                }

            }
        }

    }
    public enum PrefsNames
    {
        phpStudyPath,    //PHP Study的路径
        excelToCsPath,  //Excel 表格 生成到的路径
        UIPrefabToCsPath, ///UI Prefab 生成 CS 代码的路径
        FTPAddress,
        FTPUserName,
        FTPPassword,
        LocalFolderPath


    }
}