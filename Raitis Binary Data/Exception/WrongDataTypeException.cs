using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBD.Exception {
	/// <summary>
	/// データオブジェクトのデータを間違ったデータタイプで取得しようとした場合にスローされる例外。
	/// </summary>
	class WrongDataTypeException : System.Exception {

		#region Property

		/// <summary>
		/// 例外が発生したデータオブジェクトのデータタイプ
		/// </summary>
		public int DataType { get; }

		/// <summary>
		/// 間違ったデータタイプ
		/// </summary>
		public int WrongDataType { get; }

		#endregion

		/// <summary>
		/// データオブジェクトのデータタイプと間違ったデータタイプを指定して初期化します。
		/// </summary>
		/// <param name="dataType">例外が発生したデータオブジェクトのデータタイプ</param>
		/// <param name="wrongDataType">間違ったデータタイプ</param>
		public WrongDataTypeException(int dataType, int wrongDataType) : base("データタイプがデータオブジェクトのデータタイプと一致しません。 DataObjectType:" + dataType + " WrongType:" + wrongDataType){
			this.DataType = dataType;
			this.WrongDataType = wrongDataType;
		}

		/// <summary>
		/// データオブジェクトのデータタイプと間違ったデータタイプ、メッセージを指定して初期化します。
		/// </summary>
		/// <param name="dataType">例外が発生したデータオブジェクトのデータタイプ</param>
		/// <param name="wrongDataType">間違ったデータタイプ</param>
		/// <param name="message">例外メッセージ</param>
		public WrongDataTypeException(int dataType, int wrongDataType, string message) : base(message) {
			this.DataType = dataType;
			this.WrongDataType = wrongDataType;
		}


		/// <summary>
		/// データオブジェクトのデータタイプと間違ったデータタイプ、メッセージ、内部例外を指定して初期化します。
		/// </summary>
		/// <param name="dataType">例外が発生したデータオブジェクトのデータタイプ</param>
		/// <param name="wrongDataType">間違ったデータタイプ</param>
		/// <param name="message">例外メッセージ</param>
		/// <param name="innerException">現在の例外の原因である例外。内部例外が指定されていない場合は null 参照。</param>
		public WrongDataTypeException(int dataType, int wrongDataType, string message, System.Exception innerException) : base(message, innerException) {
			this.DataType = dataType;
			this.WrongDataType = wrongDataType;
		}


	}
}
