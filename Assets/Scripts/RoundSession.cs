using Flee.PublicTypes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundSession
{
    public string equation = "x";
    public int lowerXBound = -10;
    public int upperXBound = 10;
    public int lowerYBound = -5;
    public int upperYBound = 5;
    public float dx = 1;
    public float dy = 1;

    public float startY = 0;
    public float endY = 0;
    public float[,] slopes;

    public float canvasSize = 10;
    public SlopeFieldLineRenderer lineRenderer;

    public ExpressionContext context;
    /**
     * Creates a new session for a new round (equation)
     */
    public RoundSession(string equation, int lowerXBound, int upperXBound, int lowerYBound, int upperYBound, float dx)
    {
        this.equation = equation;
        this.lowerXBound = lowerXBound;
        this.upperXBound = upperXBound;
        this.lowerYBound = lowerYBound;
        this.upperYBound = upperYBound;
        this.dx = dx;
        this.dy = dx;

        this.context = new ExpressionContext();

        this.context.Imports.AddType(typeof(Math));
        


        this.InitializeRound();
    }

    public void InitializeRound()
    {
        slopes = new float[Mathf.RoundToInt(((this.upperYBound - this.lowerYBound) / this.dy + 1)), Mathf.RoundToInt((upperXBound - lowerXBound) / dx + 1)];

        GenerateSlopeArray();

    }



    public void GenerateSlopeArray()
    {
        for (float i = this.lowerXBound; i <= this.upperXBound; i = i + dx)
        {
            for (float j = lowerYBound; j <= upperYBound; j = j + dy)
            {
                float slopeAtPoint = this.EvaluateSlopeAtPoint(i, j);
                //let newFunc = slope.replaceAll('x',`${i}`).replaceAll('y',`${j}`);
                this.slopes[Mathf.RoundToInt((j - lowerYBound) / this.dy), Mathf.RoundToInt((i - lowerXBound) /dx )] = slopeAtPoint;
            }
        }

    }





    // Utils
    public float EvaluateSlopeAtPoint(float x, float y)
    {
        this.context = new ExpressionContext();
        if (this.equation.IndexOf("x") > -1)
        {
            this.context.Variables["x"] = x;

        }
        
        if(this.equation.IndexOf("y") > -1)
        {
            this.context.Variables["y"] = y;
        }



        IDynamicExpression eGeneric = context.CompileDynamic(this.equation);

        float result = (float)eGeneric.Evaluate();

        if (float.IsNaN(result))
        {
            return 0;
        }

      
        return result;

    }



    public void DrawOriginLines()
    {
        
    }

   
}
