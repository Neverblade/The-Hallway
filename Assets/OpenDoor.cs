using UnityEngine;
using System.Collections;

public class OpenDoor : MonoBehaviour {

    public GameObject door;
    public GameObject player;
    public int version;

    /** Opens the door when the player steps into the trigger zone.
     *  Does not work when the door is currently moving or is open.
     */
    void OnTriggerEnter(Collider other)
    {
        //print("Player entered a trigger, version is " + version + ".");
        // It's the player that entered it and the door is currently closed.
        if (other.GetComponent<Collider>().tag == player.tag)
        {
            //Grab the script
            DoorOpening dooropening = door.GetComponent<DoorOpening>();

            //print("Door state is: " + dooropening.State + " and running is " + dooropening.Running);
            // Door must be closed 
            if (dooropening.State == version && dooropening.Running == false)
            {
                //print("Manipulating the door.");
                StartCoroutine(dooropening.Open());
            }
        }
    }
}
