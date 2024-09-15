using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CementTubSpawnerScript : MonoBehaviour
{
  [SerializeField] private float cementTubSpawnRate;
  [SerializeField] private GameObject cementTub;
  private float _spawnTimer;

  private void Update()
  {
    if (_spawnTimer < cementTubSpawnRate)
    {
      _spawnTimer += Time.deltaTime;
    }
    else
    {
      _spawnTimer = 0;
      Instantiate(cementTub, transform.position, Quaternion.identity);
    }
  }
}
