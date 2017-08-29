using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

namespace ScriptTest {
	public class Program {
		static void Main(string[] args) {
			switch (args[0]) {
				case "-CSharp":
					//CSharp();
					GlobalObjectTest();
					break;
			}


			Console.WriteLine("Sleep");
			Thread.Sleep(100000);

		}

		static void CSharp() {
			var script = CSharpScript.Create("System.Console.WriteLine(\"HelloWorld!\");");
			long oldTime = 0, newTime = 0;
			var timer = new Stopwatch();
			timer.Start();
			for (int i = 0; i < 10; i++) {
				script.RunAsync().Wait();
				newTime = timer.ElapsedMilliseconds;
				Console.WriteLine("Interval {0}:{1}ms.", i, newTime - oldTime);
				oldTime = newTime;
			}
			timer.Stop();

			Console.WriteLine("Total:{0}ms.", timer.ElapsedMilliseconds);

		}

		public static void GlobalObjectTest() {
			var script = CSharpScript.Create(Script, ScriptOptions.Default.AddReferences(Assembly.GetAssembly(typeof(Program))), typeof(GlobalObject));
			var obj = new GlobalObject();
			script.RunAsync(obj).Wait();
			obj.InvokeEvent();
		}



		static string Script =
			"ScriptTest.Program.GlobalObjectTest();";
	}

	public class GlobalObject {
		public event Action NoteTap;
		public event Action Miss;

		public void Println(string text) => Console.WriteLine(text);

		public void InvokeEvent() {
			NoteTap.Invoke();
			Miss.Invoke();
		}
	}

}
