using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

using ILRuntime.CLR.TypeSystem;
using ILRuntime.CLR.Method;
using ILRuntime.Runtime.Enviorment;
using ILRuntime.Runtime.Intepreter;
using ILRuntime.Runtime.Stack;
using ILRuntime.Reflection;
using ILRuntime.CLR.Utils;

namespace ILRuntime.Runtime.Generated
{
    unsafe class BDFramework_GameConfig_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            FieldInfo field;
            Type[] args;
            Type type = typeof(BDFramework.GameConfig);

            field = type.GetField("IsExcuteHotfixUnitTest", flag);
            app.RegisterCLRFieldGetter(field, get_IsExcuteHotfixUnitTest_0);
            app.RegisterCLRFieldSetter(field, set_IsExcuteHotfixUnitTest_0);


        }



        static object get_IsExcuteHotfixUnitTest_0(ref object o)
        {
            return ((BDFramework.GameConfig)o).IsExcuteHotfixUnitTest;
        }
        static void set_IsExcuteHotfixUnitTest_0(ref object o, object v)
        {
            ((BDFramework.GameConfig)o).IsExcuteHotfixUnitTest = (System.Boolean)v;
        }


    }
}
