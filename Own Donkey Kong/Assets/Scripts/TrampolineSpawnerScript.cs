using UnityEngine;

public class TrampolineSpawnerScript : MonoBehaviour
{
    [SerializeField] private GameObject trampoline;
    private float _timer;
    [SerializeField] private float trampolineSpawnRate;

    private void Start()
    {
        Instantiate(trampoline, transform.position, Quaternion.identity);
    }

    private void Update()
    {
        if (_timer < trampolineSpawnRate)
        {
            _timer += Time.deltaTime;
        }

        else
        {
            _timer = 0;
            Instantiate(trampoline, transform.position, Quaternion.identity);
        }
    }
}