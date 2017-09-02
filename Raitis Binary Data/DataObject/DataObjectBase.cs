using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBD.DataObject {
	internal abstract class DataObjectBace : IDataObject {

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
		public abstract byte ByteData { get; set; }
		public abstract short ShortData { get; set; }
		public abstract int IntData { get; set; }
		public abstract long LongData { get; set; }
		public abstract float FloatData { get; set; }
		public abstract double DoubleData { get; set; }
		public abstract bool BoolData { get; set; }
		public abstract char CharData { get; set; }
		public abstract string StringData { get; set; }
		public abstract sbyte SByteData { get; set; }
		public abstract ushort UShortData { get; set; }
		public abstract uint UIntData { get; set; }
		public abstract ulong ULongData { get; set; }
		public abstract decimal DecimalData { get; set; }
		public abstract byte[] ByteArrayData { get; set; }
		public abstract object ObjectData { get; set; }
		#endregion
	}
}
