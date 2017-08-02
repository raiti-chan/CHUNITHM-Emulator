using System;
using DxLibDLL;
using static DxLibDLL.DX;

namespace CHUNITHM_Emulator.Renderer.Object {
	internal abstract class RendererObjectBace : IRendererObject {

		/// <summary>
		/// オブジェクトの行列
		/// </summary>
		protected MATRIX Matrix = MGetIdent();

		/// <summary>
		/// オブジェクトの頂点
		/// </summary>
		protected VERTEX3D[] Vertexs;

		/// <summary>
		/// オブジェクトの頂点インデックス
		/// </summary>
		protected ushort[] VertexIndex;

		/// <summary>
		/// Textureのハンドル
		/// </summary>
		protected int TextureHandle = DX_NONE_GRAPH;

		/// <summary>
		/// 透明度を有効にするかのフラグ
		/// </summary>
		protected bool TransFlag;

		/// <summary>
		/// レンダリングオブジェクトを生成します。
		/// </summary>
		/// <param name="textureHandle">テクスチャのハンドル</param>
		protected RendererObjectBace(int textureHandle = DX_NONE_GRAPH, bool transFlag = false) {
			this.TextureHandle = textureHandle;
			this.Matrix = MGetIdent();
			this.TransFlag = transFlag;
		}

		/// <summary>
		/// 拡大します
		/// </summary>
		/// <param name="x">X方向</param>
		/// <param name="y">Y方向</param>
		/// <param name="z">Z方向</param>
		public void Scale(float x, float y, float z) => this.Scale(VGet(x, y, z));

		/// <summary>
		/// 拡大します。
		/// </summary>
		/// <param name="vector">拡大値</param>
		public void Scale(VECTOR vector) => this.Matrix = MMult(this.Matrix, MGetScale(vector));

		/// <summary>
		/// 拡大します。これは、現在の状態より前に呼ばれたことになります。
		/// </summary>
		/// <param name="x">X方向</param>
		/// <param name="y">Y方向</param>
		/// <param name="z">Z方向</param>
		public void ScaleBefore(float x, float y, float z) => this.ScaleBefore(VGet(x, y, z));

		/// <summary>
		/// 拡大します。これは、現在の状態より前に呼ばれたことになります。
		/// </summary>
		/// <param name="vector">拡大値</param>
		public void ScaleBefore(VECTOR vector) => this.Matrix = MMult(MGetScale(vector), this.Matrix);

		/// <summary>
		/// オブジェクトを平行移動します。
		/// </summary>
		/// <param name="x">X方向</param>
		/// <param name="y">Y方向</param>
		/// <param name="z">Z方向</param>
		public void Translate(float x, float y, float z) => this.Translate(VGet(x, y, z));

		/// <summary>
		/// オブジェクトを平行移動します。
		/// </summary>
		/// <param name="vector">平行移動値</param>
		public void Translate(VECTOR vector) => this.Matrix = MMult(this.Matrix, MGetTranslate(vector));

		/// <summary>
		/// オブジェクトを平行移動します。これは、現在の状態より前に呼ばれたことになります。
		/// </summary>
		/// <param name="x">X方向</param>
		/// <param name="y">Y方向</param>
		/// <param name="z">Z方向</param>
		public void TranslateBefore(float x, float y, float z) => this.TranslateBefore(VGet(x, y, z));

		/// <summary>
		/// オブジェクトを平行移動します。これは、現在の状態より前に呼ばれたことになります。
		/// </summary>
		/// <param name="vector">平行移動値</param>
		public void TranslateBefore(VECTOR vector) => this.Matrix = MMult(MGetTranslate(vector), this.Matrix);

		/// <summary>
		/// オブジェクトをX軸回転します。
		/// </summary>
		/// <param name="rad">回転値(ラジアン)</param>
		public void RotationX(float rad) => this.Matrix = MMult(this.Matrix, MGetRotX(rad));

		/// <summary>
		/// オブジェクトをX軸回転します。これは、現在の状態より前に呼ばれたことになります。
		/// </summary>
		/// <param name="rad">回転値(ラジアン)</param>
		public void RotationXBefore(float rad) => this.Matrix = MMult(MGetRotX(rad), this.Matrix);

		/// <summary>
		/// オブジェクトをY軸回転します。
		/// </summary>
		/// <param name="rad">回転値(ラジアン)</param>
		public void RotationY(float rad) => this.Matrix = MMult(this.Matrix, MGetRotY(rad));


		/// <summary>
		/// オブジェクトをY軸回転します。これは、現在の状態より前に呼ばれたことになります。
		/// </summary>
		/// <param name="rad">回転値(ラジアン)</param>
		public void RotationYBefore(float rad) => this.Matrix = MMult(MGetRotY(rad), this.Matrix);

		/// <summary>
		/// オブジェクトをZ軸回転します。
		/// </summary>
		/// <param name="rad">回転値(ラジアン)</param>
		public void RotationZ(float rad) => this.Matrix = MMult(this.Matrix, MGetRotZ(rad));

		/// <summary>
		/// オブジェクトをZ軸回転します。これは、現在の状態より前に呼ばれたことになります。
		/// </summary>
		/// <param name="rad">回転値(ラジアン)</param>
		public void RotationZBefore(float rad) => this.Matrix = MMult(MGetRotZ(rad), this.Matrix);

		/// <summary>
		/// オブジェクトを指定軸で回転します
		/// </summary>
		/// <param name="x">軸のベクトルのX</param>
		/// <param name="y">軸のベクトルのY</param>
		/// <param name="z">軸のベクトルのZ</param>
		/// <param name="rad">回転値(ラジアン)</param>
		public void Rotation(float x, float y, float z, float rad) => this.Rotation(VGet(x, y, z), rad);

