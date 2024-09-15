using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFirstBarrel : MonoBehaviour
{
    [SerializeField] private GameObject firstBarrel;

    void Start()
    {
        Instantiate(firstBarrel, transform.position, Quaternion.identity);
    }
}