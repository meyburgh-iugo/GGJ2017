using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {
  private GameObject player;
  private Rigidbody2D playerBody;

  private float abyssStart = 60.0f;

  private float target_z = -10.0f;

  private float smoothSpeed = 0.2f;
  private float initOrtho = 0.5f;
  private float maxOrtho = 5.0f;

  private Camera cam;
	// Use this for initialization
	void Start () {
    player = GameObject.FindGameObjectWithTag("Player");
    playerBody = player.GetComponent<Rigidbody2D>();
    
    cam = GetComponent<Camera>();
    cam.transform.position = playerBody.transform.position;
    cam.orthographicSize = initOrtho;

    WaterDistortion effect = cam.GetComponent<WaterDistortion>();
    GameObject level = GameObject.Find("Level");
    ObstacleSpawner2 os2 = level.GetComponent<ObstacleSpawner2>();

    switch(ServiceLocator.Difficulty)
    {
      case EDifficulty.Hard:
        {
        os2.additionalObjectsPerLevel *= 2;
          effect._Distortion *= 2.0f;
          effect._Waves += 2;
          abyssStart *= 0.5f;
          break;
        }
      case EDifficulty.Nightmare:
        {
        os2.additionalObjectsPerLevel *= 4;
          effect._Distortion *= 4.0f;
          effect._Waves += 5;
          abyssStart *= 0.25f;
          break;
        }
    }

	}
	
	// Update is called once per frame
	void LateUpdate () {
    Vector3 pos = new Vector3(Mathf.Lerp(transform.position.x, playerBody.position.x, 0.5f * Time.deltaTime), Mathf.Lerp(transform.position.y, playerBody.position.y, Time.deltaTime), target_z);
    transform.position = pos;

    bool playerIsDead = player.GetComponent<PlayerDie>().IsDead;

    float targetOrtho = playerIsDead ? initOrtho : 8 * (1 / (1 + Mathf.Exp(-playerBody.velocity.magnitude + 1.0f)));

    targetOrtho = Mathf.Clamp(targetOrtho, initOrtho, maxOrtho);
    cam.orthographicSize = Mathf.MoveTowards (Camera.main.orthographicSize, targetOrtho, (playerIsDead ? 2.25f : 1.0f) * smoothSpeed * Time.deltaTime);

    float time = -playerBody.position.y / abyssStart; 
    cam.backgroundColor = new Color(0, Mathf.Lerp(0.44f, 0.0f, time), Mathf.Lerp(0.50f, 0.0f, time));
  }

  public float GetAbyssStart()
  {
    return abyssStart;
  }
}
