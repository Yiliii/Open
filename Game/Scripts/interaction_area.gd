extends Area2D

# SIGNALS
signal interacted
# ENUMS
# CONSTANTS
# @EXPORT VARIABLES
@export var interactable_icon : Sprite2D

# PUBLIC VARIABLES
# PRIVATE VARIABLES
var _can_interact = false
# @ONREADY VARIABLES

# PUBLIC METHODS
# PRIVATE METHODS

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	interactable_icon.visible = false
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	if not _can_interact:
		return 
	if Input.is_action_just_pressed("interact"):
		interacted.emit()



func _on_body_entered(body:Node2D) -> void:
	interactable_icon.visible = true
	_can_interact = true
	pass # Replace with function body.


func _on_body_exited(body:Node2D) -> void:
	interactable_icon.visible = false
	_can_interact = false
	pass # Replace with function body.


func _on_interacted() -> void:
	interactable_icon.visible = false
	pass # Replace with function body.
