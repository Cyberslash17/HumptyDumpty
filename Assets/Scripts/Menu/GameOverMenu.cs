using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour {

    public Animator animator;

    void Start()
    {
        SetPlayerWinner();
        GameObject.Find("PlayerName").GetComponent<Text>().text = "P" + PlayerPrefs.GetInt("WinnerPlayer");
    }

    public void OnRestartClick()
    {
        SceneManager.LoadScene("TestLoopGame");
        Time.timeScale = 1;
    }
    public void OnLeaveClick()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;
    }

    public void SetPlayerWinner()
    {
        animator.SetInteger("Player", PlayerPrefs.GetInt("WinnerPlayer"));
    }
}
