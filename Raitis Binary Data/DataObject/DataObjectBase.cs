using RBD.Exception;
using static RBD.DataObject.DataType;

namespace RBD.DataObject {
	public abstract class DataObjectBace : IDataObject {

		#region Private Field

		/// <summary>
		/// データの名前。
		/// </summary>
		protected string _Name;

		/// <summary>
		/// 親のグループデータ
		/// </summary>
		protected IGroupData _Parent;

		#endregion

		#region Property

		/// <summary>
		/// データの名前。
		/// </summary>
		public string Name {
			get => this._Name;
			set =>
				//TODO IGroupDataの登録名を変更
				this._Name = value;
		}

		/// <summary>
		/// データの種類を表します。
		/// </summary> 
		public abstract int DataType { get; }

		/// <summary>
		/// このデータがグループデータなのかを表します。
		/// </summary>
		public abstract bool IsGroup { get; }

		/// <summary>
		/// このデータの親データ。
		/// </summary>
		public IGroupData Parent { get => this._Parent; }

		/// <summary>
		/// このデータのバイト配列にした場合のサイズ。
		/// </summary>
		public abstract int ByteSize { get; }

		#endregion

		#region DataProperty

		/// <summary>
		/// byte型データのプロパティ
		/// </summary>
		public virtual byte ByteData { set => throw new WrongDataTypeException<byte>(this.DataType, Byte); get => throw new WrongDataTypeException<byte>(this.DataType, Byte); }

		/// <summary>
		/// short型データのプロパティ
		/// </summary>
		public virtual short ShortData { set => throw new WrongDataTypeException<short>(this.DataType, Short); get => throw new WrongDataTypeException<short>(this.DataType, Short); }

		/// <summary>
		/// int型データのプロパティ
		/// </summary>
		public virtual int IntData { set => throw new WrongDataTypeException<int>(this.DataType, wrongDataType:Int); get => throw new WrongDataTypeException<int>(this.DataType, wrongDataType: Int); }

		/// <summary>
		/// long型データのプロパティ
		/// </summary>
		public virtual long LongData { set => throw new WrongDataTypeException<long>(this.DataType, Long); get => throw new WrongDataTypeException<long>(this.DataType, Long); }

		/// <summary>
		/// float型データのプロパティ
		/// </summary>
		public virtual float FloatData { set => throw new WrongDataTypeException<float>(this.DataType, Float); get => throw new WrongDataTypeException<float>(this.DataType, Float); }

		/// <summary>
		/// double型データのプロパティ
		/// </summary>
		public virtual double DoubleData { set => throw new WrongDataTypeException<double>(this.DataType, Doube); get => throw new WrongDataTypeException<double>(this.DataType, Doube); }

		/// <summary>
		/// bool型データのプロパティ
		/// </summary>
		public virtual bool BoolData { set => throw new WrongDataTypeException<bool>(this.DataType, Bool); get => throw new WrongDataTypeException<bool>(this.DataType, Bool); }

		/// <summary>
		/// charデータのプロパティ
		/// </summary>
		public virtual char CharData { set => throw new WrongDataTypeException<char>(this.DataType, Char); get => throw new WrongDataTypeException<char>(this.DataType, Char); }

		/// <summary>
		/// string型データ
		/// </summary>
		public virtual string StringData { set => throw new WrongDataTypeException<string>(this.DataType, String); get => throw new WrongDataTypeException<string>(this.DataType, String); }

		/// <summary>
		/// sbyte型データ
		/// </summary>
		public virtual sbyte SByteData { set => throw new WrongDataTypeException<sbyte>(this.DataType, SByte); get => throw new WrongDataTypeException<sbyte>(this.DataType, SByte); }

		/// <summary>
		/// ushort型データ
		/// </summary>
		public virtual ushort UShortData { set => throw new WrongDataTypeException<ushort>(this.DataType, UShort); get => throw new WrongDataTypeException<ushort>(this.DataType, UShort); }

		/// <summary>
		/// uint型データ
		/// </summary>
		public virtual uint UIntData { set => throw new WrongDataTypeException<uint>(this.DataType, UInt); get => throw new WrongDataTypeException<uint>(this.DataType, UInt); }

		/// <summary>
		/// ulong型データ
		/// </summary>
		public virtual ulong ULongData { set => throw new WrongDataTypeException<ulong>(this.DataType, ULong); get => throw new WrongDataTypeException<ulong>(this.DataType, ULong); }

		/// <summary>
		/// decimal型データ
		/// </summary>
		public virtual decimal DecimalData { set => throw new WrongDataTypeException<decimal>(this.DataType, Decimal); get => throw new WrongDataTypeException<decimal>(this.DataType, Decimal); }

		/// <summary>
		/// byte配列型データ
		/// </summary>
		public virtual byte[] ByteArrayData { set => throw new WrongDataTypeException<byte[]>(this.DataType, ByteArray); get => throw new WrongDataTypeException<byte[]>(this.DataType, ByteArray); }

		/// <summary>
		/// object型データ
		/// objectじゃないデータもobject型にキャストされ取得されます。(ボックス化)
		/// 代入時は正しいデータ型にキャストされます。データ型が違う場合<see cref="WrongDataTypeException"/>がスローされます。
		/// </summary>
		public abstract object ObjectData { set; get; }

		#endregion

		#region Method

		/// <summary>
		/// データを取得します。
		/// 取得する型をジェネリクスで指定できます。
		/// </summary>
		/// <typeparam name="T">取得するデータの型</typeparam>
		/// <returns>データ</returns>
		public abstract T GetData<T>();

		/// <summary>
		/// データを設定します。
		/// 設定するデータ型をジェネリクスで指定できます。
		/// すでにデータが設定されていた場合そのデータが返されます。
		/// データがない場合nullが返されます。
		/// </summary>
		/// <typeparam name="T">設定するデータ型</typeparam>
		/// <param name="data">設定するデータ</param>
		/// <returns>すでに設定されているデータ</returns>
		public abstract T SetData<T>(T data);


		#endregion

	}
}
