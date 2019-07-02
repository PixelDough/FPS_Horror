using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedTileTexture : MonoBehaviour
{
    public float animationCycleTime;
    public int texTilesX, texTilesY;
    public bool autoPlay;
    public bool billboardingOn = false;

    protected Renderer animatedRenderer;
    protected Transform cameraTransform;
    protected bool stopSignal;
    protected bool isPlaying;
    protected bool playOnce;

    private Renderer renderer;

    public virtual void Play(bool in_playOnce = false)
    {
        //Debug.Log ("PLAY: isPlaying="+isPlaying+"  stopSignal="+stopSignal);
        playOnce = in_playOnce;
        if (!isPlaying)
        {
            stopSignal = false;
            StartCoroutine(AnimateTexture());
        }
    }
    public virtual void PlayOnce()
    {
        Play(true);
    }
    public virtual void Stop()
    {
        //Debug.Log ("STOP: isPlaying="+isPlaying+"  stopSignal="+stopSignal);
        if (isPlaying)
            stopSignal = true;
    }
    public virtual bool IsPlaying()
    {
        return isPlaying;
    }
    // ----------
    protected virtual void Awake()
    {
        renderer = GetComponent<Renderer>();
        InitController();
        ApplyScale(new Vector2(1.0f / texTilesX, 1.0f / texTilesY));

        isPlaying = false;
        stopSignal = false;
    }
    protected virtual void Start()
    {
        renderer = GetComponent<Renderer>();
        if (autoPlay)
            Play();
    }
    protected virtual void InitController()
    {
        cameraTransform = Camera.main.transform;
        animatedRenderer = renderer;
        animatedRenderer.enabled = false;
    }

    private IEnumerator AnimateTexture()
    {
        AnimationEnable();
        isPlaying = true;
        for (int i = texTilesY - 1; i > -1 && !stopSignal; i--)
        {
            for (int j = 0; j < texTilesX && !stopSignal; j++)
            {
                ApplyOffset(new Vector2(1.0f / texTilesX * j, 1.0f / texTilesY * i));
                yield return new WaitForSeconds(animationCycleTime / (texTilesX * texTilesY));
                if (i == 0 && j == texTilesX - 1 && !playOnce) //reset cycle, if !playOnce
                    i = texTilesY;
            }
        }
        AnimationDisable();
        isPlaying = false;
        stopSignal = false;
        OnFinish();
    }

    protected virtual void AnimationEnable()
    {
        animatedRenderer.enabled = true;
    }
    protected virtual void AnimationDisable()
    {
        animatedRenderer.enabled = false;
    }
    protected virtual void ApplyOffset(Vector2 offset)
    {
        animatedRenderer.material.mainTextureOffset = offset;
    }
    protected virtual void ApplyScale(Vector2 scale)
    {
        animatedRenderer.material.mainTextureScale = scale;
    }


    protected virtual void Update()
    {
        if (billboardingOn)
            transform.rotation = cameraTransform.rotation;
    }

    protected virtual void OnFinish()
    {
    }
}
