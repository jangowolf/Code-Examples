using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableGameObject : MonoBehaviour
{
    [SerializeField] private GameObject myCamera;


    // Start is called before the first frame update
   
    void awake()
    {
        myCamera.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        myCamera = GameObject.FindGameObjectWithTag("ThirdPersonCamera");

    }
}
