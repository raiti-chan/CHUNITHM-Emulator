
namespace CHUNITHM_Emulator.Renderer.Scene {
	/// <summary>
	/// ゲームシーンのインターフェイス
	/// </summary>
	interface IScene {

		/// <summary>
		/// シーンのTickUpdate
		/// </summary>
		/// <param name="flameSec">前回のフレームからの経過時間</param>
		void TickUpdate(int flameSec);

		/// <summary>
		/// シーンの描画
		/// </summary>
		void DrawScene();

		/// <summary>
		/// シーンが不要になったとき呼ばれます。
		/// </summary>
		void Dispose();

	}
}
