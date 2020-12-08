using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
#pragma warning disable 0649
    public Room startShelfPrefab, endShelfPrefab;
    public List<Room> shelfPrefabs = new List<Room>();
    public Vector2 iterationRange = new Vector2(3, 10);

    List<Doorway> availableDoorways = new List<Doorway>();

    StartShelf startShelf;
    EndShelf endShelf;
    List<Room> placedShelves = new List<Room>();

    LayerMask roomLayerMask;

    void Start()
    {
        roomLayerMask = LayerMask.GetMask("Room");
        StartCoroutine("GenerateLevel");
    }

    IEnumerator GenerateLevel()
    {
        WaitForSeconds startup = new WaitForSeconds(1);
        WaitForFixedUpdate interval = new WaitForFixedUpdate();

        yield return startup;

        //Place start shelf
        PlaceStartShelf();
        yield return interval;

        //Random iterations
        int iterations = Random.Range((int)iterationRange.x, (int)iterationRange.y);

        for(int i = 0; i < iterations; i++)
        {
            //Place random shelf from list
            PlaceShelves();
            yield return interval;
        }

        //Place end shelf
        PlaceEndShelf();
        yield return interval;

        //Level generation finished
        Debug.Log("Level generation finished");

        yield return new WaitForSeconds(3);
        ResetLevelGenerator();
        StopCoroutine("GenerateLevel");

        //delete all rooms
        if(startShelf)
        {
            Destroy(startShelf.gameObject);
        }

        if(endShelf)
        {
            Destroy(endShelf.gameObject);
        }

        foreach(Room shelf in placedShelves)
        {
            Destroy(shelf.gameObject);
        }

        //clear lists
        placedShelves.Clear();
        availableDoorways.Clear();

        //reset coroutine
        StartCoroutine("GenerateLevel");
    }

    void PlaceStartShelf()
    {
        //Instantiate Shelf
        startShelf = Instantiate(startShelfPrefab) as StartShelf;
        startShelf.transform.parent = this.transform;

        //Get shelf ends from current room and add to list of availabel
        AddDoorwaysToList(startShelf, ref availableDoorways);

        //position shelf
        startShelf.transform.position = Vector3.zero;
        startShelf.transform.rotation = Quaternion.identity;
    }

    void AddDoorwaysToList(Room shelf, ref List<Doorway> list)
    {
        foreach(Doorway doorway in shelf.doorways)
        {
            int r = Random.Range(0, list.Count);
            list.Insert(r, doorway);
        }
    }

    void PlaceShelves()
    {
        //Instantiate shelf
        Room currentShelf = Instantiate(shelfPrefabs[Random.Range(0, shelfPrefabs.Count)]) as Room;
        currentShelf.transform.parent = this.transform;

        //create doorway list to loop over
        List<Doorway> allAvailableDoorways = new List<Doorway>(availableDoorways);
        List<Doorway> currentDoorways = new List<Doorway>();
        AddDoorwaysToList(currentShelf, ref currentDoorways);

        //Get doorways from current room and add them randomly to the list of available doorways
        AddDoorwaysToList(currentShelf, ref availableDoorways);

        bool shelfPlaced = false;

        //try all available doorways
        foreach(Doorway availableDoorway in availableDoorways)
        {
            //Try all available doorways in current room
            foreach(Doorway currentDoorway in currentDoorways)
            {
                //position room
                PositionShelfAtDoorway(ref currentShelf, currentDoorway, availableDoorway);

                //check room overlaps
                if(CheckShelfOverlap(currentShelf))
                {
                    continue;
                }

                shelfPlaced = true;

                //Add shelf to list
                placedShelves.Add(currentShelf);

                //remove occupied doorways
                currentDoorway.gameObject.SetActive(false);
                availableDoorways.Remove(currentDoorway);

                availableDoorway.gameObject.SetActive(false);
                availableDoorways.Remove(availableDoorway);

                //exit loop if shelf has been placed
                break;
            }

            //exit loop if shelf has been placed
            if(shelfPlaced)
            {
                break;
            }
        }

        //shelf could'nt be placed
        if(!shelfPlaced)
        {
            Destroy(currentShelf.gameObject);
            ResetLevelGenerator();
        }
    }

    void PositionShelfAtDoorway(ref Room shelf, Doorway shelfDoorway, Doorway targetDoorway)
    {
        //reset shelf position and rotation
        shelf.transform.position = Vector3.zero;
        shelf.transform.rotation = Quaternion.identity;

        //rotate room to match previous doorway orientation
        Vector3 targetDoorwayEuler = targetDoorway.transform.eulerAngles;
        Vector3 shelfDoorwayEuler = shelfDoorway.transform.eulerAngles;
        float deltaAngle = Mathf.DeltaAngle(shelfDoorwayEuler.y, targetDoorwayEuler.y);
        Quaternion currentShelfTargetRotation = Quaternion.AngleAxis(deltaAngle, Vector3.up);
        shelf.transform.rotation = currentShelfTargetRotation * Quaternion.Euler(0, 100f, 0);

        //position shelf
        Vector3 roomPositionOffset = shelfDoorway.transform.position - shelf.transform.position;
        shelf.transform.position = targetDoorway.transform.position - roomPositionOffset;
    }

    bool CheckShelfOverlap(Room shelf)
    {
        Bounds bounds = shelf.RoomBounds;
        bounds.Expand(-0.1f);

        Collider[] colliders = Physics.OverlapBox(bounds.center, bounds.size / 2, shelf.transform.rotation, roomLayerMask);
        if(colliders.Length > 0)
        {
            //ignore collisions with current room
            foreach(Collider c in colliders)
            {
                if(c.transform.parent.gameObject.Equals(shelf.gameObject))
                {
                    continue;
                }
                else
                {
                    Debug.LogError("Overlap detected");
                    return true;
                }
            }
        }

        return false;
    }

    void PlaceEndShelf()
    {

    }

    void ResetLevelGenerator()
    {

    }
}
