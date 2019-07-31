using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public float playerSavedPositionX;
    public float playerSavedPositionY;
    public float playerSavedPositionZ;
    public Vector3 playerSavedHeadRotation;
    public Vector3 playerSavedCameraRotation;
    public List<int> playerSavedInventory = new List<int>();
    public List<string> powerSwitchesSavedActive = new List<string>();

    public static bool canSave = true;

    public Animator saveIcon;

    public GameObject Instance;

    private void Start()
    {
        Instance = this.gameObject;
        if (!GameManager.Instance.isNewGame)
        {
            LoadGame();
        }
    }

    public void SaveGame()
    {
        if (canSave)
        {
            StartCoroutine(PlaySaveIcon());

            Save save = CreateSaveGameObject();

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/saveGame.pd");
            bf.Serialize(file, save);
            file.Close();

            Debug.Log("Game Saved!");
        }
        else
        {
            Debug.Log("Cannot Save Game!");
        }

    }

    public static void QuickSave()
    {
        FindObjectOfType<Game>().SaveGame();
    }

    public void LoadGame()
    {

        if (File.Exists(Application.persistentDataPath + "/saveGame.pd"))
        {

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/saveGame.pd", FileMode.Open);
            Save save = (Save)bf.Deserialize(file);
            file.Close();

            playerSavedPositionX = save.playerPositionX;
            playerSavedPositionY = save.playerPositionY;
            playerSavedPositionZ = save.playerPositionZ;
            playerSavedHeadRotation = save.playerHeadRotation;
            playerSavedCameraRotation = save.playerCameraRotation;
            playerSavedInventory = save.playerInventory;
            FirstPersonCharacterController player = FindObjectOfType<FirstPersonCharacterController>();
            player.GetComponent<CharacterController>().enabled = false;
            player.transform.position = new Vector3(playerSavedPositionX, playerSavedPositionY, playerSavedPositionZ);
            player.GetComponent<CharacterController>().enabled = true;

            player.headJoint.transform.rotation = Quaternion.Euler(playerSavedHeadRotation);
            player.cam.transform.rotation = Quaternion.Euler(playerSavedCameraRotation);

            foreach(int i in playerSavedInventory)
            {
                player.items.Add((Interactive.Items)i);

                //foreach (Interactive thing in FindObjectsOfType(typeof(Interactive)))
                //{
                //    if ((int)thing.item == i)
                //    {
                //        Destroy(thing.gameObject);
                //    }
                //}
            }

            powerSwitchesSavedActive = save.powerSwitchesActive;

            foreach(PowerSwitch p in (PowerSwitch[])Resources.FindObjectsOfTypeAll(typeof(PowerSwitch)))
            {
                foreach(string n in powerSwitchesSavedActive)
                {
                    if (p.switchSaveName == n)
                    {
                        p.isOn = true;
                        p.SetLights(p.isOn);
                    }
                }
            }

            Debug.Log("Game Loaded!");
        }
        else
        {
            Debug.Log("No Save File!");
        }


    }

    private Save CreateSaveGameObject()
    {

        Save save = new Save();

        // Player Data
        FirstPersonCharacterController player = FindObjectOfType<FirstPersonCharacterController>();
        save.playerPositionX = player.transform.position.x;
        save.playerPositionY = player.transform.position.y;
        save.playerPositionZ = player.transform.position.z;
        save.playerHeadRotation = player.headJoint.transform.rotation.eulerAngles;
        save.playerCameraRotation = player.cam.transform.rotation.eulerAngles;
        foreach (Interactive.Items i in player.items)
        {
            save.playerInventory.Add((int)i);
        }

        // Power Switches
        PowerSwitch[] powerSwitches = (PowerSwitch[])Resources.FindObjectsOfTypeAll(typeof(PowerSwitch));
        foreach (PowerSwitch p in powerSwitches)
        {
            if (p.isOn)
            {
                save.powerSwitchesActive.Add(p.switchSaveName);
            }
        }

        return save;
    }


    public IEnumerator PlaySaveIcon()
    {
        saveIcon.Play("FadeIn", 0, 0);
        yield return new WaitForSeconds(1.0f);
        saveIcon.Play("FadeOut", 0, 0);
    }


    void OnApplicationQuit()
    {
        SaveGame();
    }
}
