Public Interface IConnectable 'Поддержка проводимости тока из одной точки во все оставшиеся
	Sub Change(from As Integer, condition As Integer)
	Sub Dispose()
	Function ForSave() As ArrayList
	Function CheckSig(from As Integer) As Integer
	'Enum Direct
	'	LEFT
	'	UP
	'	RIGHT
	'	DOWN
	'End Enum
	Function CheckUI(from As Integer, U As Single, Optional r_ As Integer = 0) As Single

End Interface
