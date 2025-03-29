extends Control

# SIGNALS
# ENUMS
# CONSTANTS
# @EXPORT VARIABLES
# PUBLIC VARIABLES
# PRIVATE VARIABLES
# @ONREADY VARIABLES
@onready var label: Label = $Label

# PUBLIC METHODS
# PRIVATE METHODS

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	self.visible = false


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	pass

# SUBCLASSES

func _on_interaction_area_interacted() -> void:
	self.visible = true
	get_parent().get_node("Statue").visible = true
	get_parent().get_node("Statue").collision_enabled = true


func _on_interaction_area_body_exited(body:Node2D) -> void:
	self.visible = true
