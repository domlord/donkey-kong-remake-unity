using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    [SerializeField] private GameObject mario;

    private void Awake()
    {
        Instantiate(mario, transform.position, Quaternion.identity);
        GameObject.FindGameObjectWithTag("Mario").GetComponent<MarioController>().enabled = true;
    }
}