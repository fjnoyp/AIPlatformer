 using UnityEngine;
using System.Collections; 

namespace Assets.Scripts {
    [RequireComponent(typeof(Collider2D))]
    class HealthPack : MonoBehaviour {
        public float healthValue;
        public int respawnDelay;
        //public GameObject display; 

        private SpriteRenderer renderer; 

        public bool isActive {
            get {
                return renderer.enabled; 
            }
        }

        public void Start() {
            renderer = this.GetComponent<SpriteRenderer>(); 
        }

        public void OnTriggerEnter2D(Collider2D collision) {
            if (collision.gameObject.GetComponent<Health>() != null) { 

                collision.gameObject.GetComponent<Health>().updateHealth(healthValue);
                StartCoroutine(Respawn());
                //display.SetActive(false);
                renderer.enabled = false;
                GetComponent<Collider2D>().enabled = false; 
            }
        }

        IEnumerator Respawn() {
            yield return new WaitForSeconds(respawnDelay);
            //display.SetActive(true);
            renderer.enabled = true;
            GetComponent<Collider2D>().enabled = true; 
        }
    }
}
