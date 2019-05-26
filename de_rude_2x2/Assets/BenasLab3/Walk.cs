using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : MonoBehaviour
{
    private GameObject player;
    public float speed = 15f;
    private Rigidbody rb;
    // Start is called before te first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveHorizontal = - (player.transform.position.x - transform.position.x);
        float moveVertical =  - (player.transform.position.z - transform.position.z);

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        transform.LookAt(player.transform);
        this.transform.position += transform.forward * speed * 0.5f * Time.deltaTime;
    }
}
