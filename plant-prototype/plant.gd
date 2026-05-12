extends Sprite2D
@export var col : Color
@export var size : Vector2
@export var growth_speed : float

var min_size = Vector2(10,10)

var current_size = Vector2(10,10)

func _ready() -> void:
	pass
	#$ColorRect.color = col
	#$ColorRect.size = size



func growth(f):
	$ColorRect.size = current_size + Vector2(1,1)*f *0.1 * growth_speed
	$ColorRect.size.x = clamp($ColorRect.size.x,1,size.x)
	$ColorRect.size.y = clamp($ColorRect.size.y,1,size.y)
	position = position -  ($ColorRect.size - current_size)/2

	$Button.size = $ColorRect.size 
	current_size = $ColorRect.size 

func setup(pos,sizee,coll, growth_speedd):
	position = pos
	size = sizee
	$ColorRect.color = coll
	$ColorRect.size = min_size
	$Button.size = $ColorRect.size 
	current_size = min_size
	growth_speed = growth_speedd
	
	
	


func _on_button_pressed() -> void:
	growth(10)
