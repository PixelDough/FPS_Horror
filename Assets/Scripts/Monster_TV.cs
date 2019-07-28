using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster_TV : MonoBehaviour
{
    public bool alive = false;
    public GameObject lights;

    public AudioClip shockSound;
    public AudioClip attackMusic;

    public PowerSwitch powerSwitch;
    public EmergencyButton emergencyButton;

    private int stepNum = 0;
    private AudioSource shockSource;
    private Vector3 startPos;
    private Quaternion startRot;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        startRot = transform.rotation;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (alive)
        {
            GetComponent<NavMeshAgent>().destination = FindObjectOfType<FirstPersonCharacterController>().gameObject.transform.position;
            //GetComponent<NavMeshAgent>().speed = 3f;
            //lights.SetActive(true);
        }

        switch (stepNum)
        {
            case 0:
                if (powerSwitch.isOn)
                {
                    stepNum = 1;
                }
                break;
            case 1:
                if (IsVisibleToCamera(transform))
                {
                    shockSource = AudioManager.PlaySound(shockSound, true);
                    stepNum = 2;
                }
                break;
            case 2:
                startPos = transform.position;
                startRot = transform.rotation;
                //transform.position = new Vector3(999, 999, 999);
                if (emergencyButton.hasBeenPressed)
                {
                    shockSource.Stop();
                    StartCoroutine(StartHit());
                    stepNum = 3;
                }
                break;
            case 3:
                
                break;
            case 4:
                transform.position = startPos;
                transform.rotation = Quaternion.Euler(startRot.eulerAngles.x, startRot.eulerAngles.y + 180, startRot.eulerAngles.z);
                GetComponentInChildren<Animator>().Play("Idle", 0, 0);
                StartCoroutine(StartAttack());
                stepNum = 5;
                break;
            case 5:

                break;
            case 6:
                alive = true;
                
                break;
        }
    }

    IEnumerator StartHit()
    {
        yield return new WaitForSeconds(2);
        stepNum = 4;
    }

    IEnumerator StartAttack()
    {
        yield return new WaitForSeconds(2);
        AudioManager.PlaySound(attackMusic, true);
        stepNum = 6;
    }

    public static bool IsVisibleToCamera(Transform transform)
    {
        Vector3 visTest = Camera.main.WorldToViewportPoint(transform.position);
        return (visTest.x >= 0 && visTest.y >= 0) && (visTest.x <= 1 && visTest.y <= 1) && visTest.z >= 0;
    }
}
