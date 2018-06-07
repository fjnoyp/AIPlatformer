using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public static class CompareUtilities
    {
        //true = a comes before b based on moveDir
        public static bool comesBefore(int moveDir, float a, float b)
        {
            return moveDir * a > moveDir * b; 
        }
        public static bool comesBeforeEqual(int moveDir, float a, float b)
        {
            return moveDir * a >= moveDir * b; 
        }
    }
}
