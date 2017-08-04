using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public float m_scrollSpeed = 5f;
	public float m_scrollSpeedIncrease = 1f;
    public int m_nbRows = 3;
    public int m_nbColumns = 5;
    public GameObject[] ItemsList;
    bool m_isGameOver;
    string m_winnerName = "";
    List<GameObject> m_players;

	private float startTime;

    public bool IsGameOver
    {
        get
        {
            return m_isGameOver;
        }
    }

    public void EndGame(GameObject loser)
    {
        
        Time.timeScale = 1;
        m_players.Remove(loser);
        m_winnerName = m_players.Count > 0 ? m_players[0].GetComponent<PlayerController>().PlayerName : "Sorry you died";
        
        PlayerPrefs.SetString("winner",m_winnerName);
        SceneManager.LoadScene("GameOverScene");

    }

    public GameObject GetRandomItem()
    {
        if (ItemsList.Length <= 0) return null;
        return ItemsList[Random.Range(0, ItemsList.Length)];
    }

    void Start()
    {
        if (AudioManager.Instance())
        {
            AudioManager.Instance().PlayBackground(0); // main track
        }
        
        m_isGameOver = false;
		startTime = Time.time;
        m_players = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));
        Cursor.lockState = CursorLockMode.Confined;
    }
	
	void Update ()
    {
        if (Cursor.lockState != CursorLockMode.Confined)
        {
            Cursor.lockState = CursorLockMode.Confined;
        }

        if (!m_isGameOver)
        {
			this.GetComponent<Rigidbody>().velocity = (m_scrollSpeed + (Time.time - startTime) * m_scrollSpeedIncrease) * Vector3.right;
        }
	}
}
