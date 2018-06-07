using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Interaction
{
    /// <summary>
    /// General utitilies shared among classes responsible for managing interactions (attacks, effects, etc.) 
    /// </summary>
    public class InteractionUtilities
    {
        public static String getTargetTag(String myTag)
        {
            return (myTag == "Player") ? "Enemy" : "Player";
        }
    }
}
