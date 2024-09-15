using UnityEngine;
using UnityEngine.SceneManagement;

public class MarioDeath : MonoBehaviour
{
    private bool _isAlive;
    private GameObject mario;
    private Transform respawnPoint;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Barrel"))
        {
            Debug.Log("owie");

          
            // }
        }
    }
}