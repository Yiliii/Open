extends TileMapLayer

# SIGNALS
# ENUMS
# CONSTANTS
# @EXPORT VARIABLES
@export var fragment_count = 1  # Number of fragments in the chest
# PUBLIC VARIABLES
var opened = false # Track if chest has been opened
# PRIVATE VARIABLES
# @ONREADY VARIABLES
@onready var label = get_node_or_null("/Chest/Control")

# PUBLIC METHODS
# PRIVATE METHODS

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	self.visible = false
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	pass

# SUBCLASSES

func _on_interaction_area_interacted() -> void:
	if not opened:
		self.visible = true
		Globals.key_fragments += fragment_count
		opened = true
		if label:
			label.update_label("You found a key fragment!")
	else:
		if label:
			label.update_label("You have opened this chest already")


func _on_interaction_area_body_exited(body:Node2D) -> void:
	self.visible = false
	if opened:
		self.visible = true
