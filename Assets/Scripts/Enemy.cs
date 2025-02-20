using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.ProBuilder.Shapes;

public class Enemy : MonoBehaviour
{
    private GameObject EnemyBody;
    private Rigidbody EnemyRB;
    public Transform Target;
    public Transform Spawn;

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
            Target = Spawn;
            Debug.Log("Player out of range, resume patrol");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Spawn.position = new Vector3(0, 0, 0);
        GameObject EnemyBody = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        EnemyBody.transform.position = Spawn.position;
        EnemyRB = EnemyBody.AddComponent<Rigidbody>();
        EnemyRB.useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 directionToTarget = Target.position - EnemyBody.transform.position;
        EnemyBody.transform.Translate(directionToTarget.normalized * Time.deltaTime *
        2);
    }
}
