using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CompleteProject
{
	public class Jump : MonoBehaviour
	{
		public Done_PlayerController player;

		private Vector3 jump;
		private float jumpForce;

		float time = 5;
		int cooldown = 15;
		int recover = 5;

		bool boost = false;

	    void Start()
	    {
			jump = new Vector3(0f, 2f, 0f);
			//jumpForce = Done_PlayerController.jBoost;
	    }

	    // Update is called once per frame
	    void Update()
	    {
			jumpForce = Done_PlayerController.jBoost;

			if(Input.GetKey(KeyCode.Space) && time == 5 && boost == false) //sets off the shield, but only when the cooldown is full
			{
				player.rBody.AddForce(jump * jumpForce, ForceMode.Impulse);
				boost = true;
			}

			if(boost == true)
			{
				time -= Time.deltaTime * cooldown; //checks to see how long the shield can be active
			}
			else //while the shield is inactive the cooldown will try to fill itself back up
			{
				time += Time.deltaTime * recover;
				if (time > 5) //insures that the coolDown never surpasses 5, making the parry unusable
				{
					time = 5;
				}
			}

			if (time < 1) //wheen the cooldown hits 0, the shield deactives
			{
				boost = false;
			}
	    }
	}
}