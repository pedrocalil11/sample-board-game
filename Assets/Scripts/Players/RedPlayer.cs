using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedPlayer : PlayerController {
	public RedPlayer(){
		Team = Teams.Red;
		BankAccount = 0;
		MetalAccount = 0;
		DockCenter = new Vector3(7f, yOffset, -16.7f);
		BaseCoordinates = new Vector2Int(3,1);
		BaseCenter = new Vector3(-5.2f, yOffset, -9.6f);
	}
	public override int GetInitialDocksUnities(Unities unity){
		if(unity == Unities.Miner) return 2;
		else if(unity == Unities.Catapult) return 3;
		else if(unity == Unities.Crusader) return 2;
		else if(unity == Unities.SpaceTower) return 1;
		else if (unity == Unities.Shuttle) return 1;
		return 0;
	}
	public override int GetInitialBaseUnities(Unities unity){
		if(unity == Unities.Miner) return 1;
		else if(unity == Unities.Crusader) return 1;
		return 0;
	}
}
