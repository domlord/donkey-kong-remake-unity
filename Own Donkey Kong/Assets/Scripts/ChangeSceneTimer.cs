using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneTimer : MonoBehaviour
{
    [SerializeField] private float timeToChangeScene;
    [SerializeField] private string nameOfSceneToChangeTo;

    private void Update()
    {
        timeToChangeScene -= Time.deltaTime;
        if (timeToChangeScene <= 0) SceneManager.LoadScene(nameOfSceneToChangeTo);
    }
}