using UnityEngine;
using System.Collections;

public class TurnOffLights : MonoBehaviour {

    public GameObject player;
    public GameObject[] lights;

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().tag == player.tag)
        {
            for (int i = 0; i < lights.Length; i++)
            {
                lights[i].SetActive(false);
            }
        }
    }
}
