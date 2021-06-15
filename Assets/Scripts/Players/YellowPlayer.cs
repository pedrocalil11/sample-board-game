using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowPlayer : PlayerController {
	public YellowPlayer(){
		Team=Teams.Yellow;
		BankAccount = 1;
		MetalAccount = 1;
		DockCenter = new Vector3(45f, yOffset, -19.2f);
		BaseCoordinates = new Vector2Int(15,1);
		BaseCenter = new Vector3(33.6f, yOffset, -11.4f);
	}

	public override int GetInitialDocksUnities(Unities unity){
		if(unity == Unities.Miner) return 2;
		else if(unity == Unities.Catapult) return 2;
		else if(unity == Unities.Crusader) return 3;
		else if(unity == Unities.SpaceTower) return 1;
		else if (unity == Unities.GreatMiner) return 1;
		return 0;
	}
	public override int GetInitialBaseUnities(Unities unity){
		if(unity == Unities.Miner) return 1;
		else if(unity == Unities.Catapult) return 1;
		return 0;
	}
}
