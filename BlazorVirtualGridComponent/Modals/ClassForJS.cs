﻿using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorVirtualGridComponent.Modals
{
    public class ClassForJS
    {

        public Action<int> CustomOnDragStart { get; set; }
        public Action<int, int> CustomOnDrop { get; set; }

        [JSInvokable]
        public void InvokeDragStartFromJS(int id)
        {
            CustomOnDragStart?.Invoke(id);
        }

        [JSInvokable]
        public void InvokeDropFromJS(int parentID, string id)
        {
            Console.WriteLine($"InvokeDropFromJS:{id}");
            CustomOnDrop?.Invoke(parentID,int.Parse(id));
        }
    }
}
