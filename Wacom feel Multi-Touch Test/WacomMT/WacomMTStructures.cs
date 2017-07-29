using System;
using System.Runtime.InteropServices;
using Wacom_feel_Multi_Touch_Test.WacomMT.WacomMTEnums;

namespace Wacom_feel_Multi_Touch_Test.WacomMT.WacomMTStructures {

	/// <summary>
	/// タブレット情報の構造体
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	struct WacomMTCapability {
		/// <summary>
		/// このデータ構造体のバージョン。
		/// </summary>
		public Int32 Version;
		/// <summary>
		/// タッチデバイスを識別する値。これはコンピュータごと、またセッションごとに異なる可能性がある固有の数値ですが、特定のセッション中は同じです。
		/// この値は、デバイスを識別するために、他のすべての呼出しで使用されます。
		/// </summary>
		public Int32 DeviceID;
		/// <summary>
		/// デバイスの種類を示す値。ペンタブレットには、画面との固定の関係はありません。液晶ペンタブレットは、単一ディスプレイと1対1の関係があります。
		/// </summary>
		public WacomMTDeviceType Type;
		/// <summary>
		/// 挿入後に報告されるデバイスの水平方向の最小値。
		/// ペンタブレットの場合、この値はゼロです。
		/// 液晶ペンタブレットの場合、この値は、デスクトップ全体との関連でマップされたディスプレイの左（上）のポイント（ピクセル単位）です。
		/// タッチデバイスがディスプレイより大きい場合、これは隣接ディスプレイにマップされる場合があります。
		/// </summary>
		public float LogicalOriginX;
		/// <summary>
		/// 挿入後に報告されるデバイスの垂直方向の最小値。
		/// ペンタブレットの場合、この値はゼロです。
		/// 液晶ペンタブレットの場合、この値は、デスクトップ全体との関連でマップされたディスプレイの上（左）のポイント（ピクセル単位）です。
		/// タッチデバイスが統合ディスプレイより大きい場合、これは隣接ディスプレイにマップされる場合があります。
		/// </summary>
		public float LogicalOriginY;
		/// <summary>
		/// 挿入後に報告されるデバイスの幅。ペンタブレットの場合、この値は1で、単位は中立です。
		/// 液晶ペンタブレットの場合、この値はデバイスがカバーするピクセルの数です。
		/// タッチデバイスがディスプレイより大きい場合、これは隣接ディスプレイにマップされる場合があります。
		/// </summary>
		public float LogicalWidth;
		/// <summary>
		/// 挿入後に報告されるデバイスの高さ。ペンタブレットの場合、この値は1で、単位は中立です。
		/// 液晶ペンタブレットの場合、この値はデバイスがカバーするピクセルの数です。
		/// タッチデバイスがディスプレイより大きい場合、これは隣接ディスプレイにマップされる場合があります。
		/// </summary>
		public float LogicalHeight;
		/// <summary>
		/// デバイスの検知領域の幅（mm単位）。ある座標系から別の座標系にデバイスデータを変換するために、他のサイズ因子と一緒に使用されます。
		/// </summary>
		public float PhysicalSizeX;
		/// <summary>
		/// デバイスの検知領域の高さ（mm単位）。ある座標系から別の座標系にデバイスデータを変換するために、他のサイズ因子と一緒に使用されます。
		/// </summary>
		public float PhysicalSizeY;
		/// <summary>
		/// デバイスの幅（ネイティブカウント単位）。データの最大解像度を判別するために、他のサイズ因子と一緒に使用されます。
		/// 水平方向の解像度は、この値をPhysicalSizeXで除算することによって計算されます。
		/// </summary>
		public Int32 ReportedSizeX;
		/// <summary>
		/// デバイスの高さ（ネイティブカウント単位）。データの最大解像度を判別するために、他のサイズ因子と一緒に使用されます。
		/// 垂直方向の解像度は、この値をPhysicalSizeYで除算することによって計算されます。
		/// </summary>
		public Int32 ReportedSizeY;
		/// <summary>
		/// デバイスの幅（スキャンコイル単位）。 これは未処理データをサポートするデバイスにのみ提供されます。 
		/// この値は、未処理データバッファーのサイズ（ScanSizeX * ScanSizeY）を計算するために使用されます。
		/// デバイスで未処理データがサポートされない場合は、この値を無視する必要があります。
		/// </summary>
		public Int32 ScanSizeX;
		/// <summary>
		/// デバイスの高さ（スキャンコイル単位）。 これは未処理データをサポートするデバイスにのみ提供されます。 
		/// この値は、未処理データバッファーのサイズ（ScanSizeX * ScanSizeY）を計算するために使用されます。 
		/// デバイスで未処理データがサポートされない場合は、この値を無視する必要があります。
		/// </summary>
		public Int32 ScanSizeY;
		/// <summary>
		/// サポートされる指の最大数。これは、このデバイスが任意の時点でダウンと報告できる指の数です。
		/// これは、指収集構造体の最大サイズの計算に役立てるために使用できます。これはFingerIDの最大値ではありません。
		/// </summary>
		public Int32 FingerMax;
		/// <summary>
		/// BLOB集合内のBLOBの最大数。BLOBは、輪郭を定義する一連のポイントです。
		/// 有効なBLOBは、基本BLOBまたはボイドBLOBです。 各ボイドBLOBには、親が1つだけあります。
		/// 一方、基本BLOBには、親BLOB内のボイド領域を定義するゼロ個以上の子BLOBがあります。
		/// 例えば、ドーナツの形は、外側の輪が親BLOBで、内側の輪が子BLOBであるBLOB集合です。
		/// </summary>
		public Int32 BlobMax;
		/// <summary>
		/// BLOBを構成するBLOBポイントの最大数。 
		/// これらのBLOBの値（BlobMaxとBlobPointsMax）を使用して、BLOB集合の最大サイズ（BlobMax * BlobPointsMax）を計算できます。
		/// </summary>
		public Int32 BlobPointsMax;
		/// <summary>
		/// 特定のデータ要素の存在を示すための値。WacomMTCapabilityFlags列挙型定義を参照してください。
		/// </summary>
		public WacomMTCapabilityFlags CapabilityFlags;

	}

