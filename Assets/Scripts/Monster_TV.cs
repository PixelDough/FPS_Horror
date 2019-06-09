using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster_TV : MonoBehaviour
{
    public bool alive = false;
    public GameObject lights;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (alive)
        {
            lights.SetActive(true);
            GetComponent<NavMeshAgent>().destination = FindObjectOfType<FirstPersonCharacterController>().gameObject.transform.position;
            GetComponent<NavMeshAgent>().speed = 3f;
        }
    }
}
