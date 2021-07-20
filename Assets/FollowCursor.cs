using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCursor : MonoBehaviour
{
    Vector3 position;
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(TheBrain.AlmightyBrain._ic.cursorMode == InputController.CursorMode.RemoveBoids)
        {
            SetAlpha(0.35f);
        }
        else {
            SetAlpha(0);
        }

        position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        position.z = 0;
        transform.position = position;
    }

    void SetAlpha(float alpha)
    {
        Color myColor = spriteRenderer.color;
        myColor.a = alpha;
        spriteRenderer.color = myColor;
    }
}
