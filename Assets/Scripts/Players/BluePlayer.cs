using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePlayer : PlayerController {
	public BluePlayer(){
		Team = Teams.Blue;
		BankAccount = 1;
		MetalAccount = 0;
		DockCenter = new Vector3(24.93f, yOffset, 37.3f);
		BaseCoordinates = new Vector2Int(9,13);
		BaseCenter = new Vector3(12.3f, yOffset, 45.8f);
	}

	public override int GetInitialDocksUnities(Unities unity){
		if(unity == Unities.Miner) return 1;
		else if(unity == Unities.Catapult) return 3;
		else if(unity == Unities.Crusader) return 3;
		else if(unity == Unities.SpaceTower) return 1;
		else if (unity == Unities.BountyHunter) return 1;
		return 0;
	}
	public override int GetInitialBaseUnities(Unities unity){
		if(unity == Unities.Miner) return 2;
		return 0;
	}
}
