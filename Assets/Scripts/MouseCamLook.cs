using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCamLook : MonoBehaviour
{
    [SerializeField]
    public float sensitivity = 2.0f;
    [SerializeField]
    public float smoothing = 2.0f;
    // The character is the capsule
    public GameObject character;

    public Shader RetroShader;

    // Get the incremental value of mouse moving
    private Vector2 mouseLook;
    // Smooth the mouse moving
    private Vector2 smoothV;
    
    void Start()
    {
        character = this.transform.parent.gameObject;

        Camera.main.RenderWithShader(RetroShader, "Opaque");
    }
    
    void Update()
    {
        // md is mouse delta
        var md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
        // The interpolated float result between the two float values.
        smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
        smoothV.y = Mathf.Clamp(smoothV.y, -80, 80);
        // Incrementally add to the camera look.
        mouseLook += smoothV;



        // Vector3.Right means the x-axis
        transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, character.transform.up);
    }
}
