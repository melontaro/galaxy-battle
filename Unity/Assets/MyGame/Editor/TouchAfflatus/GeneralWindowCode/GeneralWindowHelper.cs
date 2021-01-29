
using ETModel;
using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class GeneralWindowHelper
{
    public static Dictionary<string, string> GetWindowTemplates()
    {
        Dictionary<string, string> windowTemplates = new Dictionary<string, string>();

        string directPath = Path.Combine("../", "Tools/CodeTemplate/Window");
        List<string> filePaths = Directory.GetFiles(directPath).ToList();
        foreach (var item in filePaths)
        {
            windowTemplates.Add(Path.GetFileNameWithoutExtension(item), item);
        }
        return windowTemplates;
    }


    public static Dictionary<string, string> GetItemTemplates()
    {
        Dictionary<string, string> itemTemplates = new Dictionary<string, string>();

        string directPath = Path.Combine("../", "Tools/CodeTemplate/Item");
        List<string> filePaths = Directory.GetFiles(directPath).ToList();
        foreach (var item in filePaths)
        {
            itemTemplates.Add(Path.GetFileNameWithoutExtension(item), item);
        }
        return itemTemplates;
    }

    public static void GenerateWindowCode(string templateFilePath, GameObject root, List<GameObject> childObj, string codePath)
    {

        JsonData jd = new JsonData();
        jd["name"] = root.name;
        jd["templateFilePath"] = Path.GetFullPath(templateFilePath);
        jd["codePath"] = Path.GetFullPath(codePath);
        StringBuilder SubWins = new StringBuilder();
        StringBuilder Btns = new StringBuilder();
        StringBuilder Txts = new StringBuilder();
        StringBuilder Sps = new StringBuilder();

        foreach (var item in childObj)
        {
            if (item.name.Contains("_subWin"))
            {

                SubWins.Append(item.name);
                SubWins.Append("&");

                SubWins.Append(GetGameObjectPath(root, item));//path
                SubWins.Append("|");
                continue;
            }
            if (item.GetComponent<Button>() != null)
            {

                Btns.Append(item.name);
                Btns.Append("&");
                Btns.Append(GetGameObjectPath(root, item));//Path

                Btns.Append("|");
                continue;
            }
            if (item.GetComponent<Text>() != null)
            {

                Txts.Append(item.name);

                Txts.Append("&");
                Txts.Append(GetGameObjectPath(root, item));//path



                Txts.Append("|");
                continue;
            }
            if (item.GetComponent<Sprite>() != null)
            {
                Sps.Append(item.name);
                Sps.Append("&");
                Sps.Append(GetGameObjectPath(root, item));
                Sps.Append("|");
                continue;
            }
        }
        jd["SubWins"] = SubWins.ToString();
        jd["Btns"] = Btns.ToString();
        jd["Txts"] = Txts.ToString();
        jd["Sps"] = Sps.ToString();

        string param = JsonHelper.ToJson(jd);
        string toolPath = Path.Combine("../", "Tools/GeneraCodeFile/bin/Debug/ConsoleApp1.exe");
        toolPath = Path.GetFullPath(toolPath);
        UnityEngine.Debug.Log(toolPath);
        Process pro = StartProcess(toolPath, param);
        pro.Start();

        string fingerprint = pro.StandardOutput.ReadToEnd();//.ReadLine();
        UnityEngine.Debug.Log(fingerprint);
        pro.WaitForExit();
        pro.Close();







    }

    public static Process StartProcess(string fileName, string args)
    {
        try
        {
            fileName = "\"" + fileName + "\"";
            //args = "\"" + args + "\"";
            Process myProcess = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo(fileName, args);
            startInfo.CreateNoWindow = true;
            startInfo.RedirectStandardInput = true;
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            myProcess.StartInfo = startInfo;
            return myProcess;
        }
        catch (Exception ex)
        {
            UnityEngine.Debug.Log("出错原因：" + ex.Message);
        }
        return null;
    }

    public static string GetGameObjectPath(GameObject root, GameObject obj)
    {
        string path = obj.name;
        while (obj.transform.parent != null)
        {
            if (obj.transform.parent == root.transform)
            {
                break;
            }
            obj = obj.transform.parent.gameObject;
            path = obj.name + "/" + path;
        }

        return path;
    }
}
