extends Node2D

var plant_scene = load("res://plant.tscn")
@export var game_time : Node

var time_is_continous
var time_speed



@export var plant_menu : Node

func _ready() -> void:
	game_time.time_skipped.connect(next_day)


func _input(event: InputEvent) -> void:
	if event is InputEventMouseButton:
		
		if event.button_index == MOUSE_BUTTON_LEFT and event.pressed:
			print("Left click at: ", event.position)
			


		if event.button_index == MOUSE_BUTTON_RIGHT and event.pressed:
			print("Right click at: ", event.position)
			
			var size = plant_menu.new_size
			var col = plant_menu.new_color
			var gspeed = plant_menu.new_g_speed
			
			add_plant(event.position,size,col,gspeed)


func _process(delta: float) -> void:
	if game_time.time_is_continous:
		for p in $ColorRect.get_children():
			p.growth(game_time.time_speed)
	


func next_day():
	for p in $ColorRect.get_children():
		p.growth(10)
	

func add_plant(pos,size,col,gspeed):
	var np = plant_scene.instantiate()
	np.setup(pos,size,col,gspeed)
	$ColorRect.add_child(np)
	pass
