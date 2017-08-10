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
		string Name { set;  get; }

		/// <summary>
		/// データの種類を表します。
		/// </summary>
		int DataType { set; get; }

		/// <summary>
		/// このデータがグループデータなのかを表します。
		/// </summary>
		bool IsGroup { get; }

		/// <summary>
		/// データの値。
		/// </summary>
		T Data { set; get; }


		/// <summary>
		/// このデータの親データ。
		/// </summary>
		IDataObject<object> Parent { set;  get; }

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
		/// バイト配列を名前文字列に変換し、設定します。
		/// </summary>
		/// <param name="bytes">名前のバイト配列</param>
		/// <returns>設定された名前	</returns>
		string ByteToName(byte[] bytes);

		/// <summary>
		/// このデータなタイプをバイト配列に変換します。
		/// </summary>
		/// <returns>タイプから変換されたバイト配列</returns>
		byte[] TypeToByte();

		/// <summary>
		/// バイト配列をタイプに変換し、設定します。
		/// </summary>
		/// <param name="bytes">設定されたタイプID</param>
		/// <returns></returns>
		int ByteToType(byte[] bytes);

		/// <summary>
		/// データの値をバイト配列に変換します。
		/// </summary>
		/// <returns>データの値から変換されたバイト配列。</returns>
		byte[] DataToByte();

		/// <summary>
		/// バイト配列をデータに変換し、設定します。
		/// </summary>
		/// <param name="bytes">データのバイト配列	</param>
		/// <returns>設定された名前</returns>
		T ByteToData(byte[] bytes);

		/// <summary>
		/// このデータオブジェクトをバイト配列に変換します。
		/// </summary>
		/// <returns>データオブジェクトから変換したバイト配列</returns>
		byte[] ToByte();

		/// <summary>
		/// バイト配列をデータに変換させます。
		/// </summary>
		/// <param name="bytes">変換</param>
		void ToData(byte[] bytes);
	}
}
