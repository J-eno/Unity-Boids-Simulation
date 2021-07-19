using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    public Animator toolbarAnimator;
 
    [Header("Bools")]
    [SerializeField]
    bool toolbarOpen;
    [SerializeField]
    bool addBoids;
    [SerializeField]
    bool removeBoids;
    [SerializeField]
    bool addPredator;
    [SerializeField]
    bool addFood;

    [Space]

    public TMP_InputField inputField;

    [Header("Toggles")]
    public Toggle addBoidsToggle;
    public Image addBoidsIcon;
    public Toggle removeBoidsToggle;
    public Image removeBoidsIcon;
    public Toggle addPredatorToggle;
    public Image addPredatorIcon;
    public Toggle addFoodToggle;
    public Image addFoodIcon;

    [Header("Icons")]
    Dictionary<string, Sprite> backgrounds = new Dictionary<string, Sprite>();
    Dictionary<string, Sprite> icons = new Dictionary<string, Sprite>();
    public Sprite[] backgroundSprites;
    public Sprite[] iconSprites;


    // Start is called before the first frame update
    void Start()
    {
        SetupDictionaries();
        toolbarOpen = toolbarAnimator.GetBool("ToolbarOpen");
        ToggleToolbar();
        
        //DELEGATES
        {
            addBoidsToggle.onValueChanged.AddListener( delegate {
            ToggleAddBoids();
        });

            removeBoidsToggle.onValueChanged.AddListener( delegate {
            ToggleRemoveBoids();
        });

            addPredatorToggle.onValueChanged.AddListener( delegate {
            ToggleAddPredator();
        });
            addFoodToggle.onValueChanged.AddListener( delegate {
            ToggleAddFood();
        });
        }
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

    public void ToggleAddBoids()
    {
        if(addBoidsToggle.isOn)
        {
            addBoidsIcon.sprite = icons["addboidwhite"];
            addBoidsToggle.GetComponent<Image>().sprite = backgrounds["blueblock"];
            TheBrain.AlmightyBrain._ic.cursorMode = InputController.CursorMode.AddBoids; 
        }
        else {
            addBoidsIcon.sprite = icons["addboidblue"];
            addBoidsToggle.GetComponent<Image>().sprite = backgrounds["blueoutline"];
            CheckCursorMode();   
        }
    }

    public void ToggleRemoveBoids() {
        if(removeBoidsToggle.isOn)
        {
            removeBoidsIcon.sprite = icons["removeboidwhite"];
            removeBoidsToggle.GetComponent<Image>().sprite = backgrounds["blueblock"];
            TheBrain.AlmightyBrain._ic.cursorMode = InputController.CursorMode.RemoveBoids;  
        }
        else {
            removeBoidsIcon.sprite = icons["removeboidblue"];
            removeBoidsToggle.GetComponent<Image>().sprite = backgrounds["blueoutline"];   
            CheckCursorMode();
        }
    }

    public void ToggleAddPredator() {
        if(addPredatorToggle.isOn)
        {
            addPredatorIcon.sprite = icons["addpredwhite"];
            addPredatorToggle.GetComponent<Image>().sprite = backgrounds["redblock"];  
            TheBrain.AlmightyBrain._ic.cursorMode = InputController.CursorMode.AddPredator;
        }
        else {
            addPredatorIcon.sprite = icons["addpredred"];
            addPredatorToggle.GetComponent<Image>().sprite = backgrounds["redoutline"];   
            CheckCursorMode();
        }
    }


public void ToggleAddFood() {
        if(addFoodToggle.isOn)
        {
            addFoodIcon.sprite = icons["addfoodwhite"];
            addFoodToggle.GetComponent<Image>().sprite = backgrounds["greenblock"];
            TheBrain.AlmightyBrain._ic.cursorMode = InputController.CursorMode.AddFood;  
        }
        else {
            addFoodIcon.sprite = icons["addfoodgreen"];
            addFoodToggle.GetComponent<Image>().sprite = backgrounds["greenoutline"];   
            CheckCursorMode();
        }
    }

    void CheckCursorMode() {
        //check if at least one button is toggled on
        bool toggled = addBoidsToggle.isOn 
                        || removeBoidsToggle.isOn 
                        || addPredatorToggle.isOn
                        || addFoodToggle.isOn;
        if(!toggled) {
            TheBrain.AlmightyBrain._ic.cursorMode = InputController.CursorMode.Default;
        }
    }

    void SetupDictionaries() {
        backgrounds["blueoutline"] = backgroundSprites[0];
        backgrounds["blueblock"] = backgroundSprites[1];
        backgrounds["greenoutline"] = backgroundSprites[2];
        backgrounds["greenblock"] = backgroundSprites[3];
        backgrounds["redoutline"] = backgroundSprites[4];
        backgrounds["redblock"] = backgroundSprites[5];

        icons["addboidblue"] = iconSprites[0];
        icons["addboidwhite"] = iconSprites[1];
        icons["removeboidblue"] = iconSprites[2];
        icons["removeboidwhite"] = iconSprites[3];
        icons["addpredred"] = iconSprites[4];
        icons["addpredwhite"] = iconSprites[5];
        icons["addfoodgreen"] = iconSprites[6];
        icons["addfoodwhite"] = iconSprites[7];
    }
}
