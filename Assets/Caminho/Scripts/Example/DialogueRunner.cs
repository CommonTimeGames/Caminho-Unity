using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CommonTimeGames.UI;

public class DialogueRunner : MonoBehaviour
{
    public InputField DialogueInput;
    public InputField PackageInput;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadPressed()
    {
        var dialogueName = DialogueInput.text;
        var packageName = PackageInput.text;

        if (string.IsNullOrEmpty(dialogueName))
        {
            dialogueName = "default";
        }

        if (string.IsNullOrEmpty(packageName))
        {
            packageName = "default";
        }

        DialogueScreen.StartDialogue(package: packageName, name: dialogueName);
    }
}
