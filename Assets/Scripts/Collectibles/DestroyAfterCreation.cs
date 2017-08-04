using UnityEngine;
using System.Collections;

public class DestroyAfterCreation : MonoBehaviour {
	void Start ()
    {
        Destroy(gameObject, 2f);
	}
}
