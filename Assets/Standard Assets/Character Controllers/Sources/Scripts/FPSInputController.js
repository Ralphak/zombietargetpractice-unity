private var motor : CharacterMotor;
var walk : AudioClip;
var jump : AudioClip;
static var aiming : boolean;
static var directionVector : Vector3;

// Use this for initialization
function Awake () 
{
	motor = GetComponent(CharacterMotor);
}

// Update is called once per frame
function Update () {
	// Get the input vector from kayboard or analog stick
	directionVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
	
	if (directionVector != Vector3.zero) {
		// Get the length of the directon vector and then normalize it
		// Dividing by the length is cheaper than normalizing when we already have the length anyway
		var directionLength = directionVector.magnitude;
		directionVector = directionVector / directionLength;
		
		// Make sure the length is no bigger than 1
		directionLength = Mathf.Min(1, directionLength);
		
		// Make the input vector more sensitive towards the extremes and less sensitive in the middle
		// This makes it easier to control slow speeds when using analog sticks
		directionLength = directionLength * directionLength;
		
		// Multiply the normalized direction vector by the modified length
		directionVector = directionVector * directionLength;
		
		if(GetComponent.<AudioSource>().time==0 && motor.grounded)
		{
			GetComponent.<AudioSource>().clip=walk;
			GetComponent.<AudioSource>().Play();
		}
		else if(aiming) GetComponent.<AudioSource>().Stop();
	}
	else if(motor.grounded) GetComponent.<AudioSource>().Stop(); //Allows to play jump audio while not walking.
	
	if(Input.GetButton("Jump") && motor.grounded)
	{
		GetComponent.<AudioSource>().clip=jump;
		GetComponent.<AudioSource>().Play();
	}
		
	// Apply the direction to the CharacterMotor
	motor.inputMoveDirection = transform.rotation * directionVector;
	motor.inputJump = Input.GetButton("Jump");
}

// Require a character controller to be attached to the same game object
@script RequireComponent (CharacterMotor)
@script AddComponentMenu ("Character/FPS Input Controller")
