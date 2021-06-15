using System;
using UnityEngine;

public class Docks : MonoBehaviour {
	// From GameController
	private GameController gameController;
	public float yOffset;
	private Material[] DockMaterials;
	private Material HoverMaterial;
	public GameObject[] UnitiesPrefabs;
	// From PlayerController
	private PlayerController playerController;
	private Base playerBase;
	// Logic
	private float docksYOffset = 2f;
	private int unityRow;
	private Camera currentCamera;
	private Vector2Int previousHover = Vector2Int.one * -1;
	public GameObject[,] docksTiles;
	public SpaceShip[,] docksSpaceShips;

	private void Awake() {
		gameController = GetComponentInParent<GameController>();
		playerController = GetComponent<PlayerController>();
		yOffset = gameController.yOffset;
		DockMaterials = gameController.DockMaterials;
		UnitiesPrefabs = gameController.UnitiesPrefabs;
		HoverMaterial = gameController.HoverMaterial;
	}
	private void Start() {
		playerBase = GetComponent<Base>();
	}
	private void Update() {
		if (!currentCamera){
			currentCamera = Camera.main;
			return;
		}

		RaycastHit hoveredTile;
		Ray ray = currentCamera.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out hoveredTile, 200, LayerMask.GetMask("Docks"))){
			Vector2Int hitPosition = LookupTileIndex(hoveredTile.transform.gameObject);
			if (hitPosition != Vector2Int.one * -1){
				if (previousHover == Vector2Int.one * -1){
					previousHover = hitPosition;
				}

				if(previousHover != hitPosition) {
					docksTiles[previousHover.x, previousHover.y].GetComponent<MeshRenderer>().material = DockMaterials[(int)playerController.Team];
				}
				docksTiles[hitPosition.x, hitPosition.y].GetComponent<MeshRenderer>().material = HoverMaterial;
				previousHover = hitPosition;

				if (Input.GetMouseButtonDown(0)){
					if(docksSpaceShips[hitPosition.x, hitPosition.y] != null){
						Debug.Log(docksSpaceShips[hitPosition.x, hitPosition.y].team);
						Debug.Log(docksSpaceShips[hitPosition.x, hitPosition.y].type);
						DeploySpaceShip(hitPosition.x, hitPosition.y);
					}
				}
			}
		}
		else{
			if (previousHover != Vector2Int.one * -1)
			{
				docksTiles[previousHover.x, previousHover.y].GetComponent<MeshRenderer>().material = DockMaterials[(int)playerController.Team];
				previousHover = Vector2Int.one * -1;
			}
		}
	}
	public void GenerateDocks(){
		docksTiles = new GameObject[Enum.GetValues(typeof(Unities)).Length, 3];
		docksSpaceShips = new SpaceShip[Enum.GetValues(typeof(Unities)).Length, 3];

		GameObject docksObject = new GameObject(string.Format("Docks: {0}", playerController.Team));
		docksObject.transform.parent = transform;
		docksObject.transform.position = playerController.DockCenter;

			foreach(Unities unity in Enum.GetValues(typeof(Unities))){
				int numberOfUnities = playerController.GetInitialDocksUnities(unity);
				unityRow = (int)unity > 3 ? 4 : (int)unity;

				for (int i = 0; i < numberOfUnities; i++)
				{
					GameObject dockTile = GenerateDockTile(i, unity, docksObject);
					docksTiles[(int)unity, i] = dockTile;
					docksSpaceShips[(int)unity, i] = SpawnSpaceShip(i, unity, dockTile);
				}
			}
	}
	private GameObject GenerateDockTile(int i, Unities unity, GameObject docksObject){
		GameObject dockTile = new GameObject(string.Format("{0}_{1}", unity, i));
		dockTile.transform.parent = docksObject.transform;
		dockTile.transform.localPosition = Vector3.zero;
	
		Mesh mesh  = new Mesh();
		dockTile.AddComponent<MeshFilter>().mesh = mesh;

		Vector3[] vertices = new Vector3[4];
		vertices[0] = new Vector3((i)*gameController.tileSize, yOffset , (unityRow)*gameController.tileSize);
		vertices[1] = new Vector3((i)*gameController.tileSize, yOffset , (unityRow+1)*gameController.tileSize);
		vertices[2] = new Vector3((i+1)*gameController.tileSize, yOffset , (unityRow)*gameController.tileSize);
		vertices[3] = new Vector3((i+1)*gameController.tileSize, yOffset , (unityRow+1)*gameController.tileSize);

		mesh.vertices = vertices;
		mesh.triangles = new int[] {0,1,2,1,3,2}; 
		mesh.RecalculateNormals();

		dockTile.layer = LayerMask.NameToLayer("Docks");

		dockTile.AddComponent<MeshRenderer>().material = DockMaterials[(int)playerController.Team];

		dockTile.AddComponent<BoxCollider>();

		return dockTile;
	}
	private SpaceShip SpawnSpaceShip(int i, Unities unity, GameObject dockTile){
		SpaceShip ship = Instantiate(UnitiesPrefabs[(int)unity], transform).AddComponent<SpaceShip>();
		ship.transform.parent = dockTile.transform;
		Vector3 shipPosition = new Vector3(i*gameController.tileSize + gameController.tileSize/2,yOffset + docksYOffset,unityRow*gameController.tileSize + gameController.tileSize/2);
		
		ship.SetPosition(shipPosition);
		ship.SetScale(Vector3.one*gameController.GetUnityPrefabScale(unity));

		ship.team = playerController.Team;
		ship.type = unity;

		MeshRenderer[] pieceChildren = ship.GetComponentsInChildren<MeshRenderer>();
		foreach(MeshRenderer child in pieceChildren)
			child.material = DockMaterials[(int)playerController.Team];

		return ship;

	}
	private Vector2Int LookupTileIndex(GameObject hoveredTileObject){
		for (int x = 0; x < Enum.GetValues(typeof(Unities)).Length; x++)
		for (int y = 0; y < 3; y++)
		if (docksTiles[x,y] == hoveredTileObject)
		return new Vector2Int(x,y);

		return Vector2Int.one * -1;

	}
	private void DeploySpaceShip(int x, int y){
		SpaceShip ship = docksSpaceShips[x,y];
		docksSpaceShips[x,y] = null;
		playerBase.MoveSpaceShipToBase(ship);
	}
}
