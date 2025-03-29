extends Control

@onready var label: Label = $Label  # Ensure the Label node is correctly referenced

func _ready() -> void:
	self.visible = false  # Initially hidden
	
# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	update_label()

# Function to update the label when fragments change
func update_label():
	if Globals.key_fragments < 5:
		label.text = "It's locked! You currently have " + str(Globals.key_fragments) + "/5 key fragments"
	if Globals.key_fragments >= 5:
		label.text = "The door is already unlocked! You collected all " + str(Globals.key_fragments) + "/5 key fragments"

# Function to be called when interacting with the door
func _on_interaction_area_interacted() -> void:
	update_label()
	self.visible = true  # Show the label when interacting

# Hide the label when the player leaves
func _on_interaction_area_body_exited(_body: Node2D) -> void:
	self.visible = false
