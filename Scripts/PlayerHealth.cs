using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

namespace CompleteProject
{
    public class PlayerHealth : MonoBehaviour
    {
        public float startingHealth = 10;                            // The amount of health the player starts the game with.
        public float currentHealth;                                   // The current health the player has.
        public Slider healthSlider;                                 // Reference to the UI's health bar.

		public float startingShield = 5;
		public float currentShield;
		public Slider shieldSlider;

        public Image damageImage;                                   // Reference to an image to flash on the screen on being hurt.
		public AudioClip deathClip;                                 // The audio clip to play when the player dies.
        public float flashSpeed = 5f;                               // The speed the damageImage will fade at.
        public Color flashColour = new Color(1f, 0f, 0f, 0.1f);     // The colour the damageImage is set to, to flash.


        //Animator anim;                                              // Reference to the Animator component.
        AudioSource playerAudio;                                    // Reference to the AudioSource component.
        //PlayerMovement playerMovement;                              // Reference to the player's movement.
		Done_PlayerController playerController;                              // Reference to the PlayerShooting script.
        bool isDead;                                                // Whether the player is dead.
        bool damaged;                                               // True when the player gets damaged.
		//bool heal;

		public GameObject explosion;
		public GameObject playerExplosion;
		private Done_GameController gameController;

		public Collider other;

        void Awake ()
        {
            // Setting up the references.
            //anim = GetComponent <Animator> ();
            playerAudio = GetComponent <AudioSource> ();
			playerController = GetComponent <Done_PlayerController> ();
            //playerShooting = GetComponentInChildren <PlayerShooting> ();

            // Set the initial health of the player.
            currentHealth = startingHealth;
        }

		void Start ()
		{
			GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");
			if (gameControllerObject != null)
			{
				gameController = gameControllerObject.GetComponent <Done_GameController>();
			}
			if (gameController == null)
			{
				Debug.Log ("Cannot find 'GameController' script");
			}
		}


		void Update (/*Collider other*/)
        {
			//Check();

			// If the player has just been damaged...
            if(damaged)
            {
                // ... set the colour of the damageImage to the flash colour.
                damageImage.color = flashColour;
            }
            // Otherwise...
            else
            {
                // ... transition the colour back to clear.
                damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
            }

            // Reset the damaged flag.
            damaged = false;
			//heal = false;
        }

		public void Heal(float amount)
		{
			currentHealth += amount;
			if (currentHealth > 10)
			{
				currentHealth = 10;
			}
			healthSlider.value = currentHealth;
		}


		void Check() //makes sure that health doesn't go past the max
		{
			if (currentHealth > 10)
			{
				currentHealth = 10;
			}
			healthSlider.value = currentHealth;
		}

		public void TakeDamage (float amount)
        {
            // Set the damaged flag so the screen will flash.
            damaged = true;

            // Reduce the current health by the damage amount.
			if(currentShield > 0)
			{
				currentShield -= amount;
				shieldSlider.value = currentShield;
			}
			else
			{
				currentHealth -= amount;
				healthSlider.value = currentHealth;
			}
				

            // Set the health bar's value to the current health.
            

            // Play the hurt sound effect.
			//playerAudio.Play ();
			Instantiate(playerExplosion, transform.position, transform.rotation);

            // If the player has lost all it's health and the death flag hasn't been set yet...
            if(currentHealth <= 0 && !isDead)
            {
                // ... it should die.
                Death ();
            }
        }

		/*public void HealDamage()
		{
			healthSlider.value = currentHealth;
		}*/


        public void Death ()
        {
            // Set the death flag so this function won't be called again.
            isDead = true;

            // Turn off any remaining shooting effects.
            //playerShooting.DisableEffects ();

            // Tell the animator that the player is dead.
            //anim.SetTrigger ("Die");

            // Set the audiosource to play the death clip and play it (this will stop the hurt sound from playing).
            //playerAudio.clip = deathClip;
            //playerAudio.Play ();

            // Turn off the movement and shooting scripts.
			playerController.enabled = false;
            //playerShooting.enabled = false;

			Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
			gameController.GameOver();

			Destroy (other.gameObject);
			Destroy (gameObject);
        }
    }
}