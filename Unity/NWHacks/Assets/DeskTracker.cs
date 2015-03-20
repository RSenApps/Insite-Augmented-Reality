using UnityEngine;
using System.Collections;

public class DeskTracker : MonoBehaviour {
	/*
	bool deskSet = false;
	public GameObject surfaceTracker;
	public GameObject desk;
	public float threshold = .1f;
	Vector3 lastPossibleDeskPosition;
	float lastTime;
	public int holdTimeRequirement = 3;
	// Use this for initialization
	*/
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		/*
		if (!deskSet) {
			Vector3 trackerPosition = surfaceTracker.transform.position;
			if (lastPossibleDeskPosition == null)
			{
				lastPossibleDeskPosition = trackerPosition;
				lastTime = Time.time;
			}
			else if (lastPossibleDeskPosition.x - trackerPosition.x > threshold || 
			         lastPossibleDeskPosition.y - trackerPosition.y > threshold ||
			         lastPossibleDeskPosition.z - trackerPosition.z > threshold) {
				lastPossibleDeskPosition = trackerPosition;
				lastTime = Time.time;
			}
			else if (Time.time - lastTime > holdTimeRequirement) {
				desk.transform.position = lastPossibleDeskPosition;
				desk.transform.rotation = surfaceTracker.transform.rotation;
				desk.GetComponent<Renderer>().enabled = true;
				deskSet = true;
			}

		}
		*/

	}
}
