using System;
using UnityEngine;

public class BarrelSpawner : MonoBehaviour
{
    [SerializeField] private GameObject barrel;

    [SerializeField] private float barrelSpawnRate;
    private Transform _barrelSpawnerPosition;
    private float _timer;
    [SerializeField] private DonkeyKongScript donkeyKongScript;

    // Start is called before the first frame update
    private void Start()
    {
        _barrelSpawnerPosition = transform;
    }

    private void Awake()
    {
        donkeyKongScript.gameObject.SetActive(true);
    }

    private void SpawnBarrel()
    {
        Instantiate(barrel, new Vector2(_barrelSpawnerPosition.position.x + 1, _barrelSpawnerPosition.position.y - 1),
            _barrelSpawnerPosition.rotation);
    }
}