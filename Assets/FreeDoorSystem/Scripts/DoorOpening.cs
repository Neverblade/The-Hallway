/**
Initial code by Alexander Ameye.
Stripped down for specific use.
*/

using UnityEngine;
using System.Collections;

public class DoorOpening : MonoBehaviour {

	//INSPECTOR SETTINGS
	public enum SideOfHinge
	{
		Left,
		Right
	}
	public SideOfHinge HingeSide;

	public enum DoorSwingSide
	{
		Left,
		Right
	}
	public DoorSwingSide SwingSide;

	public float Angle = 90.0F; // Only use positive angles <180°.
	public float Speed = 3F; // Opening/closing speed of the door.

	//PRIVATE SETTINGS
	private int n = 0;
	[HideInInspector] public bool Running = false;
    [HideInInspector] public int State;

	// Define two end rotations for state 0 and 1.
	private Quaternion EndRot1, EndRot2;

	// Create a hinge.
	GameObject hinge;

	//START FUNCTION
	void Start ()
	{
		// Create a hinge.
		hinge = new GameObject();
		hinge.name = "hinge";


		//Make copy of hinge's position.
		Vector3 HingePosCopy = hinge.transform.position;

        // Math.
        HingePosCopy.x = transform.position.x;
        HingePosCopy.z = transform.position.z;
        HingePosCopy.y = transform.position.y;

        // Set the hinge to be exactly in the right-under corner of the door.
        hinge.transform.position = HingePosCopy;

		// Make the hinge the parent of the door.
		transform.parent = hinge.transform;

		// Make sure the door opens correctly when using different swingsides.
		if (SwingSide == DoorSwingSide.Left)
		{
			Angle = -Angle;
		}

		//Set 'EndRot1' to be the rotation when door is moved.
		EndRot1 = Quaternion.Euler(0, Angle, 0);

		//Set 'EndRot2' to be rotation when door is not yet moved.
		EndRot2 = Quaternion.Euler (0, transform.rotation.y, 0);

	}

	//OPEN FUNCTION
	public IEnumerator Open ()
  {
			// Set 'finalRotation' to 'Endrot1' when moving and to 'EndRot2' when moving back.
    	    Quaternion finalRotation = ((State == 0) ? EndRot1 : EndRot2);

    	    // Make the door rotate until it is fully opened/closed.
    	    while (Mathf.Abs(Quaternion.Angle(finalRotation, hinge.transform.rotation)) > 0.01f)
    	    {
				    Running = true;

    		    hinge.transform.rotation = Quaternion.Lerp (hinge.transform.rotation, finalRotation, Time.deltaTime * Speed);
      	    yield return new WaitForEndOfFrame();
    	    }
            State ^= 1;
            Running = false;
	}
}