using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum AudioAreaName
{
    ArtStudio,
    Credits,
    Cutscene,
    DiningRoom,
    Dojo,
    End,
    Garden,
    GrandmothersRoom,
    Greenhouse,
    JizosRoom,
    ServantQuarters,
    TitleScreen,
    TorahikosRoom
}

public class SceneTransition : MonoBehaviour
{
    public GameObject player;
    public string sceneToLoad;
    public bool TriggerEntered;
   
    public GameObject sceneEntrence;

    public Vector3 spawnLocation;

    public AudioAreaName AudioArea = AudioAreaName.Garden;


    //simplified script for moving to a new scene.

    //Need to force player to spawn in X spot upon scene load.

    // Start is called before the first frame update

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        // DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        TriggerEntered = false;
        player = GameObject.FindGameObjectWithTag("Player");              
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

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (Time.timeScale == 0f)
        {
            return;
        }        
        else if (TriggerEntered == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                ChangeScene();
            }
            //print ("entered");
        }
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(sceneToLoad);
        spawnLocation = sceneEntrence.transform.position;
        player.transform.position = spawnLocation;
        
        // Trigger audio transitions
        GameObject playerPackage = GameObject.FindWithTag("PlayerPackage");
        AudioPlayer audio = playerPackage.GetComponent<AudioPlayer>();
        audio.ChangeArea(AudioArea.ToString());
    }
}
