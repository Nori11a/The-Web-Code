using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CompleteProject
{
	public class PowerUp : MonoBehaviour
	{
	    
		//public Done_PlayerController player;
		public int Item;

	    void Start()
	    {
	        
	    }

	    // Update is called once per frame
	    void Update()
	    {
	        
	    }


		private void OnTriggerEnter(Collider other)
		{
			if(other.tag == "Player")
			{
				Done_PlayerController.Upgrades[Item] = 1;
				gameObject.SetActive(false);
			}
		}
	}
}