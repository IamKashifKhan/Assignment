using System;
using Android.Webkit;
using Assignment.Droid.Renderers;
using Java.Interop;
using Java.Lang;

namespace Assignment.Droid.Objects
{
    public class CustomJavaScriptInterface : Java.Lang.Object, IRunnable
    {
        readonly WeakReference<CustomWebViewRenderer> customWebViewRenderer;

        public CustomJavaScriptInterface(CustomWebViewRenderer customRenderer)
        {
            customWebViewRenderer = new WeakReference<CustomWebViewRenderer>(customRenderer);
        }

        public void Run()
        {
            // DO NOTHING
        }


        [Export("notify")]
        [JavascriptInterface]
        public void notify(Java.Lang.String result)
        {
            Console.WriteLine("JavaScript function calling C# function. The result is => " + result);
        }
    }
}