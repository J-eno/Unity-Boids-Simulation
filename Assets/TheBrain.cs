using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheBrain : MonoBehaviour
{
    public static TheBrain AlmightyBrain {get; private set;}
    public const int DEFAULT_SPAWN_AMOUNT = 25;
    public const int MAX_SPAWN_AMOUNT = 500;

    public event Action DestroyAll;

    public UIController _uic;
    public SpawnBoids _spawner;
    public InputController _ic;
    void Awake() {

        if(AlmightyBrain == null) {
            AlmightyBrain = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }
    public void Spawn()
    {
        int spawnAmount = DEFAULT_SPAWN_AMOUNT;
        //get text from input field
        string ifText = _uic.inputField.text;
        
        //if we can convert the text to an int
        if(Int32.TryParse(ifText, out spawnAmount))
        {
            //clamp int between 1 and 500
            if( spawnAmount > 500)
                spawnAmount = 500;
            else if( spawnAmount <= 0)
                spawnAmount = DEFAULT_SPAWN_AMOUNT;
        }
        //set input field text to our int
        _uic.inputField.text = spawnAmount.ToString();
        _spawner.StartSpawn(spawnAmount);
    }

    public void Clear() {
        DestroyAll?.Invoke();
        _spawner.StopAllCoroutines();
        _spawner.ResetBoidCount();
    }

}
