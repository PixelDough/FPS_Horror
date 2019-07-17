using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomVisible : MonoBehaviour
{
    [RangeAttribute(1, 100)]
    public int chanceOfBeingVisible = 50;

    // Start is called before the first frame update
    void Awake()
    {
        int roll = Random.Range(0, 100);

        if (roll <= chanceOfBeingVisible)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
