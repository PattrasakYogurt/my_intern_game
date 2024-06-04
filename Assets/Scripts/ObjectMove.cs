using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 60f;
    [SerializeField] private GameObject objectsCanMove;
    [SerializeField] private Vector3 destination;
    [SerializeField] private bool isMoved = false;
    [SerializeField] private float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        SetRandomDestination();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isMoved && timer >= 10f) //60f = 5นาที
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
        float x = Random.Range(-50f, 50f); //แกน X
        float z = Random.Range(-50f, 50f);
        destination = new Vector3(x, transform.position.y, z);
    }
}
