using System;
using UnityEngine;

public class Base : MonoBehaviour {
    // From GameController
	private GameController gameController;
	public float yOffset;
	private Material[] DockMaterials;
	public GameObject[] UnitiesPrefabs;
	// From PlayerController
	private PlayerController playerController;
    // Logic
    public SpaceShip[] baseSpaceShips;
	private GameObject baseObject;
	// Positioning and Dimensions
	private float baseYOffset = 2f;
	private float baseWidth = 6f;

    private void Awake() {
        gameController = GetComponentInParent<GameController>();
		playerController = GetComponent<PlayerController>();
		yOffset = gameController.yOffset;
		DockMaterials = gameController.DockMaterials;
		UnitiesPrefabs = gameController.UnitiesPrefabs;
    }
    public void GenerateBase(){

        baseSpaceShips = new SpaceShip[Enum.GetValues(typeof(Unities)).Length*3];

		baseObject = GenerateBaseObject();

       	int spaceShipCounter = 0;
        foreach(Unities unity in Enum.GetValues(typeof(Unities))){
            int numberOfUnities = playerController.GetInitialBaseUnities(unity);
            for (int i = 0; i < numberOfUnities; i++)
            {
                baseSpaceShips[spaceShipCounter] = SpawnSpaceShip(spaceShipCounter, unity);
                spaceShipCounter += 1;
            }
        }
    }
	private GameObject GenerateBaseObject(){
		GameObject baseObject = new GameObject(string.Format("Base: {0}", playerController.Team));
		baseObject.transform.parent = transform;
		baseObject.transform.position = playerController.BaseCenter;

		Mesh mesh  = new Mesh();
		baseObject.AddComponent<MeshFilter>().mesh = mesh;

		Vector3[] vertices = new Vector3[4];
		vertices[0] = new Vector3((-baseWidth/2)*gameController.tileSize, yOffset , (-baseWidth/2)*gameController.tileSize);
		vertices[1] = new Vector3((-baseWidth/2)*gameController.tileSize, yOffset , (baseWidth/2)*gameController.tileSize);
		vertices[2] = new Vector3((baseWidth/2)*gameController.tileSize, yOffset , (-baseWidth/2)*gameController.tileSize);
		vertices[3] = new Vector3((baseWidth/2)*gameController.tileSize, yOffset , (baseWidth/2)*gameController.tileSize);

		mesh.vertices = vertices;
		mesh.triangles = new int[] {0,1,2,1,3,2}; 
		mesh.RecalculateNormals();

		baseObject.layer = LayerMask.NameToLayer("Base");

		baseObject.AddComponent<MeshRenderer>().material = DockMaterials[(int)playerController.Team];

		baseObject.AddComponent<BoxCollider>();

		return baseObject;
	}
    private SpaceShip SpawnSpaceShip(int spaceShipCounter, Unities unity){
        SpaceShip ship = Instantiate(UnitiesPrefabs[(int)unity], transform).AddComponent<SpaceShip>();
		ship.transform.parent = baseObject.transform;
		PositionSpaceShip(ship, spaceShipCounter);

		ship.SetScale(Vector3.one*gameController.GetUnityPrefabScale(unity));

		ship.team = playerController.Team;
		ship.type = unity;

		MeshRenderer[] pieceChildren = ship.GetComponentsInChildren<MeshRenderer>();

		foreach(MeshRenderer child in pieceChildren)
			child.material = DockMaterials[(int)playerController.Team];

		ship.gameObject.layer = LayerMask.NameToLayer("Ship");

		return ship;
    }

	public void MoveSpaceShipToBase(SpaceShip ship){
		for (int i = 0; i < baseSpaceShips.Length; i++)
		{
			if (baseSpaceShips[i] == null)
			{
				baseSpaceShips[i] = ship;
				ship.gameObject.layer = LayerMask.NameToLayer("Ship");
				ship.transform.parent = baseObject.transform;
				PositionSpaceShip(ship, i);
				break;
			}
		}
	}

	private void PositionSpaceShip(SpaceShip ship, int position){
		Vector3 shipPosition = new Vector3(
			(1-position%3)*(baseWidth/4)*gameController.tileSize,
			yOffset + baseYOffset,
			((int)(position/3) - 2)*(baseWidth/5)*gameController.tileSize);
		
		ship.SetPosition(shipPosition);
	}
}
