
using System;
using System.Collections;
using System.Collections.Generic;
using ILRuntime.CLR.Method;
using ILRuntime.Runtime.Enviorment;
using ILRuntime.Runtime.Intepreter;

public class IMessageAdapter : CrossBindingAdaptor
{
    public override Type BaseCLRType
    {
        get
        {
            return typeof(Google.Protobuf.IMessage);//这是你想继承的那个类
        }
    }
    public override Type AdaptorType
    {
        get
        {
            return typeof(Adaptor);//这是实际的适配器类
        }
    }
    public override object CreateCLRInstance(ILRuntime.Runtime.Enviorment.AppDomain appdomain, ILTypeInstance instance)
    {
        return new Adaptor(appdomain, instance);//创建一个新的实例
    }
    //实际的适配器类需要继承你想继承的那个类，并且实现CrossBindingAdaptorType接口
    public class Adaptor : Google.Protobuf.IMessage, CrossBindingAdaptorType
    {
        ILTypeInstance instance;
        ILRuntime.Runtime.Enviorment.AppDomain appdomain;
        //缓存这个数组来避免调用时的GC Alloc
        object[] param1 = new object[1];
        public Adaptor()
        {

        }
        public Adaptor(ILRuntime.Runtime.Enviorment.AppDomain appdomain, ILTypeInstance instance)
        {
            this.appdomain = appdomain;
            this.instance = instance;
        }
        public ILTypeInstance ILInstance { get { return instance; } }
        bool m_bMergeFromGot = false;
        IMethod m_MergeFrom = null;
        public void MergeFrom(Google.Protobuf.CodedInputStream arg0)
        {
            if (!m_bMergeFromGot)
            {
                m_MergeFrom = instance.Type.GetMethod("MergeFrom", 1);
                m_bMergeFromGot = true;
            }
            if (m_MergeFrom != null)
            {
                appdomain.Invoke(m_MergeFrom, instance, arg0);
            }
            else
            {

            }
        }
        bool m_bWriteToGot = false;
        IMethod m_WriteTo = null;
        public void WriteTo(Google.Protobuf.CodedOutputStream arg0)
        {
            if (!m_bWriteToGot)
            {
                m_WriteTo = instance.Type.GetMethod("WriteTo", 1);
                m_bWriteToGot = true;
            }
            if (m_WriteTo != null)
            {
                appdomain.Invoke(m_WriteTo, instance, arg0);
            }
            else
            {

            }
        }
        bool m_bCalculateSizeGot = false;
        IMethod m_CalculateSize = null;
        public System.Int32 CalculateSize()
        {
            if (!m_bCalculateSizeGot)
            {
                m_CalculateSize = instance.Type.GetMethod("CalculateSize", 0);
                m_bCalculateSizeGot = true;
            }
            if (m_CalculateSize != null)
            {
                return (System.Int32)appdomain.Invoke(m_CalculateSize, instance, null);
            }
            else
            {
                return 0;
            }
        }
    }
}