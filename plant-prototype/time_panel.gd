extends Panel

var time_is_continous = false
var time_speed = 1


signal time_skipped


func _on_check_box_toggled(toggled_on: bool) -> void:
	time_is_continous = toggled_on


func _on_h_slider_value_changed(value: float) -> void:
	time_speed = value


func _on_button_pressed() -> void:
	time_skipped.emit()
