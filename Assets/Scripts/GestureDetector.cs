using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]
public struct Gesture 
{
    public string name;
    public List<Vector3> fingerData;
    public UnityEvent onRecognized;
}

public class GestureDetector : MonoBehaviour
{
    public float threshold = 0.1f;
    public OVRSkeleton skeleton;
    public List<Gesture> gestures;
    public bool debugMode = true;
    private List<OVRBone> fingerBones;
    private Gesture previousGesture;
    public Text gestureText;

    // Start is called before the first frame update
    void Start()
    {
        fingerBones = new List<OVRBone>(skeleton.Bones);
        previousGesture = new Gesture();
    }

    // Update is called once per frame
    void Update()
    {
        // if in debug mode and we press Space, we will save a gesture
        if (debugMode && Input.GetKeyDown(KeyCode.Space))
        {
            // Initialize fingerBones list - proper initialization in Start() fails
            fingerBones = new List<OVRBone>(skeleton.Bones);

            // Call the function for save the gesture
            Save();
        }

        // start to Recognize every gesture we make
        Gesture currentGesture = Recognize();

        // we will associate the recognize to a boolean to see if the Gesture
        // we are going to make is one of the gesture we already saved
        bool hasRecognized = !currentGesture.Equals(new Gesture());

        // and if the gesture is recognized
        if (hasRecognized && !currentGesture.Equals(previousGesture))
        {
            Debug.Log("New Gesture Found: " + currentGesture.name);
            previousGesture = currentGesture;

            // after that i will invoke what put in the Event if is present
            currentGesture.onRecognized.Invoke();
            //gestureText.text = currentGesture.name;
        }
    }

    void Save()
    {
        //create new gesture
        Gesture g = new Gesture();
        g.name = "New Gesture";
        List<Vector3> data = new List<Vector3>();

        //add finger data
        foreach (var bone in fingerBones)
        {
            //finger position relative to root
            data.Add(skeleton.transform.InverseTransformPoint(bone.Transform.position));
        }

        //assign finger data and gesture
        g.fingerData = data;
        gestures.Add(g);
    }

    Gesture Recognize()
    {
        // in the Update if we initialized correctly, we create a new Gesture
        Gesture currentGesture = new Gesture();

        // we set a new float of a positive infinity
        float currentMin = Mathf.Infinity;

        // we start a foreach loop inside our list of gesture
        foreach (var gesture in gestures)
        {
            // initialize a new float about the distance
            float sumDistance = 0;

            // and a new bool to check if discart a gesture or not
            bool isDiscarded = false;

            // then with a for loop we check inside the list of bones we initalized at the start with "SetSkeleton"
            for (int i = 0; i < fingerBones.Count; i++)
            {
                // then we create a Vector3 that is exactly the transform from global position to local position of the current hand
                // we are making the gesture
                Vector3 currentData = skeleton.transform.InverseTransformPoint(fingerBones[i].Transform.position);

                // with a new float we calculate the distance between the current gesture we are making with all the gesture we saved
                float distance = Vector3.Distance(currentData, gesture.fingerData[i]);

                // if the distance is bigger respect the threshold
                if (distance > threshold)
                {
                    // then we discart it because or is another gesture or we made bad the gesture we wanted to do
                    isDiscarded = true;
                    break;
                }

                // if the distance is correct we will add it to the first float we have created
                sumDistance += distance;
            }

            // if the gesture we made is not discarted and the distance of the gesture i minor then then Mathf.inifinty
            if (!isDiscarded && sumDistance < currentMin)
            {
                // then we set current min to the distance we have
                currentMin = sumDistance;

                // and we associate the correct gesture we have just done to the variable we created
                currentGesture = gesture;
            }
        }

        // so in the end we can return from the function the exact gesture we want to do
        return currentGesture;
    }
}
