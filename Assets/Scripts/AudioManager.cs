using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

    public AudioClip[] AudioClips;

    private static AudioManager m_instance;

    private AudioSource background, instant;

    public static AudioManager Instance()
    {
        if (m_instance == null)
        {
            m_instance = new AudioManager();
        }
        return m_instance;
    }

    // Use this for initialization
    void Start () {

        // Singleton
        if (m_instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        m_instance = this;
        background = gameObject.AddComponent<AudioSource>();
        instant = gameObject.AddComponent<AudioSource>();
        DontDestroyOnLoad(this.gameObject);
    }
	
	public void PlayInstant(int index, bool playOnce = true)
    {
        instant.clip = AudioClips[index];
        instant.loop = !playOnce;
        instant.Play();
    }

    public void PlayBackground(int index)
    {
        background.clip = AudioClips[index];
        background.loop = true;
        background.Play();
    }
}
