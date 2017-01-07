using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class ShipController : MonoBehaviour {
  Rigidbody2D rb;
  readonly float force_multiplier = 20;

  float screenWidth;
  float screenHeight;
  Transform[] ghosts = new Transform[8];

  void CreateGhostShips()
  {
    for (int i = 0; i < 8; i++)
    {
      ghosts[i] = Instantiate(transform, Vector3.zero, Quaternion.identity) as Transform;
      ghosts[i].transform.parent = transform;
      DestroyImmediate(ghosts[i].GetComponent<ShipController>());
    }

    PositionGhostShips();
  }

  void PositionGhostShips()
  {
    // All ghost positions will be relative to the ships (this) transform,
    // so let's star with that.
    var ghostPosition = transform.position;

    // We're positioning the ghosts clockwise behind the edges of the screen.
    // Let's start with the far right.
    ghostPosition.x = transform.position.x + screenWidth;
    ghostPosition.y = transform.position.y;
    ghosts[0].position = ghostPosition;

    // Bottom-right
    ghostPosition.x = transform.position.x + screenWidth;
    ghostPosition.y = transform.position.y - screenHeight;
    ghosts[1].position = ghostPosition;

    // Bottom
    ghostPosition.x = transform.position.x;
    ghostPosition.y = transform.position.y - screenHeight;
    ghosts[2].position = ghostPosition;

    // Bottom-left
    ghostPosition.x = transform.position.x - screenWidth;
    ghostPosition.y = transform.position.y - screenHeight;
    ghosts[3].position = ghostPosition;

    // Left
    ghostPosition.x = transform.position.x - screenWidth;
    ghostPosition.y = transform.position.y;
    ghosts[4].position = ghostPosition;

    // Top-left
    ghostPosition.x = transform.position.x - screenWidth;
    ghostPosition.y = transform.position.y + screenHeight;
    ghosts[5].position = ghostPosition;

    // Top
    ghostPosition.x = transform.position.x;
    ghostPosition.y = transform.position.y + screenHeight;
    ghosts[6].position = ghostPosition;

    // Top-right
    ghostPosition.x = transform.position.x + screenWidth;
    ghostPosition.y = transform.position.y + screenHeight;
    ghosts[7].position = ghostPosition;

    // All ghost ships should have the same rotation as the main ship
    for (int i = 0; i < 8; i++)
    {
      ghosts[i].rotation = transform.rotation;
    }
  }

  void SwapShips()
  {
    foreach (var ghost in ghosts)
    {
      // if the ghost ship is inside the big box
      if (ghost.position.x < screenWidth && ghost.position.x > -screenWidth &&
          ghost.position.y < screenHeight && ghost.position.y > -screenHeight)
      {
        // then set our middle ship, to where the ghost is
        transform.position = ghost.position;

        break;
      }
    }

    PositionGhostShips();
  }

  // Use this for initialization
  void Start () {
    rb = GetComponent<Rigidbody2D>();

    var cam = Camera.main;

    var screenBottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, transform.position.z));
    var screenTopRight = cam.ViewportToWorldPoint(new Vector3(1, 1, transform.position.z));

    screenWidth = screenTopRight.x - screenBottomLeft.x;
    screenHeight = screenTopRight.y - screenBottomLeft.y;

    Debug.Log(string.Format("w:{0} h:{1}", screenWidth, screenHeight));

    CreateGhostShips();
  }
	
  void FixedUpdate()
  {
    rb.AddForce(new Vector2(CrossPlatformInputManager.GetAxis("Horizontal"), CrossPlatformInputManager.GetAxis("Vertical")).normalized * force_multiplier);
    
    //SwapShips();
  }

	// Update is called once per frame
	void Update () {
		
	}

  void OnBecameInvisible()
  {
    SwapShips();
  }
}
