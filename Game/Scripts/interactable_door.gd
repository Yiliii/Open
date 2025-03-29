extends TileMapLayer

# SIGNALS
# ENUMS
# CONSTANTS
# @EXPORT VARIABLES
# PUBLIC VARIABLES
# PRIVATE VARIABLES
# @ONREADY VARIABLES

# PUBLIC METHODS
# PRIVATE METHODS

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	self.visible = false
	collision_enabled = false
	get_parent().get_node("ClosedDoor").visible = true
	get_parent().get_node("ClosedDoor").collision_enabled = true


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	pass

# SUBCLASSES

func _on_interaction_area_interacted() -> void:
	if Globals.key_fragments >= 5:
		get_parent().get_node("ClosedDoor").visible = false
		get_parent().get_node("ClosedDoor").collision_enabled = false
		self.visible = true 
		collision_enabled = true


func _on_interaction_area_body_exited(body:Node2D) -> void:
	if Globals.key_fragments >= 5:
		self.visible = true
		collision_enabled = false