		/// <summary>
		/// オブジェクトを指定軸で回転します。
		/// </summary>
		/// <param name="vector">軸のベクトル</param>
		/// <param name="raf">回転値(ラジアン)</param>
		public void Rotation(VECTOR vector, float rad) => this.Matrix = MMult(this.Matrix, MGetRotAxis(vector, rad));

		/// <summary>
		/// オブジェクトを指定軸で回転します。これは、現在の状態より前に呼ばれたことになります。
		/// </summary>
		/// <param name="x">軸のベクトルのX</param>
		/// <param name="y">軸のベクトルのY</param>
		/// <param name="z">軸のベクトルのZ</param>
		/// <param name="rad">回転値(ラジアン)</param>
		public void RotationBefore(float x, float y, float z, float rad) => this.RotationBefore(VGet(x, y, z), rad);

		/// <summary>
		/// オブジェクトを指定軸で回転します。これは、現在の状態より前に呼ばれたことになります。
		/// </summary>
		/// <param name="vector">軸のベクトル</param>
		/// <param name="raf">回転値(ラジアン)</param>
		public void RotationBefore(VECTOR vector, float rad) => this.Matrix = MMult(MGetRotAxis(vector,rad), this.Matrix);

		/// <summary>
		/// 行列を使ってオブジェクトを変換します。
		/// </summary>
		/// <param name="matrix">変換に使う行列</param>
		public void TransForm(MATRIX matrix) => this.Matrix = MMult(this.Matrix, matrix);

		/// <summary>
		/// 行列を使ってオブジェクトを変換します。これは、現在の状態より前に呼ばれたことになります。
		/// </summary>
		/// <param name="matrix">変換に使う行列</param>
		public void TransFormBefore(MATRIX matrix) => this.Matrix = MMult(matrix, this.Matrix);

		/// <summary>
		/// オブジェクトの頂点位置を行列で変換したものに置き換えます。
		/// このメソッドが呼ばれると、頂点が現在の行列で変換された物に置き換わり、保持している行列がリセットされます。。
		/// </summary>
		public void Reflect() {
			for (int i = 0; i < this.Vertexs.Length; i++) {
				this.Vertexs[i].pos = VTransform(this.Vertexs[i].pos, this.Matrix);
				this.Vertexs[i].norm = VTransformSR(this.Vertexs[i].norm, this.Matrix);
			}
			this.ResetMatrix();
		}

		/// <summary>
		/// 行列を単位行列にリセットします。
		/// </summary>
		public void ResetMatrix() => this.Matrix = MGetIdent();

		/// <summary>
		/// オブジェクトを描画します。
		/// </summary>
		public void Draw() {
			VERTEX3D[] vertexs = new VERTEX3D[this.Vertexs.Length];
			for (int i = 0; i < this.Vertexs.Length; i++) {
				vertexs[i].pos = VTransform(this.Vertexs[i].pos, this.Matrix);
				vertexs[i].norm = VTransformSR(this.Vertexs[i].norm, this.Matrix);
				vertexs[i].dif = this.Vertexs[i].dif;
				vertexs[i].spc = this.Vertexs[i].spc;
				vertexs[i].u = this.Vertexs[i].u;
				vertexs[i].v = this.Vertexs[i].v;
			}

			DrawPolygonIndexed3D(vertexs, vertexs.Length, this.VertexIndex, this.VertexIndex.Length / 3, this.TextureHandle, this.TransFlag ? TRUE : FALSE);

		}

		/// <summary>
		/// オブジェクトを行列で変換し、描画します。
		/// </summary>
		/// <param name="matrix">変換に使う行列</param>
		public void Draw(MATRIX matrix) {
			VERTEX3D[] vertexs = new VERTEX3D[this.Vertexs.Length];
			MATRIX matrix_2 = MMult(this.Matrix, matrix);
			for (int i = 0; i < this.Vertexs.Length; i++) {
				vertexs[i].pos = VTransform(this.Vertexs[i].pos, matrix_2);
				vertexs[i].norm = VTransformSR(this.Vertexs[i].norm, matrix_2);
				vertexs[i].dif = this.Vertexs[i].dif;
				vertexs[i].spc = this.Vertexs[i].spc;
				vertexs[i].u = this.Vertexs[i].u;
				vertexs[i].v = this.Vertexs[i].v;
			}

			DrawPolygonIndexed3D(vertexs, vertexs.Length, this.VertexIndex, this.VertexIndex.Length / 3, this.TextureHandle, this.TransFlag ? TRUE : FALSE);

		}
		
		/// <summary>
		/// オブジェクトを行列で変換し、描画します。これは、現在の状態より前に変換されたことになります。
		/// </summary>
		/// <param name="matrix">変換に使う行列</param>
		public void DrawBefore(MATRIX matrix) {
			VERTEX3D[] vertexs = new VERTEX3D[this.Vertexs.Length];
			MATRIX matrix_2 = MMult(matrix, this.Matrix);
			for (int i = 0; i < this.Vertexs.Length; i++) {
				vertexs[i].pos = VTransform(this.Vertexs[i].pos, matrix_2);
				vertexs[i].norm = VTransformSR(this.Vertexs[i].norm, matrix_2);
				vertexs[i].dif = this.Vertexs[i].dif;
				vertexs[i].spc = this.Vertexs[i].spc;
				vertexs[i].u = this.Vertexs[i].u;
				vertexs[i].v = this.Vertexs[i].v;
			}

			DrawPolygonIndexed3D(vertexs, vertexs.Length, this.VertexIndex, this.VertexIndex.Length / 3, this.TextureHandle, this.TransFlag ? TRUE : FALSE);

		}

	}
}
