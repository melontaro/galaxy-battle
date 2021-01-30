using System;
using System.IO;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using System.Collections;

namespace BDFramework.Editor.TouchAfflatus
{
    public class EditorPrefabs
    {

        public EditorPrefabs()
        {
            //foreach (var item in UnityEditorInternal.InternalEditorUtility.layers)
            //{
            //    Debug.Log(item);
            //}
            List<string> layers = new List<string>();
            for (int i = 0; i < UnityEditorInternal.InternalEditorUtility.layers.Length; i++)
            {

                layers.Add(LayerMask.LayerToName(i));

            }
            ValueDropdownList<string> csHtmlTemplatesTmp = csHtmlTemplates as ValueDropdownList<string>;
            foreach (var item in layers)
            {
                csHtmlTemplatesTmp.Add(item);
            }


            List<string> spriteLayers = new List<string>();
            //Debug.Log(SortingLayer.layers[1].name);
            for (int i = 0; i < SortingLayer.layers.Length; i++)
            {
                spriteLayers.Add(SortingLayer.layers[i].name);
            }

            ValueDropdownList<string> spriteLayerTemplatesTmp = spriteLayerTemplates as ValueDropdownList<string>;
            foreach (var item in spriteLayers)
            {
                spriteLayerTemplatesTmp.Add(item);
            }

        }

        [TitleGroup("预制体绑定")]
        [BoxGroup("预制体绑定/文件路径")]
        [FolderPath]
        public string prefabsFolder = Application.dataPath + "/Resource/Runtime/Monsters";
        [BoxGroup("预制体绑定/节点名称")]
        public string Binds = "Binds";
        [BoxGroup("预制体绑定/增加子节点")]
        public List<ChildObj> childObjs = new List<ChildObj>();

        [BoxGroup("预制体绑定/预制体缩放")]
        public float scale;

        [BoxGroup("预制体绑定/预制体缩放")]
        [HorizontalGroup("预制体绑定/预制体缩放/Split")]
        [VerticalGroup("预制体绑定/预制体缩放/Split/Left")]
        [Sirenix.OdinInspector.Button("应用", ButtonSizes.Large, ButtonStyle.FoldoutButton), GUIColor(0.3f, 0.8f, 1)]
        void ScalePrefab()
        {
            string[] files = Directory.GetFiles(prefabsFolder, "*.prefab");
            for (int i = 0; i < files.Length; i++)
            {
                string prefabPath = files[i];
                GameObject go = PrefabUtility.LoadPrefabContents(prefabPath);
                if (go != null)
                {
                    go.transform.localScale = new Vector3(scale, scale, scale);
                    Debug.Log(go.ToString());

                }
                else
                {
                    Debug.Log("prefab is not null");
                }
                PrefabUtility.SaveAsPrefabAsset(go, prefabPath);
                PrefabUtility.UnloadPrefabContents(go);
            }
        }

        [BoxGroup("预制体绑定/预制体缩放")]
        [HorizontalGroup("预制体绑定/预制体缩放/Split")]
        [VerticalGroup("预制体绑定/预制体缩放/Split/Right")]
        [Sirenix.OdinInspector.Button("还原", ButtonSizes.Large, ButtonStyle.FoldoutButton), GUIColor(0.3f, 0.8f, 1)]
        void CancelScalePrefab()
        {
            string[] files = Directory.GetFiles(prefabsFolder, "*.prefab");
            for (int i = 0; i < files.Length; i++)
            {
                string prefabPath = files[i];
                GameObject go = PrefabUtility.LoadPrefabContents(prefabPath);
                if (go != null)
                {
                    go.transform.localScale = new Vector3(1, 1, 1);
                }
                else
                {
                    Debug.Log("prefab is not null");
                }
                PrefabUtility.SaveAsPrefabAsset(go, prefabPath);
                PrefabUtility.UnloadPrefabContents(go);
            }
        }





        [BoxGroup("预制体绑定/增加子节点")]
        [HorizontalGroup("预制体绑定/增加子节点/Split")]
        [VerticalGroup("预制体绑定/增加子节点/Split/Left")]
        [Sirenix.OdinInspector.Button("生成prefab绑点", ButtonSizes.Medium, ButtonStyle.FoldoutButton), GUIColor(0.3f, 0.8f, 1)]
        void GeneralPrefab()
        {
            string[] files = Directory.GetFiles(prefabsFolder, "*.prefab");
            for (int i = 0; i < files.Length; i++)
            {
                string prefabPath = files[i];
                GameObject go = PrefabUtility.LoadPrefabContents(prefabPath);
                if (go != null)
                {
                    //if (go.GetComponent<CharacterClick>() == null)
                    //{
                    //    go.AddComponent<CharacterClick>();
                    //}
                    if (go.GetComponent<CapsuleCollider2D>() == null)
                    {
                        CapsuleCollider2D capsuleCollider2D = go.AddComponent<CapsuleCollider2D>();
                        capsuleCollider2D.offset = new Vector2(0, 1.28f);
                        capsuleCollider2D.size = new Vector2(3, 3);
                    }

                    GameObject bnds = new GameObject("Binds");
                    bnds.transform.SetParent(go.transform);
                    go.transform.localPosition = new Vector3(0, 0, 0);

                    foreach (var item in childObjs)
                    {
                        GameObject obj = new GameObject(item.name);
                        obj.transform.SetParent(bnds.transform);
                        obj.transform.localPosition = item.v3;


                    }
                    PrefabUtility.SaveAsPrefabAsset(go, prefabPath);
                    PrefabUtility.UnloadPrefabContents(go);
                }
                else
                {
                    Debug.Log("prefab is not null");
                }
            }
        }
        [BoxGroup("预制体绑定/增加子节点")]
        [VerticalGroup("预制体绑定/增加子节点/Split/Right")]
        [Sirenix.OdinInspector.Button("删除prefab绑点", ButtonSizes.Medium, ButtonStyle.FoldoutButton), GUIColor(0.3f, 0.8f, 1)]
        void DeleteBinds()
        {
            string[] files = Directory.GetFiles(prefabsFolder, "*.prefab");
            for (int i = 0; i < files.Length; i++)
            {
                string prefabPath = files[i];
                GameObject go = PrefabUtility.LoadPrefabContents(prefabPath);
                Transform bindTrasform = go.transform.Find(Binds);

                if (bindTrasform != null)
                {
                    GameObject.DestroyImmediate(bindTrasform.gameObject);

                    PrefabUtility.SaveAsPrefabAsset(go, prefabPath);
                    PrefabUtility.UnloadPrefabContents(go);
                }
                else
                {
                    Debug.Log("prefab is not null");
                }
            }
        }

