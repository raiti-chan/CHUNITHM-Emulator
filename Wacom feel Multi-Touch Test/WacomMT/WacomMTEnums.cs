
namespace Wacom_feel_Multi_Touch_Test.WacomMT.WacomMTEnums {

	/// <summary>
	/// エラー
	/// </summary>
	public enum WacomMTError : int {
		/// <summary>
		/// 呼び出しが成功した場合に返されます。
		/// </summary>
		WMTErrorSuccess = 0,
		/// <summary>
		/// インストール済みで実行中のドライバがAPIで検出されない場合に返されます。
		/// </summary>
		WMTErrorDriverNotFound = 1,
		/// <summary>
		/// ドライバのバージョンがAPIと互換性がない場合に返されます。
		/// これは、アプリケーションがドライバでサポートされていない新しいAPIデータ構造体を要求する場合に発生します。
		/// </summary>
		WMTErrorBadVersion = 2,
		/// <summary>
		/// アプリケーションが、ドライバでサポートされなくなったAPIデータ構造体を要求する場合に返されます。
		/// </summary>
		WMTErrorAPIOutdated = 3,
		/// <summary>
		/// 1つ以上のパラメータが無効の場合に返されます。
		/// </summary>
		WMTErrorInvalidParam = 4,
		/// <summary>
		/// API終了呼び出しが行われる場合、またはAPIが正常に初期化されなかった場合に、待機関数によって返されます。
		/// </summary>
		WMTErrorQuit = 5,
		/// <summary>
		/// 提供されたバッファーが小さすぎる場合に返されます。
		/// </summary>
		WMTErrorBufferTooSmall = 6
	}

	/// <summary>
	/// センサーデバイスの種類。<see cref="WacomMTCapability"/>構造体内で使用されます。
	/// </summary>
	public enum WacomMTDeviceType : int {
		/// <summary>
		/// タッチセンサーはディスプレイに組み込まれません。液晶表示のないトラックパッドのようなデバイスが、この値を返します。
		/// </summary>
		WMTDeviceTypeOpaque = 0,
		/// <summary>
		/// タッチセンサーがディスプレイに組み込まれます。タブレットPCなどのオンスクリーンタッチデバイスが、この値を返します。
		/// </summary>
		WMTDeviceTypeIntegrated = 1
	}

	/// <summary>
	/// デバイスでサポートされるデータの種類を示すために、ケイパビリティー構造体内で使用されます。
	/// </summary>
	public enum WacomMTCapabilityFlags : int {
		/// <summary>
		/// このフラグが設定される場合、デバイスはRAWデータをサポートし、RAWデータの読み取りが可能になります。
		/// </summary>
		WMTCapabilityFlagsRawAvailable = (1 << 0),
		/// <summary>
		/// このフラグが設定される場合、デバイスはBLOBデータをサポートし、BLOBデータの読み取りが可能になります。
		/// </summary>
		WMTCapabilityFlagsBlobAvailable = (1 << 1),
		/// <summary>
		/// このフラグが設定される場合、フィンガーデータで感度データが使用可能になります。
		/// 感度データは、0から0xFFFFまでの値です。
		/// このフラグが設定されない場合、感度データは各アップパケットでゼロに設定され、ダウンパケット/ホールドパケットで最大に設定されます。
		/// 感度の定義については、WacomMTFingerデータ構造体を参照してください。
		/// </summary>
		WMTCapabilityFlagsSensitivityAvailable = (1 << 2),
		/// <summary>
		/// ?
		/// </summary>
		WMTCapabilityFlagsReserved = (1 << 31)
	}

	/// <summary>
	/// 指の状態を示すために、<see cref="WacomMTFinger"/>構造体内で使用されます。
	/// </summary>
	public enum WacomMTFingerState : int {
		/// <summary>
		/// 指バッファーには、複数の接触のスペースが含まれる可能性があります。
		/// 使用されていない追加の接触は「None」に設定されます。
		/// 接触が「アップ」状態を通過すると、TouchStateが「None」に設定され、これ以上の処理は不要であることが示されます。
		/// この状態で組み込まれているデータがあれば、そのデータは無効です。
		/// </summary>
		WMTFingerStateNone = 0,
		/// <summary>
		/// 最初の指の接触を示します。特定の接触用の最初のタッチパケット。
		/// </summary>
		WMTFingerStateDown = 1,
		/// <summary>
		/// 指が接触している間の後続のパケット。
		/// </summary>
		WMTFingerStateHold = 2,
		/// <summary>
		/// 特定の指の接触用の最後のタッチパケット。指を上げるときに報告されます。これは、認識された最後の有効な位置で報告されます。
		/// </summary>
		WMTFingerStateUp = 3
	}

	/// <summary>
	/// BLOBの種類を識別するために、BLOB構造体によって使用されます。
	/// </summary>
	public enum WacomMTBlobType : int {
		/// <summary>
		/// これは外側の基本BLOBの輪郭です。
		/// </summary>
		WMTBlobTypePrimary = 0,
		/// <summary>
		/// 外側の基本BLOB内に含まれている内側のBLOBの輪郭です。
		/// ボイド BLOBは、基本BLOB内のタッチされていない領域です。基本 BLOBには、1つ以上のボイドBLOBが含まれている場合があります。
		/// ボイド BLOBには、他のBLOBは含まれていません。固有の基本BLOBが別のBLOBのボイド内に存在することは可能ですが、ボイドはそのBLOBを参照しません。
		/// </summary>
		WMTBlobTypeVoid = 1
	}

	/// <summary>
	/// コールバック関数によって使用されます。データの処理方法の指示を与えます。
	/// フラグが設定されていない場合は、データがコールバックに送信され、システムによって処理されません。
	/// </summary>
	public enum WacomMTProcessingMode {
		/// <summary>
		/// データがコールバックに送信され、これ以上の処理は行われません。
		/// </summary>
		WMTProcessingModeNone = 0,
		/// <summary>
		/// データが処理のためにOSに並行して送られます。
		/// </summary>
		WMTProcessingModeObserver = (1 << 0),
		/// <summary>
		/// ?
		/// </summary>
		WMTProcessingModeReserved = (1 << 31)
	}

	/// <summary>
	/// WindowMassage
	/// </summary>
	public enum WMTWindowMassage : int {
		WM_FINGERDATA = 0x6205,
		WM_BLOBDATA = 0x6206,
		WM_RAWDATA = 0x6207
	}
}
