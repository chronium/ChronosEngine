//
//  Resolution.cs
//
//  Author:
//       Chronium Silver (Andrei Dimitriu) <onlivechronium@gmail.com>
//
//  Copyright (c) 2015 Chronium @ ChronoStudios
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;

namespace ChronosEngine.Structures {
	/// <summary>
	/// Resolution struct used for the <see cref="ChronosEngine.Window"/> class.
	/// </summary>
	public struct Resolution {
		/// <summary>
		/// The width.
		/// </summary>
		public int Width;
		/// <summary>
		/// The height.
		/// </summary>
		public int Height;

		/// <summary>
		/// Initializes a new instance of the <see cref="ChronosEngine.Structures.Resolution"/> struct.
		/// </summary>
		/// <param name="width">Width.</param>
		/// <param name="height">Height.</param>
		public Resolution(int width, int height) {
			Width = width;
			Height = height;
		}
	}
}

