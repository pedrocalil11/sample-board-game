using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	[SerializeField] private float dragSpeed = 50f;
	[SerializeField] private float wheelSpeed = 200f;
	private Vector3 dragOrigin;  
	Camera cam;
	private float X;
     private float Y;
	void Update () {
		if (!cam){
			cam = Camera.main;
			return;
		}
		// RaycastHit hoveredTile;
		// Ray ray = cam.ScreenPointToRay(Input.mousePosition);
		// if (Physics.Raycast(ray, out hoveredTile, 200, LayerMask.GetMask("Docks", "Tile"))) return;

		Vector3 pos = transform.position;


		if (Input.GetAxis("Mouse ScrollWheel") > 0f ) // forward
		{
			pos += transform.forward *wheelSpeed * Time.deltaTime;
		}
		else if (Input.GetAxis("Mouse ScrollWheel") < 0f ) // backwards
		{
			pos -= transform.forward *wheelSpeed * Time.deltaTime;
		}

		if(Input.GetMouseButton(0)) {
			pos += new Vector3(-Input.GetAxis("Mouse X") * dragSpeed*Time.deltaTime, 0, -Input.GetAxis("Mouse Y") * dragSpeed*Time.deltaTime);
		}

		transform.position = pos;

		if(Input.GetMouseButton(1)) {
             transform.Rotate(new Vector3(Input.GetAxis("Mouse Y") * dragSpeed*Time.deltaTime, -Input.GetAxis("Mouse X") * dragSpeed*Time.deltaTime, 0));
             X = transform.rotation.eulerAngles.x;
             Y = transform.rotation.eulerAngles.y;
             transform.rotation = Quaternion.Euler(X, Y, 0);
         }

		
	}  
}
