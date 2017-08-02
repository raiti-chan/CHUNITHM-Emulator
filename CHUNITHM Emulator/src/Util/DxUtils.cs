using System.Drawing;
using static DxLibDLL.DX;

namespace CHUNITHM_Emulator.Util {
	/// <summary>
	/// DXlib用のユーティリティーメソッド
	/// </summary>
	internal static class DxUtils {

		/// <summary>
		/// 度数からラジアンを取得します。
		/// </summary>
		/// <param name="degree">角度(度数法)</param>
		/// <returns>ラジアン</returns>
		internal static float GetRadian(float degree) => degree * 0.0174532F;

		/// <summary>
		/// VERTEX2D構造体の位置を設定します。
		/// </summary>
		/// <param name="value">VERTEX2D構造体</param>
		/// <param name="x">x</param>
		/// <param name="y">y</param>
		internal static void SetPosition(ref VERTEX2D value, float x, float y) {
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
		internal static void SetColor(ref VERTEX2D value, byte r, byte g, byte b, byte a) => value.dif = GetColorU8(r, g, b, a);

		/// <summary>
		/// VERTEX2D構造体の色を設定します
		/// </summary>
		/// <param name="value">VERTEX2D構造体</param>
		/// <param name="color">色オブジェクト</param>
		internal static void SetColor(ref VERTEX2D value, Color color) => SetColor(ref value ,color.R, color.G, color.B, color.A);

		/// <summary>
		/// 頂点のテクスチャ座標
		/// </summary>
		/// <param name="value">VERTEX2D構造体</param>
		/// <param name="x">テクスチャのX座標(0.0F~1.0F)</param>
		/// <param name="y">テクスチャのY座標(0.0F~1.0F)</param>
		internal static void SetTexturePosition(ref VERTEX2D value, float x, float y) {
			value.u = x;
			value.v = y;
		}

		/// <summary>
		/// VERTEX3D構造体の位置を設定します。
		/// </summary>
		/// <param name="value">VERTEX3D構造体</param>
		/// <param name="x">x</param>
		/// <param name="y">y</param>
		/// <param name="z">z</param>
		internal static void SetPosition(ref VERTEX3D value, float x, float y, float z) => value.pos = VGet(x, y, z);

		/// <summary>
		/// VERTEX3D構造体の法線を設定します
		/// </summary>
		/// <param name="value">VERTEX3D構造体</param>
		/// <param name="x">x</param>
		/// <param name="y">y</param>
		/// <param name="z">z</param>
		internal static void SetNormal(ref VERTEX3D value, float x, float y, float z) => value.norm = VGet(x, y, z);

		/// <summary>
		/// VERTEX3D構造体の色を設定します。
		/// </summary>
		/// <param name="value">VERTEX3D構造体</param>
		/// <param name="r">赤</param>
		/// <param name="g">緑</param>
		/// <param name="b">青</param>
		/// <param name="a">アルファ</param>
		internal static void SetColor(ref VERTEX3D value, byte r, byte g, byte b, byte a) => value.dif = GetColorU8(r, g, b, a);

		/// <summary>
		/// VERTEX3D構造体の色を設定します
		/// </summary>
		/// <param name="value">VERTEX3D構造体</param>
		/// <param name="color">色オブジェクト</param>
		internal static void SetColor(ref VERTEX3D value, Color color) => SetColor(ref value,color.R, color.G, color.B, color.A);

		/// <summary>
		/// VERTEX3D構造体の反射光色を設定します。
		/// </summary>
		/// <param name="value">VERTEX3D構造体</param>
		/// <param name="r">赤</param>
		/// <param name="g">緑</param>
		/// <param name="b">青</param>
		/// <param name="a">アルファ</param>
		internal static void SetSpcColor(ref VERTEX3D value, byte r, byte g, byte b, byte a) => value.spc = GetColorU8(r, g, b, a);

		/// <summary>
		/// VERTEX3D構造体の反射光色を設定します。
		/// </summary>
		/// <param name="value">VERTEX3D構造体</param>
		/// <param name="color">色オブジェクト</param>
		internal static void SetSpcColor(ref VERTEX3D value, Color color) => SetSpcColor(ref value,color.R, color.G, color.B, color.A);
		

		/// <summary>
		/// 頂点のテクスチャ座標
		/// </summary>
		/// <param name="value">VERTEX3D構造体</param>
		/// <param name="x">テクスチャのX座標(0.0F~1.0F)</param>
		/// <param name="y">テクスチャのY座標(0.0F~1.0F)</param>
		internal static void SetTexturePosition(ref VERTEX3D value, float x, float y) {
			value.u = x;
			value.v = y;
		}

	}
}
