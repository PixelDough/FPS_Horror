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

    public FlashBlack blackout;

    public Transform screenTransform;

    public CanvasGroup screenStaticUI;

    private int stepNum = 0;
    private AudioSource shockSource;
    private Vector3 startPos;
    private Quaternion startRot;
    private Vector3 startScale;
    private float startSpeed;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        startRot = transform.rotation;
        startScale = transform.localScale;
        startSpeed = GetComponent<NavMeshAgent>().speed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (alive)
        {
            if (Vector3.Distance(transform.position, GetComponent<NavMeshAgent>().destination) > 10)
            {
                GetComponent<NavMeshAgent>().speed = startSpeed * 2;
            }
            else
            {
                GetComponent<NavMeshAgent>().speed = startSpeed;
            }

            GetComponent<NavMeshAgent>().destination = FindObjectOfType<FirstPersonCharacterController>().gameObject.transform.position;
            GetComponent<NavMeshAgent>().updateRotation = false;
            if (blackout.IsOnBlack())
            {
                GetComponent<NavMeshAgent>().updatePosition = true;

            }
            else
            {
                GetComponent<NavMeshAgent>().updatePosition = false;
                InstantlyTurn(GetComponent<NavMeshAgent>().destination);
                screenStaticUI.alpha = 0.01f + Mathf.Max(0, (0.2f - Vector3.Distance(transform.position, GetComponent<NavMeshAgent>().destination)/20));

            }
            //GetComponent<NavMeshAgent>().speed = 3f;
            //lights.SetActive(true);
        }
        else
        {
            screenStaticUI.alpha = 0f;
            blackout.StopFlashing();
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
                transform.localScale = new Vector3(0, 0, 0);
                if (emergencyButton.hasBeenPressed)
                {
                    shockSource.Stop();
                    StartCoroutine(StartHit());
                    stepNum = 3;
                    powerSwitch.SetLights(false);
                }
                break;
            case 3:
                
                break;
            case 4:
                powerSwitch.SetLights(true);
                transform.position = startPos;
                transform.rotation = Quaternion.Euler(startRot.eulerAngles.x, startRot.eulerAngles.y + 180, startRot.eulerAngles.z);
                transform.localScale = startScale;
                GetComponentInChildren<Animator>().Play("Idle", 0, 0);
                StartCoroutine(StartAttack());
                stepNum = 5;
                break;
            case 5:

                break;
            case 6:
                alive = true;
                stepNum = 7;
                break;
            case 7:

                break;
        }
    }

    private void InstantlyTurn(Vector3 destination)
    {
        if ((destination - transform.position).magnitude < 0.1f) return;

        Vector3 direction = (destination - transform.position).normalized;
        Quaternion qDir = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, qDir, Time.deltaTime * 100f);
    }

    IEnumerator StartHit()
    {
        yield return new WaitForSeconds(2);
        blackout.StartFlashing();
        while (!blackout.IsOnBlack()) { yield return new WaitForSeconds(0f); }

        stepNum = 4;
    }

    IEnumerator StartAttack()
    {
        yield return new WaitForSeconds(0);
        //AudioManager.PlaySound(attackMusic, true);
        GetComponent<AudioSource>().Play();
        stepNum = 6;
    }

    public static bool IsVisibleToCamera(Transform transform)
    {
        Vector3 visTest = Camera.main.WorldToViewportPoint(transform.position);
        return (visTest.x >= 0 && visTest.y >= 0) && (visTest.x <= 1 && visTest.y <= 1) && visTest.z >= 0;
    }
}
