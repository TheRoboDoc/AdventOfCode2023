extends Node2D

const FILE_PATH: String = "res://Engine Schematic.txt"

var lines: Array[String]

var total: int = 0
var ratio: int = 0

# Called when the node enters the scene tree for the first time.
func _ready():
	lines = load_file(FILE_PATH)
	
	var regex_number: RegEx = RegEx.new()
	var regex_symbol: RegEx = RegEx.new()
	
	regex_number.compile("\\d+")
	regex_symbol.compile("[^.0-9]")
	
	var numbers: Dictionary = {}
	var symbols: Dictionary = {}
	
	for i in range(lines.size()):
		numbers[i] = regex_number.search_all(lines[i])
		symbols[i] = regex_symbol.search_all(lines[i])
	
	for i in range(numbers.keys().size()):
		for number in numbers[i] as Array[RegExMatch]:
			var num_start: int = number.get_start()
			var num_end: int = number.get_end()
			
			for symbol in symbols[i] as Array[RegExMatch]:
				var char_start: int = symbol.get_start()
				var char_end: int = symbol.get_end()
				
				if char_start == num_end:
					total += int(number.get_string())
				elif char_end == num_start:
					total += int(number.get_string())
			
			if i > 0:
				for symbol in symbols[i - 1] as Array[RegExMatch]:
					var char_start: int = symbol.get_start()
					var char_end: int = symbol.get_end()
					
					if char_start == num_end or char_start == num_end - 1:
						total += int(number.get_string())
					elif char_end == num_start or char_end == num_start + 1:
						total += int(number.get_string())
					elif char_start >= num_start and char_end <= num_end:
						total += int(number.get_string())
			
			if i < numbers.keys().size():
				for symbol in symbols[i + 1] as Array[RegExMatch]:
					var char_start: int = symbol.get_start()
					var char_end: int = symbol.get_end()
					
					if char_start == num_end or char_start == num_end - 1:
						total += int(number.get_string())
					elif char_end == num_start or char_end == num_start + 1:
						total += int(number.get_string())
					elif char_start >= num_start and char_end <= num_end:
						total += int(number.get_string())
	
	for i in range(symbols.keys().size()):
		for symbol in symbols[i] as Array[RegExMatch]:
			if symbol.get_string() != "*":
				continue
			
			var sym_start = symbol.get_start()
			var sym_end = symbol.get_end()
			
			var near_numbers: Array[int] = []
			
			for number in numbers[i] as Array[RegExMatch]:
				var num_start: int = number.get_start()
				var num_end: int = number.get_end()
				
				if sym_end == num_start or sym_start == num_end:
					near_numbers.append(int(number.get_string()))
				elif sym_start == num_end or sym_start == num_end:
					near_numbers.append(int(number.get_string()))
			
			if i > 0:
				for number in numbers[i - 1] as Array[RegExMatch]:
					var num_start: int = number.get_start()
					var num_end: int = number.get_end()
					
					if sym_end == num_start or sym_start == num_end:
						near_numbers.append(int(number.get_string()))
					elif sym_start == num_end or sym_start == num_end:
						near_numbers.append(int(number.get_string()))
					elif sym_start >= num_start and sym_start <= num_end:
						near_numbers.append(int(number.get_string()))
			
			if i < symbols.keys().size():
				for number in numbers[i + 1] as Array[RegExMatch]:
					var num_start: int = number.get_start()
					var num_end: int = number.get_end()
					
					if sym_end == num_start or sym_start == num_end:
						near_numbers.append(int(number.get_string()))
					elif sym_start == num_end or sym_start == num_end:
						near_numbers.append(int(number.get_string()))
					elif sym_start >= num_start and sym_start <= num_end:
						near_numbers.append(int(number.get_string()))
			
			if near_numbers.size() != 2:
				continue
			
			ratio += near_numbers[0] * near_numbers[1]
	
	$Label.text += str("Total: ", total, "\n")
	$Label.text += str("Ratio: ", ratio, "\n")
	print("Total: ", total)
	print("Ratio: ", ratio)


func load_file(FILE_PATH: String) -> Array[String]:
	var file = FileAccess.open(FILE_PATH, FileAccess.READ)

	var lines: Array[String] = []

	while not file.eof_reached():
		var line: String = file.get_line()
		lines.append(line.strip_edges())
	
	return lines
