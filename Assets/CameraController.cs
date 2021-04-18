using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController cam;
    public Vector2 min;
    public Vector2 max;
    void Awake() {
        cam = this;
    }

    void Start() {
        Camera mainCam = Camera.main;
        min = mainCam.ViewportToWorldPoint(new Vector2(0, 0));
        max = mainCam.ViewportToWorldPoint(new Vector2(1, 1));
    }
}
