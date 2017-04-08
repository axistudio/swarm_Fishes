using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flock : MonoBehaviour {
	public float fishSpeed = 0.3f;
	float rotationSpeed = 4.0f;
	Vector3 averageHeading;
	Vector3 averagePosition;


	float neighbourDistance = 5.0f;
	// Use this for initialization

	bool turning = false;
	void Start () {
		fishSpeed = Random.Range (0.5f, 5.3f);
	}
	
	// Update is called once per frame
	void Update () {



		if (Vector3.Distance(transform.position, Vector3.zero) >= GlobalFlock.tankSize) {
			turning = true;
		} else {
			turning = false;
		}
		if (turning) {
			Vector3 direction = Vector3.zero - transform.position;
			transform.rotation = Quaternion.Slerp (transform.rotation,
				Quaternion.LookRotation (direction), rotationSpeed * Time.deltaTime);

			fishSpeed = Random.Range (3.5f, 7);
		} else {
			

			if (Random.Range (0, 5) < 1)
				ApplyRules ();
		}
		transform.Translate (Time.deltaTime*-fishSpeed, 0, 0);
	}

	void ApplyRules(){
		GameObject[] gos;
		gos = GlobalFlock.allFish;
		Vector3 ventre = Vector3.zero;
		Vector3 vavoid = Vector3.zero;

		float gSpeed = 0.1f;
		Vector3 goalPos = GlobalFlock.goalPos;

		float dist;

		int groupSize = 0;
		foreach (GameObject go in gos) {
			if (go != this.gameObject) {
				dist = Vector2.Distance (go.transform.position, this.transform.transform.position);
				if (dist <= neighbourDistance) {
					ventre += go.transform.position;
					groupSize++;
					if (dist < 1.0f) {
						vavoid = vavoid + (this.transform.position - go.transform.position);

					}
					flock antherFlock = go.GetComponent<flock> ();
					gSpeed = gSpeed + antherFlock.fishSpeed;
				}
			}
		}
		if (groupSize > 0) {
			ventre = ventre / groupSize + (goalPos - this.transform.position);
			fishSpeed = gSpeed / groupSize;

			Vector3 direction = (ventre + vavoid) - transform.position;
			if (direction != Vector3.zero)
				transform.rotation = Quaternion.Slerp (transform.rotation,
														Quaternion.LookRotation (direction), 
														rotationSpeed * Time.deltaTime);
		}

	}
}
