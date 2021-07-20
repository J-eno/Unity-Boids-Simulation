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


    public float removeRadius = 1.5f;



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
                case CursorMode.RemoveBoids:
                    if(!EventSystem.current.IsPointerOverGameObject())
                        DeleteInRadius();
                    break;
            }
        }
        else if(Input.GetMouseButtonUp(0))
        {
            if(buttonHeld) {
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
                    //if in add boids mode, start the spawn boids coroutine
                    case CursorMode.AddBoids:
                        if(!inCoroutine){
                            StartCoroutine(SpawnBoidsWhenHeld());
                            inCoroutine = true;
                        }
                        break;
                    //if in remove boids mode, delete boids in radius of cursor    
                    case CursorMode.RemoveBoids:
                        if(!EventSystem.current.IsPointerOverGameObject())
                            DeleteInRadius();
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

    void DeleteInRadius() {
        Vector3 click_pos = GetCursorPosition();
        RaycastHit2D[] hits = Physics2D.CircleCastAll(click_pos, removeRadius, Vector2.zero);
        foreach(RaycastHit2D hit in hits) {
            hit.collider.GetComponent<HealthController>().TakeDamage();
        }
    }

    Vector3 GetCursorPosition() {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        return pos;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(Camera.main.ScreenToWorldPoint(Input.mousePosition), removeRadius);
    }
    
}
