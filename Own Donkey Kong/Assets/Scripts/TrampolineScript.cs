using System.Linq;
using UnityEngine;

public class TrampolineScript : MonoBehaviour
{
    private GameObject _trampolinePath;
    [SerializeField] private float trampolineMoveSpeed;
    private Transform[] _trampolinePathNodesArray;
    private int _trampolinePathIndex;


    private void Awake()
    {
        _trampolinePath = GameObject.FindGameObjectWithTag("TrampolinePath");

        _trampolinePathNodesArray = _trampolinePath.transform.Cast<Transform>().ToArray();

        transform.position = _trampolinePathNodesArray[_trampolinePathIndex].position;
    }

    private void Update()
    {
        if (_trampolinePathIndex <= _trampolinePathNodesArray.Length - 1)
        {
            transform.position = Vector2.MoveTowards(transform.position,
                _trampolinePathNodesArray[_trampolinePathIndex].transform.position,
                trampolineMoveSpeed * Time.deltaTime);

            if (transform.position == _trampolinePathNodesArray[_trampolinePathIndex].transform.position)

            {
                _trampolinePathIndex++;
            }
        }

        if (_trampolinePathIndex == _trampolinePathNodesArray.Length)
        {
            Destroy(gameObject);
        }
    }
}