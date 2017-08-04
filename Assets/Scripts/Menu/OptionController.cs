using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OptionController : MonoBehaviour {

    public GameObject MusicScroller;
    public GameObject SoundScroller;
    // Use this for initialization
    void Start ()
    {
        MusicScroller.GetComponent<Scrollbar>().value = PlayerPrefs.GetFloat("Music");
        SoundScroller.GetComponent<Scrollbar>().value = PlayerPrefs.GetFloat("Sound");
    }
	
	public void OnMusicChange()
    {
        PlayerPrefs.SetFloat("Music", MusicScroller.GetComponent<Scrollbar>().value);
    }
    public void OnSoundChange()
    {
        PlayerPrefs.SetFloat("Sound", SoundScroller.GetComponent<Scrollbar>().value);
    }
}
