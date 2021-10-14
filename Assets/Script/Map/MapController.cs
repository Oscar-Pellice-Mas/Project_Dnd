using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapController : MonoBehaviour
{
    public static MapController Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private Vector3 maxPosition;
    private Vector3 minPosition;

    private Dictionary<string, Entity> entityMap;

    private GameObject previousTile;
    private Material basicTile;
    private Material mouseTile;

    private Vector3 posMin = new Vector3(0, 0, 0);
    private Vector3 posMax = new Vector3(10, 5, 10);

    [SerializeField]
    private GameObject tilePrefab;

    // ------------- Functions ------------------

    private void Start()
    {
        basicTile = Resources.Load<Material>("Materials/Basic_Tile");
        mouseTile = Resources.Load<Material>("Materials/Mouse_Tile");
        InstantiateMap();
        GenerateMap(posMin, posMax);
    }

    private void InstantiateMap()
    {
        for (int i = (int)posMin.x; i < (int)posMax.x; i++)
        {
            for (int j = (int)posMin.z; j < (int)posMax.z; j++)
            {
                GameObject tile = Instantiate(tilePrefab, new Vector3(i, 0, j), Quaternion.identity);
                tile.name = tile.transform.position.ToString();
                tile.transform.parent = MapController.Instance.transform;
            }
        }
    }

    public bool GenerateMap(Vector3 min, Vector3 max)
    {
        if (min.x > max.x || min.y > max.y || min.z > max.z)
        {
            return false;
        }
        maxPosition = max;
        minPosition = min;
        entityMap = new Dictionary<string, Entity>();
        return true;
    }

    public bool IsOcupied(Vector3 position)
    {
        return entityMap.ContainsKey(position.ToString());
    }

    public bool SetPosition(Entity entity)
    {
        if (IsOcupied(entity.GetPosition())) return false;
        entityMap.Add(entity.GetPosition().ToString(), entity);
        return true;
    }

    public bool RemovePosition(Vector3 position)
    {
        if (IsOcupied(position)) return false;
        entityMap.Remove(position.ToString());
        return true;
    }

    public bool MovePosition(Entity entity, Vector3 position)
    {
        if (position.x > maxPosition.x || position.y > maxPosition.y || position.z > maxPosition.z ||
            position.x < minPosition.x || position.y < minPosition.y || position.z < minPosition.z)
        {
            return false;
        }
        entityMap.Remove(entity.GetPosition().ToString());
        if (IsOcupied(position)) return false;
        entityMap.Add(position.ToString(), entity);
        return true;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (previousTile == hit.transform.gameObject) return;
            if (previousTile != null) previousTile.GetComponent<MeshRenderer>().material = basicTile; 
            previousTile = hit.transform.gameObject;
            if (hit.transform.tag == "Tile")
            {
                MeshRenderer meshRenderer = hit.transform.GetComponent<MeshRenderer>();
                meshRenderer.material = mouseTile;
            }
        }
    }
}