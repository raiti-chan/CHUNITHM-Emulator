using static DxLibDLL.DX;

namespace CHUNITHM_Emulator.Renderer.Object {

	/// <summary>
	/// 描画オブジェクトのインターフェイス
	/// </summary>
	interface IRendererObject {

		/// <summary>
		/// 拡大します。
		/// </summary>
		/// <param name="x">X方向</param>
		/// <param name="y">Y方向</param>
		/// <param name="z">Z方向</param>
		void Scale(float x, float y, float z);

		/// <summary>
		/// 拡大します。
		/// </summary>
		/// <param name="vector">拡大値</param>
		void Scale(VECTOR vector);

		/// <summary>
		/// 拡大します。これは、現在の状態より前に呼ばれたことになります。
		/// </summary>
		/// <param name="x">X方向</param>
		/// <param name="y">Y方向</param>
		/// <param name="z">Z方向</param>
		void ScaleBefore(float x, float y, float z);

		/// <summary>
		/// 拡大します。これは、現在の状態より前に呼ばれたことになります。
		/// </summary>
		/// <param name="vector">拡大値</param>
		void ScaleBefore(VECTOR vector);

		/// <summary>
		/// オブジェクトを平行移動します。
		/// </summary>
		/// <param name="x">X方向</param>
		/// <param name="y">Y方向</param>
		/// <param name="z">Z方向</param>
		void Translate(float x, float y, float z);

		/// <summary>
		/// オブジェクトを平行移動します。
		/// </summary>
		/// <param name="vector">平行移動値</param>
		void Translate(VECTOR vector);

		/// <summary>
		/// オブジェクトを平行移動します。これは、現在の状態より前に呼ばれたことになります。
		/// </summary>
		/// <param name="x">X方向</param>
		/// <param name="y">Y方向</param>
		/// <param name="z">Z方向</param>
		void TranslateBefore(float x, float y, float z);

		/// <summary>
		/// オブジェクトを平行移動します。これは、現在の状態より前に呼ばれたことになります。
		/// </summary>
		/// <param name="vector">平行移動値</param>
		void TranslateBefore(VECTOR vector);

		/// <summary>
		/// オブジェクトをX軸回転します。
		/// </summary>
		/// <param name="rad">回転値(ラジアン)</param>
		void RotationX(float rad);

		/// <summary>
		/// オブジェクトをX軸回転します。これは、現在の状態より前に呼ばれたことになります。
		/// </summary>
		/// <param name="rad">回転値(ラジアン)</param>
		void RotationXBefore(float rad);

		/// <summary>
		/// オブジェクトをY軸回転します。
		/// </summary>
		/// <param name="rad">回転値(ラジアン)</param>
		void RotationY(float rad);

		/// <summary>
		/// オブジェクトをY軸回転します。これは、現在の状態より前に呼ばれたことになります。
		/// </summary>
		/// <param name="rad">回転値(ラジアン)</param>
		void RotationYBefore(float rad);

		/// <summary>
		/// オブジェクトをZ軸回転します。
		/// </summary>
		/// <param name="rad">回転値(ラジアン)</param>
		void RotationZ(float rad);

		/// <summary>
		/// オブジェクトをZ軸回転します。これは、現在の状態より前に呼ばれたことになります。
		/// </summary>
		/// <param name="rad">回転値(ラジアン)</param>
		void RotationZBefore(float rad);

		/// <summary>
		/// オブジェクトを指定軸で回転します
		/// </summary>
		/// <param name="x">軸のベクトルのX</param>
		/// <param name="y">軸のベクトルのY</param>
		/// <param name="z">軸のベクトルのZ</param>
		/// <param name="rad">回転値(ラジアン)</param>
		void Rotation(float x, float y, float z, float rad);

		/// <summary>
		/// オブジェクトを指定軸で回転します。
		/// </summary>
		/// <param name="vector">軸のベクトル</param>
		/// <param name="raf">回転値(ラジアン)</param>
		void Rotation(VECTOR vector, float rad);

		/// <summary>
		/// オブジェクトを指定軸で回転します。これは、現在の状態より前に呼ばれたことになります。
		/// </summary>
		/// <param name="x">軸のベクトルのX</param>
		/// <param name="y">軸のベクトルのY</param>
		/// <param name="z">軸のベクトルのZ</param>
		/// <param name="rad">回転値(ラジアン)</param>
		void RotationBefore(float x, float y, float z, float rad);

		/// <summary>
		/// オブジェクトを指定軸で回転します。これは、現在の状態より前に呼ばれたことになります。
		/// </summary>
		/// <param name="vector">軸のベクトル</param>
		/// <param name="raf">回転値(ラジアン)</param>
		void RotationBefore(VECTOR vector, float rad);

		/// <summary>
		/// 行列を使ってオブジェクトを変換します。
		/// </summary>
		/// <param name="matrix">変換に使う行列</param>
		void TransForm(MATRIX matrix);

		/// <summary>
		/// 行列を使ってオブジェクトを変換します。これは、現在の状態より前に呼ばれたことになります。
		/// </summary>
		/// <param name="matrix">変換に使う行列</param>
		void TransFormBefore(MATRIX matrix);

		/// <summary>
		/// オブジェクトの頂点位置を行列で変換したものに置き換えます。
		/// このメソッドが呼ばれると、頂点が現在の行列で変換された物に置き換わり、保持している行列がリセットされます。。
		/// </summary>
		void Reflect();

		/// <summary>
		/// 行列を単位行列にリセットします。
		/// </summary>
		void ResetMatrix();

		/// <summary>
		/// オブジェクトを描画します。
		/// </summary>
		void Draw();

		/// <summary>
		/// オブジェクトを行列で変換し、描画します。
		/// </summary>
		/// <param name="matrix">変換に使う行列</param>
		void Draw(MATRIX matrix);

		/// <summary>
		/// オブジェクトを行列で変換し、描画します。これは、現在の状態より前に変換されたことになります。
		/// </summary>
		/// <param name="matrix">変換に使う行列</param>
		void DrawBefore(MATRIX matrix);

	}

}
