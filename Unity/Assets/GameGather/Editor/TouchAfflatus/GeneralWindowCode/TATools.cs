using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace BDFramework.Editor.TouchAfflatus
{
    public class TATools : OdinMenuEditorWindow
    {

        private string name = "";
        [MenuItem("Tools/TouchAfflatus", false, 2010)]
        private static void Open()
        {
            var window = GetWindow<TATools>("个人工具箱");
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 500);
        }
        protected override OdinMenuTree BuildMenuTree()
        {
            //_othersWindow = 
            var tree = new OdinMenuTree(true);
            tree.Selection.SupportsMultiSelect = false;
            tree.Add("生产UI代码", new GeneralWindowCode());
            tree.Add("Prefab修改",new EditorPrefabs());
            tree.Add("批量命名",new EditorPrefabs());
            tree.Add("其它", new OthersWindow());

            return tree;
        }


    }


}
