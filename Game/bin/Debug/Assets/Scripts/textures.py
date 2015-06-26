import bindings

from ChronosEngine.Render2D import Sprite2D
from ChronosEngine.Textures import Texture2D
from System.Drawing import RectangleF
from OpenTK import Vector2

class TextureSheet(object):
	def __init__(self, texture, numx, numy):
		self.texture = texture
		self.dx = 1.0 / float(numx)
		self.dy = 1.0 / float(numy)
		self.numx = numx
		self.numy = numy

	def get_texture_rect(self, x, y):
		if x > self.numx or x < 0:
			x = 0
		if y > self.numy or y < 0:
			y = 0

		dx = self.dx
		dy = self.dy

		return RectangleF(x * dx, y * dy, dx, dy)

	def draw_crop(self, position, x, y, renderer, scale = 1):
		width = self.texture.Size.X * self.dx * scale
		height = self.texture.Size.Y * self.dy * scale

		renderer.Draw(self.texture, position, Vector2(width, height), self.get_texture_rect(x, y))

	def draw_crop(self, position, x, y, size, renderer):
		width = size.X
		height = size.Y

		renderer.Draw(self.texture, position, Vector2(width, height), self.get_texture_rect(x, y))


