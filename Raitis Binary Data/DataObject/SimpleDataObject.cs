using System;
using RBD.Exception;

namespace RBD.DataObject {

	/// <summary>
	/// Byte型データを格納するデータオブジェクト。
	/// </summary>
	public class ByteDataObject : DataObjectBace {

		#region Field

		/// <summary>
		/// 格納するバイトデータ
		/// </summary>
		protected byte _Data;

		#endregion

		#region Property

		/// <summary>
		/// データの種類 Byteのタイプ値。
		/// </summary>
		public override int DataType => DataObject.DataType.Byte;

		/// <summary>
		/// グループデータではないのでfalseを返します。
		/// </summary>
		public override bool IsGroup => false;

		/// <summary>
		/// データのバイトサイズ。
		/// <code>sizeof(byte);</code>の値
		/// </summary>
		public override int ByteSize => sizeof(byte);

		#endregion

		#region DataProperty

		/// <summary>
		/// byte型データ
		/// </summary>
		public override byte ByteData { get => this._Data; set => this._Data = value; }

		/// <summary>
		/// object型データ
		/// objectじゃないデータもobject型にキャストされ取得されます。(ボックス化)
		/// 代入時は正しいデータ型にキャストされます。データ型が違う場合<see cref="WrongDataTypeException"/>がスローされます。
		/// </summary>
		public override object ObjectData {
			get => _Data;
			set {
				if (value is byte) {
					this._Data = (byte)value;
				} else {
					throw new WrongDataTypeException<object>(this.DataType, data: value);
				}
			}
		}

		#endregion

		#region Constructor

		/// <summary>
		/// 名前、データを指定してオブジェクトを初期化します。
		/// </summary>
		/// <param name="name">データ名</param>
		/// <param name="data">データ</param>
		public ByteDataObject(string name, byte data) {
			this._Name = name;
			this._Data = data;
		}
		
		/// <summary>
		/// データを指定してオブジェクトを初期化します。データ名はnullです。
		/// </summary>
		/// <param name="data"></param>
		public ByteDataObject(byte data) {
			this._Name = null;
			this._Data = data;
		}

		/// <summary>
		/// 名前を指定してオブジェクトを初期化します。データは0です。
		/// </summary>
		/// <param name="name">データ名</param>
		public ByteDataObject(string name) {
			this._Name = name;
			this._Data = 0;
		}

		/// <summary>
		/// 名前をnull、データを0でオブジェクトを初期化します。
		/// </summary>
		public ByteDataObject() {
			this._Name = null;
			this._Data = 0;
		}

		#endregion

		#region Method

		/// <summary>
		/// データを取得します。
		/// 取得する型をジェネリクスで指定できます。
		/// キャストの際object型にキャストされボックス化されるのでパフォーマンスは低めです。
		/// </summary>
		/// <typeparam name="T">取得するデータの型</typeparam>
		/// <returns>データ</returns>
		public override T GetData<T>() {
			if (!typeof(T).Equals(typeof(byte))) {
				throw new WrongDataTypeException<T>(this.DataType);
			}
			return (T)(object)this._Data;
		}

		/// <summary>
		/// データを設定します。
		/// 設定するデータ型をジェネリクスで指定できます。
		/// すでにデータが設定されていた場合そのデータが返されます。
		/// データがない場合nullが返されます。
		/// キャストの際object型にキャストされボックス化されるのでパフォーマンスは低めです。
		/// </summary>
		/// <typeparam name="T">設定するデータ型</typeparam>
		/// <param name="data">設定するデータ</param>
		/// <returns>すでに設定されているデータ</returns>
		public override T SetData<T>(T data) {
			if (!typeof(T).Equals(typeof(byte))) {
				throw new WrongDataTypeException<T>(this.DataType, data);
			}
			object oldData = this._Data;
			this._Data = (byte)(object)data;
			return (T)oldData;
		}

		/// <summary>
		/// データをテキストに変換します。
		/// </summary>
		/// <returns>名前:タイプ=データ の形式の文字列。</returns>
		public override string ToString() => this.Name + ":" + this.DataType + " = " + this._Data;

		#endregion
	}
}
