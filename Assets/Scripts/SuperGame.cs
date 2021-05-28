using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/**
 * Manages game-wide state and actions
 */
public class SuperGame : MonoBehaviour
{

    public static string[] equations =
    {
        "x/(2*y)",
        "x/3",
        "y/2"
    };
    public static string equation = "y/2";
    public RoundSession gameRound;
    public SlopeFieldLineRenderer lr;

    public Button toMenu;

    public Text equationLabel;
    public Text endLabel;

    public Text startLabel;

    public GameObject planet;


    // Start is called before the first frame update
    void Start()
    {
        int eqID = Random.Range(0, equations.Length);
        equation = equations[eqID];
        toMenu.onClick.AddListener(ToHome);
        int leftx = Random.Range(-10, 5);

        int rightx = leftx + 5;

        int bottomy = Random.Range(-15, -5);
        int topy = -bottomy;
        gameRound = new RoundSession(equation, leftx, rightx, bottomy, topy, 1);
        this.Calculate();


    }

    void ToHome()
    {
        SceneManager.LoadScene("Launch");

    }
    // Update is called once per frame
    void Update()
    {
        
    }
    float ConvertPlaneXToCanvasCoordinate(float x)
    {
        return ((x) * this.gameRound.canvasSize);

    }
    float ConvertPlaneYToCanvasCoordinate(float y)
    {
        return ((y) * this.gameRound.canvasSize);

    }
    public void Calculate()
    {

        bool run = true;
        while (run)
        {
            float start = Random.Range(this.gameRound.lowerYBound * 10, this.gameRound.upperYBound * 10);
            start = Mathf.Round(start) / 10;

            float currY = start;

            Debug.Log("Start");
            Debug.Log(start);
            for (float x = this.gameRound.lowerXBound; x <= this.gameRound.upperXBound; x = x + this.gameRound.dx)
            {
                currY = currY + (this.gameRound.EvaluateSlopeAtPoint(x, currY) * this.gameRound.dx);
            }

            currY = Mathf.Round(currY * 10) / 10;

            if(Mathf.Abs(currY) < this.gameRound.upperYBound)
            {
                this.gameRound.startY = start;
                this.gameRound.endY = currY;

                equationLabel.text = "dy/dx = " + this.gameRound.equation;
                endLabel.text = "f(" + this.gameRound.upperXBound + ") = " + this.gameRound.endY;
                startLabel.text = "f(" + this.gameRound.lowerXBound + ") = ?";

                float px = ConvertPlaneXToCanvasCoordinate(this.gameRound.upperXBound);
                float py = ConvertPlaneYToCanvasCoordinate(currY);

                planet.transform.SetPositionAndRotation(new Vector3(px, py, 0), new Quaternion());
                run = false;
            }
            else
            {
                Debug.LogWarning(currY);
            }

            
        }
       

    }

   
}
