using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaMovement : MonoBehaviour
{
    public Transform pos1, pos2;//the start and end position of the enemie's movement path
    public float speed;//the speed at which the enemy moves at
    public Transform startPos;//where the enemy starts once loaded in

    Vector3 nextPos;//end goal position of enemy, always going to either be start point or end point

    // Start is called before the first frame update
    void Start()
    {
        nextPos = startPos.position;
    }

    // Update is called once per frame
    void Update()
    {
        //conditionals are to change enemie's facing to the opposite direction once it hits the start/end point
        if (transform.position == pos1.position)
        {
            nextPos = pos2.position;
        }
        if (transform.position == pos2.position)
        {
            nextPos = pos1.position;
        }
        //moves enemy towards nextPos
        transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
    }

    //draws a line of the enemie's path
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(pos1.position, pos2.position);
    }
}
