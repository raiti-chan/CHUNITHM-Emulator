using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBD.DataObject {
	internal abstract class DataObjectBace : IDataObject {

		#region Property
		public abstract string Name { get; set; }
		public abstract int DataType { get; }
		public abstract bool IsGroup { get; }
		public abstract IGroupData Parent { get; }
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
