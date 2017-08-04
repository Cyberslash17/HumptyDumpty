using UnityEngine;
using System.Collections;

public class Umbrella : Pickable
{
    public float LaunchForce = 25f;
    public float RotationTransitionDuration = 0.25f;

    private bool isHostile = false;

    private PlayerController thrower;

    protected override void ActivateSpecialBehavior(Vector2 dir)
	{
        AudioManager.Instance().PlayInstant(6);

        switch (currentState)
        {
            case PickableState.PickedUp:
                Debug.Log("Activating Umbrella");

                StartCoroutine(RotateUmbrella());

                currentState = PickableState.InTheAir;
                break;

            case PickableState.InTheAir:
                Debug.Log("Throwing umbrella");

                ThrowUmbrella(dir);

                currentState = PickableState.UmbrellaThrown;
                break;
        }
    }

    private void ThrowUmbrella(Vector2 dir)
    {
        thrower = transform.parent.GetComponent<PlayerController>();

        transform.parent = null;
        
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }

        rb.useGravity = false;

        rb.AddForce(dir * LaunchForce, ForceMode.Impulse);
        rb.angularVelocity = new Vector3(0, 0, -20);

        currentState = PickableState.InTheAir;
        Destroy(gameObject, 5);

        isHostile = true;
        gameObject.tag = "Obstacle";
    }

    private IEnumerator RotateUmbrella()
    {
        float ratio = 0f;

        while (ratio < 1f)
        {
            ratio += Time.deltaTime / RotationTransitionDuration;
            float newAngle = Mathf.Lerp(0, -90, ratio);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, newAngle);

            yield return null;
        }
    }

	protected override void OnTriggerEnter(Collider other){
		base.OnTriggerEnter (other);

        if(currentState == PickableState.InTheAir && other.gameObject.CompareTag("Obstacle")){
			Debug.Log ("Death Averted");
			Destroy (other.gameObject);
			Destroy (gameObject);
		}
        else if (currentState == PickableState.UmbrellaThrown && other.GetComponent<PlayerController>() != null)
        {
            PlayerController otherController = other.GetComponent<PlayerController>();

            if (isHostile && thrower != otherController)
            {
                otherController.DecreaseLuck();
                Debug.Log("Player got hit by an Umbrella");
            }
        }
	}
}
