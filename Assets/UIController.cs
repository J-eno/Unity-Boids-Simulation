using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public Animator toolbarAnimator;
    bool toolbarOpen;
    // Start is called before the first frame update
    void Start()
    {
        toolbarOpen = toolbarAnimator.GetBool("ToolbarOpen");
        ToggleToolbar();
    }
    public void ToggleToolbar() {
        toolbarOpen = !toolbarOpen;
        if(toolbarOpen)
        {
            toolbarAnimator.SetBool("ToolbarOpen", false);
        }
        else {
            toolbarAnimator.SetBool("ToolbarOpen", true);
        }
    }
}
