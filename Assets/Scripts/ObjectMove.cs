using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class ObjectMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 60f;
    [SerializeField] private GameObject objectsCanMove;
    [SerializeField] private Vector3 destination;
    [SerializeField] private bool isMoved = false;
    [SerializeField] private float timer = 0f;
    //[SerializeField] private Transform groundTopLeft;
    //[SerializeField] private Transform groundBottomRight;
    public Vector3 groundMin; // Minimum corner of the ground area
    public Vector3 groundMax; // Maximum corner of the ground area

    private Vector3 objectSize; // Size of the object
    
    void Start()
    {
        SetRandomDestination();
        // Calculate object size using collider bounds
        
        Collider collider = GetComponent<Collider>();
        if (collider != null)
        {
            objectSize = collider.bounds.extents;
        }
        else
        {
            Debug.LogError("No collider found on the object!");
        }
        
    }

    void Update()
    {
        Vector3 position = transform.position;

         //Clamp the position to keep the object within the ground bounds
        position.x = Mathf.Clamp(position.x, groundMin.x + objectSize.x, groundMax.x - objectSize.x);
        position.y = Mathf.Clamp(position.y, groundMin.y + objectSize.y, groundMax.y - objectSize.y);
        position.z = Mathf.Clamp(position.z, groundMin.z + objectSize.z, groundMax.z - objectSize.z);

        // Apply the clamped position back to the object
       transform.position = position;
        if (!isMoved && timer >= 10f) //60f = 5นาที
        {
            SetRandomDestination();
            isMoved = true;
            timer = 0f;
        }
        if(isMoved)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
            if(transform.position == destination)
            {
                isMoved = false;
            }
        }
        timer += Time.deltaTime;
    }
    private void SetRandomDestination()
    {
        float x = Random.Range(-40f, 40f); //แกน X
        float z = Random.Range(-40f, 40f);
        destination = new Vector3(x, transform.position.y, z); 
        //transform.position = Clamp(groundMin, groundMax); //ขยับได้ไม่เกินนี้
    }
    /*
    private Vector3 Clamp(Vector3 lowerLeft, Vector3 topRight)
    {
        Vector3 pos = new Vector3(Mathf.Clamp(transform.position.x, lowerLeft.x, topRight.x)
            , transform.position.y,
            Mathf.Clamp(transform.position.x, lowerLeft.x, topRight.x));
        return pos;
    }
    */

}
