using System;
using System.Collections.Generic; 
using UnityEngine; 

namespace Assets.Scripts {
    public static class AiUtilities {
        
    
        public static string getTargetTag(string myTag) {
            if(myTag == "Player") { return "Enemy";  }
            else if(myTag == "Enemy") { return "Player";  }
            else {
                Debug.Log("Error AiUtilities.getTargetTag invalid tag given");
                return null; 
            }
        }

        public static GameObject getNearest(Vector2 pos, GameObject[] gameObjects) {
            GameObject closest = null;
            float distance = Mathf.Infinity;
            foreach (GameObject go in gameObjects) { 
                Vector2 diff = (Vector2)go.transform.position - pos;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance) {
                    closest = go;
                    distance = curDistance;
                }
            }
            return closest; 
        }

        public static GameObject getNearestWithTag(Vector2 pos, String tag) {
            GameObject[] tagObjects = GameObject.FindGameObjectsWithTag(tag);
            return getNearest(pos, tagObjects); 
        }

        /* Deprecated, melee attacks done with collider2D instead of raycasting 
        public static bool enemyWithinMeleeRange(Vector2 pos, bool isFacingRight, float distance, String targetTag) {

            int dir = isFacingRight ? 1 : -1;
            RaycastHit2D[] hits = Physics2D.RaycastAll(pos, new Vector2(dir * distance, 0));

            for (int i = 0; i < hits.Length; i++) {
                //Debug.Log(hits[i].transform.tag); 

                if(hits[i].transform.tag == targetTag) {
                    return true; 
                }

            }
            return false;
        }
        */
    }
}
