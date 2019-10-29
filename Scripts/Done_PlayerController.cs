﻿using UnityEngine;
using System.Collections;

//[System.Serializable]

	
namespace CompleteProject
{
	public class Done_PlayerController : MonoBehaviour
	{
		//public Done_Boundary boundary;
		private Done_GameController gameController;
		private Parry parry;

		public PickUpSpin pickUp;
		GameObject enemy;
		EnemyHealth enemyHealth; 

		public float RotateSpeed = 100f; //speed of the rotation

		/*public GameObject shot;
		public Transform shotSpawn;
		private float nextFire;*/

		public static float fireRate = 1.75f;
		public static float power = 0;
		public static float speed = 4;  //4
		public static int reflector = 0;
		public static int node = 0;
		public static float jBoost = /*5f;*/ 15f;

		public float Mode = 1f;  //changes speed.  .5 for half speed, 1 for full

		//these are used to calculate the foward/back and rotation of the player
		private float xAxis;
		public float yAxis; //used for ramps, still testing, delete if not functioning correctly
		private float zAxis;

		private Vector3 movement;

		public Rigidbody rBody;
		int floorMask;
		float camRayLength = 100f;

		public Objects[] items;
		public int boughtItem = 0;
		public int[] amount = new int[] {0, 0, 0, 0, 0, 0, 0};

		// [0] = Fast Mode  [1] = Jump Booster  [2-5] = Parry Upgrade [6] Wide Bullet
		public static int[] Upgrades = new int[] {0, 0, 0, 0, 0, 0, 0};

		public static bool gun = true;

		void Awake()
		{
			floorMask = LayerMask.GetMask ("Floor");
			//playerHealth = gameObject.GetComponent <PlayerHealth> ();

			rBody = GetComponent <Rigidbody>();

			enemy = GameObject.FindGameObjectWithTag("Enemy");
			enemyHealth = enemy.GetComponent<EnemyHealth>();
		}

		void Start ()
		{
			GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");
			gameController = gameControllerObject.GetComponent <Done_GameController>();

		}

		void Update()
		{
			//this allows the player to shoot lasers
			/*if (Input.GetButton("Fire1") && Time.time > nextFire && gun == true) //Input.GetKey is used to figure out what key needs to be pushed, and it'll know it's the Space Bar because of "KeyCode.Space"
			{
				nextFire = Time.time + fireRate;

				Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
				GetComponent<AudioSource>().Play();
			}*/

			if(Input.GetKeyDown(KeyCode.LeftShift))
			{
				if(Mode == 1)
				{
					if(Upgrades[0] == 1)
					{
						Mode = 1.5f;
					}
					else
					{
						Mode = 0.5f;
					}
				}
				else if(Mode == 0.5)
				{
					Mode = 1f;
				}
				else if(Mode == 1.5)
				{
					Mode = 0.5f;
				}
			}

			if(speed > 10)
			{
				speed = 10;
			}
			if(power > 15)
			{
				power = 15;
			}
			if(jBoost > 20)
			{
				jBoost = 20;
			}

			gameController.SetGear(Mode);
		}

		void FixedUpdate()
		{
			xAxis = Input.GetAxis("Horizontal");
			zAxis = Input.GetAxis("Vertical");

			Move(); //checks for position
			Turn(); //checks for rotation
			itemBought();

		}

		private void Move()
		{
			
			movement.Set(xAxis, yAxis, zAxis);

			movement = movement.normalized * speed * Mode * Time.deltaTime;

			rBody.MovePosition (transform.position + movement);

		}

		private void Turn()
		{
			Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);

			RaycastHit floorHit;

			if(Physics.Raycast (camRay, out floorHit, camRayLength, floorMask))
			{
				Vector3 mouse = floorHit.point - transform.position;

				mouse.y = 0f;

				Quaternion rotation = Quaternion.LookRotation (mouse);

				rBody.MoveRotation(rotation);
			}

		}

		void itemBought()
		{
			switch(boughtItem)
			{
				case 1:
					fireRate -= (.05f * amount[1]);
					//fireRate -= (.2f * amount[1]); //this one is just to show off the power up, not meant for actual gameplay
					boughtItem = 0;
					amount[1] = 0;
					break;
				case 2:
					power += (1f * amount[2]);
					//power += (2f * amount[2]); //this one is just to show off the power up, not meant for actual gameplay
					boughtItem--;
					amount[2] = 0;
					break;
				case 3:
					speed += (1f * amount[3]);  
					//speed += (2f * amount[3]); //this one is just to show off the power up, not meant for actual gameplay
					boughtItem--;
					amount[3] = 0;
					break;
				case 4:
					gameController.AddLives(amount[4]);
					boughtItem--;
					amount[4] = 0;
					break;
				case 5:
					reflector += (amount[5]);
					boughtItem--;
					amount[5] = 0;
					break;
				case 6:
					node += (amount[6]);
					boughtItem--;
					amount[6] = 0;
					break;
			}
		}

		private void OnTriggerEnter (Collider other)
		{
			if(other.gameObject.layer ==LayerMask.NameToLayer("PickUp"))
			{
				switch(other.gameObject.tag)
				{
					case "Coin":
					//other.gameObject.SetActive(false);	
					gameController.AddCoin(1);
						//pickUp.Sound();
						break;

					case "Rate":
						fireRate -= .01f;
						//fireRate = .025f;  //this one is just to show off the power up, not meant for actual gameplay
						other.gameObject.SetActive(false);
						break;

					case "Power":
						
						power += .25f;
						//power += 10f;  //this one is just to show off the power up, not meant for actual gameplay
						other.gameObject.SetActive(false);
						break;

					case "Speed":
						speed += .2f;
						//speed += 10f;  //this one is just to show off the power up, not meant for actual gameplay
						other.gameObject.SetActive(false);
						break;
					case null:
					other.gameObject.SetActive(false);
					break;
				}

			}


		}
			
	}
}