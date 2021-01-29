﻿using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BDFramework.UFlux
{
    abstract public class AComponentAdaptor
    {
        protected Dictionary<string, Action<UIBehaviour, object>> setPropActionMap =
            new Dictionary<string, Action<UIBehaviour, object>>();

        protected Dictionary<string, Action<Transform, object>> setPropCustomAdaptorMap =
            new Dictionary<string, Action<Transform, object>>();

        public AComponentAdaptor()
        {
            Init();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public virtual void Init()
        {
            setPropActionMap[nameof(UIBehaviour.enabled)] = SetProp_Enable;
            setPropActionMap[nameof(UIBehaviour.gameObject.active)] = SetProp_Active;
        }

        /// <summary>
        /// 设置数据
        /// </summary>
        /// <param name="uiBehaviour"></param>
        /// <param name="propName"></param>
        /// <param name="propValue"></param>
        public virtual void SetData(UIBehaviour uiBehaviour, string propName, object propValue)
        {
            Action<UIBehaviour, object> action = null;
            this.setPropActionMap.TryGetValue(propName, out action);
            if (action != null)
            {
                action(uiBehaviour, propValue);
            }
            else
            {
                BDebug.LogError("不存在赋值字段:" + propName);
            }
        }

        /// <summary>
        /// 设置数据
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="propName"></param>
        /// <param name="propValue"></param>
        public virtual void SetData(Transform transform, string propName, object propValue)
        {
            Action<Transform, object> action = null;
            this.setPropCustomAdaptorMap.TryGetValue(propName, out action);
            if (action != null)
            {
                action(transform, propValue);
            }
            else
            {
                BDebug.LogError("不存在赋值字段:" + propName);
            }
        }

        /// <summary>
        /// enable
        /// </summary>
        /// <param name="propValue"></param>
        private void SetProp_Enable(UIBehaviour uiBehaviour, object propValue)
        {
            uiBehaviour.enabled = (bool) propValue;
        }

        /// <summary>
        /// active
        /// </summary>
        /// <param name="propValue"></param>
        private void SetProp_Active(UIBehaviour uiBehaviour, object propValue)
        {
            uiBehaviour.gameObject.SetActive((bool) propValue);
        }
    }
}