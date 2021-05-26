using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SlopeFieldLineRenderer : MonoBehaviour
{
    // Start is called before the first frame update
    public SuperGame sg;

    bool run = true;

    private LineRenderer l;
    public GameObject drawingPrefab;

    public Material lineMaterial;




    void Start()
    {
        Debug.Log("Starting");
        
        run = true;


    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void GenerateSlopes()
    {
        Color originColor = new Color(0, 0, 1, .5f);

        float ux = this.sg.gameRound.upperXBound;
        if(-1*this.sg.gameRound.lowerXBound > this.sg.gameRound.upperXBound)
        {
            ux = -1 * this.sg.gameRound.lowerXBound;
        }

        float lx = this.sg.gameRound.lowerXBound;
        if (-1 * this.sg.gameRound.upperXBound < this.sg.gameRound.lowerXBound)
        {
            lx = -1 * this.sg.gameRound.upperXBound;
        }
        this.RawDrawLine(this.ConvertPlaneXToCanvasCoordinate(0), this.ConvertPlaneYToCanvasCoordinate(this.sg.gameRound.lowerYBound), this.ConvertPlaneXToCanvasCoordinate(0), this.ConvertPlaneYToCanvasCoordinate(this.sg.gameRound.upperYBound), originColor);
        this.RawDrawLine(this.ConvertPlaneXToCanvasCoordinate(lx), this.ConvertPlaneYToCanvasCoordinate(0), this.ConvertPlaneXToCanvasCoordinate(ux), this.ConvertPlaneYToCanvasCoordinate(0), originColor);

        for (float i = this.sg.gameRound.lowerXBound; i <= this.sg.gameRound.upperXBound; i = i + 1f)
        {
            for (float j = this.sg.gameRound.lowerYBound; j <= this.sg.gameRound.upperYBound; j = j + 1f)
            {
                
                this.DrawLine(i, j, this.sg.gameRound.slopes[Mathf.RoundToInt((j - this.sg.gameRound.lowerYBound) / 1), Mathf.RoundToInt((i - this.sg.gameRound.lowerXBound) / 1)]);
            }
        }
    }

    // UTILITY FUNCTIONS

    float ConvertPlaneXToCanvasCoordinate(float x)
    {
        return ((x) * this.sg.gameRound.canvasSize);

    }
    float ConvertPlaneYToCanvasCoordinate(float y)
    {
        return ((y)  * this.sg.gameRound.canvasSize);

    }

    void RawDrawLine(float x1, float y1, float x2, float y2, Color color)
    {
        GameObject drawing = Instantiate(drawingPrefab);

        this.l = drawing.GetComponent<LineRenderer>();
        this.l.material = new Material(Shader.Find("Sprites/Default")); ;
        this.l.material.color = color;


        List<Vector3> pos = new List<Vector3>();

        pos.Add(new Vector3(x1, y1));
        pos.Add(new Vector3(x2, y2));
        this.l.startWidth = 1f;
        this.l.endWidth = 1f;
     
        l.SetColors(color, color);
        
        this.l.SetPositions(pos.ToArray());

    }
    void DrawLine(float x, float y, float slope)
    {




        //float delta = this.sg.gameRound.dx / 3;
        float delta = 1 / 3;
        float linlen = 0.25f;
        float theta = Mathf.Atan(slope);

        float startX = this.ConvertPlaneXToCanvasCoordinate(
            x - linlen * Mathf.Cos(theta)
        );
        float startY = this.ConvertPlaneYToCanvasCoordinate(
            y - linlen * Mathf.Sin(theta)
        );
        float endX = this.ConvertPlaneXToCanvasCoordinate(
            x + linlen * Mathf.Cos(theta)
        );
        float endY = this.ConvertPlaneYToCanvasCoordinate(
            y + linlen * Mathf.Sin(theta)
        );

        this.RawDrawLine(startX, startY, endX, endY, Color.white);
    }
}
