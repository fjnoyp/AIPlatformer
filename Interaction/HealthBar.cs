using UnityEngine; 

namespace Assets.Scripts {
  public class HealthBar : MonoBehaviour {
        private float maxScale;
        private float maxHealth;

        public GameObject healthBar; 

        public void Start() {
            Health health = GetComponent<Health>();
            maxHealth = health.maxHealth;
            maxScale = healthBar.transform.localScale.x;
        }

        public void updateHealthBar(float health) {
            Vector3 scale = healthBar.transform.localScale;

            Debug.Log(health + " " + (health / maxHealth)); 

            scale.x = maxScale * (health / maxHealth);
            healthBar.transform.localScale = scale; 
        }
    }
}
