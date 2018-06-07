
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts {

    public class CharacterMover : MonoBehaviour {
        public float moveSpeed;
        public float maxSpeed;

        public float jumpForce;

        public Animator animator;
        public Rigidbody2D rigidBody2D;

        //apply forces to all attached rigidBodies, use rigidBody2D for determining current physical state 
        public Rigidbody2D[] rigidBodies; 

        /* 
        //Deprecated: Characters are composed of multiple colliders
        private float _height = float.NaN; 
        public float height {
            get {
                if(_height == float.NaN) 
                return maxYOffset - minYOffset;
                return _height; 
            }
        }

        public float minYOffset {
            get {
                float min = this.transform.position.y; 
                foreach(Collider2D c in GetComponents<Collider2D>()) {
                    if (c.bounds.min.y < min) min = c.bounds.min.y; 
                }
                return min - this.transform.position.y;
            }
        }

        public float maxYOffset {
            get {
                float max = this.transform.position.y; 
                foreach(Collider2D c in GetComponents<Collider2D>()) {
                    if (c.bounds.max.y > max) max = c.bounds.max.y; 
                }
                return max - this.transform.position.y;
            }
        }
        */


        public bool isFacingRight {
            get; set; 
        }

        public Transform groundCheck;

        private float jumpTimeout = .3f;
        private float lastJumpButtonTime = -10.0f;

        //store inputs to apply on FixedUpdate
        private float horizontalInput;
        private bool verticalInput;

        //track how long character is stuck trying to move midair - slideDown
        private int stuckTimes = 0;

        void FixedUpdate() {
            applyInput(horizontalInput, verticalInput);

        }

        private void setRigidBodyVelocity(Vector2 velocity) {
            for(int i = 0; i<rigidBodies.Length; i++) {
                rigidBodies[i].velocity = velocity; 
            }
        }

        public void setInput(float horizontalInput, bool verticalInput) {
            this.horizontalInput = horizontalInput;
            this.verticalInput = verticalInput;
        }

        //Force character to slide down if they are stuck midair trying to move into platform 
        private void slideDown(float horizontalInput) {
            if (horizontalInput != 0 && Mathf.Abs(rigidBody2D.velocity.x) < .05f && Mathf.Abs(rigidBody2D.velocity.y) < .05f) {
                if (stuckTimes > 0) {
                    setRigidBodyVelocity(new Vector2(0, -5)); 
                    stuckTimes = 0;
                }
                else {
                    stuckTimes++;
                }
            }
            else {
                stuckTimes = 0;
            }
        }

        protected void applyInput(float horizontalInput, bool verticalInput) {
            //detect stuck midair problem 
            slideDown(horizontalInput); 

            //update facing dir 
            if( (isFacingRight && horizontalInput < 0) || (!isFacingRight && horizontalInput > 0)) {
                flip(); 
            }

            //sign (-1 or 1) horizontal input 
            if (!Mathf.Approximately(horizontalInput, 0))
                applyHorizontalInputForce(Mathf.Sign(horizontalInput));

            //else slow down character movement 
            else {
                Vector2 slowedVelocity = rigidBody2D.velocity;
                if (Mathf.Abs(slowedVelocity.x) > .01f) {
                    slowedVelocity.x *= .8f;
                    setRigidBodyVelocity(slowedVelocity); 
                }
            }

            if (verticalInput && IsGrounded() && Time.time >= lastJumpButtonTime + jumpTimeout) {
                applyVerticalInputForce();
                lastJumpButtonTime = Time.time;
            }

            animateCharacter(horizontalInput);

            /*
            // If the player's horizontal velocity is greater than the maxSpeed...
            if (Mathf.Abs(rigidBody2D.velocity.x) > maxSpeed)
                // ... set the player's velocity to the maxSpeed in the x axis.
                rigidBody2D.velocity = new Vector2(Mathf.Sign(rigidBody2D.velocity.x) * maxSpeed, rigidBody2D.velocity.y);
                */
        }

        private void applyHorizontalInputForce(float dir) {
/*
            Vector2 curVelocity = rigidBody2D.velocity;
            rigidBody2D.velocity = new Vector2(maxSpeed * dir, curVelocity.y);
*/

            if(dir * rigidBody2D.velocity.x < maxSpeed) {
                rigidBody2D.AddForce(Vector2.right * dir * maxSpeed*10); 
}

if(Mathf.Abs(rigidBody2D.velocity.x) > maxSpeed) {
                //rigidBody2D.velocity = new Vector2(dir * maxSpeed, rigidBody2D.velocity.y); 
            }


        }

        private void applyVerticalInputForce() {
            //Set upward velocity instead of applying force as player's downward gravity can confusingly counteract upward jump force
            setRigidBodyVelocity(new Vector2(rigidBody2D.velocity.x, jumpForce)); 
        }

        private void animateCharacter(float dir) {
            if (animator != null) {
                //face direction 
                if (dir != 0) {
                    updateFaceDir(dir > 0);

                    if (!animator.GetBool("IsWalking")) {
                        animator.SetBool("IsWalking", true);
                    }
                }
                else { //h==0 
                    animator.SetBool("IsWalking", false);
                }
            }

        }

        private void flip() {
            // Switch the way the player is labelled as facing.
            isFacingRight = !isFacingRight;

            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }

        private void updateFaceDir(bool faceRight) {
            if (faceRight && !isFacingRight) {
                flip();
                isFacingRight = true;
            }
            else if (!faceRight && isFacingRight) {
                flip();
                isFacingRight = false;
            }
        }

        public bool IsGrounded() {
            return Physics2D.Linecast(transform.position,
                groundCheck.position,
                1 << LayerMask.NameToLayer("Environment"));
        }

        public void OnDrawGizmos() {
            float x1 = transform.position.x;
            float y1 = transform.position.y;
            float x2 = x1 + 5;
            float moveTime = (x2 - x1) * moveSpeed;

            Vector2 previousPos = transform.position;
            for (float t = 0; t < 10; t += .01f) {
                Vector2 curPos = new Vector2(x1 + moveSpeed * t, y1 + jumpForce * t + .5f * Physics2D.gravity.y * Mathf.Pow(t, 2));
                Gizmos.DrawLine(previousPos, curPos);
                previousPos = curPos;
            }
        }

    }
}
