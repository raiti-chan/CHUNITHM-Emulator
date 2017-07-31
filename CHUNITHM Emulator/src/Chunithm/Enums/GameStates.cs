using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHUNITHM_Emulator.Chunithm.Enums {
	/// <summary>
	/// ゲームの状態を示します。
	/// </summary>
	internal enum GameState {
		/// <summary>
		/// 起動した状態の画面
		/// </summary>
		Start,
		/// <summary>
		/// モード選択の画面
		/// </summary>
		ModeSelect,
		/// <summary>
		/// マップ選択の画面
		/// </summary>
		MapSelect,
		/// <summary>
		/// チケット選択画面
		/// </summary>
		TicketSelect,
		/// <summary>
		/// 楽曲選択画面
		/// </summary>
		SongSelect,
		/// <summary>
		/// キャラ選択画面
		/// </summary>
		CharacterSelect,
		/// <summary>
		/// スキル選択画面
		/// </summary>
		SkillSelect,
		/// <summary>
		/// 設定画面
		/// </summary>
		Setting,
		/// <summary>
		/// プレイ画面
		/// </summary>
		Playing,
		/// <summary>
		/// リザルト画面
		/// </summary>
		Result,
		/// <summary>
		/// マップ画面
		/// </summary>
		Map,
		/// <summary>
		/// ゲーム終了画面
		/// </summary>
		TotalResult,
		/// <summary>
		/// エディット画面
		/// </summary>
		Edit,
	}
}
