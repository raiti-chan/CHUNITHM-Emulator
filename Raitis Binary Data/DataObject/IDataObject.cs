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

		#endregion
	}
}
