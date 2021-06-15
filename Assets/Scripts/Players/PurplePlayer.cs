using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurplePlayer : PlayerController {
	public PurplePlayer(){
		Team = Teams.Purple;
		BankAccount = 0;
		MetalAccount = 0;
		DockCenter = new Vector3(41.3f, yOffset,21.7f);
		BaseCoordinates = new Vector2Int(17,9);
		BaseCenter = new Vector3(48.9f, yOffset,10.8f);
	}

	public override int GetInitialDocksUnities(Unities unity){
		if(unity == Unities.Miner) return 2;
		else if(unity == Unities.Catapult) return 3;
		else if(unity == Unities.Crusader) return 2;
		else if(unity == Unities.SpaceTower) return 1;
		else if (unity == Unities.Pathfinder) return 1;
		return 0;
	}
	public override int GetInitialBaseUnities(Unities unity){
		if(unity == Unities.Miner) return 1;
		else if(unity == Unities.Crusader) return 1;
		return 0;
	}
}
