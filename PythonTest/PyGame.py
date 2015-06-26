import clr
clr.AddReference("ChronosEngine.dll")

class PyGame(ChronoGame):
    def __init__(self):
        super(PyGame, self).__init__()

    def OnLoad(self, e):
        super(PyGame, self).OnLoad(e);