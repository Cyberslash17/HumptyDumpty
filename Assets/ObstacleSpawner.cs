using UnityEngine;
using System.Collections;

public class ObstacleSpawner : MonoBehaviour {

	public static float chance = 0.3f;
	public GameObject[] obstacles;
	// Use this for initialization
	void Start () {
		if (Random.value < chance) {
			GameObject ob = Instantiate (obstacles [Random.Range (0, obstacles.Length)], transform.position, Quaternion.identity) as GameObject;
			ob.transform.parent = transform;
			Pickable p = ob.GetComponent<Pickable> ();
			if (p != null)
				p.Activate (Vector2.zero);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
