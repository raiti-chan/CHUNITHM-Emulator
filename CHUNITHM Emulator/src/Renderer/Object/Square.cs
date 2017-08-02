using System.Drawing;
using static CHUNITHM_Emulator.Util.DxUtils;
using static DxLibDLL.DX;

namespace CHUNITHM_Emulator.Renderer.Object {

	/// <summary>
	/// 四角形の描画オブジェクト
	/// </summary>
	internal class Square : RendererObjectBace {

		/// <summary>
		/// 指定された位置に平面の白い四角を描写するオブジェクトを生成します。
		/// </summary>
		/// <param name="x">X</param>
		/// <param name="y">Y</param>
		/// <param name="z">Z</param>
		/// <param name="width">幅</param>
		/// <param name="hight">高さ</param>
		/// <param name="textureHandle">テクスチャ</param>
		internal Square(float x, float y, float z, float width, float hight, int textureHandle = DX_NONE_GRAPH, bool transFlag = false) : this(x, y, z, width, hight, Color.White, textureHandle, transFlag) {

		}

		/// <summary>
		/// 指定された位置に平面の四角を描写するオブジェクトを生成します。
		/// </summary>
		/// <param name="x">X</param>
		/// <param name="y">Y</param>
		/// <param name="z">Z</param>
		/// <param name="width">幅</param>
		/// <param name="hight">高さ</param>
		/// <param name="color">色</param>
		/// <param name="textureHandle">テクスチャ</param>
		internal Square(float x, float y, float z, float width, float hight, Color color, int textureHandle = DX_NONE_GRAPH, bool transFlag = false) : base(textureHandle, transFlag) {
			this.Vertexs = new VERTEX3D[4];

			SetPosition(ref this.Vertexs[0], x, y + hight, z);
			SetNormal(ref this.Vertexs[0], 0.0F, 0.0F, -1.0F);
			SetTexturePosition(ref this.Vertexs[0], 0.0F, 0.0F);
			SetColor(ref this.Vertexs[0], color);

			SetPosition(ref this.Vertexs[1], x + width, y + hight, z);
			SetNormal(ref this.Vertexs[1], 0.0F, 0.0F, -1.0F);
			SetTexturePosition(ref this.Vertexs[1], 1.0F, 0.0F);
			SetColor(ref this.Vertexs[1], color);

			SetPosition(ref this.Vertexs[2], x, y, z);
			SetNormal(ref this.Vertexs[2], 0.0F, 0.0F, -1.0F);
			SetTexturePosition(ref this.Vertexs[2], 0.0F, 1.0F);
			SetColor(ref this.Vertexs[2], color);

			SetPosition(ref this.Vertexs[3], x + width, y, z);
			SetNormal(ref this.Vertexs[3], 0.0F, 0.0F, -1.0F);
			SetTexturePosition(ref this.Vertexs[3], 1.0F, 1.0F);
			SetColor(ref this.Vertexs[3], color);

			this.VertexIndex = new ushort[] { 0, 1, 2, 3, 2, 1 };
		}

		/// <summary>
		/// テクスチャの座標を設定します
		/// </summary>
		/// <param name="x1">左上X座標</param>
		/// <param name="y1">左上Y座標</param>
		/// <param name="x2">右上X座標</param>
		/// <param name="y2">右上Y座標</param>
		/// <param name="x3">左下X座標</param>
		/// <param name="y3">左下Y座標</param>
		/// <param name="x4">右下X座標</param>
		/// <param name="y4">右下Y座標</param>
		internal void SetTexturePositions(float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4) {
			SetTexturePosition(ref this.Vertexs[0], x1, y1);
			SetTexturePosition(ref this.Vertexs[1], x2, y2);
			SetTexturePosition(ref this.Vertexs[2], x3, y3);
			SetTexturePosition(ref this.Vertexs[3], x4, y4);
		}

	}
}
