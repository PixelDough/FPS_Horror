using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashBlack : MonoBehaviour
{
    public void StartFlashing()
    {
        StartCoroutine(Flash());
    }

    public bool IsOnBlack()
    {
        if (GetComponent<CanvasGroup>().alpha == 1) { return true; }

        return false;
    }

    private IEnumerator Flash()
    {

        while (true)
        {
            GetComponent<Animator>().Play("FlashBlack", 0, 0f);
            yield return new WaitForSeconds(2.0f);
            
        }

    }
}
