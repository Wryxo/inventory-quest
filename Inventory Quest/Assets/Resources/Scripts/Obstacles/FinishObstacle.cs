using UnityEngine;
using System.Collections;

public class FinishObstacle : MonoBehaviour
{

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            GameMaster.instance.StartVictory();
        }
    }
}
