using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBD.DataObject {
	/// <summary>
	/// データオブジェクトのベースクラス。
	/// </summary>
	/// <typeparam name="T">格納するデータの型</typeparam>
	public abstract class DataObjectBase<T> : IDataObject<T> {

		#region Property

		/// <summary>
		/// データの名前
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// データの種類を表します。
		/// </summary>
		public abstract int DataType { get; }

		/// <summary>
		/// このデータがグループデータなのかを表します。
		/// </summary>
		public abstract bool IsGroup { get; }

		/// <summary>
		/// データの値。
		/// </summary>
		public T Data { get; set; }

		/// <summary>
		/// このデータの親データ。
		/// </summary>
		public IDataObject<object> Parent { get; }

		/// <summary>
		/// このデータのバイト配列にした場合のサイズ。
		/// </summary>
		public byte ByteSize { get; }



		#endregion

	}
}
