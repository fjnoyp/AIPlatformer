using UnityEngine; 

namespace Assets.Scripts {

    [RequireComponent(typeof(HealthBar))] 

    public class Health : MonoBehaviour, IHealth {
        public float maxHealth;
        public float curHealth; 
        private HealthBar healthBar; 

        public void Start() {
            healthBar = GetComponent<HealthBar>();
            //curHealth = maxHealth; 
        }

        public void updateHealth(float change) {
            curHealth += change; 

            if (curHealth <= 0) Destroy(this.gameObject); 
            healthBar.updateHealthBar(curHealth); 
        }
    }



}
