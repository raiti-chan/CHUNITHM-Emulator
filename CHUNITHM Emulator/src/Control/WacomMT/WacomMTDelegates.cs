using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHUNITHM_Emulator.Control.WacomMT {

	/// <summary>
	/// 新しいタッチデバイスの接続時に呼び出すコールバック関数。
	/// </summary>
	/// <param name="deviceInfo">接続されたデバイスの<see cref="WacomMTCapability"/>造体へのポインタ</param>
	/// <param name="userData">呼び出しの登録時に提供されたuserDataへのポインタ</param>
	public delegate void WMT_ATTACH_CALLBACK(IntPtr deviceInfo, IntPtr userData);

	/// <summary>
	/// タッチデバイスの切り離し時に呼び出すコールバック関数。
	/// </summary>
	/// <param name="deviceID">切り離されたデバイスのID</param>
	/// <param name="userData">呼び出しの登録時に提供されたuserDataへのポインタ</param>
	public delegate void WMT_DETACH_CALLBACK(int deviceID, IntPtr userData);

	/// <summary>
	/// 指タッチパケットの準備が整って要求されたデバイスのヒット矩形内にあるときに呼び出すコールバック関数。
	/// </summary>
	/// <param name="fingerPacket"><see cref="WacomMTFingerCollection"/>構造体へのポインタ</param>
	/// <param name="userData">呼び出しの登録時に提供されたuserDataへのポインタ</param>
	/// <returns>0を返す</returns>
	public delegate int WMT_FINGER_CALLBACK(IntPtr fingerPacket, IntPtr userData);

	/// <summary>
	/// BLOBデータの準備が整って要求されたデバイスのヒット矩形内にあるときに呼び出すコールバック関数。
	/// </summary>
	/// <param name="blobPacket"><see cref="WacomMTBlobAggregate"/>構造体へのポインタ</param>
	/// <param name="userData">呼び出しの登録時に提供されたuserDataへのポインタ</param>
	/// <returns>0を返す</returns>
	public delegate int WMT_BLOB_CALLBACK(IntPtr blobPacket, IntPtr userData);

	/// <summary>
	/// RAWデータの準備が整ったときに呼び出すコールバック関数。
	/// </summary>
	/// <param name="rawPacket"><see cref="WacomMTRawData"/>構造体へのポインタ</param>
	/// <param name="userData">呼び出しの登録時に提供されたuserDataへのポインタ</param>
	/// <returns>0を返す</returns>
	public delegate int WMT_RAW_CALLBACK(IntPtr rawPacket, IntPtr userData);

}
