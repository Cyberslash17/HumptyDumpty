using UnityEngine;
using System.Collections;

public class TriggerGround : MonoBehaviour {

    bool m_waitForCheck = false;
    float m_checkDelay = .5f;
    void OnTriggerEnter(Collider other)
    {
        CheckObjectGround(other.gameObject);
    }

    void OnTriggerStay(Collider other)
    {
        CheckObjectGround(other.gameObject);
    }

    void OnTriggerExit(Collider other)
    {
        CheckObjectGround(other.gameObject);
    }

    void CheckObjectGround(GameObject o)
    {
        if (m_waitForCheck)
        {
            return;
        }
        bool isPlatform = o.CompareTag("Platform");
        bool isNotParent = PlayerIsNotParent(o.gameObject);
        transform.parent.GetComponent<PlayerController>().IsGrounded = isPlatform || isNotParent;
        StartCoroutine(WaitBeforeNextCheck());
    }

    bool PlayerIsNotParent(GameObject o)
    {
        if (o.CompareTag("Player"))
        {
            return o.GetInstanceID() != transform.parent.GetInstanceID();
        }
        return false;
    }

    IEnumerator WaitBeforeNextCheck()
    {
        m_waitForCheck = true;
        yield return new WaitForSeconds(m_checkDelay);
        m_waitForCheck = false;
    }
}
