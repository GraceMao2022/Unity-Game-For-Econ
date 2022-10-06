using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlatform : MonoBehaviour
{
    public GameObject player;
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            player.GetComponent<PlayerMovement>().DeathAnimation();
        }
    }
}
