using UnityEngine;

public class VelocityChanger : MonoBehaviour
{
    public GameObject gameobj; //thing that enters the material
    public string tag; //thing that enters the material
    public string material;

    public float honeyFactor = 0.4f;
    public float iceFactor = 1.7f;
    public float slimeFactor = 0.4f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(tag))
        {
            CoinMovement movt = gameobj.GetComponent<CoinMovement>();
            if (material == "Ice")
                EnterIce(movt);
            else if (material == "Slime")
                EnterSlime(movt);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(tag))
        {
            CoinMovement movt = gameobj.GetComponent<CoinMovement>();
            movt.materialVelChange = 1f;
        }
    }

    private void EnterSlime(CoinMovement movt)
    {
        movt.materialVelChange = slimeFactor;
    }

    private void EnterIce(CoinMovement movt)
    {
        movt.materialVelChange = iceFactor;
    }
}
