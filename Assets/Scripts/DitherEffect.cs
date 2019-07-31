using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// originally from bitshifter at https://gist.github.com/josephbk117/8344b204588f328e50556a45db042e9c
[ExecuteInEditMode, ImageEffectAllowedInSceneView, RequireComponent(typeof(Camera))]
public class DitherEffect : MonoBehaviour
{
    private Material ditherMat;
    private Shader shader;
    [Range(0.0f, 1.0f)]
    public Texture mainTexture;
    public Texture ditherTexture;
    [Range(1, 64)]
    public int colourDepth = 32;

    private Material material
    {
        get
        {
            if (ditherMat == null)
            {
                shader = Shader.Find("Jazz/Dither");
                ditherMat = new Material(shader) { hideFlags = HideFlags.DontSave };
            }

            return ditherMat;
        }
    }

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        // for DitherJazz
        material.SetTexture("_MainTex", src);
        material.SetTexture("_DitherPattern", ditherTexture);
        material.SetFloat("_Colors", colourDepth);

        Graphics.Blit(src, dest, material);
    }
}