using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GestureRecognizer : MonoBehaviour
{
    public Text gestureR;
    public Text gestureL;
    public Text command;

    string gName = "";

    public void showCommand(string g)
    {
        gName = g;

        if (gestureL.text.Equals(gestureR.text))
        {
            if (gestureL.text == "PlayDead")
            {
                gName = "Fight";
            }
            else if (gestureL.text == "Kick")
            {
                gName = "Fly";
            }
        }

        command.text = gName;
    }
}
