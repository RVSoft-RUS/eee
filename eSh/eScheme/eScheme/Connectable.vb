Public Interface IConnectable 'Поддержка проводимости тока из одной точки во все оставшиеся
    Sub Change(from As Integer, condition As Integer)
    Sub Dispose()
	Function ForSave() As ArrayList
	'Enum Direct
	'	LEFT
	'	UP
	'	RIGHT
	'	DOWN
	'End Enum


End Interface
