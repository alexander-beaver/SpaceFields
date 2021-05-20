using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Manages game-wide state and actions
 */
public class SuperGame : MonoBehaviour
{
    public static string equation = "x*y";
    public RoundSession gameRound;
    public SlopeFieldLineRenderer lr;
    // Start is called before the first frame update
    void Start()
    {
        gameRound = new RoundSession(equation, -15, 5, -5, 5, 1);
        this.lr.GenerateSlopes();


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
