using System;
using System.Runtime.InteropServices;
using Wacom_feel_Multi_Touch_Test.WacomMT.WacomMTDelegates;
using Wacom_feel_Multi_Touch_Test.WacomMT.WacomMTEnums;
using Wacom_feel_Multi_Touch_Test.WacomMT.WacomMTStructures;


/// <summary>
/// wacommt.dllのラッパー関数群
/// </summary>
namespace Wacom_feel_Multi_Touch_Test.WacomMT {
	class WacomMTAPI {

		/// <summary>
		/// DLLの名前
		/// </summary>
		public const string DllName = "wacommt.dll";

		/// <summary>
		/// APIバージョン
		/// </summary>
		public const int WACOM_MULTI_TOUCH_API_VERSION = 4;

		/// <summary>
		/// ドライバーへの接続
		/// </summary>
		/// <param name="libAPIVersion">使用するAPIのバージョン</param>
		/// <returns><see cref="WacomMTError"/></returns>
		[DllImport(DllName, CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl, EntryPoint = "WacomMTInitialize")]
		protected static extern WacomMTError _WacomMTInitialize(Int32 libAPIVersion);

		/// <summary>
		/// ドライバーへの接続
		/// </summary>
		/// <returns><see cref="WacomMTError"/></returns>
		public static WacomMTError WacomMTInitialize() => _WacomMTInitialize(WACOM_MULTI_TOUCH_API_VERSION);


		/// <summary>
		/// ドライバーから切断します。
		/// </summary>
		[DllImport(DllName, CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
		public static extern void WacomMTQuit();



		/// <summary>
		/// システムに接続されているマルチタッチセンサーの数を取得します。
		/// </summary>
		/// <param name="deviceArray">不明</param>
		/// <param name="bufferSize">不明</param>
		/// <returns>システムに接続されているセンサーの数</returns>
		[DllImport(DllName, CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl, EntryPoint = "WacomMTGetAttachedDeviceIDs")]
		protected static extern int _WacomMTGetAttachedDeviceIDs(IntPtr deviceArray, UInt32 bufferSize);

		/// <summary>
		/// システムに接続されているマルチタッチセンサーのIDを取得します。
		/// </summary>
		/// <returns>センサーのIDの配列</returns>
		public static int[] WacomMTGetAttachedDeviceIDs() {
			int[] ids = new int[0];// IDの格納される配列
			int res = _WacomMTGetAttachedDeviceIDs(IntPtr.Zero, 0);//接続されているセンサーの数を取得

			if (res != 0) { //センサーが接続されていた場合

				IntPtr pIds = IntPtr.Zero;//ポインタを用意

				try {
					pIds = Marshal.AllocHGlobal(sizeof(int) * res);//IDを格納する配列のメモリを確保し、その先頭ポインタを取得
					res = _WacomMTGetAttachedDeviceIDs(pIds, sizeof(int) * (UInt32)res);//センサーのIDを取得
					ids = new int[(uint)res];//IDを格納する配列を用意

					IntPtr nowPtr = pIds;//ポインタのコピー(解放時に使用する)

					for (int i = 0; i < (uint)res; i++) {//値を取得
						ids[i] = (int)Marshal.PtrToStructure(nowPtr, typeof(int));//メモリからintのデータを取得する。
						nowPtr = (IntPtr)((long)nowPtr + sizeof(int));//ポインターを1データ分ずらす。
					}
				} catch (Exception e) {
					throw e;
				} finally {
					if (pIds != IntPtr.Zero) {//ポインタが0を指していない場合
						Marshal.FreeHGlobal(pIds);//メモリの解放
						pIds = IntPtr.Zero;//ポインタを0へ
					}
				}
			}

			return ids;
		}


		/// <summary>
		/// 呼び出し側が割り振ったWacomMTCapability構造体に、要求されたデバイス識別子のケイパビリティー情報を入れます。
		/// </summary>
		/// <param name="deviceID"></param>
		/// <param name="capabilityBuffer"></param>
		/// <returns></returns>
		[DllImport(DllName, CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl, EntryPoint = "WacomMTGetDeviceCapabilities")]
		protected static extern WacomMTError _WacomMTGetDeviceCapabilities(int deviceID, IntPtr capabilityBuffer);

		/// <summary>
		/// 要求されたデバイス識別子のケイパビリティー情報を取得します。
		/// </summary>
		/// <param name="deviceID">デバイス識別子</param>
		/// <returns><see cref="WacomMTCapability"/>構造体</returns>
		public static WacomMTCapability WacomMTGetDeviceCapabilities(int deviceID) {
			IntPtr capabilitiesPtr = IntPtr.Zero;//ポインタの用意

			try {
				capabilitiesPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(WacomMTCapability)));//構造体を格納するメモリを確保
				WacomMTError error = _WacomMTGetDeviceCapabilities(deviceID, capabilitiesPtr);//デバイスの情報を取得

				if (error != WacomMTError.WMTErrorSuccess) {//失敗したら例外を発生させる
					if (capabilitiesPtr != IntPtr.Zero) { //メモリが0以外を指していたら
						Marshal.FreeHGlobal(capabilitiesPtr);//メモリを解放
						capabilitiesPtr = IntPtr.Zero;//ポインタを0に
					}
					throw new Exception("Error WacomMTGetDeviceCapabilities : " + error);
				}

				return (WacomMTCapability)Marshal.PtrToStructure(capabilitiesPtr, typeof(WacomMTCapability));
			} catch (Exception e) {
				throw e;
			} finally {
				if (capabilitiesPtr != IntPtr.Zero) {//メモリが0以外を指していたら
					Marshal.FreeHGlobal(capabilitiesPtr);//メモリ解放
					capabilitiesPtr = IntPtr.Zero;//ポインタを0に
				}
			}
		}

