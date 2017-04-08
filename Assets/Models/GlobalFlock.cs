using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalFlock : MonoBehaviour {

	public Renderer babbole;

	public GameObject fishPreFab;
	public GameObject goalPreFab;
	public static int tankSize = 3;
	public static float fishSize = 1.0f;

	static int numFIsh = 20;
	public static GameObject[] allFish = new GameObject[numFIsh];
	// Use this for initialization

	public GameObject spawnTarget;


	public static Vector3 goalPos = Vector3.zero;

	void Start () {


		babbole = GetComponent<Renderer> ();
		babbole.enabled = true; 


	}
	Vector3 NewTargetPostion;
	void OnTriggerEnter(Collider other) {
		
		Debug.Log ("Colllide!");
		//babbole.enabled = false; 

		if (babbole.enabled) {
			NewTargetPostion = spawnTarget.transform.position;

			NewTargetPostion.y -= 5.0f;
			for (int i = 0; i < numFIsh; i++) {
				Vector3 pos = new Vector3 ( Random.Range (-tankSize+NewTargetPostion.x, NewTargetPostion.x+tankSize),
					Random.Range (-tankSize+NewTargetPostion.y, NewTargetPostion.y+tankSize), 
					Random.Range (-tankSize+NewTargetPostion.z, NewTargetPostion.z+tankSize));
				float fishscale = Random.Range (0.2f, fishSize);
				Transform transFormScale = new GameObject ().transform;
				transFormScale.localScale = new Vector3 ( fishscale,fishscale,fishscale);
				allFish [i] = (GameObject)Instantiate (fishPreFab, pos, Quaternion.identity,transFormScale);
			}

			babbole.enabled = false;
		}
		//Destroy(other.gameObject);			

	}
	
	// Update is called once per frame
	void Update () {
		goalPos = goalPreFab.transform.position;
//		if (Random.Range (0, 10000) < 50) {
//			goalPos = new Vector3 ( Random.Range (-tankSize+NewTargetPostion.x, NewTargetPostion.x+tankSize),
//				Random.Range (-tankSize+NewTargetPostion.y, NewTargetPostion.y+tankSize), 
//				Random.Range (-tankSize+NewTargetPostion.z, NewTargetPostion.z+tankSize));
//			goalPreFab.transform.position = goalPos;
//		}
	}
}
