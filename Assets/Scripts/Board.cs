using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType {
	Normal=1000,
	Pirate=1001,
	Oasis=1007,
	Moon=1008,
	Outpost=1009,
	CoinAsteroid=1010,
	MetalAsteroid=1011,
	KappaAsteroid=1012,
	MetalCoinAsteroid=1013,
	GreenZone=0,
	GreenPath=1,
	RedZone=100,
	RedPath=101,
	BlueZone=200,
	BluePath=201,
	YellowZone=300,
	YellowPath=301,
	PurpleZone=400,
	PurplePath=401
}

public class Board : MonoBehaviour {
	private GameController gameController;
 
	// Dimension and Positioning Stuff
	private float yOffset;
	private float xStep;
	private float yStep;
	
	Vector3 boardCenter = Vector3.zero;
	Vector3 bounds;

	// Logic
    public GameObject[,] tiles;
	private void Awake() {
		gameController = GetComponentInParent<GameController>();
		yOffset = gameController.yOffset;
	}
    public void GenerateAllTiles(){
		yOffset += transform.position.y;
		xStep = gameController.tileSize*0.75f;
		yStep = gameController.tileSize*0.5f*Mathf.Pow(3f,0.5f);

		tiles = new GameObject[gameController.mapWidth, gameController.mapHeight];
        for (int x = 0; x < gameController.mapWidth; x++)
            for (int y = 0; y < gameController.mapHeight; y++)
                if (CheckTileExistence(x,y)) 
                    tiles[x,y] = GenerateSingleTile(x,y);
		
		foreach(Teams team in gameController.teamsList){
			Vector2Int baseCoordinates = gameController.playerControllers[(int)team].BaseCoordinates;
			if (CheckTileExistence(baseCoordinates.x, baseCoordinates.y)){
				throw new UnassignedReferenceException("Tried to create a base in a existent coordinate.");
			}
			GenerateBaseTile(team, baseCoordinates.x, baseCoordinates.y);

		}
    }
	private void GenerateBaseTile(Teams team, int x, int y){
		Material tileMaterial = GetBaseMaterials(team);

		GameObject tileObject = new GameObject(string.Format("Base {0}", team));
		tileObject.transform.parent = transform;
		tileObject.transform.rotation = Quaternion.identity;
		tileObject.transform.position = Vector3.zero;

		Mesh mesh  = new Mesh();
		tileObject.AddComponent<MeshFilter>().mesh = mesh;
		
		bounds = new Vector3((xStep/2)*gameController.tileSize, 0, (yStep/2)) + boardCenter;

		Vector3[] vertices = new Vector3[7];
		if (x%2 == 0){
			vertices[0] = new Vector3(x*xStep, yOffset, y*yStep)-bounds;
			vertices[1] = new Vector3(x*xStep - gameController.tileSize/2, yOffset, y*yStep)-bounds;
			vertices[2] = new Vector3(x*xStep - gameController.tileSize/4, yOffset, (y+0.5f)*yStep)-bounds;
			vertices[3] = new Vector3(x*xStep + gameController.tileSize/4, yOffset, (y+0.5f)*yStep)-bounds;
			vertices[4] = new Vector3(x*xStep + gameController.tileSize/2, yOffset, y*yStep)-bounds;
			vertices[5] = new Vector3(x*xStep + gameController.tileSize/4, yOffset, (y-0.5f)*yStep)-bounds;
			vertices[6] = new Vector3(x*xStep - gameController.tileSize/4, yOffset, (y-0.5f)*yStep)-bounds;
		}
		else{
			vertices[0] = new Vector3(x*xStep, yOffset, (y+0.5f)*yStep)-bounds;
			vertices[1] = new Vector3(x*xStep - gameController.tileSize/2, yOffset, (y+0.5f)*yStep)-bounds;
			vertices[2] = new Vector3(x*xStep - gameController.tileSize/4, yOffset, (y+1f)*yStep)-bounds;
			vertices[3] = new Vector3(x*xStep + gameController.tileSize/4, yOffset, (y+1f)*yStep)-bounds;
			vertices[4] = new Vector3(x*xStep + gameController.tileSize/2, yOffset, (y+0.5f)*yStep)-bounds;
			vertices[5] = new Vector3(x*xStep + gameController.tileSize/4, yOffset, y*yStep)-bounds;
			vertices[6] = new Vector3(x*xStep - gameController.tileSize/4, yOffset, y*yStep)-bounds;
		}

		mesh.vertices = vertices;
		mesh.triangles = new int[] {0,1,2,2,3,0,0,3,4,0,4,5,6,0,5,1,0,6}; 
		mesh.RecalculateNormals();

		tileObject.AddComponent<MeshRenderer>().material = tileMaterial;

		tileObject.AddComponent<BoxCollider>();
	}

