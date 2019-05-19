Public Interface IMovable
	Function Move(from As IMovable, dX As Integer, dY As Integer) As Boolean
	Function GetX() As Integer
	Function GetY() As Integer
	Sub MoveOK()
End Interface
