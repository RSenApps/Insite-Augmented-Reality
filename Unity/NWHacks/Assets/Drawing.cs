using UnityEngine;
using System.Collections;

public class Drawing : MonoBehaviour {
	public GameObject finger;
	public GameObject whiteboard;
	public GameObject whiteboardMarker;
	float refreshRate = .1f;
	float lastUploadTime = 0;
	// Use this for initialization
	bool drawing = false;
	void Start () {
	
		Texture2D tex = new Texture2D (500, 350, TextureFormat.RGB24, false);

		Color[] colors = new Color[500*350];
		for (int i = 0; i < 500*350; i++)
		{
			colors[i] = Color.white;
		}
		tex.SetPixels (colors);
		GetComponent<Renderer>().material.mainTexture = tex;
		
	}
	void OnTouchEnter()
	{
		drawing = true;
		whiteboardMarker.GetComponent<Renderer>().material.color=Color.green;

	}
	void OnTouchExit()
	{
		drawing = false;
		whiteboardMarker.GetComponent<Renderer>().material.color=Color.red;

	}
	// Update is called once per frame
	void Update () {
		if (drawing && Time.time - lastUploadTime > refreshRate) {

			lastUploadTime = Time.time;

		Texture2D texture = (Texture2D) GetComponent<Renderer>().material.mainTexture;
			Vector3 position = Meta.Hands.right.pointer.position;
		BoxCollider whiteBoardCollider = whiteboard.GetComponent<BoxCollider> ();

			Vector3 closestPoint = position; //whiteBoardCollider.ClosestPointOnBounds(position);
		//float distance = Vector3.Distance(closestPoint, position);
		whiteboardMarker.transform.position = closestPoint;

		//if (distance < .15) {


		
			float xPercent = .5F + whiteboardMarker.transform.localPosition.x / whiteBoardCollider.size.x;
			float zPercent = .5F + whiteboardMarker.transform.localPosition.z / whiteBoardCollider.size.z;
			if (xPercent < 1 && xPercent > 0 && zPercent < 1 && zPercent > 0) {
				for (int x = (int)(xPercent*500-5); x < xPercent*500+5; x++) {
					for (int z = (int)(zPercent*350-5); z < zPercent*350+5; z++) {
						texture.SetPixel (x, z, Color.red);
					}
				}
				UploadScreenshots.updated = true;
				texture.Apply ();
			}
			//}
		}

	}


}
