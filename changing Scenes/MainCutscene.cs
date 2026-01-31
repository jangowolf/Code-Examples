using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainCutscene : MonoBehaviour
{
    public GameObject player;
    public bool TriggerEntered;
    public string sceneToLoad;

    public bool FuneralCutsceneActivated = false;


    void Start()
    {
        TriggerEntered = false;
        player = GameObject.FindGameObjectWithTag("Player");

        if (FuneralCutsceneActivated == true)
        {
            this.gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            TriggerEntered = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            TriggerEntered = false;
        }
    }

    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
        if (TriggerEntered == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                //PlayerPrefs.SetString("LastExitName", exitName);
                FuneralCutsceneActivated = true;
                SceneManager.LoadScene(sceneToLoad);
            }
        }

        if (FuneralCutsceneActivated == true)
        {
            this.gameObject.SetActive(false);
        }
    }
}
