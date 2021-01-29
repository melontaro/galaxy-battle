using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace ETModel
{
    public sealed class Hotfix : Object
    {
        private ILRuntime.Runtime.Enviorment.AppDomain appDomain;
        private MemoryStream dllStream;
        private MemoryStream pdbStream;


        private IStaticMethod start;
        private List<Type> hotfixTypes;

        public Action Update;
        public Action LateUpdate;
        public Action OnApplicationQuit;

        public void GotoHotfix()
        {
            ILHelper.InitILRuntime(this.appDomain);

            this.start.Run();
        }

        public List<Type> GetHotfixTypes()
        {
            return this.hotfixTypes;
        }

        public void LoadHotfixAssembly()
        {
            Game.Scene.GetComponent<ResourcesComponent>().LoadBundle($"code.unity3d");
            GameObject code = (GameObject)Game.Scene.GetComponent<ResourcesComponent>().GetAsset("code.unity3d", "Code");

            byte[] assBytes = code.Get<TextAsset>("Hotfix.dll").bytes;
            byte[] pdbBytes = code.Get<TextAsset>("Hotfix.pdb").bytes;

            Log.Debug($"当前使用的是ILRuntime模式");
            this.appDomain = new ILRuntime.Runtime.Enviorment.AppDomain();

            this.dllStream = new MemoryStream(assBytes);
            this.pdbStream = new MemoryStream(pdbBytes);
            this.appDomain.LoadAssembly(this.dllStream, this.pdbStream, new Mono.Cecil.Pdb.PdbReaderProvider());

            this.start = new ILStaticMethod(this.appDomain, "ETHotfix.Init", "Start", 0);

            this.hotfixTypes = this.appDomain.LoadedTypes.Values.Select(x => x.ReflectionType).ToList();


            Game.Scene.GetComponent<ResourcesComponent>().UnloadBundle($"code.unity3d");
        }
    }
}