using UnityEngine;
using System.Collections;

public class ItemSpawner : MonoBehaviour {

    public float SpawnChance = 0.5f;

	// Use this for initialization
	void Start ()
    {
        if (Random.Range(0f, 1f) <= SpawnChance)
        {
            Debug.Log("Spawn collectible");
            
            GameController gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
            GameObject.Instantiate(gc.GetRandomItem(), transform.position, Quaternion.identity);
        }
        Destroy(this.gameObject);
    }
}
