using System;
using System.Collections.Generic; 
using UnityEngine; 

namespace Assets.Scripts.Environment.Destructibles
{

    /*




        WARNING UNTESTED CLASS 

 *  *  *  *  *  *      * 
 */

    [RequireComponent(typeof(Collider2D))]
    public class DestructibleRock : AbDestructible 
    {
        //Rocks that are effected by us 
        public DestructibleRock[] attachedRocks;
        public ParticleSystem destructionEffect;

        public GameObject childDestructionObject;
        public float destroyForceThreshold;

        private bool detached = false; 

        //Split this rock into smaller rocks, loosen and detach all attached rocks 
        public override void destroy(Vector2 force, List<Vector2> points)
        {
            //create children rock rubble 
            if (destructionEffect != null)
            {
                destructionEffect.transform.position = this.transform.position;
                destructionEffect.Play();
            }

            if (childDestructionObject != null)
            {
                GameObject newChildDestructionObject = Instantiate(childDestructionObject, this.transform.position, this.transform.rotation);
                newChildDestructionObject.transform.localScale = this.transform.localScale; 

                //detach children from parent, adjust transform 
                for(int i = newChildDestructionObject.transform.childCount-1; i>=0; i--)
                {
                    Transform child = newChildDestructionObject.transform.GetChild(i); 
                    child.gameObject.transform.parent = null;

                    if (child.GetComponent<DestructibleRock>() != null)
                    {
                        child.GetComponent<DestructibleRock>().detach();
                        float roughSize = child.GetComponent<Collider2D>().bounds.size.x * child.GetComponent<Collider2D>().bounds.size.y;
                        //WARNING HARD CODED VALUE: 
                        child.GetComponent<Rigidbody2D>().AddForce((child.position - this.transform.position).normalized * 200 * roughSize);
                    }
                }
                //Destroy(newChildDestructionObject); 
            }

            //disturb attached rocks 
            foreach(DestructibleRock rock in attachedRocks)
            {
                rock.detach(); 
            }

            GameObject.Destroy(this.gameObject); 
        }

        public void OnCollisionEnter2D(Collision2D collision)
        {
            if (detached && collision.gameObject.GetComponent<Rigidbody2D>() != null)
            {
                //destroy on high impact
                Rigidbody2D otherBody2D = collision.gameObject.GetComponent<Rigidbody2D>();
                Rigidbody2D thisBody2D = GetComponent<Rigidbody2D>();
                float combMass = otherBody2D.mass + thisBody2D.mass;
                float difForce = collision.relativeVelocity.x * combMass + collision.relativeVelocity.y * combMass;

                Debug.Log(difForce);
                if (difForce > destroyForceThreshold)
                {
                    //destroy(); 
                }

                /*
                //dust on collisions
                foreach(ContactPoint2D contact in collision.contacts)
                {
                    contact.point;
                }
                */
            }
        }

        public void detach()
        {
            Rigidbody2D body2D = gameObject.AddComponent<Rigidbody2D>();
            body2D.useAutoMass = true; //note automass adjusts based on local scale 

            detached = true; 
        }

    }
}
