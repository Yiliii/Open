extends TileMapLayer

# SIGNALS
# ENUMS
# CONSTANTS
# @EXPORT VARIABLES
# PUBLIC VARIABLES
# PRIVATE VARIABLES
# @ONREADY VARIABLES
@onready var interactable_control = get_node("/Door/Control") 
# PUBLIC METHODS
# PRIVATE METHODS

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	self.visible = true
	await get_tree().process_frame
	self.collision_enabled = true
	

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	pass

# SUBCLASSES

func _on_interaction_area_interacted() -> void:
	print("Hello from Godot!")
	if Globals.key_fragments >= 5:
		self.visible = false  # Hide locked door
		self.collision_enabled = false
	else:
		self.visible = true
		self.collision_enabled = true
		if interactable_control:
			interactable_control.show_locked_door_message()

func _on_interaction_area_body_exited(body:Node2D) -> void:
	self.visible = true
	self.collision_enabled = true
	pass
