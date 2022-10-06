using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour
{
    private bool stoooopppbug = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && stoooopppbug)
        {
            UtilManager.instance.ChangeUtil();
            stoooopppbug = false;
            Destroy(this.gameObject); 
        }
    }
}
