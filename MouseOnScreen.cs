using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOnScreen : MonoBehaviour
{

    // used to toggle the mouse in game.
    public bool mouseLock = true;
    public bool mouseVisible = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

       
        if (mouseLock == true)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
       
        if (mouseLock == false)
        {
            Cursor.lockState = CursorLockMode.None;
        }

        // used to hide the mouse in game.
        if (mouseVisible == true)
        {
            Cursor.visible = false;
        }

        if (mouseVisible == false)
        {
            Cursor.visible = true;
        }
        
        // when key M is pressed it toggles the state of the mouse
        if (Input.GetKeyDown(KeyCode.M))
        {
            changeStatus();

        }
       
    }

    public void changeStatus()
    {
        if (mouseVisible == true)
        {
            print("Mouse use state toggle on");
            mouseLock = false;
            mouseVisible = false;
        }

        else
        {
            print("Mouse use state toggled off");
            mouseLock = true;
            mouseVisible = true;
        }
    }
}
