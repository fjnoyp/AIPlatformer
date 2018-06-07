using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class TestCharacterPosPrediction : MonoBehaviour
    {
        public GizmosDrawer gizmosDrawer;

        //Jump trajectory variables 
        public float moveSpeed;
        public float jumpForce;
        public Transform startTransform;

        //Other test variables 
        public float lineOffset;
        public Vector2 lineStart; 

        private List<List<Vector2>> debugJumpTrajectories = new List<List<Vector2>>();

        public GameObject worldObjectParent;
        private List<BoxCollider2D> platforms = new List<BoxCollider2D>(); 
        private ITrajectoryPrediction prediction;

        public void Awake()
        {
            prediction = new TrajectoryPrediction();
            prediction.setGizmosDrawer(this.gizmosDrawer); 

            foreach (Transform child in worldObjectParent.transform)
            {
                platforms.Add(child.GetComponent<BoxCollider2D>());
            }
            platforms.Sort(new PlatformPosComparer()); 
        }

        private void gizmosEarliestHitPosPlatformTest()
        {
            gizmosDrawer.clear();

            gizmosDrawer.addHyperbola(moveSpeed, jumpForce, 10, startTransform.position);

            Pair<Vector2, Enums.PlatformHitLoc> hitPos;
            hitPos = prediction.getEarliestHitPosInfoForPlatforms(moveSpeed, jumpForce, startTransform.position,platforms);
            if (hitPos != null) gizmosDrawer.addDebugPoint(hitPos.key);
        }

        public void Update() {
            gizmosEarliestHitPosPlatformTest(); 
            //gizmosHypHorizLineTest(); 
        }
    }
}
