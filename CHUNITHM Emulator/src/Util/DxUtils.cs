using System.Drawing;
using static DxLibDLL.DX;

namespace CHUNITHM_Emulator.Util {
	/// <summary>
	/// DXlib用のユーティリティーメソッド
	/// </summary>
	internal static class DxUtils {

		/// <summary>
		/// VERTEX2D構造体の位置を設定します。
		/// </summary>
		/// <param name="value">VERTEX2D構造体</param>
		/// <param name="x">x</param>
		/// <param name="y">y</param>
		internal static void SetPosition(this VERTEX2D value, float x, float y) {
			value.pos.x = x;
			value.pos.y = y;
			value.pos.z = 0.0F;
			value.rhw = 1.0F;
			
		}

		/// <summary>
		/// VERTEX2D構造体の色を設定します。
		/// </summary>
		/// <param name="value">VERTEX2D構造体</param>
		/// <param name="r">赤</param>
		/// <param name="g">緑</param>
		/// <param name="b">青</param>
		/// <param name="a">アルファ</param>
		internal static void SetColor(this VERTEX2D value, byte r, byte g, byte b, byte a) {
			value.dif.r = r;
			value.dif.g = g;
			value.dif.b = b;
			value.dif.a = a;
		}

		/// <summary>
		/// VERTEX2D構造体の色を設定します
		/// </summary>
		/// <param name="value">VERTEX2D構造体</param>
		/// <param name="color">色オブジェクト</param>
		internal static void SetColor(this VERTEX2D value, Color color) => value.SetColor(color.R, color.G, color.B, color.A);

		/// <summary>
		/// 頂点のテクスチャ座標
		/// </summary>
		/// <param name="value">VERTEX2D構造体</param>
		/// <param name="x">テクスチャのX座標(0.0F~1.0F)</param>
		/// <param name="y">テクスチャのY座標(0.0F~1.0F)</param>
		internal static void SetTexturePosition(this VERTEX2D value, float x, float y) {
			value.u = x;
			value.v = y;
		}

		/// <summary>
		/// VERTEX2D構造体の位置を設定します。
		/// </summary>
		/// <param name="value">VERTEX2D構造体</param>
		/// <param name="x">x</param>
		/// <param name="y">y</param>
		internal static void SetPosition(this VERTEX3D value, float x, float y, float z) {
			value.pos.x = x;
			value.pos.y = y;
			value.pos.z = z;
		}

		/// <summary>
		/// VERTEX2D構造体の色を設定します。
		/// </summary>
		/// <param name="value">VERTEX2D構造体</param>
		/// <param name="r">赤</param>
		/// <param name="g">緑</param>
		/// <param name="b">青</param>
		/// <param name="a">アルファ</param>
		internal static void SetColor(this VERTEX3D value, byte r, byte g, byte b, byte a) {
			value.dif.r = r;
			value.dif.g = g;
			value.dif.b = b;
			value.dif.a = a;
		}

		/// <summary>
		/// VERTEX2D構造体の色を設定します
		/// </summary>
		/// <param name="value">VERTEX2D構造体</param>
		/// <param name="color">色オブジェクト</param>
		internal static void SetColor(this VERTEX3D value, Color color) => value.SetColor(color.R, color.G, color.B, color.A);

		/// <summary>
		/// 頂点のテクスチャ座標
		/// </summary>
		/// <param name="value">VERTEX2D構造体</param>
		/// <param name="x">テクスチャのX座標(0.0F~1.0F)</param>
		/// <param name="y">テクスチャのY座標(0.0F~1.0F)</param>
		internal static void SetTexturePosition(this VERTEX3D value, float x, float y) {
			value.u = x;
			value.v = y;
		}

	}
}
