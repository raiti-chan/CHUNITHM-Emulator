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
		public string Name { set; get; }

		/// <summary>
		/// データの種類を表します。
		/// </summary>
		public abstract int DataType { set; get; }

		/// <summary>
		/// このデータがグループデータなのかを表します。
		/// </summary>
		public abstract bool IsGroup { get; }

		/// <summary>
		/// データの値。
		/// </summary>
		public abstract T Data { set; get; }

		/// <summary>
		/// このデータの親データ。
		/// </summary>
		public IDataObject<object> Parent { set; get; }

		/// <summary>
		/// このデータのバイト配列にした場合のサイズ。
		/// </summary>
		public abstract int ByteSize { get; }

		#endregion

		/// <summary>
		/// 指定された名前のデータを作成します。
		/// </summary>
		/// <param name="name"></param>
		public DataObjectBase(string name, T data = default(T)) {
			this.Name = name;
			this.Data = data;
		}

		/// <summary>
		/// データの名前をバイト配列に変換します。
		/// </summary>
		/// <returns>名前から変換されたバイト配列(UTF-8)</returns>
		public byte[] NameToByte() => Encoding.UTF8.GetBytes(this.Name);

		/// <summary>
		/// バイト配列を名前文字列に変換し、設定します。
		/// </summary>
		/// <param name="bytes">名前のバイト配列</param>
		/// <returns>設定された名前	</returns>
		public string ByteToName(byte[] bytes) {
			this.Name = Encoding.UTF8.GetString(bytes);
			return this.Name;
		}

		/// <summary>
		/// このデータなタイプをバイト配列に変換します。
		/// </summary>
		/// <returns>タイプから変換されたバイト配列</returns>
		public byte[] TypeToByte() => BitConverter.GetBytes(this.DataType);

		/// <summary>
		/// バイト配列をタイプに変換し、設定します。
		/// </summary>
		/// <param name="bytes">設定されたタイプID</param>
		/// <returns></returns>
		public int ByteToType(byte[] bytes) {
			this.DataType = BitConverter.ToInt32(bytes, 0);
			return this.DataType;
		}

		/// <summary>
		/// データの値をバイト配列に変換します。
		/// </summary>
		/// <returns>データの値から変換されたバイト配列。</returns>
		public abstract byte[] DataToByte();

		/// <summary>
		/// バイト配列をデータに変換し、設定します。
		/// </summary>
		/// <param name="bytes">データのバイト配列	</param>
		/// <returns>設定された名前</returns>
		public abstract T ByteToData(byte[] bytes);

		/// <summary>
		/// このデータオブジェクトをバイト配列に変換します。
		/// </summary>
		/// <returns>データオブジェクトから変換したバイト配列</returns>
		public byte[] ToByte() {
			byte[] data = new byte[this.ByteSize];
			byte[] nameByte = this.NameToByte();
			byte[] typeByte = this.TypeToByte();
			byte[] dataByte = this.TypeToByte();
			int index = 0;
			for (int i = 0; i < nameByte.Length; i++) {
				data[index] = nameByte[i];
				index++;
			}
			data[index] = 0;
			index++;
			for (int i = 0; i < typeByte.Length; i++) {
				data[index] = typeByte[i];
				index++;
			}
			for (int i = 0; i < typeByte.Length; i++) {
				data[index] = typeByte[i];
				index++;
			}
			return data;
		}


		public void ToData(byte[] bytes) {
			int index = 0;
			#region 名前変換
			while (bytes[index] != 0) {
				if (index > bytes.Length) {
					//TODO: 例外を発生 EncodingExceptionFailed 名前の区切りのnull文字が見つからず終点に到達しました。	
				}
				index++;
			}
			if (index == 0) {
				//TODO: 例外を発生 EncodingExceptionFailed 名前のデータがありません。
			}
			byte[] dataByte = new byte[index];
			for (int i = 0; i < dataByte.Length; i++) {
				dataByte[i] = bytes[i];
			}
			this.ByteToName(dataByte);
			index++; //区切りの値が0のバイトを飛ばす。
			#endregion

			#region タイプ変換
			dataByte = new byte[sizeof(int)];
			if (index + dataByte.Length + 1 > bytes.Length) {
				//TODO: 例外を発生 EncodingExceptionFaied データの長さが不正です、DataTypeの取得に失敗しました。
			}
			for (int i = 0; i < dataByte.Length; i++) {
				dataByte[index] = bytes[index];
				index++;
			}
			this.ByteToType(dataByte);
			#endregion

			#region データ変換
			dataByte = new byte[bytes.Length - (index + 1)];
			for (int i = 0; i < dataByte.Length; i++) {
				dataByte[i] = bytes[index];
			}
			this.ByteToData(dataByte);
			#endregion

		}
	}
}
