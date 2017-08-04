using UnityEngine;
using System.Collections;

public class Horseshoe : Pickable {

	public float LaunchForce = 10f;
    
	private int frameCount = 0;

	private bool isHostile = false;

	// Update is called once per frame
	void Update () {
		if (currentState == PickableState.InTheAir){
			frameCount++;
			if (frameCount == 90) {
				isHostile = true;
				gameObject.tag = "Obstacle";
				GetComponent<Rigidbody> ().velocity *= -1;
			}
		}
	}

	protected override void ActivateSpecialBehavior(Vector2 dir)
	{
        transform.parent = null;
        Debug.Log ("Activating Horseshoe");
        AudioManager.Instance().PlayInstant(8);

        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
		if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }
			
		rb.useGravity = false;
		
		rb.AddForce(dir * LaunchForce, ForceMode.Impulse);
		rb.angularVelocity = new Vector3 (0, 0, -20);

        transform.localScale = new Vector3(3, 3, 3);

        currentState = PickableState.InTheAir;
		Destroy (gameObject, 5);
	}

	protected override void OnTriggerEnter(Collider other)
	{
        base.OnTriggerEnter(other);

		if (other.GetComponent<PlayerController>() == null) return;

        PlayerController otherController = other.GetComponent<PlayerController>();

        if (otherController == null) return;

		if (currentState == PickableState.InTheAir){
			if(isHostile || other.gameObject != ownerCharacter.gameObject)
            {
                otherController.DecreaseLuck();
                Debug.Log("Player got hit by a Horseshoe");
            }
		}
	}
}
