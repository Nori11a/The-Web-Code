using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CompleteProject
{
	public class PlayerWeaponControls : MonoBehaviour
	{
		//location of where bullet spawn
		public Transform shotSpawn;

		//different weapon types
		public GameObject bullet;  //the most basic weapon
		public GameObject wide; //upgraded version of the basic weapon by making it wider


		private float nextFire;


	    void Start()
	    {
	        
	    }


	    void Update()
	    {
			Toggle();
			Fire();
	    }

		private void Toggle()
		{
			//here would be where I had a weapon toggle system using the number keys
		}

		private void Fire()
		{

			if(Done_PlayerController.Upgrades[6] == 1)
			{
				if (Input.GetButton("Fire1") && Time.time > nextFire && Done_PlayerController.gun == true) //Input.GetKey is used to figure out what key needs to be pushed, and it'll know it's the Space Bar because of "KeyCode.Space"
				{
					nextFire = Time.time + Done_PlayerController.fireRate;

					Instantiate(wide, shotSpawn.position, shotSpawn.rotation);
					GetComponent<AudioSource>().Play();
				}
			}
			else
			{
				if (Input.GetButton("Fire1") && Time.time > nextFire && Done_PlayerController.gun == true) //Input.GetKey is used to figure out what key needs to be pushed, and it'll know it's the Space Bar because of "KeyCode.Space"
				{
					nextFire = Time.time + Done_PlayerController.fireRate;

					Instantiate(bullet, shotSpawn.position, shotSpawn.rotation);
					GetComponent<AudioSource>().Play();
				}
			}

		}
	}
}