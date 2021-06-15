using System;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	// Player Characteristics
	public Teams Team { get; set; }
	public Vector3 DockCenter {get;set;}
	public Vector2Int BaseCoordinates {get;set;}
	public Vector3 BaseCenter {get;set;}
	public int BankAccount {get;set;}
	public int MetalAccount {get;set;}
	// From GameController
	public GameController gameController;
	public float yOffset;
	// 
	private Camera currentCamera;
	SpaceShip onSelectionShip;
	SpaceShip selectedShip;
	bool clickOutside = false;
	
	private void Awake() {
		gameController = GetComponentInParent<GameController>();
		this.gameObject.AddComponent<Docks>();
		this.gameObject.AddComponent<Base>();
		yOffset = gameController.yOffset;
	}
	// private void Update() {
	// 	if (!currentCamera){
	// 		currentCamera = Camera.main;
	// 		return;
	// 	}
	// 	RaycastHit hoveredShip;
	// 	Ray ray = currentCamera.ScreenPointToRay(Input.mousePosition);
	// 	if (Physics.Raycast(ray, out hoveredShip, 200, LayerMask.GetMask("Ship"))){
	// 		SpaceShip spaceShipComponent = hoveredShip.transform.gameObject.GetComponent<SpaceShip>();
	// 		if (Input.GetMouseButtonDown(0)){
	// 			onSelectionShip = spaceShipComponent;
	// 		}
	// 		else if(onSelectionShip != null && onSelectionShip == spaceShipComponent &&Input.GetMouseButtonUp(0)){
	// 			if (selectedShip != null && selectedShip != spaceShipComponent) RemoveSelectionShip(selectedShip);
	// 			SelectShip(spaceShipComponent);
	// 		}
	// 	}
	// 	else{
	// 		if (Input.GetMouseButtonDown(0)){
	// 			clickOutside = true;
	// 		}
	// 		else if(clickOutside && Input.GetMouseButtonUp(0) && selectedShip != null){
	// 			RemoveSelectionShip(selectedShip);
	// 		}
	// 	}
		
	// 	if (selectedShip != null && gameController.teamsList[gameController.playerTurn] == Team){

	// 		if (Input.GetKeyDown(KeyCode.M)){
	// 			Debug.Log("Moove this " + selectedShip.type);
	// 		}
	// 		else if(Input.GetKeyDown(KeyCode.C)){
	// 			Debug.Log("Collect this " + selectedShip.type);
	// 		}
	// 	}
	// }
	// private void SelectShip(SpaceShip ship){
	// 	onSelectionShip = null;
	// 	selectedShip = ship;		
	// 	MeshRenderer[] pieceChildren = ship.GetComponentsInChildren<MeshRenderer>();

	// 	foreach(MeshRenderer child in pieceChildren)
	// 		child.material = gameController.HoverMaterial;

	// }
	// private void RemoveSelectionShip(SpaceShip ship){
	// 	onSelectionShip = null;
	// 	selectedShip = null;
	// 	MeshRenderer[] pieceChildren = ship.GetComponentsInChildren<MeshRenderer>();

	// 	foreach(MeshRenderer child in pieceChildren)
	// 		child.material = gameController.DockMaterials[(int)ship.team];
	// }

	// Move
	// Collect
	// Pass Resources
	// Collect Resource in Base
	// Deliver Resource to Base
	// Build Space Defense
	// Remove Space Defense
	// ########
	// Stock
	// Refine
	// Tech Tree
	// Research Track
	// Buy Cards
	// Use Cards
	public virtual int GetInitialDocksUnities(Unities unity){
		return 0;
	}
	public virtual int GetInitialBaseUnities(Unities unity){
		return 0;
	}

}
