using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemy_script : MonoBehaviour
{
    private NavMeshAgent agent;
    
    // Start is called before the first frame update
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<CapsuleCollider>().transform.position;
        //Debug.Log(pos);
        agent.SetDestination(pos);
    }
}
