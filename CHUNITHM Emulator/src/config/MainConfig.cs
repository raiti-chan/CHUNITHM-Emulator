using System.IO;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace CHUNITHM_Emulator.Config {
	/// <summary>
	/// メインコンフィグクラス
	/// </summary>
	[DataContract]
	internal class MainConfig {

		#region シングルトンパターン

		/// <summary>
		/// コンフィグファイルへのパス
		/// </summary>
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
				if (_instance != null) {
					return _instance;//インスタンスが存在していたら返す
				}
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
				_instance.SaveJson();//追加された項目を保存
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

		#region 画面設定

		/// <summary>
		/// 垂直同期を使用するか(再起動後反映)
		/// </summary>
		[DataMember]
		internal bool UseASyncLoad { set; get; } = true;

		/// <summary>
		/// 解像度、16:9(再起動後反映)
		/// </summary>
		[DataMember]
		internal int Resolution { get; set; } = 1920;

		/// <summary>
		/// フルスクリーンモード(再起動後反映)
		/// </summary>
		[DataMember]
		internal bool IsFullScreen { get; set; } = true;

		/// <summary>
		/// アンチエイリアスの解像度倍率(再起動後反映)
		/// </summary>
		[DataMember]
		internal int AntiAliasSamples { get; set; } = 2;

		/// <summary>
		/// アンチエイリアスのクオリティー0~3(再起動後反映)
		/// </summary>
		[DataMember]
		internal int AntiAliasQuality { get; set; } = 2;

		#endregion

		#region 操作

		/// <summary>
		/// オート
		/// </summary>
		[DataMember]
		internal bool IsAuto { get; set; } = false;

		/// <summary>
		/// Airのオート設定
		/// </summary>
		[DataMember]
		internal AirMode AirMode { get; set; } = AirMode.Manual;

		#endregion

		#region デバッグ

		/// <summary>
		/// システムの情報を表示するか
		/// </summary>
		[DataMember]
		internal bool IsDrawSystemProperties { set; get; } = false;

		/// <summary>
		/// システム情報を表示するフォント
		/// </summary>
		[DataMember]
		internal string SystemPropertyFont { set; get; } = "Meirio UI";

		#endregion


		#endregion

	}
}
