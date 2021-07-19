using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputController : MonoBehaviour
{
    public enum CursorMode
    {
        Default,
        AddBoids,
        RemoveBoids,
        AddPredator,
        AddFood
    }

    public CursorMode cursorMode = CursorMode.Default;

    Vector3 click_pos;
    float holdThreshold = 0.25f;
    float clickedTime = 0;
    bool buttonHeld = false;

    bool inCoroutine = false;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (cursorMode == CursorMode.Default)
            return;

        if(Input.GetMouseButtonDown(0))
        {
            clickedTime = Time.timeSinceLevelLoad;
            buttonHeld = false;
            click_pos = GetCursorPosition();
            switch (cursorMode)
            {
                case CursorMode.AddBoids:
                    if(!EventSystem.current.IsPointerOverGameObject())
                        TheBrain.AlmightyBrain._spawner.SpawnABoid(click_pos);
                    break;
            }
        }
        else if(Input.GetMouseButtonUp(0))
        {
            if(!buttonHeld) {
                //clicked

            }
            else {
                //end hold
                StopAllCoroutines();
                inCoroutine = false;
            }
        }
        if(Input.GetMouseButton(0)) {
            if(Time.timeSinceLevelLoad - clickedTime > holdThreshold && buttonHeld == false) {
                buttonHeld = true;
            }
            if(buttonHeld) {
                //do hold stuff
                switch (cursorMode)
                {
                    case CursorMode.AddBoids:
                        if(!inCoroutine)
                            StartCoroutine(SpawnBoidsWhenHeld());
                            inCoroutine = true;
                        break;
                }
            }
        }

    }

    IEnumerator SpawnBoidsWhenHeld() {
        while(true)
        {
            click_pos = GetCursorPosition();
            TheBrain.AlmightyBrain._spawner.SpawnABoid(click_pos);
            yield return new WaitForSeconds(0.1f);
        }
    }

    Vector3 GetCursorPosition() {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        return pos;
    }
}
