﻿using System;
using Assignment.Interfaces;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Assignment.ViewModels
{
	public class EditorViewModel : BindableObject
	{
        private static string CUSTOM_HTML = @"
            <html>
                    <head>
                        <script src=""js/init.js""></script>
                        <title></title>
                    </head>
                    <body>
                        <div>
                            Welcome to Xamarin.Forms!
                            <p>
                            <input type=""button"" value=""Eval C#"" onclick=""evalCSharp();"" />
                        </div>
                    </body> 
                </html>";


        public Func<string, Task<string>> EvaluateJavascript { get; set; }

        public WebViewSource CustomSource
        {
            get
            {
                var root = DependencyService.Get<IBaseUrl>().Get();
                string url = $"{root}index.html";
                return new UrlWebViewSource()
                {
                    Url = url,
                };
            }
        }

        public ICommand EvalJS
        {
            get
            {
                return new Command(async () =>
                {
                    var result = await EvaluateJavascript("evalCSharp();");
                });
            }
        }

        public ICommand EvalJS2
        {
            get
            {
                return new Command(async () =>
                {
                    string arg1 = "this is arg1";
                    string arg2 = "this is arg2";
                    var result = await EvaluateJavascript("evalCSharpArgs('" + arg1 + "', '" + arg2 + "');");
                });
            }
        }
    }


}

