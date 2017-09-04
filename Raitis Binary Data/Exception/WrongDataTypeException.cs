using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBD.Exception {
	/// <summary>
	/// データオブジェクトのデータを間違ったデータタイプで取得しようとした場合にスローされる例外。
	/// </summary>
	/// <typeparam name="T">間違えられたデータの型</typeparam>
	class WrongDataTypeException<T> : System.Exception {

		#region Property

		/// <summary>
		/// 一致しなかったデータ
		/// </summary>
		public T Data { get; }

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
		/// <param name="wrongDataType">間違ったデータタイプ</param>
		/// <param name="data">間違えられたデータ</param>
		public WrongDataTypeException(int wrongDataType, T data = default(T)) : base("データタイプがデータオブジェクトのデータタイプと一致しません。 DataObjectType:Unknown WrongType:" + wrongDataType) {
			this.Data = data;
			this.DataType = -1;
			this.WrongDataType = wrongDataType;
		}

		/// <summary>
		/// データオブジェクトのデータタイプと間違ったデータタイプを指定して初期化します。
		/// </summary>
		/// <param name="dataType">例外が発生したデータオブジェクトのデータタイプ</param>
		/// <param name="wrongDataType">間違ったデータタイプ</param>
		/// <param name="data">間違えられたデータ</param>
		public WrongDataTypeException(int dataType, int wrongDataType, T data = default(T)) :
			base("データタイプがデータオブジェクトのデータタイプと一致しません。 DataObjectType:" + dataType + " WrongType:" + wrongDataType){
			this.Data = data;
			this.DataType = dataType;
			this.WrongDataType = wrongDataType;
		}

		/// <summary>
		/// データオブジェクトのデータタイプと間違ったデータタイプ、メッセージを指定して初期化します。
		/// </summary>
		/// <param name="dataType">例外が発生したデータオブジェクトのデータタイプ</param>
		/// <param name="wrongDataType">間違ったデータタイプ</param>
		/// <param name="message">例外メッセージ</param>
		/// <param name="data">間違えられたデータ</param>
		public WrongDataTypeException(int dataType, int wrongDataType, string message, T data = default(T)) : base(message) {
			this.Data = data;
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
		/// <param name="data">間違えられたデータ</param>
		public WrongDataTypeException(int dataType, int wrongDataType, string message, System.Exception innerException, T data = default(T)) : base(message, innerException) {
			this.Data = data;
			this.DataType = dataType;
			this.WrongDataType = wrongDataType;
		}


	}
}
