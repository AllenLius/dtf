﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dtf.Endpoint.Win
{
    using System.ServiceModel;
    using Dtf.Core;

    [ServiceContract]
    public interface IWinAutomation
    {
        [OperationContract]
        int Process_Start(string fileName, string arguments, string workingDirectory);

        [OperationContract]
        void Process_Close(int processId);

        [OperationContract]
        bool UiObject_Exists(string ui, TimeSpan timeout);
        
        [OperationContract]
        Rect UiObject_GetRect(string ui);

        [OperationContract]
        string UiObject_GetProperty(string ui, string propertyName);

        [OperationContract]
        string[] UiObject_GetProperties(string ui);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ui">Subtree of UI</param>
        [OperationContract]
        string UiObject_GetUi(string ui);

        //[OperationContract]
        //void UiObject_Wait(string ui, 

        [OperationContract]
        void MousePattern_Click(MouseButton mouseButton);

        [OperationContract]
        void MousePattern_ClickOn(MouseButton mouseButton, string ui);

        [OperationContract]
        void MousePattern_Down(MouseButton mouseButton);

        [OperationContract]
        void MousePattern_Move(int x ,int y);

        [OperationContract]
        void MousePattern_Up(MouseButton mouseButton);

        [OperationContract]
        void InvokePattern_Invoke(string ui);

        [OperationContract]
        void ValuePattern_SetValue(string ui, string value);

        [OperationContract]
        void TreeWalker_Set(string filter);

        #region IResourceManager
        /// <summary>
        /// Get resource used by UiElement 
        /// </summary>
        /// <param name="handlerType"></param>
        /// <param name="resourceKey"></param>
        /// <returns>String resource text or serialized object</returns>
        [OperationContract]
        string ResourceManager_GetObject(string handlerType, string resourceKey);
        #endregion
    }
}
