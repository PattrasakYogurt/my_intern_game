using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickAndDrop : MonoBehaviour
{
    public Transform pickUpCenter;
    public float pickUpRadius;
    public LayerMask pickUpLayer;

    public Transform carryPoint;
    public Transform dropPoint;
    public Transform holdingItem;
    public Collider[] objectInPickArea;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            if(holdingItem == null)
            {
                //pick up
                PickUpObject();
            }
            else
            {
                //drop
                DropItem();
            }
        }
    }
    public void PickUpObject()
    {
        objectInPickArea = Physics.OverlapSphere(pickUpCenter.position, pickUpRadius, pickUpLayer);
        if(objectInPickArea != null )
        {
            float nearestDistance = 9999.0f;
            foreach(Collider collider in objectInPickArea)
            {
                if(Vector3.Distance(collider.transform.position, transform.position) < nearestDistance)
                {
                    nearestDistance = Vector3.Distance(collider.transform.position, transform.position);
                    holdingItem = collider.transform;
                }
            }
            holdingItem.parent = carryPoint;
            holdingItem.localPosition = Vector3.zero;
            holdingItem.localRotation = Quaternion.identity;
            holdingItem.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
    public void DropItem()
    {
        holdingItem.parent = null;
        holdingItem.position = dropPoint.position;
        holdingItem.rotation = transform.rotation;
        holdingItem.GetComponent <Rigidbody>().isKinematic = false;
        holdingItem = null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(pickUpCenter.position, pickUpRadius);

        Gizmos.color = Color.grey;
        Gizmos.DrawWireCube(carryPoint.position, Vector3.one);
    }
}
