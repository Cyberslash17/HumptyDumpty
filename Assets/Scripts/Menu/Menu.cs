using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Menu : MonoBehaviour {

    public GameObject CanvasOption;
    public GameObject CanvasCredit;
    [SerializeField]
    EventSystem m_eventSystem;
    [SerializeField]
    GameObject m_playButton;

    private bool first = true;

    void Update()
    {
        if(first)
        {
            AudioManager.Instance().PlayBackground(0); //TODO: Change to menu sound - now it's main track
            first = false;
        }
    }

    public void OnClickStart()
    {
        SceneManager.LoadScene("Introduction");
    }
    public void OnClickTuto()
    {
        SceneManager.LoadScene("Tutorial");
    }
    public void OnClickOption()
    {
        CanvasOption.SetActive(true);
    }
    public void OnClickCredit()
    {
        CanvasCredit.SetActive(true);
    }
    public void OnClickExit()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
    public void OnOptionQuit()
    {
        CanvasOption.SetActive(false);
        m_eventSystem.SetSelectedGameObject(m_playButton);
    }
    public void OnCreditsQuit()
    {
        CanvasCredit.SetActive(false);
        m_eventSystem.SetSelectedGameObject(m_playButton);
    }
}
