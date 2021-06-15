using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Unities{
	Miner=0,
	Catapult=1,
	Crusader=2,
	SpaceTower=3,
	SpaceFortress=4,
	Shuttle=5,
	BountyHunter=6,
	GreatMiner=7,
	Pathfinder=8
}

public class SpaceShip : MonoBehaviour {
	public Unities type;
	public Teams team;
	public int currentX;
	public int currentY;

	private Vector3 desiredScale;
	private Vector3 desiredPosition;

	private void Update() {
		transform.localPosition = Vector3.Lerp(transform.localPosition, desiredPosition, Time.deltaTime*10);
		transform.localScale = Vector3.Lerp(transform.localScale, desiredScale, Time.deltaTime*10);
	}

	public virtual void SetPosition(Vector3 position, bool smooth = false){
		desiredPosition = position;
		if (!smooth){
			transform.localPosition = desiredPosition;
		}
	}

	public virtual void SetScale(Vector3 scale, bool smooth = false){
		desiredScale = scale;
		if (!smooth){
			transform.localScale = desiredScale;
		}
	}
}
