using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoNotDestroyOnLoad : MonoBehaviour
{
    public static DoNotDestroyOnLoad instance;

    public KillPlayer _killPlayer;

    public bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);

        _killPlayer = GameObject.FindObjectOfType<KillPlayer>();
    }

    void destroyPlayerPackage()
    {
        Destroy(gameObject);       
    }
    // Update is called once per frame
    void Update()
    {
        if (_killPlayer.isDead)
        {
            isDead = true;
            if (isDead == true)
            {
                destroyPlayerPackage();
            }
        }
        
        if (SceneManager.GetActiveScene().name == "End")
        {          
            destroyPlayerPackage();
        }
    }
}
