using System;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

using Wacom_feel_Multi_Touch_Test.WacomMT;
using Wacom_feel_Multi_Touch_Test.WacomMT.WacomMTEnums;
using Wacom_feel_Multi_Touch_Test.WacomMT.WacomMTStructures;

using static System.Console;

namespace Wacom_feel_Multi_Touch_Test {
	class WacomMultiTouchTestMain {

		/// <summary>
		/// デバイスのIDの配列
		/// </summary>
		private static int[] _Ids = null;

		/// <summary>
		/// メインメソッド
		/// </summary>
		/// <param name="args">プログラム引数</param>
		[STAThread]
		static void Main(string[] args) {
			CursorVisible = false;
			try {
				WacomMTError error = WacomMTAPI.WacomMTInitialize();

				if (error == WacomMTError.WMTErrorSuccess) {

					_Ids = WacomMTAPI.WacomMTGetAttachedDeviceIDs();

					foreach (int id in _Ids) {
						WriteLine("Tablet Device #" + id);
						WacomMTCapability capability = WacomMTAPI.WacomMTGetDeviceCapabilities(id);
						WriteLine("Virsion : " + capability.Version);
						WriteLine("DeviceID : " + capability.DeviceID);
						WriteLine("Type : " + capability.Type);
						WriteLine("LogicalOriginX : " + capability.LogicalOriginX);
						WriteLine("LogicalOriginY : " + capability.LogicalOriginY);
						WriteLine("LogicalWidth : " + capability.LogicalWidth);
						WriteLine("LogicalHeight : " + capability.LogicalHeight);
						WriteLine("PhysicalSizeX : " + capability.PhysicalSizeX);
						WriteLine("PhysicalSizeY : " + capability.PhysicalSizeY);
						WriteLine("ReportedSizeX : " + capability.ReportedSizeX);
						WriteLine("ReportedSizeY : " + capability.ReportedSizeY);
						WriteLine("ScanSizeX : " + capability.ScanSizeX);
						WriteLine("ScanSizeY : " + capability.ScanSizeY);
						WriteLine("FingerMax : " + capability.FingerMax);
						WriteLine("BlobMax : " + capability.BlobMax);
						WriteLine("BlobPointsMax : " + capability.BlobPointsMax);
						WriteLine("CapabilityFlags : " + capability.CapabilityFlags);
						WriteLine();
					}

					error = WacomMTAPI.WacomMTRegisterFingerReadCallback(_Ids[0], IntPtr.Zero, WacomMTProcessingMode.WMTProcessingModeNone, FingerCallback, IntPtr.Zero);

					if (error != WacomMTError.WMTErrorSuccess) throw new Exception("Errpr WacomMTRegisterFingerReadCallback : " + error);

				} else {
					throw new Exception("Error WacomMTInitialize : " + error);
				}
			} catch (Exception e) {
				SetCursorPosition(0, CursorTop + 2);
				WriteLine(e.Message);
			}

			using (GrandSlider slider = new GrandSlider()) {
				slider.Run(60);
			}

			/**
			do {
				Thread.Sleep(10);
			} while (!Keyboard.IsKeyDown(Key.Enter));
			*/
				WacomMTAPI.WacomMTQuit();
		}

		public static UInt16 TouchState = 0;

		public static UInt16 HoldState = 0;

