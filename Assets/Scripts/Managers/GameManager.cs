using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI youWonTextPlayer1;
    public TextMeshProUGUI youLostTextPlayer1;
    public TextMeshProUGUI youWonTextPlayer2;
    public TextMeshProUGUI youLostTextPlayer2;
    public GameObject player1RulesMenuUI;
    public GameObject player2RulesMenuUI;
    public TextMeshProUGUI title;
    public Button restartButton;
    public bool isGameActive;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GameOver(String playerId, GameObject enemyPlayer)
    {
        PlayerController enemyScript = enemyPlayer.GetComponent<PlayerController>();

        ShowYouLostTextForPlayer(playerId);
        ShowYouWonTextForPlayer(enemyScript.playerId);
        ShowRestartButton();

        isGameActive = false;
    }

    public void ClosePlayerRulesMenu(string playerId)
    {
        if(playerId.Equals("1"))
            player1RulesMenuUI.gameObject.SetActive(false);
        else
            player2RulesMenuUI.gameObject.SetActive(false);
    }

    public void StartGame()
    {
        title.gameObject.SetActive(false);
        isGameActive = true;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void ShowRestartButton()
    {
        restartButton.gameObject.SetActive(true);
    }

    private void ShowYouWonTextForPlayer(string playerId)
    {
        if (playerId.Equals("1"))
            youWonTextPlayer1.gameObject.SetActive(true);
        else
            youWonTextPlayer2.gameObject.SetActive(true);
    }

    private void ShowYouLostTextForPlayer(string playerId)
    {
        if (playerId.Equals("1"))
            youLostTextPlayer1.gameObject.SetActive(true);
        else
            youLostTextPlayer2.gameObject.SetActive(true);
    }
}
