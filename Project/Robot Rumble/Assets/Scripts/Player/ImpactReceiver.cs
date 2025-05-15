using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactReceiver : MonoBehaviour {
	float mass = 3.0F; // Defines the character mass
	Vector3 impact = Vector3.zero;
	private CharacterController character;
	
	void Start () {
		character = GetComponent<CharacterController>();
	}
	
	void Update () {
		// Apply the impact force over time
		if (impact.magnitude > 0.2F){
			character.Move(impact * Time.deltaTime);
		}
			
		// Consume the impact energy over time
		impact = Vector3.Lerp(impact, Vector3.zero, 5*Time.deltaTime);
	}
	
	// Call this function to add an impact force to the character controller
	public void AddImpact(Vector3 dir, float force){
		impact = Vector3.zero;
		dir.Normalize();
		impact = dir.normalized * force / mass;
	}
}

