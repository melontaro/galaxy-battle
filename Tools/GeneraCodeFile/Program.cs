using RazorEngine;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using RazorEngine.Configuration;
using RazorEngine.Templating;
using System.IO;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string param = "";
            if (args.Length > 0)
            {
                Console.WriteLine(args[0].ToString());

                param = args[0].ToString();
            }
            //Console.WriteLine("llll");

            // param = @"{name:Window_Map,templateFilePath:D:\\Workspace\\Unity3d\\clickfish\\Tools\\CodeTemplate\\Window\\BaseWindow.cshtml,codePath:D:\\Workspace\\Unity3d\\clickfish\\BDFramework.Core\\Assets\\ClickFish\\Game@hotfix\\UI,SubWins:,Btns:btn_kill&btn_kill|btn_back2lobby&btn_back2lobby|btnStart&btnStart|,Txts:Text&Text|Text&Text|txt_info&txt_info|Text&Text|,Sps:}
            //";
            //  param = @"{name:Window_Map,templateFilePath:D:\\Workspace\\Unity3d\\clickfish\\Tools\\CodeTemplate\\Window\\\BaseWindow1.cshtml,codePath:D:\\Workspace\\Unity3d\\clickfish\\BDFramework.Core\\Assets\\ClickFish\\Game@hotfix\\UI,SubWins:,Btns:btn_kill&btn_kill|btn_back2lobby&btn_back2lobby|btnStart&btnStart|,Txts:Text&Text|Text&Text|txt_info&txt_info|Text&Text|,Sps:}
            //";
            //param = @"{name:Window_Map,templateFilePath:D:\\workspace\\clickfish\\Tools\\CodeTemplate\\Window\\BaseWindow1.cshtml,codePath:D:\\workspace\\clickfish\\BDFramework.Core\\Assets\\ClickFish\\Game@hotfix\\UI,SubWins:,Btns:btn_kill&btn_kill|
            //btn_back2lobby&btn_back2lobby|btnStart&btnStart|,Txts:Text&Text|Text&Text|txt_info&txt_info|Text&Text|,Sps:}";
            GeneralCodeFile(param);

        }
        private static void GeneralCodeFile(string param)
        {
            try
            {
                if (string.IsNullOrEmpty(param))
                {
                    Console.WriteLine("param 参数不能为空");
                    return;
                }
                param = param.Replace("{", "").Replace("}", "");

                string[] params1 = param.Split(',');
                Dictionary<string, string> paramDic = new Dictionary<string, string>();
                for (int i = 0; i < params1.Length; i++)
                {
                    string[] item = params1[i].Split(':');
                    if (item.Length == 2)
                    {
                        paramDic.Add(item[0].Trim(), item[1].Trim());
                    }
                    else if (item.Length == 3)//防止出现这种"path:D:\\xx\xxx"
                    {
                        paramDic.Add(item[0].Trim(), item[1].Trim() + ":" + item[2].Trim());
                    }

                }

                string winFullName = paramDic["name"];
                string shortName = winFullName.Replace("Window_", "");

                /* */
                Console.WriteLine("fullName ; " + winFullName);
                //Console.WriteLine(shortName);
                string template = File.ReadAllText(paramDic["templateFilePath"]);


                dynamic viewBag = new ExpandoObject();
                viewBag.Name = shortName;// paramDic["name"];

                string[] SubWinsStr = paramDic["SubWins"].Split('|');
                string[] BtnsStr = paramDic["Btns"].Split('|');
                string[] TxtsStr = paramDic["Txts"].Split('|');
                string[] SpsStr = paramDic["Sps"].Split('|');

                List<ExpandoObject> SubWinsObj = new List<ExpandoObject>();
                List<ExpandoObject> BtnsObj = new List<ExpandoObject>();
                List<ExpandoObject> TxtsObj = new List<ExpandoObject>();
                List<ExpandoObject> SpsObj = new List<ExpandoObject>();

                for (int i = 0; i < SubWinsStr.Length; i++)
                {
                    if (string.IsNullOrEmpty(SubWinsStr[i]))
                    {
                        continue;
                    }
                    string[] items = SubWinsStr[i].Split('&');
                    dynamic itemObj = new ExpandoObject();
                    itemObj.Name = items[0];
                    itemObj.Path = items[1];

                    SubWinsObj.Add(itemObj);
                }
                for (int i = 0; i < BtnsStr.Length; i++)
                {
                    if (string.IsNullOrEmpty(BtnsStr[i]))
                    {
                        continue;
                    }
                    string[] items = BtnsStr[i].Split('&');
                    dynamic itemObj = new ExpandoObject();
                    itemObj.Name = items[0];
                    itemObj.Path = items[1];

                    BtnsObj.Add(itemObj);
                }
                for (int i = 0; i < TxtsStr.Length; i++)
                {
                    if (string.IsNullOrEmpty(TxtsStr[i]))
                    {
                        continue;
                    }
                    string[] items = TxtsStr[i].Split('&');
                    dynamic itemObj = new ExpandoObject();
                    itemObj.Name = items[0];
                    itemObj.Path = items[1];

                    TxtsObj.Add(itemObj);
                }
                for (int i = 0; i < SpsStr.Length; i++)
                {
                    if (string.IsNullOrEmpty(SpsStr[i]))
                    {
                        continue;
                    }
                    string[] items = SpsStr[i].Split('&');
                    dynamic itemObj = new ExpandoObject();
                    itemObj.Name = items[0];
                    itemObj.Path = items[1];

                    SpsObj.Add(itemObj);
                }



                viewBag.SubWins = SubWinsObj;
                viewBag.Btns = BtnsObj;
                viewBag.Txts = TxtsObj;
                viewBag.Sps = SpsObj;





                string result = Razor.Parse(template, viewBag);
                if (result.Contains("<a>"))
                {
                    result = result.Replace("<a>", "").Replace("</a>", "");
                }
                string codePath = paramDic["codePath"];
                string direc = codePath + @"\" + winFullName;
                if (!Directory.Exists(direc))
                {
                    Directory.CreateDirectory(direc);
                }
                codePath = direc + @"\" + paramDic["name"] + ".cs";

                File.WriteAllText(codePath, result);
                Console.WriteLine(result);
                Console.ReadKey();
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
        }
    }
}
