using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Polynomial
{
    public float intercept;
    public List<float> coefficients;

    public Polynomial(params float[] coefficients)
    {
        this.intercept = coefficients[0];
        this.coefficients = coefficients.Skip(1).ToList();
    }

    public float Solve(float x)
    {
        float y = intercept;

        for (int i = 0; i < coefficients.Count; i++)
        {
            y += coefficients[i] * Mathf.Pow(x, i);
        }

        return y;
    }
}
