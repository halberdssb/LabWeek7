using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.ProBuilder.Shapes;
using UnityEditor;
using System.Transactions;
using Unity.VisualScripting;

public class Enemy : MonoBehaviour
{
    public Transform Target;
    public Vector3 Spawn;
    public float speed = 2f;

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            Target = other.transform;
            Debug.Log("Player detected - attack!");
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            Target = null;
            Debug.Log("Player out of range, resume patrol");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Spawn = transform.position;
        Target = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (Target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, Target.position, speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, Spawn, speed * Time.deltaTime);
        }
    }
}
