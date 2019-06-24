using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    Transform m_StartTransform;
    void Start()
    {
        m_StartTransform = transform;
    }
    
    void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, m_StartTransform.position.y + (Mathf.Sin(Time.time * 5)/2) * Time.deltaTime, transform.position.z);
        transform.rotation = Quaternion.Euler(m_StartTransform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + (100 * Time.deltaTime), m_StartTransform.rotation.eulerAngles.z);
    }
}
