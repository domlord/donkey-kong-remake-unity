using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CementTubScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("DestroyCementTub")) Destroy(gameObject);

        if (other.CompareTag("CementTub")) Destroy(gameObject);
    }
}