﻿using BDFramework;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;
using BDFramework.Mgr;
using System.Linq;
using BDFramework.GameStart;
using BDFramework.UFlux;
using BDFramework.UnitTest;


public class BDLauncherBridge
{
    static private IGameStart hotfixStart = null;

    /// <summary>
    /// 这里注册整个游戏类型
    /// </summary>
    /// <param name="isILRMode"></param>
    static public void Start(bool isILRMode = false, bool isRefMode = false)
    {
        BDebug.Log("解释执行:" + isILRMode, "yellow");
        //组件加载
        List<Type> allTypes = new List<Type>();
        //编辑器环境下 寻找dll
        if (isILRMode)
        {
            allTypes = ILRuntimeHelper.GetHotfixTypes();
        }
        else
        {
            //获取DLL ALLtype
            var assembly = Assembly.GetAssembly(typeof(BDLauncherBridge));
            if (assembly == null)
            {
                Debug.Log("当前dll is null");
            }

            allTypes = assembly.GetTypes().ToList();
        }

        //
        var mgrs = new List<IMgr>();

        var gsaType = typeof(GameStartAtrribute);
        //寻找所有的管理器
        allTypes = allTypes.Distinct().ToList();
        foreach (var t in allTypes)
        {
            if (t != null 
                && t.BaseType != null 
                && t.BaseType.FullName != null
                && t.BaseType.FullName.Contains(".ManagerBase`2"))
            {
                BDebug.Log("加载管理器-" + t, "green");
                var i = t.BaseType.GetProperty("Inst").GetValue(null, null) as IMgr;
                mgrs.Add(i);
                continue;
            }

            //游戏启动器
            //这里主要寻找
            if (hotfixStart == null)
            {
                var attrs = t.GetCustomAttributes(gsaType, false);
                if (attrs.Length > 0 
                    && attrs[0] is GameStartAtrribute
                    &&((GameStartAtrribute)attrs[0]).Index==1)
                {
                    hotfixStart = Activator.CreateInstance(t) as IGameStart;
                }
            }
            
        }

        
  
        //类型注册
        foreach (var t in allTypes)
        {
            foreach (var iMgr in mgrs)
            {
                iMgr.CheckType(t);
            }
        }

        //管理器初始化
        foreach (var m in mgrs)
        {
            m.Init();
        }
        
        //gamestart生命注册
        if (hotfixStart != null)
        {
            hotfixStart.Start();
            BDLauncher.OnUpdate = hotfixStart.Update;
            BDLauncher.OnLateUpdate = hotfixStart.LateUpdate;
        }
        //执行框架初始化完成的测试
        BDLauncher.OnBDFrameInitialized?.Invoke();
        BDLauncher.OnBDFrameInitializedForTest?.Invoke();
        //所有管理器开始工作
        foreach (var m in mgrs)
        {
            m.Start();
        }


         IEnumeratorTool.WaitingForExec(5, () =>
         {
             //执行单元测试
             if (BDLauncher.Inst.GameConfig.IsExcuteHotfixUnitTest && ILRuntimeHelper.IsRunning)
             {
                 HotfixTestRunner.RunHotfixUnitTest();
             }
         });
  
    }
}