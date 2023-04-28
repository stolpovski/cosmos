using UnityEngine;
using System.Collections;

namespace AstronautPlayer
{

	public class AstronautPlayer : MonoBehaviour {

		private Animator anim;
		private CharacterController controller;

		public float speed = 600.0f;
		public float turnSpeed = 400.0f;
		private Vector3 moveDirection = Vector3.zero;
		public float gravity = 20.0f;

		void Start () {
			controller = GetComponent <CharacterController>();
			anim = gameObject.GetComponentInChildren<Animator>();
		}

		void FixedUpdate (){
			float v = Input.GetAxis("Vertical");


			if(controller.isGrounded){
				
				if (v < 0) v = 0;

				if (v > 0)
                {
					anim.SetInteger("AnimationPar", 1);
				}
				else
                {
					anim.SetInteger("AnimationPar", 0);
				}
				moveDirection = transform.forward * v * speed;
			}

			float turn = Input.GetAxis("Horizontal");
			transform.Rotate(0, turn * turnSpeed * Time.deltaTime, 0);
			controller.Move(moveDirection * Time.deltaTime);
			moveDirection.y -= gravity * Time.deltaTime;
		}
	}
}
