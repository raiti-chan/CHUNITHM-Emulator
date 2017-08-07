using System;

namespace RBD.DataObject {
	/// <summary>
	/// Raiti's Binary Data のデータオブジェクトのインターフェイス。
	/// </summary>
	/// <typeparam name="T">格納されるデータの型</typeparam>
	public interface IDataObject<T> {

		#region Property

		/// <summary>
		/// データの名前。
		/// </summary>
		string Name { get; set; }

		/// <summary>
		/// データの種類を表します。
		/// </summary>
		int DataType { get; }

		/// <summary>
		/// このデータがグループデータなのかを表します。
		/// </summary>
		bool IsGroup { set; get; }

		/// <summary>
		/// データの値。
		/// </summary>
		T Data { set; get; }


		/// <summary>
		/// このデータの親データ。
		/// </summary>
		IDataObject<object> Parent { get; }

		/// <summary>
		/// このデータのバイト配列にした場合のサイズ。
		/// </summary>
		int ByteSize { get; }

		#endregion

		/// <summary>
		/// データの名前をバイト配列に変換します。
		/// </summary>
		/// <returns>名前から変換されたバイト配列</returns>
		byte[] NameToByte();

		/// <summary>
		/// データの値をバイト配列に変換します。
		/// </summary>
		/// <returns>データの値から変換されたバイト配列。</returns>
		byte[] DataToByte();

		/// <summary>
		/// このデータオブジェクトをバイト配列に変換します。
		/// </summary>
		/// <returns>データオブジェクトから変換したバイト配列</returns>
		byte[] ToByte();
	}
}
