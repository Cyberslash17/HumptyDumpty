using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TitleMenu : MonoBehaviour {

    public Sprite[] MySprite;
    private float m_timer;
    private int m_i;
	// Use this for initialization
	void Start () {
        m_timer = Time.time;
        m_i = 1;
    }
	
	// Update is called once per frame
	void Update () {
	    if(Time.time > m_timer+0.2f)
        {
            GetComponent<Image>().sprite = MySprite[m_i];
            m_i++;
            if(m_i==MySprite.Length)
            {
                m_i = 0;
            }
            m_timer = Time.time;
        }
	}
}
