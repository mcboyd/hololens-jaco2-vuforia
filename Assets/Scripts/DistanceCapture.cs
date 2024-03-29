﻿using HoloToolkit.Unity.InputModule;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.WSA.Input;
using UnityEngine.XR.WSA.WebCam;

public class DistanceCapture : MonoBehaviour {

    private GestureRecognizer recognizer;

    private Transform _sphere;

    private TextMesh _sphereLabel;

    private int tapCount = 0;

    // Use this for initialization
    void Start () {
        recognizer = new GestureRecognizer();
        recognizer.SetRecognizableGestures(GestureSettings.Tap);
        recognizer.Tapped += TapHandler;
        recognizer.StartCapturingGestures();
		
	}
	
    private void TapHandler(TappedEventArgs obj)
    {
        if (tapCount == 0)
        {
            // Instantiate plane and sphere
            ArmPlane.instance.CreatePlane();
            TargetSphere.instance.CreateSphere();
            //Vector3 vectPtoS = ArmPlane.instance.transform.position - TargetSphere.instance.transform.position;
            //string displayText = "Arm Pos: " + ArmPlane.instance.transform.position.ToString() + "\r\nSphere Pos: " + TargetSphere.instance.transform.position.ToString() + "\n\r" + vectPtoS.ToString();
            // TargetSphere.instance.lastSphereLabelPlacedText.text = (new Vector3(-0.5f, 0.5f, 0.5f)).ToString();
            // TargetSphere.instance.lastSphereLabelPlacedText.text = displayText; // (vectPtoS).ToString();
        }
        else if (tapCount > 0)
        {
            //string test = GazeManager.Instance.HitObject.name;
            //_sphereLabel = GameObject.Find("SphereLabel1").GetComponent<TextMesh>();
            //_sphereLabel.text = "YES! " + test;
            //_sphereLabel.color = Color.red;
            // ResultsLabel.instance.CreateLabel();
            string objName = GazeManager.Instance.HitObject.name;
            if (objName.StartsWith("Sphere"))
            {
                //string sphereTagNumber = objName.Substring(objName.Length - 1);
                //_sphereLabel = GameObject.Find("SphereLabel1").GetComponent<TextMesh>();
                //_sphereLabel.text = "YES! Tag#: " + sphereTagNumber + "\r\nX: " + GazeManager.Instance.HitObject.GetComponent<SphereXYZ>().X.ToString();
                //_sphereLabel.color = Color.red;

                //Set all other sphere's back to default render look
                for (int i = 0; i < 6; i++)
                {
                    Renderer rendi = GameObject.Find("Sphere"+i.ToString()).GetComponent<Renderer>();
                    rendi.material.color = new Color(1f, 1f, 1f, 0.576f);  // Default white color for all spheres
                }

                //Fetch the Renderer from the GameObject
                Renderer rend = GazeManager.Instance.HitObject.GetComponent<Renderer>();
                rend.material.color = new Color(0.502f, 1f, 0f, 0.8f);  // Green color for the selected sphere

                float _x = GazeManager.Instance.HitObject.GetComponent<SphereXYZ>().X;
                float _y = GazeManager.Instance.HitObject.GetComponent<SphereXYZ>().Y;
                float _z = GazeManager.Instance.HitObject.GetComponent<SphereXYZ>().Z;
                //Debug.Log(String.Format("About to send data, X: {0}", _x.ToString()));
                StartCoroutine(SendData.instance.SendDataToAPI(_x, _y, _z));
            }
            else
            {
                // ResultsLabel.instance.CreateLabel();
            }
        }
        else
        {
            // Nothing really...
        }
        tapCount += 1;
    }

	// Update is called once per frame
	void Update () {
		
	}
}
