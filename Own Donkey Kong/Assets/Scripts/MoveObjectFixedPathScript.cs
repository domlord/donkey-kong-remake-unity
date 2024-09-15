using UnityEngine;

public class FireballScript : MonoBehaviour
{
    [SerializeField] private Transform[] pathToMoveObject;
    private int _currentPath;
    [SerializeField] private float fireballMoveSpeed;


    private void Start()
    {
        transform.position = pathToMoveObject[_currentPath].transform.position;
    }

    private void Update()
    {
        if (_currentPath <= pathToMoveObject.Length - 1)
        {
            transform.position = Vector2.MoveTowards(transform.position,
                pathToMoveObject[_currentPath].transform.position,
                fireballMoveSpeed * Time.deltaTime);

            if (transform.position == pathToMoveObject[_currentPath].transform.position)

            {
                _currentPath++;
            }
        }

        if (_currentPath == pathToMoveObject.Length)
        {
            _currentPath = 0;
        }
    }
}