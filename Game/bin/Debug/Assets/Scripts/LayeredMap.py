import bindings

from textures import TextureSheet
from ChronosEngine.Textures import Texture2D
from OpenTK import Vector2

class MapMeta(object):
	def __init__(self, tile_w, tile_h):
		self.tile_w = tile_w
		self.tile_h = tile_h

class MapLayer(object):
	def __init__(self, texture_sheet, map_meta):
		self.map = {}
		self.texture_sheet = texture_sheet
		self.map_meta = map_meta

	def add_tile(self, tile, coords):
		self.map[coords] = tile

	def draw_map(self, renderer):
		for kvp in self.map.items():
			position, tile = kvp
			x, y = position
			size = Vector2(self.map_meta.tile_w, self.map_meta.tile_h)
			pos = Vector2(x * size.X, y * size.Y)
			tx, ty = tile
			self.texture_sheet.draw_crop(pos, tx, ty, size, renderer)

class Map(object):
	def __init__(self, map_list, map_meta, texture_sheet):
		self.map_layers = []
		for layer in map_list:
			map_layer = MapLayer(texture_sheet, map_meta)
			map_layer.map = layer
			self.map_layers.append(map_layer)

	def draw(self, renderer):
		for layer in self.map_layers:
			layer.draw_map(renderer)

class LayeredMap(object):
	def __init__(self, map_layered, map_meta, texture_sheet):
		self.map = Map(map_layered, map_meta, texture_sheet)
		
	def draw(self, renderer):
		self.map.draw(renderer)
