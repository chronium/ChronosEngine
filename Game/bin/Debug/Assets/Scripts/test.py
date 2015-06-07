import clr
clr.AddReference("ChronosEngine")
clr.AddReference("OpenTK")

from ChronosEngine.Render2D import Sprite2D
from OpenTK import Vector2

def load():
    add_object(Sprite2D(Vector2(128, 128), Vector2(32, 64)))

