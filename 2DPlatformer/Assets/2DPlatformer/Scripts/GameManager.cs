using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public Text pickedUpPointsUIText;
    public Text playerLivesUIText;
    public Text gameOverText;

    int totalPoints = 0;

    GameObject thePlayer;

    public static GameManager Instance { get; set; }

    private void OnEnable()
    {
        EnemyBase.OnHitByEnemy += EnemyBase_OnHitByEnemy;
    }

    private void OnDisable()
    {
        EnemyBase.OnHitByEnemy -= EnemyBase_OnHitByEnemy;
    }

    // Use this for initialization
    void Awake () 
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);

        thePlayer = GameObject.FindGameObjectWithTag("Player");

		if (gameOverText)
		{
            gameOverText.enabled = false;
		}
	}
	
	
    public void AddPoints(int _points)
    {
        totalPoints += _points;

        pickedUpPointsUIText.text = totalPoints.ToString();
    }

    public void UpdatePlayerLives(int _playerLives)
    {
        if (playerLivesUIText)
            playerLivesUIText.text = _playerLives.ToString();
    }
    void EnemyBase_OnHitByEnemy(Vector3 hitPosition)
    {
        if (thePlayer.GetComponent<PlatformController2DSimple>().IsPlayerDead())
            StartCoroutine(RestartGame());
    }



    IEnumerator RestartGame()
    {
        if(gameOverText)
        {
            gameOverText.enabled = true;
        }

        yield return new WaitForSeconds(4f);

        SceneManager.LoadScene(0);
    }

}
