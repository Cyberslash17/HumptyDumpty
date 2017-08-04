using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Generation : MonoBehaviour {

    public GameObject[] Tiles;
    public GameObject TilesBeginning;
    public GameObject Camera;
    public float TileOffset;

    private float m_nextsquarecheck; 
    private Queue<GameObject> m_tilesQueue;

	// Use this for initialization
	void Start () {
        m_nextsquarecheck = Camera.transform.position.x;
        m_tilesQueue = new Queue<GameObject>();
        m_nextsquarecheck -= TileOffset; // Hack for first tile.
        GameObject nextTile = (GameObject)Instantiate(TilesBeginning, new Vector3(-30, 0, 0), Quaternion.identity);
        m_tilesQueue.Enqueue(nextTile);
        nextTile = (GameObject)Instantiate(TilesBeginning, new Vector3(10, 0, 0), Quaternion.identity);
        m_tilesQueue.Enqueue(nextTile);
        m_nextsquarecheck = 10;
        SpawnNextPlatform(); // First tile.
        SpawnNextPlatform();
        SpawnNextPlatform();
    }
	
	// Update is called once per frame
	void Update () {
        float currentPosition = Camera.transform.position.x;

        // Spawn next tile if we get too far.
        if(currentPosition > m_nextsquarecheck)
        {
            SpawnNextPlatform();
            Destroy(m_tilesQueue.Dequeue());
        }
	}

    private void SpawnNextPlatform()
    {
        // Offset next check.
        m_nextsquarecheck += TileOffset;

        // Spawn next tile and destroy previous tile.
        GameObject nextTile = (GameObject)Instantiate(Tiles[Mathf.FloorToInt(Random.Range(0f, Tiles.Length))], new Vector3(m_nextsquarecheck, 0, 0), Quaternion.identity);
        m_tilesQueue.Enqueue(nextTile);  

    }
}
