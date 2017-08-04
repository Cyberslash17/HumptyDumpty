using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class TutorialController : MonoBehaviour {

    [SerializeField]
    Button m_nextButton;
    [SerializeField]
    Button m_backButton;
    [SerializeField]
    Button m_exitButton;
    [SerializeField]
    GameObject m_LeaveButton;
    [SerializeField]
    GameObject m_NextButton;
    [SerializeField]
    EventSystem m_eventSystem;
    [SerializeField]
    List<GameObject> m_sections;

    int m_currentSection = 0;
    bool m_isFirstSection = false;
    bool m_isLastSection = false;

	void Start ()
    {
        m_currentSection = 0;
        m_isFirstSection = true;
        m_backButton.gameObject.SetActive(false);
        for (int i = 1; i < m_sections.Count; ++i)
        {
            m_sections[i].SetActive(false);
        }
	}

    void Update()
    {
        m_backButton.gameObject.SetActive(true);
        m_nextButton.gameObject.SetActive(true);
        if (m_currentSection == 0 && m_backButton.gameObject.activeSelf)
        {
            m_backButton.gameObject.SetActive(false);
            m_eventSystem.SetSelectedGameObject(m_NextButton);
        }
        if(m_currentSection == m_sections.Count - 1 && m_nextButton.gameObject.activeSelf)
        {
            m_nextButton.gameObject.SetActive(false);
            m_eventSystem.SetSelectedGameObject(m_LeaveButton);
        }
    }
	
    public void NextSection()
    {
        m_sections[m_currentSection].SetActive(false);
        m_currentSection += 1;
        m_sections[m_currentSection].SetActive(true);
    }

    public void PreviousSection()
    {
        m_sections[m_currentSection].SetActive(false);
        m_currentSection -= 1;
        m_sections[m_currentSection].SetActive(true);
    }

    public void ExitTutorial()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }
}
