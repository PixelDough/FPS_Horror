using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Camera camera;
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        

        Collider[] overlap = Physics.OverlapSphere(transform.position, 5f);
        foreach (Collider c in overlap)
        {
            if (c.gameObject.GetComponent<Monster_TV>())
            {
                c.gameObject.GetComponent<Monster_TV>().alive = true;
                LockOnObject(c.gameObject);
            }
        }
    }

    void LockOnObject(GameObject gameObject)
    {
        Cinemachine.CinemachineVirtualCamera[] VCameras = FindObjectsOfType<Cinemachine.CinemachineVirtualCamera>();
        foreach (Cinemachine.CinemachineVirtualCamera cam in VCameras)
        {
            
        }
        camera.GetComponent<Cinemachine.CinemachineBrain>().ActiveVirtualCamera.LookAt = gameObject.transform;
    }

    
}
