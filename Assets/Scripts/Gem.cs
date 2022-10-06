using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Gem.cs

    This code calls the ChangeScore() method in ScoreManager when it detects that
    the player has walked into a gem.

*/

public class Gem : MonoBehaviour
{
    public int gemValue = 1;
    private bool stoooopppbug = true;

    // detects when the player walks into a gem, then calls ChangeScore() in ScoreManager
    // and deletes the gem
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && stoooopppbug)
        {
            ScoreManager.instance.ChangeScore(gemValue);
            stoooopppbug = false;
            Destroy(this.gameObject); //deletes the gem that this script is attached to
        }
    }
}
