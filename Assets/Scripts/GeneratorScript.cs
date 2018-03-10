using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GeneratorScript : MonoBehaviour {

	#region vars
	//cache view width
	private float screenWidthInPoints;

	//for rooms
	//------------------------------------------------------------------------
	//array of available room prefabs
	public GameObject[] availableRooms;
	//list of rooms generated in scene
	public List<GameObject> currentRooms;

	//for objects
	//------------------------------------------------------------------------
	//array of available object prefabs
	public GameObject[] availableObjects;
	//list of objects generated in scene
	public List<GameObject> objects;
	//objects variation params
	public float objectsMinDistance = 5.0f;
	public float objectsMaxDistance = 10.0f;
	public float objectsMinY = -1.4f;
	public float objectsMaxY = 1.4f;
	public float objectsMinRotation = -45.0f;
	public float objectsMaxRotation = 45.0f;
	#endregion
	
	#region onStart
	void Start () {
		//get view height from camera
		float height = 2.0f * Camera.main.orthographicSize;
		//calculate view width from height
		screenWidthInPoints = height * Camera.main.aspect;
	}
	#endregion
	
	#region onUpdate
	void Update () {

	}
	#endregion

	#region onFixedUpdate
	void FixedUpdate () {
		//check if room is required
		GenerateRoomIfRequired();
		//check if object is required
		GenerateObjectsIfRequired();
	}
	#endregion

	#region methods
	//adding new room to the scene
	//------------------------------------------------------------------------
	void AddRoom(float furthestRoomEndX) {
		//get random index from available rooms array as access key
		int randomRoomIndex = Random.Range(0, availableRooms.Length);
		//init new room prefab instance using random index key
		GameObject room = (GameObject)Instantiate(availableRooms[randomRoomIndex]);
		//get width from new room prefab instance (4.8 x 3 = 14.4)
		float roomWidth = room.transform.FindChild("floor").localScale.x;
		//calculate target insert position by
		//add half room's size to where the level ends
		//which is the rightmost point of the available scene
		float roomCenter = furthestRoomEndX + roomWidth / 2;
		//set new room's position
		room.transform.position = new Vector3(roomCenter, 0, 0);
		//add newly added room to the list for tracking
		currentRooms.Add(room);
	}

	//check if new room is required in the scene
	//------------------------------------------------------------------------
	void GenerateRoomIfRequired() {
		//create rooms list for removal
		List<GameObject> roomsToRemove = new List<GameObject>();
		//add room flag
		bool addRooms = true;

		//get self (player)'s x position
		float playerX = transform.position.x;
		//calculate x position to remove out-of-view room prefabs
		float removeRoomX = playerX - screenWidthInPoints;
		//calculate x position to add a new room prefab
		float addRoomX = playerX + screenWidthInPoints;

		//init furthestRoomEndX with 0
		float furthestRoomEndX = 0;

		//loop through current list of added rooms in scene
		foreach(var room in currentRooms) {
			//get this room's width and
			//calculate its left and right edges' positions
			float roomWidth = room.transform.FindChild("floor").localScale.x;
			float roomStartX = room.transform.position.x - (roomWidth * 0.5f);
			float roomEndX = roomStartX + roomWidth;

			//check if needed to add new room when
			//if this room's left edge positon is to the right of addRoom x position
			if (roomStartX > addRoomX)
				addRooms = false;

			//add this to remove when
			//if this room's right edge position is to the left of removeRoom x position
			if (roomEndX < removeRoomX)
				roomsToRemove.Add(room);
			
			//set end-of-scene x position to the roomEndX of the rightmost room instance
			furthestRoomEndX = Mathf.Max(furthestRoomEndX, roomEndX);
		}
		
		//loop through roomsToRemove
		foreach(var room in roomsToRemove) {
			//remove this room from scene room list
			currentRooms.Remove(room);
			//and remove(destroy) from the running scene
			Destroy(room);
		}

		//if adding new room is required
		//add new room with the x position of the rightmost room in scene
		if (addRooms) {
			AddRoom(furthestRoomEndX);
		}
	}

	//add objects
	//------------------------------------------------------------------------
	void AddObject(float lastObjectX) {
		//get an object to add to scene
		//via a random key from array
		int randomIndex = Random.Range(0, availableObjects.Length);
		GameObject obj = (GameObject)Instantiate(availableObjects[randomIndex]);
		//calculate a ranged x-position based on last object's x-position
		float objectPositionX = lastObjectX + Random.Range(objectsMinDistance, objectsMaxDistance);
		//calculate a ranged y-position
		float randomY = Random.Range(objectsMinY, objectsMaxY);
		//calculate random rotation within range
		float rotation = Random.Range(objectsMinRotation, objectsMaxRotation);
		//apply calculated position to new object
		obj.transform.position = new Vector3(objectPositionX, randomY, 0);
		//apply calculated rotation to new object
		//Quaternion.Euler takes a vector 3 input: (0,0,45.0f)
		//and returns a rotation for object transform
		obj.transform.rotation = Quaternion.Euler(Vector3.forward * rotation);
		//add object to objects list
		objects.Add(obj);
	}

	//check if new object is required in the scene
	//------------------------------------------------------------------------
	void GenerateObjectsIfRequired() {
		//create objs list for removal
		List<GameObject> objectsToRemove = new List<GameObject>();

		//get self's position x
		float playerX = transform.position.x;
		//calculate x position to remove out-of-view obj prefabs
		float removeObjectsX = playerX - screenWidthInPoints;
		//calculate x position to add a new obj prefab
		float addObjectX = playerX + screenWidthInPoints;

		//init farthestObjectX with 0
		float farthestObjectX = 0;
		
		//loop through object prefabs
		foreach (var obj in objects) {
			//get largest x-position as last object's x-position
			float objX = obj.transform.position.x;
			farthestObjectX = Mathf.Max(farthestObjectX, objX);
			
			//add this obj to clean up list
			//if this obj's x-pos smaller than remove limit
			//this will clean up any out-of-view object from the scene list
			if (objX < removeObjectsX) {
				objectsToRemove.Add(obj);
			}
		}
		
		//loop through object in removal list
		foreach (var obj in objectsToRemove) {
			//remove this obj from removal list
			objects.Remove(obj);
			//and remove obj from scene
			Destroy(obj);
		}
		
		//if x-position of last object in scene (farthestObjectX)
		//is smaller than the add add limit
		//add a new object to scene based on farthestObjectX
		if (farthestObjectX < addObjectX) {
			AddObject(farthestObjectX);
		}
	}
	#endregion
}
