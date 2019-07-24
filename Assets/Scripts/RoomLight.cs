using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class RoomLight : MonoBehaviour
{
    void Update()
    {
        GetComponent<Light>().range = 10f * (transform.lossyScale.x + transform.lossyScale.z) / 2f;
    }

}
