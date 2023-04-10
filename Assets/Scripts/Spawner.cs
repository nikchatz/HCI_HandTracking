using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public List<GameObject> prefabs;
    public Text gestureName;

    public GestureRecognizer gRecognizer;

    public Transform handTransform;
    bool shouldShow = false;

    public void Spawn(int index)
    {
        Instantiate(prefabs[index], transform.position, transform.rotation);
    }

    public void showGestureName(string gName)
    {
        if (gName != "PushUps")
        {
            shouldShow = true;
        }
        else
        {
            if (gName == "PushUps" && handTransform.rotation.z < 0)
            {
                shouldShow = true;
            }
        }

        if (shouldShow)
        {
            gestureName.text = gName;
            gRecognizer.showCommand(gName);
        }
    }
}