    private GameObject GenerateSingleTile(int x, int y){

		TileType type = GetInitialTileType(x,y);
		Material tileMaterial = GetMaterialByType(type);
		string tileTag = GetTagByType(type);

		GameObject tileObject = new GameObject(string.Format("X:{0}, Y:{1}", x, y));
		tileObject.transform.parent = transform;
		tileObject.transform.rotation = Quaternion.identity;
		tileObject.transform.position = Vector3.zero;
		tileObject.tag = tileTag;


		Mesh mesh  = new Mesh();
		tileObject.AddComponent<MeshFilter>().mesh = mesh;
		
		bounds = new Vector3((xStep/2)*gameController.tileSize, 0, (yStep/2)) + boardCenter;

		Vector3[] vertices = new Vector3[7];
		if (x%2 == 0){
			vertices[0] = new Vector3(x*xStep, yOffset, y*yStep)-bounds;
			vertices[1] = new Vector3(x*xStep - gameController.tileSize/2, yOffset, y*yStep)-bounds;
			vertices[2] = new Vector3(x*xStep - gameController.tileSize/4, yOffset, (y+0.5f)*yStep)-bounds;
			vertices[3] = new Vector3(x*xStep + gameController.tileSize/4, yOffset, (y+0.5f)*yStep)-bounds;
			vertices[4] = new Vector3(x*xStep + gameController.tileSize/2, yOffset, y*yStep)-bounds;
			vertices[5] = new Vector3(x*xStep + gameController.tileSize/4, yOffset, (y-0.5f)*yStep)-bounds;
			vertices[6] = new Vector3(x*xStep - gameController.tileSize/4, yOffset, (y-0.5f)*yStep)-bounds;
		}
		else{
			vertices[0] = new Vector3(x*xStep, yOffset, (y+0.5f)*yStep)-bounds;
			vertices[1] = new Vector3(x*xStep - gameController.tileSize/2, yOffset, (y+0.5f)*yStep)-bounds;
			vertices[2] = new Vector3(x*xStep - gameController.tileSize/4, yOffset, (y+1f)*yStep)-bounds;
			vertices[3] = new Vector3(x*xStep + gameController.tileSize/4, yOffset, (y+1f)*yStep)-bounds;
			vertices[4] = new Vector3(x*xStep + gameController.tileSize/2, yOffset, (y+0.5f)*yStep)-bounds;
			vertices[5] = new Vector3(x*xStep + gameController.tileSize/4, yOffset, y*yStep)-bounds;
			vertices[6] = new Vector3(x*xStep - gameController.tileSize/4, yOffset, y*yStep)-bounds;
		}

		mesh.vertices = vertices;
		mesh.triangles = new int[] {0,1,2,2,3,0,0,3,4,0,4,5,6,0,5,1,0,6}; 
		mesh.RecalculateNormals();

		tileObject.layer = LayerMask.NameToLayer("Tile");

		tileObject.AddComponent<MeshRenderer>().material = tileMaterial;

		tileObject.AddComponent<BoxCollider>();

		return tileObject;
    }
    private bool CheckTileExistence(int x, int y)
    {

        List<int> existentTiles = new List<int>();
        if (x==0)
            existentTiles = new List<int>(){8};
        else if (x==1)
            existentTiles = new List<int>(){5,7,8};
        else if (x==2)
            existentTiles = new List<int>(){6,7,8,9};
        else if (x==3)
            existentTiles = new List<int>(){2,5,6,7,8,10};
        else if (x==4)
            existentTiles = new List<int>(){2,3,5,6,7,8,9,10};
        else if (x==5)
            existentTiles = new List<int>(){1,2,3,4,5,6,7,8,9,10};
        else if (x==6)
            existentTiles = new List<int>(){2,3,4,5,6,7,8,9,10,11};
        else if (x==7)
            existentTiles = new List<int>(){2,3,4,5,6,7,8,9,10};
        else if (x==8)
            existentTiles = new List<int>(){1,2,3,4,5,6,7,8,9,10,11,12,13};
        else if (x==9)
            existentTiles = new List<int>(){1,2,3,4,5,6,7,8,9,10,11,12};
        else if (x==10)
            existentTiles = new List<int>(){1,2,3,4,5,6,7,8,9,10,11,12,13};
        else if (x==11)
            existentTiles = new List<int>(){0,1,2,3,4,5,6,7,8,9,10};
        else if (x==12)
            existentTiles = new List<int>(){2,3,4,5,6,7,8,9,10,11};
        else if (x==13)
            existentTiles = new List<int>(){1,2,3,4,5,6,7,8,9,10};
        else if (x==14)
            existentTiles = new List<int>(){1,2,3,4,5,6,7,8,9,10,11,12};
        else if (x==15)
            existentTiles = new List<int>(){2,3,4,5,6,7,8,9};
        else if (x==16)
            existentTiles = new List<int>(){4,5,6,7,8,9,10};
        else if (x==17)
            existentTiles = new List<int>(){3,5,8};

        return existentTiles.Contains(y);
    }
	private TileType GetInitialTileType(int x, int y)
	{
		if (x==0)
		{
			if(y==8) return TileType.GreenZone;
		}
		else if(x==1)
		{
			if(y==5) return TileType.Pirate;
			else if(y==7) return TileType.CoinAsteroid;
			else if(y==8 | y==9) return TileType.GreenZone;
		}
		else if(x==2)
		{
			if(y==7) return TileType.KappaAsteroid;
			else if(y==8 | y==9) return TileType.GreenZone;
		}
		else if(x==3)
		{
			if(y==1 | y==2) return TileType.RedZone;
			else if(y==8) return TileType.MetalAsteroid;
			else if(y==10) return TileType.Pirate;
		}
		else if(x==4)
		{
			if(y==2) return TileType.RedZone;
			else if(y==3) return TileType.MetalCoinAsteroid;
			else if(y==5) return TileType.Pirate;
		}
		else if(x==5)
		{
			if(y==1 | y==2) return TileType.RedZone;
			else if(y==3) return TileType.KappaAsteroid;
			else if(y==7) return TileType.MetalCoinAsteroid;
		}
		else if(x==6)
		{
			if(y==2) return TileType.CoinAsteroid;
			else if(y==6) return TileType.Outpost;
			else if(y==11) return TileType.Pirate;
		}
		else if(x==7)
		{
			if(y==3) return TileType.MetalCoinAsteroid;
			else if(y==8) return TileType.Moon;
		}
		else if(x==8)
		{
			if(y==1) return TileType.Pirate;
			else if(y==10) return TileType.MetalCoinAsteroid;
			else if(y==12) return TileType.CoinAsteroid;
			else if(y==13) return TileType.BlueZone;
		}
		else if(x==9)
		{
			if(y==3) return TileType.Moon;
			else if(y==6) return TileType.Oasis;
			else if(y>=11) return TileType.BlueZone;		
		}
		else if(x==10)
		{
			if(y==11) return TileType.KappaAsteroid;
			else if(y==12) return TileType.MetalAsteroid;
			else if(y==13) return TileType.BlueZone;		
		}
		else if(x==11)
		{
			if(y==0) return TileType.Pirate;
			else if(y==8) return TileType.Outpost;	
		}
		else if(x==12)
		{
			if(y==3) return TileType.KappaAsteroid;
			else if(y==4) return TileType.MetalAsteroid;	
			else if(y==6) return TileType.Outpost;
			else if(y==11) return TileType.Pirate;	
		}
		else if(x==13)
		{
			if(y==1) return TileType.MetalAsteroid;
			else if(y==2) return TileType.YellowZone;	
			else if(y==3) return TileType.CoinAsteroid;	
		}
		else if(x==14)
		{
			if(y==1 | y==2) return TileType.YellowZone;
			else if(y==3) return TileType.CoinAsteroid;	
			else if(y==8) return TileType.MetalCoinAsteroid;
			else if(y==12) return TileType.Pirate;
		}
		else if(x==15)
		{
			if(y==1 | y==2) return TileType.YellowZone;
			else if(y==8) return TileType.PurpleZone;	
			else if(y==9) return TileType.MetalAsteroid;
		}
		else if(x==16)
		{
			if(y==7) return TileType.KappaAsteroid;
			else if(y==8) return TileType.CoinAsteroid;	
			else if(y==9 | y==10) return TileType.PurpleZone;
		}
		else if(x==17)
		{
			if(y==3 | y==5) return TileType.Pirate;
			else if(y==8 | y==9) return TileType.PurpleZone;
		}
		return TileType.Normal;
	}
	private Material GetMaterialByType(TileType type){
		Material tileMaterial = gameController.NormalMaterial;

		if (type == TileType.Normal) tileMaterial = gameController.NormalMaterial;
		else if (type == TileType.Pirate) tileMaterial = gameController.PiratesTerrainMaterial;
		else if (type == TileType.Oasis) tileMaterial = gameController.CPOasisMaterial;
		else if (type == TileType.Moon) tileMaterial = gameController.CPMoonMaterial;
		else if (type == TileType.Outpost) tileMaterial = gameController.CPOutpostMaterial;
		else if (type == TileType.CoinAsteroid) tileMaterial = gameController.AsteroidCoinMaterial;
		else if (type == TileType.MetalAsteroid) tileMaterial = gameController.AsteroidMetalMaterial;
		else if (type == TileType.KappaAsteroid) tileMaterial = gameController.AsteroidKappaMaterial;
		else if (type == TileType.MetalCoinAsteroid) tileMaterial = gameController.AsteroidMetalCoinMaterial;
		else if (type == TileType.GreenZone) tileMaterial = gameController.GreenZoneMaterial;
		else if (type == TileType.GreenPath) tileMaterial = gameController.GreenPathMaterial;
		else if (type == TileType.BlueZone) tileMaterial = gameController.BlueZoneMaterial;
		else if (type == TileType.BluePath) tileMaterial = gameController.BluePathMaterial;
		else if (type == TileType.YellowZone) tileMaterial = gameController.YellowZoneMaterial;
		else if (type == TileType.YellowPath) tileMaterial = gameController.YellowPathMaterial;
		else if (type == TileType.PurpleZone) tileMaterial = gameController.PurpleZoneMaterial;
		else if (type == TileType.PurplePath) tileMaterial = gameController.PurplePathMaterial;
		else if (type == TileType.RedZone) tileMaterial = gameController.RedZoneMaterial;
		else if (type == TileType.RedPath) tileMaterial = gameController.RedPathMaterial;

		return tileMaterial;

	}
	private string GetTagByType(TileType type){
		string tileTag = "Normal";

		if (type == TileType.Normal) tileTag = "Normal";
		else if (type == TileType.Pirate) tileTag = "Pirate";
		else if (type == TileType.Oasis) tileTag = "Oasis";
		else if (type == TileType.Moon) tileTag = "Moon";
		else if (type == TileType.Outpost) tileTag = "Outpost";
		else if (type == TileType.CoinAsteroid) tileTag = "CoinAsteroid";
		else if (type == TileType.MetalAsteroid) tileTag = "MetalAsteroid";
		else if (type == TileType.KappaAsteroid) tileTag = "KappaAsteroid";
		else if (type == TileType.MetalCoinAsteroid) tileTag = "MetalCoinAsteroid";
		else if (type == TileType.GreenZone) tileTag = "GreenZone";
		else if (type == TileType.GreenPath) tileTag = "GreenPath";
		else if (type == TileType.BlueZone) tileTag = "BlueZone";
		else if (type == TileType.BluePath) tileTag = "BluePath";
		else if (type == TileType.YellowZone) tileTag = "YellowZone";
		else if (type == TileType.YellowPath) tileTag = "YellowPath";
		else if (type == TileType.PurpleZone) tileTag = "PurpleZone";
		else if (type == TileType.PurplePath) tileTag = "PurplePath";
		else if (type == TileType.RedZone) tileTag = "RedZone";
		else if (type == TileType.RedPath) tileTag = "RedPath";

		return tileTag;
	}
	private Material GetBaseMaterials(Teams team){
		Material tileMaterial = gameController.NormalMaterial;

		if (team == Teams.Green) tileMaterial = gameController.GreenBaseMaterial;
		else if (team == Teams.Red) tileMaterial = gameController.RedBaseMaterial;
		else if (team == Teams.Purple) tileMaterial = gameController.PurpleBaseMaterial;
		else if (team == Teams.Yellow) tileMaterial = gameController.YellowBaseMaterial;
		else if (team == Teams.Blue) tileMaterial = gameController.BlueBaseMaterial;

		return tileMaterial;
	}
}
