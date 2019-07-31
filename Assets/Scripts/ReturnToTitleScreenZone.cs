using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToTitleScreenZone : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<FirstPersonCharacterController>())
        {
            SceneManager.LoadScene(0);
        }
    }

}
