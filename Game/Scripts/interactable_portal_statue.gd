extends TileMapLayer

# SIGNALS
# ENUMS
# CONSTANTS
# @EXPORT VARIABLES
# PUBLIC VARIABLES
var active = false # Track if chest has been opened
# PRIVATE VARIABLES
# @ONREADY VARIABLES

# PUBLIC METHODS
# PRIVATE METHODS

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	get_parent().get_node("Statue").visible = false
	get_parent().get_node("Statue").collision_enabled = false
	self.visible = true
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	pass

# SUBCLASSES

func _on_interaction_area_interacted() -> void:
	self.visible = false 
	collision_enabled = false
	active = true


func _on_interaction_area_body_exited(body:Node2D) -> void:
	if active:
		self.visible = false
	else:
		self.visible = true
