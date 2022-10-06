using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public GameObject Portal;
    private GameObject Object;
    public bool isInside;
    private bool isInsideOther;
    private bool canTP = true;
    private bool otherCanTP = true;

    void Update()
    {
        isInsideOther = Portal.GetComponent<Teleporter>().isInside;
        canTP = Portal.GetComponent<Teleporter>().otherCanTP;
    }

    public void StartTeleport()
    {
        StartCoroutine(Teleport());
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        Object = other.gameObject;
        Debug.Log(canTP);

        isInside = true; 
        if (canTP)
        {   
            if (other.gameObject.tag == "Player" || other.gameObject.tag == "Obstacle")
            {
                StartTeleport();
                otherCanTP = false;
                //isInside = false; 
            }
        }
    }
    
    public void OnTriggerExit2D(Collider2D other)
    {
        isInside = false;
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Obstacle")
        {
            if (isInsideOther)
                otherCanTP = false;
            else
            {
                Portal.GetComponent<Teleporter>().otherCanTP = true;
                canTP = true;
            }
            Debug.Log("Exit"+canTP);
        }
    }
    
    IEnumerator Teleport()
    {
        yield return new WaitForSeconds(0.5f);
        Object.transform.position = new Vector2(Portal.transform.position.x, Portal.transform.position.y);
    }
}
