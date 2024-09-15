using System;
using System.Collections;
using TMPro;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class UserInterFaceManager : MonoBehaviour
{
    [SerializeField] private TextMeshPro scoreEffectText;
    public Image marioLifeIconHolder;
    public GameObject marioLifeIcon;
    [SerializeField] private TextMeshProUGUI marioScore;
    [SerializeField] private AudioClip scoreSoundEffect;


    /**
     * life counter holder represents the image in the UI that the life icons are assigned to as children
     * life icon is the image that is to be displayed as an indicator for a life
     * lives count is number of lives currently
     */
    private void Start()
    {
        SetLivesCounter(GameManager.Instance.NumberOfLives, marioLifeIconHolder, marioLifeIcon);
        marioScore.SetText(GameManager.Instance.MarioScore.ToString());
    }

    private void SetLivesCounter(int livesCount, Image lifeIconHolder, GameObject lifeIcon)
    {
        for (var i = 0; i < livesCount; i++)
        {
            Instantiate(lifeIcon, lifeIconHolder.transform);
        }
    }

    private void ClearLivesCounter(Image lifeCounterHolder)
    {
        foreach (Transform lifeIcon in lifeCounterHolder.transform) Destroy(lifeIcon);
    }

    public void OnPlayerDamage(int livesCount, Image lifeCounterHolder, GameObject lifeIcon)
    {
        GameManager.Instance.ChangeNumberOfLives(-1);
        ClearLivesCounter(lifeCounterHolder);
        SetLivesCounter(livesCount, lifeCounterHolder, lifeIcon);
    }

    public void ChangeMarioScore(int valueToChangeScoreBy, Transform positionToSpawnPoints)
    {
        GameManager.Instance.ChangeScore(valueToChangeScoreBy);
        marioScore.SetText(GameManager.Instance.MarioScore.ToString());
        scoreEffectText.text = valueToChangeScoreBy.ToString();
        TextMeshPro scoreEffectTextInstance =
            Instantiate(scoreEffectText, positionToSpawnPoints.position,
                Quaternion.identity); //should be on mario's position,
        //or the position of the object collected/ barrel jumped over, as opposed to the position of the ui manager itself
        scoreEffectTextInstance.rectTransform.position = new Vector3(scoreEffectTextInstance.rectTransform.position.x,
            scoreEffectTextInstance.rectTransform.position.y,
            0);
        AudioManagerScript.Instance.PlaySoundFxClip(scoreSoundEffect, transform, 0.002f);
        StartCoroutine(TimeToLeaveScoreEffectAlive(1f, scoreEffectTextInstance));
    }

    public void ChangeMarioScoreNoSound(int valueToChangeScoreBy, Transform positionToSpawnPoints)
    {
        GameManager.Instance.ChangeScore(valueToChangeScoreBy);
        marioScore.SetText(GameManager.Instance.MarioScore.ToString());
        scoreEffectText.text = valueToChangeScoreBy.ToString();
       TextMeshPro scoreEffectTextInstance =
            Instantiate(scoreEffectText, positionToSpawnPoints.position,
                Quaternion.identity); //should be on mario's position,
        //or the position of the object collected/ barrel jumped over, as opposed to the position of the ui manager itself
        scoreEffectTextInstance.rectTransform.position = new Vector3(scoreEffectTextInstance.rectTransform.position.x,
            scoreEffectTextInstance.rectTransform.position.y,
            0);
        scoreEffectTextInstance.tag = "ScoreEffectText";
    }

    private static IEnumerator TimeToLeaveScoreEffectAlive(float timeToLeaveScoreEffectAliveSeconds,
        TextMeshPro scoreEffectTextInstance)
    {
        yield return new WaitForSeconds(timeToLeaveScoreEffectAliveSeconds);
        Destroy(scoreEffectTextInstance);
    }
}