	/// <summary>
	/// この構造体には、個別のタッチ接触用のデータが含まれています。
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	struct WacomMTFinger {
		/// <summary>
		/// 接触の固有の識別子。
		/// この値は最初の接触の1で始まり、後続の接触ごとに増やされます。
		/// この値は、すべての接触がなくなると1にリセットされます。これは、フレームごとに接触を追跡するために使用されます。
		/// これは特定の指の固有の値を表わしているのではなく、接触ごとに固有であり、接触期間中は同じ接触点を表わします。
		/// </summary>
		public int FingerID;
		/// <summary>
		/// 論理単位内の接触領域のスケールされたX。
		/// </summary>
		public float X;
		/// <summary>
		/// 論理単位内の接触領域のスケールされたY。
		/// </summary>
		public float Y;
		/// <summary>
		/// 論理単位内の接触領域の幅。
		/// </summary>
		public float Width;
		/// <summary>
		/// 論理単位内の接触領域の高さ。
		/// </summary>
		public float Height;
		/// <summary>
		/// 接触の強さ。これは圧力ではありません。
		/// これは接触点の強さをデバイス/ユーザごとに個別に表わしたものです。
		/// 同じフレーム/ジェスチャー内の他の指との関係でのみ有効です。
		/// </summary>
		public UInt16 Sensitivity;
		/// <summary>
		/// 接触点の方向（角度）。
		/// </summary>
		public float Orientation;
		/// <summary>
		/// trueの場合、これが指による有効なタッチであるとドライバは認識します。
		/// falseの場合、これは不測のタッチ（前腕または手のひら）である可能性があるとドライバは見なします。
		/// </summary>
		public bool Confidence;
		/// <summary>
		/// この指の接触の状態。<see cref="WacomMTFingerState"/>列挙型定義を参照してください。
		/// </summary>
		public WacomMTFingerState TouchState;
	}

