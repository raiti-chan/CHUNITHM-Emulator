using System.IO;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace CHUNITHM_Emulator.config {
	/// <summary>
	/// メインコンフィグクラス
	/// </summary>
	[DataContract]
	internal class MainConfig {

		#region シングルトンパターン

		private const string _MainConfigPath = "MainConfig.json";

		/// <summary>
		/// クラスのインスタンス
		/// </summary>
		private static MainConfig _instance;

		/// <summary>
		/// クラスのインスタンス
		/// </summary>
		public static MainConfig Instance {
			get {
				if (_instance != null) return _instance;//インスタンスが存在していたら返す
														//存在していなかったらインスタンスを生成する
				DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(MainConfig));//Jsonシリアライザを作成
				if (!File.Exists(_MainConfigPath)) {
					//ファイルが存在していなかったら新規作成
					Trace.WriteLine("Create a new Config. Path:" + _MainConfigPath);
					_instance = new MainConfig();//インスタンスの新規作成
					using (StreamWriter writer = new StreamWriter(_MainConfigPath, false)) {
						ser.WriteObject(writer.BaseStream, _instance);//書き込み
						writer.Flush();
					}
					return _instance;
				}
				//ファイルが存在していたら読み込み
				Trace.WriteLine("Load a MainConfig. Path:" + _MainConfigPath);
				using (StreamReader reader = new StreamReader(_MainConfigPath)) {
					_instance = (MainConfig)ser.ReadObject(reader.BaseStream);//読み込み
				}

				return _instance;

			}
		}


		/// <summary>
		/// privateコンストラクタ
		/// </summary>
		private MainConfig() {

		}

		#endregion

		#region メソッド

		/// <summary>
		/// 設定データを保存します。
		/// </summary>
		internal void SaveJson() {
			Trace.WriteLine("Save MainConfig. path:" + _MainConfigPath);
			DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(MainConfig));
			using (StreamWriter writer = new StreamWriter(_MainConfigPath, false)) {
				ser.WriteObject(writer.BaseStream, this);
				writer.Flush();
			}
		}

		#endregion


		#region 設定プロパティ

		/// <summary>
		/// 垂直同期を使用するか
		/// </summary>
		[DataMember]
		internal bool UseASyncLoad { set; get; } = false;

		#endregion

	}
}
