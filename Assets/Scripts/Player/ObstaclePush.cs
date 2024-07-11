using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePush : MonoBehaviour
{
    [SerializeField] private float forceMagnitude = 1;


    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.tag == "ObjectForPush" || hit.collider.tag == "PushObject")
        {
            Rigidbody rigibody = hit.collider.attachedRigidbody;
            if (rigibody != null)
            {
                //Vector3 forceDirection = hit.gameObject.transform.position - transform.position; old
                Vector3 forceDirection = hit.gameObject.transform.position - transform.position;
                forceDirection.y = 0;
                forceDirection.Normalize();

                rigibody.AddForceAtPosition(forceDirection * forceMagnitude, hit.point, ForceMode.Impulse);
            }
        }
    }

}