        [BoxGroup("预制体绑定/增加子节点")]
        [VerticalGroup("预制体绑定/增加子节点/Split/Right")]
        [Sirenix.OdinInspector.Button("添加prefab父节点", ButtonSizes.Medium, ButtonStyle.FoldoutButton), GUIColor(0.3f, 0.8f, 1)]
        void AddParentBind()
        {
            string[] files = Directory.GetFiles(prefabsFolder, "*.prefab");
            for (int i = 0; i < files.Length; i++)
            {
                string prefabPath = files[i];
                string name = Path.GetFileNameWithoutExtension(prefabPath);
                GameObject go = PrefabUtility.LoadPrefabContents(prefabPath);
                Debug.Log(name);
                GameObject parentObj = new GameObject(name);
                go.transform.Find("Binds").transform.SetParent(parentObj.transform);
                go.transform.SetParent(parentObj.transform);
                go.name = "body";


                PrefabUtility.SaveAsPrefabAsset(parentObj, prefabPath);
                //PrefabUtility.UnloadPrefabContents(go);

            }
        }

        [BoxGroup("预制体绑定/预制体更改layer")]
        IEnumerable csHtmlTemplates = new ValueDropdownList<string>();
        [BoxGroup("预制体绑定/预制体更改layer")]
        [ValueDropdown("csHtmlTemplates", SortDropdownItems = true)]
        public string layer = "下拉选择";

        [BoxGroup("预制体绑定/预制体更改layer")]
        [Sirenix.OdinInspector.Button("更改预制体Layer", ButtonSizes.Large, ButtonStyle.FoldoutButton), GUIColor(0.3f, 0.8f, 1)]
        void SetLayer()
        {
            string[] files = Directory.GetFiles(prefabsFolder, "*.prefab");
            for (int i = 0; i < files.Length; i++)
            {
                string prefabPath = files[i];
                GameObject go = PrefabUtility.LoadPrefabContents(prefabPath);
                if (go != null)
                {

                    go.layer = LayerMask.NameToLayer(layer);
                    Debug.Log(go.layer);

                }
                else
                {
                    Debug.Log("prefab is not null");
                }
                PrefabUtility.SaveAsPrefabAsset(go, prefabPath);
                PrefabUtility.UnloadPrefabContents(go);
            }
        }

        [BoxGroup("预制体绑定/预制体更改spriteLayer")]
        IEnumerable spriteLayerTemplates = new ValueDropdownList<string>();
        [BoxGroup("预制体绑定/预制体更改spriteLayer")]
        [ValueDropdown("spriteLayerTemplates", SortDropdownItems = true)]
        public string sortingLayerName = "下拉选择";
        [BoxGroup("预制体绑定/预制体更改spriteLayer")]
        public int OrderInLayer = 0;

        [BoxGroup("预制体绑定/预制体更改spriteLayer")]
        [Sirenix.OdinInspector.Button("更改spriteLayer", ButtonSizes.Large, ButtonStyle.FoldoutButton), GUIColor(0.3f, 0.8f, 1)]
        void SetSpriteLayer()
        {
            string[] files = Directory.GetFiles(prefabsFolder, "*.prefab");
            for (int i = 0; i < files.Length; i++)
            {
                string prefabPath = files[i];
                GameObject go = PrefabUtility.LoadPrefabContents(prefabPath);
                if (go != null)
                {
                    foreach (Transform child in go.GetComponentsInChildren<Transform>(true))
                    {
                        var sprite = child.GetComponent<SpriteRenderer>();
                        if (sprite != null)
                        {
                            sprite.sortingLayerName = sortingLayerName;
                            //sprite.sortingOrder = OrderInLayer;

                            Debug.Log(sortingLayerName);
                            Debug.Log(OrderInLayer);
                        }
                        //var skeletonAnimation = child.GetComponent<SkeletonAnimation>();
                        //if (skeletonAnimation != null)
                        //{
                        //     skeletonAnimation.state. = sortingLayerName;
                        //    //sprite.sortingOrder = OrderInLayer;


                        //}

                    }

                }
                else
                {
                    Debug.Log("prefab is not null");
                }
                PrefabUtility.SaveAsPrefabAsset(go, prefabPath);
                PrefabUtility.UnloadPrefabContents(go);
            }
        }

        [Serializable]
        public class ChildObj
        {
            [BoxGroup("属性")]
            public string name;

            [BoxGroup("属性")]
            public Vector3 v3;

        }


    }
}
