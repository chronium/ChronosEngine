using OpenTK;

namespace ChronosEngine.Interfaces {
	public interface ICamera {

		#region Methods

		/// <summary>
		/// Updates this camera
		/// </summary>
		/// <param name="time">
		/// A <see cref="System.Double"/> containg the amount of time since the last update
		/// </param>
		void Update(double time);
		void UpdateMouse(double time);

		/// <summary>
		/// Returns the Projection matrix for this camera
		/// </summary>
		/// <param name="matrix">
		/// A <see cref="Matrix4"/> containing the Projection matrix
		/// </param>
		void GetProjectionMatrix(out Matrix4 matrix);

		/// <summary>
		/// Returns the Modelview Matrix for this camera
		/// </summary>
		/// <param name="matrix">
		/// A <see cref="Matrix4"/> containing the Modelview Matrix
		/// </param>
		void GetViewMatrix(out Matrix4 matrix);

		/// <summary>
		/// Returns the Modelview Matrix multiplied by the Projection matrix
		/// </summary>
		/// <param name="result">
		/// A <see cref="Matrix4"/> containg the multiplied matrices
		/// </param>
		void GetViewProjectionMatrix(out Matrix4 matrix);

		#endregion

	}
}
