using UnityEngine;
using System.Collections;

public class Cat : Pickable {
    public float LaunchForce = 8f;
    public float AttackDuration = 3f;

    [Range(0f, 90f)]
    public float LaunchAngleDegrees = 45f;

    public CatPawAttack CatPawAttackClass;

    protected override void ActivateSpecialBehavior(Vector2 dir)
    {
        if (AudioManager.Instance())
        {
            AudioManager.Instance().PlayInstant(2);
        }
        Debug.Log ("Activating Cat");

		if (currentState != PickableState.PickedUp) {
			Debug.Log ("Cat: Invalid state.");
			return;
		}

        transform.parent = null;

        float unitX = Mathf.Cos(LaunchAngleDegrees * Mathf.Deg2Rad) * transform.right.x;
        float unitY = Mathf.Sin(LaunchAngleDegrees * Mathf.Deg2Rad);

        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.AddForce(dir * LaunchForce, ForceMode.Impulse);
        currentState = PickableState.InTheAir;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        PlayerController otherController = other.GetComponent<PlayerController>();

        if (otherController == null) return;

        switch (currentState)
        {
            case PickableState.InTheAir:
            if (ownerCharacter.gameObject != other.gameObject)
            {
                Debug.Log("Player got hit by a cat");

                CatPawAttack pawAttack = GameObject.Instantiate(CatPawAttackClass, other.transform.position, Quaternion.identity) as CatPawAttack;
                pawAttack.SetAttackDuration(AttackDuration);
                otherController.GetCatSlashed(AttackDuration);
                pawAttack.SetParent(other.transform);

                Destroy(gameObject);
            }
                
            break;
        }
    }
}