		/// <summary>
		/// この関数は、新しいタッチデバイスの接続時に呼び出すコールバック関数を登録できます。
		/// 登録すると、APIは、現在接続されているデバイスごとにコールバックを発行します。各プロセスに登録できるアタッチコールバックは1つのみです。
		/// アタッチコールバックをキャンセルするには、プロセスはNULLを使用して、この関数を呼び出すことができます。
		/// </summary>
		/// <param name="attachCallback">この関数は、接続されているデバイスのWacomMTCapability構造体と、呼び出しの登録時に提供されたuserDataを使用して呼び出されます。</param>
		/// <param name="userData"><see cref="WacomMTError"/></param>
		/// <returns></returns>
		[DllImport(DllName, CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
		public static extern WacomMTError WacomMTRegisterAttachCallback([MarshalAs(UnmanagedType.FunctionPtr)] WMT_ATTACH_CALLBACK attachCallback, IntPtr userData);

		/// <summary>
		/// この関数は、タッチデバイスの切り離し時に呼び出すコールバック関数を登録できます。各プロセスに登録できるデタッチコールバックは1つのみです。
		/// デタッチコールバックをキャンセルするには、プロセスはNULLを使用して、この関数を呼び出すことができます。
		/// </summary>
		/// <param name="detachCallback">この関数は、切り離されているセンサーのdeviceIDと、呼び出しの登録時に提供されたuserDataを使用して呼び出されます。</param>
		/// <param name="userData"><see cref="WacomMTError"/></param>
		/// <returns></returns>
		[DllImport(DllName, CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
		public static extern WacomMTError WacomMTRegisterDetachCallback([MarshalAs(UnmanagedType.FunctionPtr)] WMT_DETACH_CALLBACK detachCallback, IntPtr userData);

		/// <summary>
		/// この関数を使用すると、指タッチパケットの準備が整って要求されたデバイスのヒット矩形内にあるときに呼び出すコールバック関数を登録できます。
		/// プロセスは必要なだけのコールバックを作成できますが、デバイスヒット矩形ごとに作成できるコールバックは1つのみです。
		/// コールバックをキャンセルする場合は、この関数にNULLを指定して既存のデバイスヒット矩形に対して呼び出します。コールバックは、作成された順に処理されます。
		/// </summary>
		/// <param name="deviceID">デバイスのdeviceID。</param>
		/// <param name="hitRect">データをヒットテストするために使用される矩形<see cref="WacomMTHitRect"/>構造体。指がヒット矩形内に接触し始めると、コールバックが呼び出されます。
		/// コールバックは、デバイスとの接触がなくなるまで、その指に対して呼び出され続けます。
		/// 接触がヒット矩形の外側で始まる場合、コールバックはその指に対しては呼び出されません。
		/// NULLの矩形は、デバイスの表面全体を想定します。ヒット矩形は、デバイスの論理単位内で定義されます。
		/// 論理単位の定義については、<see cref="WacomMTCapability"/>構造体を参照してください。</param>
		/// <param name="mode">コールバック中にAPIがデータに対して行うことを指定します。</param>
		/// <param name="fingerCallback">この関数は、fingerPacket構造体が入力された状態で、呼び出しの登録時に提供されたuserDataを使用して呼び出されます。
		/// fingerPacketは、コールバックが処理されている間のみ有効です。このメモリーは、コールバックから戻ると、解放/再使用されます。戻り値は予約済みでゼロでなければなりません。</param>
		/// <param name="userData">　呼び出し側によって提供されるパラメータで、コールバック関数にエコーバックされます。</param>
		/// <returns><see cref="WacomMTError"/></returns>
		[DllImport(DllName, CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
		public static extern WacomMTError WacomMTRegisterFingerReadCallback(int deviceID, IntPtr hitRect, WacomMTProcessingMode mode, [MarshalAs(UnmanagedType.FunctionPtr)] WMT_FINGER_CALLBACK fingerCallback, IntPtr userData);

		[DllImport(DllName, CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
		public static extern WacomMTError WacomMTUnRegisterFingerReadCallback(int deviceID, IntPtr hitRect, WacomMTProcessingMode mode, IntPtr userData);

		/// <summary>
		/// この関数を使用すると、BLOBデータの準備が整って要求されたデバイスのヒット矩形内にあるときに呼び出すコールバック関数を登録できます。
		/// プロセスは必要なだけのコールバックを作成できますが、デバイスヒット矩形ごとに作成できるコールバックは1つのみです。
		/// コールバックをキャンセルする場合は、この関数にNULLを指定して既存のデバイスヒット矩形に対して呼び出します。コールバックは、作成された順に処理されます。
		/// </summary>
		/// <param name="deviceID">デバイスのdeviceID。</param>
		/// <param name="hitRect">データをヒットテストするために使用される矩形<see cref="WacomMTHitRect"/>構造体。指がヒット矩形内に接触し始めると、コールバックが呼び出されます。
		/// コールバックは、デバイスとの接触がなくなるまで、その指に対して呼び出され続けます。
		/// 接触がヒット矩形の外側で始まる場合、コールバックはその指に対しては呼び出されません。
		/// NULLの矩形は、デバイスの表面全体を想定します。ヒット矩形は、デバイスの論理単位内で定義されます。
		/// 論理単位の定義については、<see cref="WacomMTCapability"/>構造体を参照してください。</param>
		/// <param name="mode">コールバック中にAPIがデータに対して行うことを指定します。</param>
		/// <param name="blobCallback">この関数は、 blobPacket構造体が入力された状態で、呼び出しの登録時に提供されたuserDataを使用して呼び出されます。
		/// blobPacketは、コールバックが処理されている間のみ有効です。このメモリーは、コールバックから戻ると、解放/再使用されます。戻り値は予約済みでゼロでなければなりません。</param>
		/// <param name="userData">　呼び出し側によって提供されるパラメータで、コールバック関数にエコーバックされます。</param>
		/// <returns><see cref="WacomMTError"/></returns>
		[DllImport(DllName, CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
		public static extern WacomMTError WacomMTRegisterBlobReadCallback(int deviceID, IntPtr hitRect, WacomMTProcessingMode mode, [MarshalAs(UnmanagedType.FunctionPtr)] WMT_BLOB_CALLBACK blobCallback, IntPtr userData);

		[DllImport(DllName, CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
		public static extern WacomMTError WacomMTUnRegisterBlobReadCallback(int deviceID, IntPtr hitRect, WacomMTProcessingMode mode, IntPtr userData);

		/// <summary>
		/// この関数は、指定されたデバイスでRAWデータの準備が整ったときに呼び出すコールバック関数を登録できます。デバイスごとに登録できるコールバックは1つのみです。
		/// デバイスコールバックをキャンセルする場合は、コールバックにNULLを指定して、この関数を呼び出します。コールバックは、作成された順に処理されます。
		/// </summary>
		/// <param name="deviceID">デバイスのdeviceID。</param>
		/// <param name="mode">コールバック中にAPIがデータに対して行うことを指定します。</param>
		/// <param name="rawCallback">この関数は、rawPacket構造体が入力された状態で、呼び出しの登録時に提供されたuserDataを使用して呼び出されます。
		/// rawPacketは、コールバックが処理されている間のみ有効です。このメモリーは、コールバックから戻ると、解放/再使用されます。戻り値は予約済みでゼロでなければなりません。</param>
		/// <param name="userData">　呼び出し側によって提供されるパラメータで、コールバック関数にエコーバックされます。</param>
		/// <returns><see cref="WacomMTError"/></returns>
		[DllImport(DllName, CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
		public static extern WacomMTError WacomMTRegisterRawReadCallback(int deviceID, WacomMTProcessingMode mode, [MarshalAs(UnmanagedType.FunctionPtr)] WMT_RAW_CALLBACK rawCallback, IntPtr userData);

		[DllImport(DllName, CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
		public static extern WacomMTError WacomMTUnRegisterRawReadCallback(int deviceID, WacomMTProcessingMode mode, IntPtr userData);

		/// <summary>
		/// この関数を使用すると、フィンガーデータを受信するためのウィンドウハンドルを登録できます。
		/// 指定されたデバイスのフィンガーデータのパケットの準備が整ったら、WM_FINGERDATAメッセージがウィンドウハンドルに送信されます。
		/// lParamパラメータは、フィンガーデータが入っているWacomMTFingerCollection構造体へのポインタになります。
		/// bufferDepthパラメータは、作成されるコールバックバッファーの数を決定します。
		/// バッファーはリング形式で使用されます。
		/// ウィンドウハンドルにつき、デバイスごとに使用できるフィンガーデータコールバックの数は1つのみです。
		/// ウィンドウハンドルは、ヒット矩形を作成するためにも使用されます。
		/// ヒット矩形は、この関数が呼び出されるときに計算されます。
		/// ウィンドウを移動するかサイズ変更すると、この関数をもう一度呼び出す必要があります。
		/// bufferDepthが変更されると、既存のすべてのバッファーが無効になります。
		/// デバイスコールバックをキャンセルするには、ゼロのbufferDepthを指定して、この関数を呼び出す必要があります
		/// </summary>
		/// <param name="deviceID">デバイス識別子</param>
		/// <param name="mode">コールバック中にAPIがデータに対して行うことを指定します。</param>
		/// <param name="hWnd">これは、WM_FINGERDATAメッセージを受信するウィンドウハンドルです。</param>
		/// <param name="bufferDepth">これは、メッセージコールバックに割り振られるWacomMTFingerCollectionデータ構造体の数です。</param>
		/// <returns><see cref="WacomMTError"/></returns>
		[DllImport(DllName, CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
		public static extern WacomMTError WacomMTRegisterFingerReadHWND(int deviceID, WacomMTProcessingMode mode, IntPtr hWnd, int bufferDepth);

		/// <summary>
		/// この関数を使用すると、フィンガーデータを受信するためのウィンドウハンドルを解除できます。
		/// </summary>
		/// <param name="hwnd">解除するウィンドウハンドル</param>
		/// <returns><see cref="WacomMTError"/></returns>
		[DllImport(DllName, CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
		public static extern WacomMTError WacomMTUnRegisterFingerReadHWND(IntPtr hwnd);

		/// <summary>
		/// この関数を使用すると、BLOBデータを受信するためのウィンドウハンドルを登録できます。
		/// 指定のデバイスのBLOBパケットの準備が整ったら、WM_BLOBDATAメッセージがウィンドウハンドルに送信されます。
		/// lParamパラメータは、BLOBデータが入っているWacomMTBlobAggregate構造体へのポインタになります。
		/// bufferDepthパラメータは、作成されるコールバックバッファーの数を決定します。バッファーはリング形式で使用されます。
		/// ウィンドウハンドルにつき、デバイスごとに使用できるBLOBデータコールバックの数は1つのみです。ウィンドウハンドルは、ヒット矩形を作成するためにも使用されます。
		/// ヒット矩形は、この関数が呼び出されるときに計算されます。ウィンドウを移動するかサイズ変更すると、この関数をもう一度呼び出す必要があります。
		/// bufferDepthが変更されると、既存のすべてのバッファーが無効になります。デバイスコールバックをキャンセルするには、ゼロのbufferDepthを指定して、この関数を呼び出す必要があります。
		/// この関数はCintiq 24HD touchのみ対応しています。
		/// </summary>
		/// <param name="deviceID">デバイス識別子</param>
		/// <param name="mode">コールバック中にAPIがデータに対して行うことを指定します。</param>
		/// <param name="hWnd">これは、WM_BLOBDATAメッセージを受信するウィンドウハンドルです。</param>
		/// <param name="bufferDepth"></param>
		/// <returns></returns>
		[DllImport(DllName, CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
		public static extern WacomMTError WacomMTRegisterBlobReadHWND(int deviceID, WacomMTProcessingMode mode, IntPtr hWnd, int bufferDepth);

		/// <summary>
		/// この関数を使用すると、BLOBデータを受信するためのウィンドウハンドルを解除できます。
		/// </summary>
		/// <param name="hWnd">解除するウィンドウハンドル</param>
		/// <returns><see cref="WacomMTError"/></returns>
		[DllImport(DllName, CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
		public static extern WacomMTError WacomMTUnRegisterBlobReadHWND(IntPtr hWnd);

		/// <summary>
		/// この関数を使用すると、RAWデータを受信するためのウィンドウハンドルを登録できます。
		/// 指定されたデバイスのデータフレームの準備が整ったら、WM_RAWDATAメッセージがウィンドウハンドルに送信されます。
		/// lParamパラメータは、WacomMTRawData構造体へのポインタになります。
		/// bufferDepthパラメータは、作成されるコールバックバッファーの数を決定します。バッファーはリング形式で使用されます。
		/// ウィンドウハンドルにつき、デバイスごとに使用できるRAWデータコールバックの数は1つのみです。
		/// bufferDepthが変更されると、既存のすべてのバッファーが無効になります。
		/// デバイスコールバックをキャンセルするには、ゼロのbufferDepthを指定して、この関数を呼び出す必要があります。
		/// この関数はCintiq 24HD touchのみ対応しています。
		/// </summary>
		/// <param name="deviceID">デバイス識別子</param>
		/// <param name="mode">コールバック中にAPIがデータに対して行うことを指定します。</param>
		/// <param name="hWnd">これは、WM_RAWDATAメッセージを受信するウィンドウハンドルです。</param>
		/// <param name="bufferDepth">これは、メッセージコールバックに割り振られるRawDataデータ構造体の数です。</param>
		/// <returns></returns>
		[DllImport(DllName, CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
		public static extern WacomMTError WacomMTRegisterRawReadHWND(int deviceID, WacomMTProcessingMode mode, IntPtr hWnd, int bufferDepth);
	}
}
