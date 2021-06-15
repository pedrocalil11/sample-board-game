using System;
using UnityEngine;


public enum Teams{
	Green=0,
	Red=1,
	Blue=2,
	Yellow=3,
	Purple=4
}

public class GameController : MonoBehaviour {
	// ############################ Material and Prefabs Stuff
	[SerializeField] public Material NormalMaterial;
	[SerializeField] public Material PiratesTerrainMaterial;
	[SerializeField] public Material CPOasisMaterial;
	[SerializeField] public Material CPMoonMaterial;
	[SerializeField] public Material CPOutpostMaterial;
	[SerializeField] public Material AsteroidCoinMaterial;
	[SerializeField] public Material AsteroidMetalMaterial;
	[SerializeField] public Material AsteroidKappaMaterial;
	[SerializeField] public Material AsteroidMetalCoinMaterial;
	[SerializeField] public Material GreenZoneMaterial;
	[SerializeField] public Material GreenPathMaterial;
	[SerializeField] public Material GreenBaseMaterial;
	[SerializeField] public Material BlueZoneMaterial;
	[SerializeField] public Material BluePathMaterial;
	[SerializeField] public Material BlueBaseMaterial;
	[SerializeField] public Material YellowZoneMaterial;
	[SerializeField] public Material YellowPathMaterial;
	[SerializeField] public Material YellowBaseMaterial;
	[SerializeField] public Material PurpleZoneMaterial;
	[SerializeField] public Material PurplePathMaterial;
	[SerializeField] public Material PurpleBaseMaterial;
	[SerializeField] public Material RedZoneMaterial;
	[SerializeField] public Material RedPathMaterial;
	[SerializeField] public Material RedBaseMaterial;
	[SerializeField] public Material[] DockMaterials;
	[SerializeField] public Material HoverMaterial;
	[SerializeField] public GameObject[] UnitiesPrefabs;
	// ########## Dimensions and Positioning
	public int mapWidth = 18;
	public int mapHeight = 14;
	public float yOffset = 0f;
	public float tileSize = 10f;
	// ########### Logic
	public PlayerController[] playerControllers;
    Board theBoard;
	public Teams[] teamsList;
	public int playerTurn = 0; 
   	private void Awake() {
		   teamsList = new Teams[5]{Teams.Blue,Teams.Red, Teams.Green, Teams.Yellow, Teams.Purple};
		   InitializeBoard();
		   InitializePlayers(teamsList);		
	}
	private void Start() {
		theBoard.GenerateAllTiles();
		foreach (Teams player in teamsList){
			playerControllers[(int)player].GetComponent<Docks>().GenerateDocks();
			playerControllers[(int)player].GetComponent<Base>().GenerateBase();
		}
	}
	public void FinishMyTurn(){
		playerTurn +=1;
		if (playerTurn == teamsList.Length) playerTurn =0; 
	}
	private void InitializeBoard(){
		GameObject boardObject = new GameObject("Board");
		boardObject.transform.parent = transform;
		boardObject.AddComponent<Board>();
		theBoard = boardObject.GetComponent<Board>();
	}

	private void InitializePlayers(Teams[] players){
		playerControllers = new PlayerController[Enum.GetValues(typeof(Teams)).Length];
		foreach (Teams player in players)
		{
			GameObject playerObject = new GameObject(string.Format("Player {0}",player));
			playerObject.transform.parent = transform;
			switch(player){
				case Teams.Blue:
					playerObject.AddComponent<BluePlayer>();
					break;
				case Teams.Green:
					playerObject.AddComponent<GreenPlayer>();
					break;
				case Teams.Purple:
					playerObject.AddComponent<PurplePlayer>();
					break;
				case Teams.Red:
					playerObject.AddComponent<RedPlayer>();
					break;
				case Teams.Yellow:
					playerObject.AddComponent<YellowPlayer>();
					break;
				default:
					throw new Exception("Not valid team: " + player);
			}
			playerControllers[(int)player] = playerObject.GetComponent<PlayerController>();
		}
	}
	public float GetUnityPrefabScale(Unities unity){
		if(unity == Unities.Miner) return 2f;
		else if(unity == Unities.Catapult) return 2f;
		else if(unity == Unities.Crusader) return 2f;
		else if(unity == Unities.SpaceTower) return 2f;
		else if (unity == Unities.SpaceFortress) return 2f;
		else if (unity == Unities.Shuttle) return 2f;
		else if (unity == Unities.GreatMiner) return 2f;
		else if (unity == Unities.Pathfinder) return 2f;
		else if (unity == Unities.BountyHunter) return 2f;
		return 0f;
	}

}
