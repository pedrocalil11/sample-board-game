using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenPlayer : PlayerController {
	public GreenPlayer(){
		Team = Teams.Green;
		BankAccount = 1;
		MetalAccount = 1;
		DockCenter = new Vector3(-35.3f, yOffset, 6.4f);
		BaseCoordinates = new Vector2Int(1,9);
		BaseCenter = new Vector3(-15.3f, yOffset, 15.9f);
	}
	public override int GetInitialDocksUnities(Unities unity){
		if(unity == Unities.Miner) return 2;
		else if(unity == Unities.Catapult) return 2;
		else if(unity == Unities.Crusader) return 3;
		else if(unity == Unities.SpaceTower) return 1;
		else if (unity == Unities.SpaceFortress) return 1;
		return 0;
	}
	public override int GetInitialBaseUnities(Unities unity){
		if(unity == Unities.Miner) return 1;
		else if(unity == Unities.Catapult) return 1;
		return 0;
	}
}