	/// <summary>
	/// この構造体では、指のリストを使用できます。
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	struct WacomMTFingerCollection {
		/// <summary>
		/// このデータ構造体のバージョン。
		/// </summary>
		public int Version;
		/// <summary>
		/// タッチデバイスを識別する値。
		/// これはコンピュータごと、またセッションごとに異なる可能性がある固有の数値ですが、特定のセッション中は同じです。
		/// </summary>
		public int DeviceID;
		/// <summary>
		/// ?
		/// </summary>
		public int FrameNumber;
		/// <summary>
		/// フィンガーデータ配列内のエレメントの数。この値はフレームごとに異なりますが、特定のデバイスのFingerMax値より大きくなることはありません。
		/// </summary>
		public int FingerCount;
		/// <summary>
		/// 複数の<see cref="WacomMTFinger"/>で構成される配列へのポインタ。FingerDataブロックのサイズは、FingerCount * sizeof(WacomMTFinger)で計算されます。
		/// </summary>
		public IntPtr FingerData;
	}

	/// <summary>
	/// <see cref="WacomMTFingerCollection"/>構造体のラッパークラスです。
	/// </summary>
	class WacomMTFingerCollectionClass {

		/// <summary>
		/// このデータのバージョン
		/// </summary>
		public int Version { protected set; get; }
		/// <summary>
		/// タッチデバイスを識別する値。
		/// これはコンピュータごと、またセッションごとに異なる可能性がある固有の数値ですが、特定のセッション中は同じです。
		/// </summary>
		public int DeviceID { protected set; get; }
		/// <summary>
		/// ?
		/// </summary>
		public int FrameNumber { protected set; get; }
		/// <summary>
		/// フィンガーデータ配列内のエレメントの数。この値はフレームごとに異なりますが、特定のデバイスのFingerMax値より大きくなることはありません。
		/// </summary>
		public int FingerCount { protected set; get; }
		/// <summary>
		/// 複数の<see cref="WacomMTFinger"/>で構成される配列。長さは<see cref="FingerCount"/>に一致します。
		/// </summary>
		public WacomMTFinger[] FingerData { protected set; get; }

		/// <summary>
		/// ポインターから<see cref="WacomMTFingerCollection"/>構造体データを取得しオブジェクトに変換します。
		/// </summary>
		/// <param name="structPointer"></param>
		public WacomMTFingerCollectionClass(IntPtr structPointer) {
			WacomMTFingerCollection collection = (WacomMTFingerCollection)Marshal.PtrToStructure(structPointer, typeof(WacomMTFingerCollection));//メモリのポインタの位置から構造体を取得
			Version = collection.Version;
			DeviceID = collection.DeviceID;
			FrameNumber = collection.FrameNumber;
			FingerCount = collection.FingerCount;

			WacomMTFinger[] fingers = new WacomMTFinger[FingerCount];//長さがFingerCountの配列作成
			IntPtr nowPtr = collection.FingerData;//ポインタを複製
			for (int i = 0; i < FingerCount; i++) {
				fingers[i] = (WacomMTFinger)Marshal.PtrToStructure(nowPtr, typeof(WacomMTFinger));
				nowPtr = (IntPtr)((int)nowPtr + Marshal.SizeOf(typeof(WacomMTFinger)));
			}

			FingerData = fingers;

		}

	}


	/// <summary>
	/// この構造体には、BLOBデータの特定のポイントについての情報が含まれています。
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	struct WacomMTBlobPoint {
		/// <summary>
		/// 論理単位内のBLOBポイントのスケールされたX値
		/// </summary>
		public float X;
		/// <summary>
		/// 論理単位内のBLOBポイントのスケールされたY値
		/// </summary>
		public float Y;
		/// <summary>
		/// この BLOBポイントでの信号の強さ。これは圧力ではありません。
		/// これは接触点の強さをデバイス/ユーザごとに個別に表わしたものです。同じフレーム内の他のBLOBとの関係でのみ有効です。
		/// </summary>
		public UInt16 Sensitivity;
	}

