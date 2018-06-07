using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine; 

namespace Assets.Scripts
{
/// <summary>
/// Math function utilities.  
/// Static class should only be one implementation for this.
/// </summary>
    static class MathFunction
    {

        public static float getQuadraticMax(float a, float b, float c)
        {
            return (float)(c - (Math.Pow(b, 2) / (4 * a)));
        }

        //input as in x value 
        public static float evaluateQuadraticAt(float a, float b, float c, float input) {
            return a * Mathf.Pow(input, 2) + b * input + c; 
        }

        //return value is number of solutions 
        //solving for x that satisfies 0 = ax^2 + bx + c 
        public static int solveRealQuadratic(float a, float b, float c, out float solA, out float solB)
        {
            float sqrtPortion = b * b - 4 * a * c;

            if (sqrtPortion > 0) {
                solA = (-b + Mathf.Sqrt(sqrtPortion)) / (2 * a); 
                solB = (-b - Mathf.Sqrt(sqrtPortion)) / (2 * a); 
                return 2; 
            }
            else if(sqrtPortion < 0) {
                solA = float.NaN;
                solB = float.NaN; 
                return 0; 
            }
            else {
                solA = -b / (2 * a);
                solB = float.NaN; 
                return 1; 
            }
        }

        public static Vector2 getPointOnCircle(Vector2 start, float radius, float angle)
        {
            return new Vector2(start.x + radius*Mathf.Cos(angle), start.y + radius*Mathf.Sin(angle)); 
        }

        
    }
}
