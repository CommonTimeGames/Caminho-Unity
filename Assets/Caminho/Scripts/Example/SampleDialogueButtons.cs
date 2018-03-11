using System.Collections;
using System.Collections.Generic;
using CommonTimeGames.UI;
using UnityEngine;

public class SampleDialogueButtons : MonoBehaviour
{
    public void ButtonPressed(int index)
    {
        switch (index)
        {
            case 0:
                DialogueScreen.StartDialogue(name: "test");
                break;

            case 1:
                DialogueScreen.StartDialogue(name: "test", package: "dialogue2");
                break;

            case 2:
                DialogueScreen.StartDialogue(name: "romance");
                break;
        }
    }
}
