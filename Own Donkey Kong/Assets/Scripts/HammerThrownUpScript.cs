using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerThrownUpScript : MonoBehaviour
{
    void Update()
    {
        if (gameObject.GetComponent<Rigidbody2D>().velocity.y < 0)
        {
            gameObject.tag = "Hammer";
        }
    }
}