		private static int FingerCallback(IntPtr fingerPacket, IntPtr userData) {
			WacomMTFingerCollectionClass fingerCollection = new WacomMTFingerCollectionClass(fingerPacket);
			/*
			WriteLine("DeviceID : " + fingerCollection.DeviceID);
			WriteLine("-Version : " + fingerCollection.Version);
			WriteLine("-FrameNumber : " + fingerCollection.FrameNumber);
			WriteLine("-FingerCount : " + fingerCollection.FingerCount);
			*/

			TouchState = 0;
			foreach (WacomMTFinger finger in fingerCollection.FingerData) {
				/*
				WriteLine("-FingerID : " + finger.FingerID);
				WriteLine("--X : " + finger.X);
				WriteLine("--Y : " + finger.Y);
				WriteLine("--Width : " + finger.Width);
				WriteLine("--Hight : " + finger.Height);
				WriteLine("--Sensitivity : " + finger.Sensitivity);
				WriteLine("--Orientation : " + finger.Orientation);
				WriteLine("--Confidence : " + finger.Confidence);
				WriteLine("--TouchState : " + finger.TouchState);
				WriteLine();
				*/
				int underPanelIndex = (int)((finger.X - (finger.Width / 2)) * 16);
				int centerpanelIndex = (int)(finger.X * 16);
				int topPanelIndexs = (int)((finger.X + (finger.Width / 2)) * 16);

				switch(finger.TouchState) {
					case WacomMTFingerState.WMTFingerStateDown:
						for (int i = underPanelIndex; i <= topPanelIndexs; i++) TouchState = (UInt16)(TouchState | (1 << i));
						//TouchState = (UInt16)(TouchState | (1 << panelIndex));
						break;
					case WacomMTFingerState.WMTFingerStateHold:
						for (int i = underPanelIndex; i <= topPanelIndexs; i++) TouchState = (UInt16)(TouchState | (1 << i));
						//TouchState = (UInt16)(TouchState | (1 << panelIndex));
						break;
					case WacomMTFingerState.WMTFingerStateUp:
						break;
				}

			}
			
			char[] chars = Convert.ToString(TouchState, 2).ToCharArray();
			Array.Reverse(chars);
			string text = new string(chars);
			for (int i = text.Length; i < 16; i++) text += "0";
			string text2 = "";
			foreach (char c in text.ToCharArray()) text2 += (c == '0' ? "□" : "■");
			WriteLine(text2);
			
			//WriteLine();
			return 0;
		}

	}
	
	class GrandSlider : GameWindow {

		public GrandSlider() : base (800,100,GraphicsMode.Default, "GrandSlider") {

		}

		protected override void OnLoad(EventArgs e) {
			base.OnLoad(e);
			GL.ClearColor(Color4.Black);
		}

		protected override void OnResize(EventArgs e) {
			base.OnResize(e);

			GL.Viewport(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width, ClientRectangle.Height);
			GL.Viewport(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width, ClientRectangle.Height);
			GL.MatrixMode(MatrixMode.Projection);
			Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4, Width / Height, 1.0f, 64.0f);
			GL.LoadMatrix(ref projection);
		}

		protected override void OnRenderFrame(FrameEventArgs e) {
			base.OnRenderFrame(e);

			GL.Clear(ClearBufferMask.ColorBufferBit);

			GL.MatrixMode(MatrixMode.Projection);
			GL.LoadIdentity();

			Matrix4 view = Matrix4.CreateOrthographicOffCenter(0, 1024, 100, 0, 0, 10);
			GL.MultMatrix(ref view);

			GL.Disable(EnableCap.Normalize);
			GL.Disable(EnableCap.Lighting);
			GL.Disable(EnableCap.CullFace);
			GL.Disable(EnableCap.DepthTest);

			for (int i = 0; i < 16; i++) {

				GL.Color4((WacomMultiTouchTestMain.TouchState & (1 << i)) > 0 ? Color4.Pink : Color4.Orange);
				GL.Begin(PrimitiveType.Quads);
				GL.Vertex2(64 * i, 0);
				GL.Vertex2(64 * i, 100);
				GL.Vertex2(64 * (i + 1), 100);
				GL.Vertex2(64 * (i + 1), 0);
				GL.End();
				GL.Color4(Color4.White);
				GL.Begin(PrimitiveType.Lines);
				GL.Vertex2(64 * (i + 1), 0);
				GL.Vertex2(64 * (i + 1), 100);
				GL.End();
			}

			GL.Enable(EnableCap.DepthTest);
			GL.Enable(EnableCap.CullFace);
			GL.Enable(EnableCap.Lighting);
			GL.Enable(EnableCap.Normalize);

			GL.Flush();

			SwapBuffers();
			
		}

	}









}
