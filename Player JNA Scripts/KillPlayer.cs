using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class KillPlayer : MonoBehaviour
{

    public GameObject _player;
    public GameObject _playerAstral;
    public GameObject _baku;

    public bool _protected;
    public bool isDead = false;

    public AIController _AIController; //reference to baku script
    public Rigidbody otherRigidbody; // Assign the other GameObject's Rigidbody in the Inspector

    [InjectOptional] private ProtectiveCharmManager _protectiveCharmManager;
    [Inject] private SignalBus _signalBus;
    
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerAstral = GameObject.FindGameObjectWithTag("Player_Astral");

        _baku = GameObject.FindGameObjectWithTag("BakuDemon"); //assigns baku gameobject by tag
        if(_baku != null) //if baku dmeon is not in scene this will stop a error from being thrown in console
        {
            _AIController = _baku.GetComponent<AIController>(); //gets the baku ai script for bool toggle
        }
        else
        {
            return;
        }
        
        

        //gets the playerpackage so that it can then get the ProtectiveCharmManager so it can use the ResetCharge function.
        GameObject playerpackage = GameObject.FindGameObjectWithTag("PlayerPackage"); 
        ProtectiveCharmManager ResetCharge = playerpackage.GetComponent<ProtectiveCharmManager>(); 

        otherRigidbody = _baku.GetComponent<Rigidbody>();

        isDead = false;
        _protected = false;                
    }

    private void SubscribeSignals() {
        try {
            _signalBus.Subscribe<ProtectiveChargeUpdatedSignal>(OnProtectiveChargeUpdated);
        } catch (Exception e) {
           Debug.LogException(e);
        }
        
    }

    //checks if charge level is max then triggers protection

    private void OnProtectiveChargeUpdated() {
        if (_protectiveCharmManager.ChargeValue >= _protectiveCharmManager.MaxCharge) {
            _protected = true;
        } else {
            _protected = false;
        }
    }
    
    private void UnsubscribeSignals() {
        _signalBus.TryUnsubscribe<ProtectiveChargeUpdatedSignal>(OnProtectiveChargeUpdated);
    }

    private void OnDestroy() {
        UnsubscribeSignals();
    }

    // Update is called once per frame
    void Update()
    {
        OnProtectiveChargeUpdated();

        if (isDead == true)
        {
            GameOver();
        }
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerAstral = GameObject.FindGameObjectWithTag("Player_Astral");

        _baku = GameObject.FindGameObjectWithTag("BakuDemon");
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "BakuDemon")
        {
            if (_protected == false)
            {
               isDead = true;                
            }
            else
            {
                if (_AIController.bakuStunned == true)
                {
                    return; //checks if baku is already stunned and then stops player from toggling state again.
                }
                else
                {
                    _AIController.bakuStunned = !_AIController.bakuStunned; //triggers bool bakuStunned in AIcontroller on baku.

                    //calls the reset function from the ProtectiveCharmManager script
                    _protectiveCharmManager.ResetCharge();
                }                
            }
        }      
    }
        
    void GameOver()
    {
        //The Scene to load when player is dead or what to do.
        SceneManager.LoadScene("Death");
    }
}
