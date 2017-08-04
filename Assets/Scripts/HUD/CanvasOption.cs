using UnityEngine;
using System.Collections;

public class CanvasOption : MonoBehaviour {

    public GameObject TextOption;
    public GameObject TextMusic;
    public GameObject TextSound;
    public GameObject ScrollMusic;
    public GameObject ScrollSound;
    public GameObject ButtonBack;

    private float m_screenwidth;
    private float m_screenheight;
    // Use this for initialization
    void Start()
    {
        m_screenwidth = Screen.width;
        m_screenheight = Screen.height;

        TextOption.GetComponent<RectTransform>().position = new Vector3(m_screenwidth * 0.5f, m_screenheight * 0.9f, 0);
        TextMusic.GetComponent<RectTransform>().position = new Vector3(m_screenwidth * 0.4f, m_screenheight * 0.4f, 0);
        TextSound.GetComponent<RectTransform>().position = new Vector3(m_screenwidth * 0.4f, m_screenheight * 0.6f, 0);
        ScrollMusic.GetComponent<RectTransform>().position = new Vector3(m_screenwidth * 0.6f, m_screenheight * 0.4f, 0);
        ScrollSound.GetComponent<RectTransform>().position = new Vector3(m_screenwidth * 0.6f, m_screenheight * 0.6f, 0);
        ButtonBack.GetComponent<RectTransform>().position = new Vector3(m_screenwidth * 0.85f, m_screenheight * 0.1f, 0);
    }
}
