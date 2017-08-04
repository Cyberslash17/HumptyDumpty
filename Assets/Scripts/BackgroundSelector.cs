using UnityEngine;
using System.Collections;

public class BackgroundSelector : MonoBehaviour {

    public Texture[] Textures;
    public int ActiveTexture;

	// Set texture of background plane.
	void Start () {
        if (ActiveTexture < Textures.Length)
        {
            GetComponent<Renderer>().material.mainTexture = Textures[ActiveTexture];
        }
        else
        {
            Debug.Log("Error: Invalid background texture!");
        }
    }
}
