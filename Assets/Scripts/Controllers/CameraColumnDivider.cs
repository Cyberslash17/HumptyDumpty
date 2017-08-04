using UnityEngine;
using System.Collections;

public class CameraColumnDivider : MonoBehaviour {
    
	public int columns = 5;
	public float snapSpeed = 3;
	public GameObject[] runners;

	private float _columnWidth = 0;
	private float _columnOffset;

	// Use this for initialization
	void Start () {
		_columnWidth = Camera.main.orthographicSize * Camera.main.aspect / columns * 2;
		if(columns%2 == 0){
			_columnOffset = 0.5f;
		}else{
			_columnOffset = 0f;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		foreach (GameObject runner in runners) {
			PlayerController pc = runner.GetComponent<PlayerController> ();
			if (pc != null) {
				int _charOffset = Mathf.FloorToInt(pc.GetLuck() - columns/2 + _columnOffset);

				Vector3 currentVelocity = runner.GetComponent<Rigidbody> ().velocity;
				Vector3 _pos = Vector3.right * (_columnWidth * (_charOffset + _columnOffset) + gameObject.transform.position.x);


				currentVelocity.x = (_pos.x + Vector3.left.x * runner.transform.position.x) * snapSpeed + GetComponent<Rigidbody>().velocity.x;
				runner.GetComponent<Rigidbody> ().velocity = currentVelocity;
			}
        }
	}
}
