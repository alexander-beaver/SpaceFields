using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGamePlayer : MonoBehaviour
{
    // Start is called before the first frame update

    public SuperGame sg;
    public InputField input;


    float ConvertPlaneXToCanvasCoordinate(float x)
    {
        return ((x) * this.sg.gameRound.canvasSize);

    }
    float ConvertPlaneYToCanvasCoordinate(float y)
    {
        return ((y) * this.sg.gameRound.canvasSize);

    }


    public void UpdateLocation(string arg0)
    {

        
        float location = 0f;
        string loc = arg0;
     
        bool notErr = float.TryParse(loc, out location);

        if (notErr)
        {
            Debug.Log(location.ToString());
            float x = ConvertPlaneXToCanvasCoordinate(this.sg.gameRound.lowerXBound);
            float y = ConvertPlaneYToCanvasCoordinate(location);

            gameObject.transform.SetPositionAndRotation(new Vector3(x, y, 0), new Quaternion());
        }
    }
    void Start()
    {
        var se = new InputField.OnChangeEvent();

        se.AddListener(UpdateLocation);
        this.input.onValueChanged = se;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
