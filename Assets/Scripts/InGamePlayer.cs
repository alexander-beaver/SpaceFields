using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGamePlayer : MonoBehaviour
{
    // Start is called before the first frame update

    public SuperGame sg;
    public InputField input;
    private bool runTrajectory;

    public Button launchButton;


    private float x;    
    private float y;
    private float sy;

    float ConvertPlaneXToCanvasCoordinate(float x)
    {
        return ((x) * this.sg.gameRound.canvasSize);

    }
    float ConvertPlaneYToCanvasCoordinate(float y)
    {
        return ((y) * this.sg.gameRound.canvasSize);

    }

    public void RunTrajectory()
    {
        this.runTrajectory = true;
    }
    public void UpdateLocation(string arg0)
    {

        
        float location = 0f;
        string loc = arg0;
     
        bool notErr = float.TryParse(loc, out location);

        if (notErr)
        {
            x = this.sg.gameRound.lowerXBound;
            y = location;
            sy = location;

            gameObject.transform.SetPositionAndRotation(new Vector3(ConvertPlaneXToCanvasCoordinate(x), ConvertPlaneYToCanvasCoordinate(y), 0), new Quaternion());
        }
    }
    void Start()
    {
        var se = new InputField.OnChangeEvent();

        se.AddListener(UpdateLocation);
        this.input.onValueChanged = se;

        this.launchButton.onClick.AddListener(RunTrajectory);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.runTrajectory)
        {
            ProcessMovement();
        }   
    }

    public void ProcessMovement()
    {
        if (x >= this.sg.gameRound.upperXBound)
        {
            if ((Mathf.Abs(y-this.sg.gameRound.endY )< .1f) || (Mathf.Abs(sy-this.sg.gameRound.startY)<.1f))
            {
                //Success
                Debug.Log("Success");
                SceneManager.LoadScene("Congratulations");
            }
            else
            {
                Debug.Log("Failure");
            }

            this.runTrajectory = false;
        }
        else
        {
            try
            {
                float slope = this.sg.gameRound.EvaluateSlopeAtPoint(x, y);


                float thisdx = 0.05f;

                x = x + thisdx;
                y = y + (slope * thisdx);

                gameObject.transform.SetPositionAndRotation(new Vector3(ConvertPlaneXToCanvasCoordinate(x), ConvertPlaneYToCanvasCoordinate(y), 0), new Quaternion());
            }
            catch (Exception e)
            {
                Debug.LogError("Error running");
                this.runTrajectory = false;
            }
           

        }



    }
}