	/// <summary>
	/// この構造体には、不規則な領域の接触データが含まれています。
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	struct WacomMTBlob {
		/// <summary>
		/// これは、このBLOBを一意に識別する値です。この値はフレームごとに持続します。
		/// </summary>
		public int BlobID;
		/// <summary>
		/// BLOB領域のスケールされたX重心（論理単位）。
		/// </summary>
		public float X;
		/// <summary>
		/// BLOB領域のスケールされたY重心（論理単位）。
		/// </summary>
		public float Y;
		/// <summary>
		/// trueの場合、これは有効なタッチであるとドライバは認識します。falseの場合、これは不測のタッチ（前腕または手のひら）である可能性があるとドライバは見なします。
		/// </summary>
		public bool Confidence;
		/// <summary>
		/// この構造体が表わすBLOBの種類。WacomMTBlobType列挙型定義を参照してください。
		/// </summary>
		public WacomMTBlobType BlobType;
		/// <summary>
		/// これは親BLOBを識別します。BlobTypeが「Void」の場合にのみ有効です。
		/// </summary>
		public int ParentID;
		/// <summary>
		/// BLOBポイント配列内のエレメントの数。これはBLOBの輪郭を構成するポイントの数です。
		/// </summary>
		public int PointCount;
		/// <summary>
		/// 複数のBLOBポイントで構成される配列へのポインタ。
		/// BlobPointsブロックのサイズは、PointCount * sizeof(WacomMTBlobPoint)で計算されます。
		/// これらのBLOBポイントは、閉じられた領域を形成します。
		/// </summary>
		public IntPtr BlobPoints;
	}

	/// <summary>
	/// この構造体には、不規則な領域の接触データが含まれています。
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	struct WacomMTBlobAggregate {
		/// <summary>
		/// このデータ構造体のバージョン。
		/// </summary>
		public int Version;
		/// <summary>
		/// タッチデバイスを識別する値。これはコンピュータごと、またセッションごとに異なる可能性がある固有の数値ですが、特定のセッション中は同じです。
		/// </summary>
		public int DeviceID;
		/// <summary>
		/// ?
		/// </summary>
		public int FrameNumber;
		/// <summary>
		/// BLOBデータ配列内のエレメントの数。
		/// </summary>
		public int BlobCount;
		/// <summary>
		/// BLOBの配列。BlobArrayブロックのサイズは、BlobCount * sizeof(WacomMTBlob)で計算されます。
		/// </summary>
		public IntPtr BlobArray;
	}

	/// <summary>
	/// この構造体は、デバイスの表面の強さの値を表わします。
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	struct WacomMTRawData {
		/// <summary>
		/// このデータ構造体のバージョン。
		/// </summary>
		public int Version;
		/// <summary>
		/// タッチデバイスを識別する値。これはコンピュータごとに異なる可能性がある固有の数値ですが、特定のセッション中は同じです。
		/// </summary>
		public int DeviceID;
		/// <summary>
		/// ?
		/// </summary>
		public int FrameNumber;
		/// <summary>
		/// 接触配列内のエレメントの数。この値は、ScanSizeY * ScanSizeXで計算されます。
		/// </summary>
		public int ElementCount;
		/// <summary>
		/// 配列感度値へのポインタ。感度ブロックのサイズは、ElementCount * sizeof(unsigned short)で計算されます。各ポイントの位置は、(Y * ScanSizeX) + Xで計算されます。
		/// </summary>
		public IntPtr Sensitivity;
	}

	/// <summary>
	/// この構造体は、ヒットテスト矩形を表わします。
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	struct WacomMTHitRect {
		/// <summary>
		/// 矩形の原点の水平方向の値。
		/// </summary>
		public float originX;
		/// <summary>
		/// 矩形の原点の垂直方向の値。
		/// </summary>
		public float originY;
		/// <summary>
		/// 矩形の水平方向の幅。
		/// </summary>
		public float width;
		/// <summary>
		/// 矩形の垂直方向の高さ。
		/// </summary>
		public float height;
	}


}
