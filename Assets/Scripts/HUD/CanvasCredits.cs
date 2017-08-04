using UnityEngine;
using System.Collections;

public class CanvasCredits : MonoBehaviour {
    
    public GameObject ButtonBack;

    private float m_screenwidth;
    private float m_screenheight;
    // Use this for initialization
    void Start ()
    {
        m_screenwidth = Screen.width;
        m_screenheight = Screen.height;
        ButtonBack.GetComponent<RectTransform>().position = new Vector3(m_screenwidth * 0.85f, m_screenheight * 0.1f, 0);
    }
}
