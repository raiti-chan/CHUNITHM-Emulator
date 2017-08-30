using System;

namespace RBD.DataObject {
	/// <summary>
	/// Raiti's Binary Data のデータオブジェクトのインターフェイス。
	/// </summary>
	public interface IDataObject {

		#region Property

		/// <summary>
		/// データの名前。
		/// </summary>
		string Name { set; get; }

		/// <summary>
		/// データの種類を表します。
		/// </summary>
		int DataType { get; }

		/// <summary>
		/// このデータがグループデータなのかを表します。
		/// </summary>
		bool IsGroup { get; }

		/// <summary>
		/// このデータの親データ。
		/// </summary>
		IGroupData Parent { get; }

		/// <summary>
		/// このデータのバイト配列にした場合のサイズ。
		/// </summary>
		int ByteSize { get; }

		#endregion

		#region DataProperty
		/// <summary>
		/// byte型データのプロパティ
		/// </summary>
		byte ByteData { set; get; }

		/// <summary>
		/// short型データのプロパティ
		/// </summary>
		short ShortData { set; get; }

		/// <summary>
		/// int型データのプロパティ
		/// </summary>
		int IntData { set; get; }

		/// <summary>
		/// long型データのプロパティ
		/// </summary>
		long LongData { set; get; }

		/// <summary>
		/// float型データのプロパティ
		/// </summary>
		float FloatData { set; get; }

		/// <summary>
		/// double型データのプロパティ
		/// </summary>
		double DoubleData { set; get; }

		/// <summary>
		/// bool型データのプロパティ
		/// </summary>
		bool BoolData { set; get; }

		/// <summary>
		/// charデータのプロパティ
		/// </summary>
		char CharData { set; get; }

		/// <summary>
		/// string型データ
		/// </summary>
		string StringData { set; get; }

		/// <summary>
		/// sbyte型データ
		/// </summary>
		sbyte SByteData { set; get; }

		/// <summary>
		/// ushort型データ
		/// </summary>
		ushort UShortData { set; get; }

		/// <summary>
		/// uint型データ
		/// </summary>
		uint UIntData { set; get; }

		/// <summary>
		/// ulong型データ
		/// </summary>
		ulong ULongData { set; get; }

		/// <summary>
		/// decimal型データ
		/// </summary>
		decimal DecimalData { set; get; }

		/// <summary>
		/// byte配列型データ
		/// </summary>
		byte[] ByteArrayData { set; get; }

		/// <summary>
		/// object型データ
		/// </summary>
		object ObjectData { set; get; }

		#endregion
	}
}
