﻿//
//  IRenderer.cs
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
using OpenTK;
using ChronosEngine.Render2D;

namespace ChronosEngine.Interfaces {
	/// <summary>
	/// Renderer interface.
	/// </summary>
	public interface IRenderer2D {
		void Begin(ref Matrix4 matrix);
		void Draw(Sprite2D sprite, bool centered = false);
		void End();
	}
}

