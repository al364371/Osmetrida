using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour {



	public int[,] mapMatrix;
	public Chamber[,] chamberMatrix;
	public RuleTile[] tiles;
	public List<Section> sections;

	public int originX;
	public int originY;
	public int chamberSizeX;
	public int chamberSizeY;

	public GameObject vortexPrefab;
	private Tilemap tileMatrix;

	// Use this for initialization
	void Start () 
	{
		MapManagerEvents.TriggerSection += CreateSection;
		tileMatrix = GetComponent<Tilemap>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
	public void Initialize(ref int[,] theMatrix, ref Chamber[,] theChamberMatrix, Vector2 origin, List<Section> thesections)
	{
		mapMatrix = theMatrix;
		chamberMatrix = theChamberMatrix;
		originX = (int)origin.x;
		originY = (int)origin.y;
		sections = thesections;
	}
	public void CreateSection(int section)
	{
		ClearMap();
		foreach(Transform child in gameObject.transform)
		{
			Destroy(child.gameObject);
		}
		Vector2 mins = new Vector2(mapMatrix.GetLength(0), mapMatrix.GetLength(1));
		Vector2 minIndex = Vector2.zero;
		Vector2 maxIndex = Vector2.zero;
		Vector2 maxs = new Vector2(-mapMatrix.GetLength(0), -mapMatrix.GetLength(1));
		foreach(Vector2 chamber in sections[section].chambers)
		{
			if(chamber.x < mins.x)
			{
				mins.x = chamber.x;
				minIndex.x = sections[section].chambers.IndexOf(chamber);
			}
			if(chamber.x > maxs.x)
			{
				maxs.x = chamber.x;
				maxIndex.x = sections[section].chambers.IndexOf(chamber);
			}
			if(chamber.y < mins.y)
			{
				mins.y = chamber.y;
				minIndex.y = sections[section].chambers.IndexOf(chamber);
			}
			if(chamber.y > maxs.y)
			{
				maxs.y = chamber.y;
				maxIndex.y = sections[section].chambers.IndexOf(chamber);
			}
		}
		PrepareChambers(maxIndex, minIndex, sections[section]);
		foreach(Vector2 chamber in sections[section].chambers )
		{
			SpawnChamber(chamber, ref chamberMatrix );
		}
		if(sections[section].chambers.Count>1)
		{//Esta seccion tiene más de una cámara, esto quiere decir que es una seccion de leftright o de topBottom\\
			if(sections[section].connections[(int)ChamberConnectionType.Top -1] != -1)
			{//Esta seccion conecta con otra en top
				Vector2 chamberPosition = sections[section].chambers[(int) maxIndex.y];
				Chamber theChamber = chamberMatrix[(int) chamberPosition.x, (int) chamberPosition.y];
				Vector3 positionToInstantiate = new Vector3(theChamber.Origin.x + (theChamber.connectionPoints[(int)ChamberConnectionType.Top -1]) +1 , theChamber.Origin.y + (chamberSizeY) +1);
				GameObject toInstantiate = Instantiate(vortexPrefab, positionToInstantiate, Quaternion.identity);
				toInstantiate.transform.parent = gameObject.transform;
				toInstantiate.GetComponent<LevelConnector>().newSection = sections[section].connections[(int)ChamberConnectionType.Top -1];
			}
			if(sections[section].connections[(int)ChamberConnectionType.Bottom -1] != -1)
			{//Esta seccion conecta con otra en bottom
				Vector2 chamberPosition = sections[section].chambers[(int) minIndex.y];
				Chamber theChamber = chamberMatrix[(int) chamberPosition.x, (int) chamberPosition.y];
				Vector3 positionToInstantiate = new Vector3(theChamber.Origin.x + (theChamber.connectionPoints[(int)ChamberConnectionType.Bottom -1] +1), theChamber.Origin.y +1);
				GameObject toInstantiate = Instantiate(vortexPrefab, positionToInstantiate, Quaternion.identity);
				toInstantiate.transform.parent = gameObject.transform;
				toInstantiate.GetComponent<LevelConnector>().newSection = sections[section].connections[(int)ChamberConnectionType.Bottom -1];
			}
			if(sections[section].connections[(int)ChamberConnectionType.Left -1] != -1)
			{//Esta seccion conecta con otra en left
				Vector2 chamberPosition = sections[section].chambers[(int) minIndex.x];
				Chamber theChamber = chamberMatrix[(int) chamberPosition.x, (int) chamberPosition.y];
				Vector3 positionToInstantiate = new Vector3( theChamber.Origin.x +1, theChamber.Origin.y + (theChamber.connectionPoints[(int)ChamberConnectionType.Left -1] ) +1);
				GameObject toInstantiate = Instantiate(vortexPrefab, positionToInstantiate, Quaternion.identity);
				toInstantiate.transform.parent = gameObject.transform;
				toInstantiate.GetComponent<LevelConnector>().newSection = sections[section].connections[(int)ChamberConnectionType.Left -1];
			}
			if(sections[section].connections[(int)ChamberConnectionType.Right -1] != -1)
			{//Esta seccion conecta con otra en left
				Vector2 chamberPosition = sections[section].chambers[(int) maxIndex.x];
				Chamber theChamber = chamberMatrix[(int) chamberPosition.x, (int) chamberPosition.y];
				Vector3 positionToInstantiate = new Vector3( theChamber.Origin.x + (chamberSizeX) +1 , theChamber.Origin.y + (theChamber.connectionPoints[(int)ChamberConnectionType.Right -1] ) +1);
				GameObject toInstantiate = Instantiate(vortexPrefab, positionToInstantiate, Quaternion.identity);
				toInstantiate.transform.parent = gameObject.transform;
				toInstantiate.GetComponent<LevelConnector>().newSection = sections[section].connections[(int)ChamberConnectionType.Right -1];
			}
		}
		else
		{//Esta seccion solo tiene una camara, es decir, es una esquina o una triple o cuadruple\\
			if(sections[section].connections[(int)ChamberConnectionType.Top -1] != -1)
			{//Esta seccion conecta con otra en top
				Vector2 chamberPosition = sections[section].chambers[0];
				Chamber theChamber = chamberMatrix[(int) chamberPosition.x, (int) chamberPosition.y];
				Vector3 positionToInstantiate = new Vector3(theChamber.Origin.x + (theChamber.connectionPoints[(int)ChamberConnectionType.Top -1]) +1 , theChamber.Origin.y + (chamberSizeY) +1);
				GameObject toInstantiate = Instantiate(vortexPrefab, positionToInstantiate, Quaternion.identity);
				toInstantiate.transform.parent = gameObject.transform;
				toInstantiate.GetComponent<LevelConnector>().newSection = sections[section].connections[(int)ChamberConnectionType.Top -1];
			}
			if(sections[section].connections[(int)ChamberConnectionType.Bottom -1] != -1)
			{//Esta seccion conecta con otra en bottom
				Vector2 chamberPosition = sections[section].chambers[0];
				Chamber theChamber = chamberMatrix[(int) chamberPosition.x, (int) chamberPosition.y];
				Vector3 positionToInstantiate = new Vector3(theChamber.Origin.x + (theChamber.connectionPoints[(int)ChamberConnectionType.Bottom -1] +1), theChamber.Origin.y +1);
				GameObject toInstantiate = Instantiate(vortexPrefab, positionToInstantiate, Quaternion.identity);
				toInstantiate.transform.parent = gameObject.transform;
				toInstantiate.GetComponent<LevelConnector>().newSection = sections[section].connections[(int)ChamberConnectionType.Bottom -1];
			}
			if(sections[section].connections[(int)ChamberConnectionType.Left -1] != -1)
			{//Esta seccion conecta con otra en left
				Vector2 chamberPosition = sections[section].chambers[0];
				Chamber theChamber = chamberMatrix[(int) chamberPosition.x, (int) chamberPosition.y];
				Vector3 positionToInstantiate = new Vector3( theChamber.Origin.x +1, theChamber.Origin.y + (theChamber.connectionPoints[(int)ChamberConnectionType.Left -1] ) +1);
				GameObject toInstantiate = Instantiate(vortexPrefab, positionToInstantiate, Quaternion.identity);
				toInstantiate.transform.parent = gameObject.transform;
				toInstantiate.GetComponent<LevelConnector>().newSection = sections[section].connections[(int)ChamberConnectionType.Left -1];
			}
			if(sections[section].connections[(int)ChamberConnectionType.Right -1] != -1)
			{//Esta seccion conecta con otra en left
				Vector2 chamberPosition = sections[section].chambers[0];
				Chamber theChamber = chamberMatrix[(int) chamberPosition.x, (int) chamberPosition.y];
				Vector3 positionToInstantiate = new Vector3( theChamber.Origin.x + (chamberSizeX) +1 , theChamber.Origin.y + (theChamber.connectionPoints[(int)ChamberConnectionType.Right -1] ) +1);
				GameObject toInstantiate = Instantiate(vortexPrefab, positionToInstantiate, Quaternion.identity);
				toInstantiate.transform.parent = gameObject.transform;
				toInstantiate.GetComponent<LevelConnector>().newSection = sections[section].connections[(int)ChamberConnectionType.Right -1];
			}
		}
	}
	public void SpawnChamber( Vector2 toSpawn, ref Chamber[,] theChamberMatrix)
	{
		tileMatrix = GetComponent<Tilemap>();
		
		Chamber toBeConnected = theChamberMatrix[(int) toSpawn.x, (int) toSpawn.y];
		Debug.Log("Got chamber for paint, origin is: " + toBeConnected.Origin);
		Debug.Log("The chamber is " + toSpawn);


		for(int y=0; y<chamberSizeY; y++)
        {
            for(int x= 0; x<chamberSizeX; x++)
            {
                if(toBeConnected.mapMatrix[(y * chamberSizeX) + x] == 1)
                { //Tile is of type 1 (Ground)
					Vector3Int toPaint = new Vector3Int((int) toBeConnected.Origin.x + x, (int) toBeConnected.Origin.y + chamberSizeY - y,0);
                    tileMatrix.SetTile(toPaint,tiles[0]);
                }
				else
				{
					Vector3Int toPaint = new Vector3Int((int) toBeConnected.Origin.x + x, (int) toBeConnected.Origin.y + chamberSizeY - y,0);
                    tileMatrix.SetTile(toPaint,null);
				}
            }
        }
	}

	public void PrepareChambers(Vector2 maxes, Vector2 mins, Section section)
	{
		Debug.Log("Maxes " + maxes);
		Debug.Log("Mins " + mins);
		Vector2 minXChamber = section.chambers[(int) mins.x];
		Vector2 maxXChamber = section.chambers[(int) maxes.x];
		Vector2 minYChamber = section.chambers[(int) mins.y];
		Vector2 maxYChamber = section.chambers[(int) maxes.y];
		for(int j= (int) (chamberMatrix[(int) minYChamber.x, (int) minYChamber.y].Origin.y - chamberSizeY); j< chamberMatrix[(int) maxYChamber.x, (int) maxYChamber.y].Origin.y + (2 * chamberSizeY); j++ )
		{
			for (int i= (int) (chamberMatrix[(int) minXChamber.x, (int) minXChamber.y].Origin.x - chamberSizeX); i < chamberMatrix[(int) maxXChamber.x, (int) maxXChamber.y].Origin.x + (2 *chamberSizeX) ; i++)
			{
				tileMatrix.SetTile(new Vector3Int(i,j,0), tiles[0]);
			}
		}
	}

	public void ClearMap()
	{
		tileMatrix = GetComponent<Tilemap>();
		tileMatrix.ClearAllTiles();
	}
}

public static class MapManagerEvents
{
	public delegate void SectionHandler(int section);
	public static event SectionHandler TriggerSection;
	public static void DrawSection(int section)
	{
		TriggerSection(section);
	}
}
