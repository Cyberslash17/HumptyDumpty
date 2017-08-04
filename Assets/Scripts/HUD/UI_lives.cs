using UnityEngine;
using System.Collections;

public class UI_lives : MonoBehaviour {

    public GameObject _UI_P1;
    public GameObject _UI_P2;
    private float _width;
    private float _height;
	// Use this for initialization
	void Start () {
        _height = Screen.height;
        _width = Screen.width;
        _UI_P1.GetComponent<RectTransform>().position = new Vector3(_width * 0.1f, _height * 0.1f, 0);
        _UI_P2.GetComponent<RectTransform>().position = new Vector3(_width * 0.9f, _height * 0.1f, 0);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
