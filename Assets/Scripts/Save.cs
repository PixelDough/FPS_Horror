using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Save
{

    public float playerPositionX;
    public float playerPositionY;
    public float playerPositionZ;
    public SerializableVector3 playerHeadRotation;
    public SerializableVector3 playerCameraRotation;
    public List<int> playerInventory = new List<int>();

    public List<string> powerSwitchesActive = new List<string>();
    
}
