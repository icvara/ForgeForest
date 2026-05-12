extends Panel

var new_color = Color(0.183, 0.605, 0.415, 1.0)
var new_size = Vector2(30,30)
var new_g_speed = 1.0
func _on_h_slider_pressed() -> void:
	$Label4/ColorPicker.show()


func _on_color_picker_color_changed(color: Color) -> void:
	new_color = color


func _on_button_pressed() -> void:
	$Label4/ColorPicker.hide()





func _on_h_slider_value_changed(value: float) -> void:
		new_size = Vector2(10,10) * value


func _on_h_slider_growth_value_changed(value: float) -> void:
	new_g_speed = value
