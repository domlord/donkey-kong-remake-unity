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

    // private void OnCollisionEnter2D(Collision2D other)
    // {
    //     if ((other.collider.CompareTag("Barrel") || other.collider.CompareTag("CementTub")) && !_isHammering)
    //     {
    //         /*/
    //    This function (not technically a function, but you know what I mean) is used to reset all components- e.g. mario dies, all barrels deleted, back to start place etc.
    //    */
    //         _userInterFaceManager.OnPlayerDamage(GameManager.Instance.NumberOfLives,
    //             _userInterFaceManager.marioLifeIconHolder, _userInterFaceManager.marioLifeIcon);
    //         Scene currentScene =
    //             SceneManager
    //                 .GetActiveScene(); //cannot be done on awake or start, as the scene may change during playtime (player gets to end of level)
    //         SceneManager.LoadScene(currentScene.name);
    //     }
    // }
}