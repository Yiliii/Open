extends CharacterBody2D

# SIGNALS
# ENUMS
# CONSTANTS
# @EXPORT VARIABLES
@export var speed : float
@export var inventory : Resource

# PUBLIC VARIABLES

# PRIVATE VARIABLES
# @ONREADY VARIABLES

# PUBLIC METHODS
func move(move_input):
	velocity = Vector2(0,0)
	match move_input:
		Globals.InputDirection.LEFT:
			velocity += Vector2(-1,0)
		Globals.InputDirection.RIGHT:
			velocity += Vector2(1,0)
		Globals.InputDirection.UP:
			velocity += Vector2(0,-1)
		Globals.InputDirection.DOWN:
			velocity += Vector2(0,1)
	velocity = velocity * speed
	move_and_slide()
# PRIVATE METHODS

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	pass

# SUBCLASSES
