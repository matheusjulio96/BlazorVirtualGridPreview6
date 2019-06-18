using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Mono.WebAssembly.Interop;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace BlazorVirtualGridComponent
{
    public class BvgJsInterop
    {
        
        public static IJSRuntime jsRuntime;


        public static Task<bool> Alert(string msg)
        {

            return jsRuntime.InvokeAsync<bool>(
                "BvgJsFunctions.Alert", msg);
        }

        public static Task<double> GetElementActualWidth(string elementID)
        {
            return jsRuntime.InvokeAsync<double>(
                "BvgJsFunctions.GetElementActualWidth", elementID);
        }

        public static Task<double> GetElementActualHeight(string elementID)
        {
            return jsRuntime.InvokeAsync<double>(
                "BvgJsFunctions.GetElementActualHeight", elementID);
        }

        public static Task<double> GetElementActualTop(string elementID)
        {
            return jsRuntime.InvokeAsync<double>(
                "BvgJsFunctions.GetElementActualTop", elementID);
        }

        public static Task<double> GetWindowHeight()
        {
            return jsRuntime.InvokeAsync<double>(
                "BvgJsFunctions.GetWindowHeight");
        }
        

        public static Task<bool> SetElementScrollLeft(string elementID, double val)
        {
            return jsRuntime.InvokeAsync<bool>(
                "BvgJsFunctions.SetElementScrollLeft", elementID, val);
        }

        public static Task<bool> SetElementScrollTop(string elementID, double val)
        {
            return jsRuntime.InvokeAsync<bool>(
                "BvgJsFunctions.SetElementScrollTop", elementID, val);
        }


        public static Task<bool> SetDivsScrollTop(double val)
        {
            return jsRuntime.InvokeAsync<bool>(
                "BvgJsFunctions.SetDivsScrollTop", val);
        }

        public static Task<double> GetElementScrollLeft(string elementID)
        {
           
            return jsRuntime.InvokeAsync<double>(
                "BvgJsFunctions.GetElementScrollLeft", elementID);
        }

        public static Task<double> GetElementScrollTop(string elementID)
        {

            return jsRuntime.InvokeAsync<double>(
                "BvgJsFunctions.GetElementScrollTop", elementID);
        }


        public static Task<bool> SetFocus(string elementID)
        {
            return jsRuntime.InvokeAsync<bool>(
                "BvgJsFunctions.SetFocus", elementID);
        }


        public static bool UpdateRowContentBatch(string[] updatepkg)
        {


            if (jsRuntime is MonoWebAssemblyJSRuntime mono)
            {

                return mono.InvokeUnmarshalled<string, bool>(
                    "BvgJsFunctions.UpdateRowContentBatch",
                    JsonSerializer.ToString(updatepkg));
            }


            return false;
        }


        public static bool UpdateCellClassBatch(string[] updatepkg)
        {


            if (jsRuntime is MonoWebAssemblyJSRuntime mono)
            {

                return mono.InvokeUnmarshalled<string, bool>(
                    "BvgJsFunctions.UpdateCellClassBatch",
                    JsonSerializer.ToString(updatepkg));
            }


            return false;
        }


        public static bool UpdateCellClassBatchMonoByteArray(string[] pkgIDs, string[] updatepkg)
        {

            if (jsRuntime is MonoWebAssemblyJSRuntime mono)
            {

                return mono.InvokeUnmarshalled<byte[], byte[], bool>(
                    "BvgJsFunctions.UpdateCellClassBatchMonoByteArray",
                     Encoding.UTF8.GetBytes(JsonSerializer.ToString(pkgIDs)),
                     Encoding.UTF8.GetBytes(JsonSerializer.ToString(updatepkg)));
            }

            return false;
        }


        public static bool UpdateRowWidthsBatch(string[] pkgIDs, string[] updatepkg)
        {
            if (jsRuntime is MonoWebAssemblyJSRuntime mono)
            {

                return mono.InvokeUnmarshalled<string, string, bool>(
                    "BvgJsFunctions.UpdateRowWidthsBatch",
                    JsonSerializer.ToString(pkgIDs),
                    JsonSerializer.ToString(updatepkg));
            }
            return false;
        }


        public static bool UpdateColContentsBatch(string[] updatepkg)
        {


            if (jsRuntime is MonoWebAssemblyJSRuntime mono)
            {

                return mono.InvokeUnmarshalled<string, bool>(
                    "BvgJsFunctions.UpdateColContentsBatch",
                    JsonSerializer.ToString(updatepkg));
            }


            return false;
        }


        public static bool UpdateElementContentBatchMonoByteArray(string[] pkgIDs, string[] updatepkg)
        {

            if (jsRuntime is MonoWebAssemblyJSRuntime mono)
            {

                return mono.InvokeUnmarshalled<byte[], byte[], bool>(
                    "BvgJsFunctions.UpdateRowContentBatchMonoByteArray",
                    Encoding.UTF8.GetBytes(JsonSerializer.ToString(pkgIDs)),
                    Encoding.UTF8.GetBytes(JsonSerializer.ToString(updatepkg)));
            }

            return false;
        }

        public static bool SetAttributeBatch(string[] updatepkg, string attr)
        {


            if (jsRuntime is MonoWebAssemblyJSRuntime mono)
            {

                return mono.InvokeUnmarshalled<string, string, bool>(
                    "BvgJsFunctions.SetAttributeBatch",
                    JsonSerializer.ToString(updatepkg), attr);
            }


            return false;
        }


        public static Task<bool> SetValueToCheckBox(string el, string val)
        {



            return jsRuntime.InvokeAsync<bool>(
                 "BvgJsFunctions.SetValueToCheckBox", el, val);
        }

        

        public static bool UpdateStyle(string el, string val)
        {
            if (jsRuntime is MonoWebAssemblyJSRuntime mono)
            {
                return mono.InvokeUnmarshalled<string,string, bool>(
                    "BvgJsFunctions.UpdateStyle",
                    el, val);
            }
            return false;

        }

        internal static Task<bool> HandleDrag(string elementID, int id, object dotnetHelper)//DotNetObjectRef dotnetHelper
        {
            return jsRuntime.InvokeAsync<bool>(
                "BvgJsFunctions.handleDragStart", elementID, id, dotnetHelper);
        }


        internal static Task<bool> HandleDrop(string elementID, int id, object dotnetHelper)
        {
            return jsRuntime.InvokeAsync<bool>(
                "BvgJsFunctions.handleDrop", elementID, id, dotnetHelper);
        }



    }
}
