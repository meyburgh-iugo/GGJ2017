using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner2 : MonoBehaviour {

  class Field {
    public List<GameObject> obstacles;
    public List<GameObject> fishes;
  }

  public GameObject[] primitives;
  public Transform player;
  public GameObject[] prefab_fishes;
  public int fieldSize = 20;
  public int baseObjectCount = 10;
  public int additionalObjectsPerLevel = 1;
  public int baseFishCount = 1;
  public int additionalFishPerLevel = 1;

  private Field[,] fields;
  private Rect currentField;
  private int currentX;
  private int currentY;

  private float maxSpeed = 0.1f;
  private float maxSpin = 0.5f;
  private float minScale = 1;
  private float maxScale = 5;

  // Use this for initialization
  void Start ()
  {
    currentField = new Rect (-fieldSize / 2, -fieldSize / 2, fieldSize, fieldSize);
    currentX = 0;
    currentY = 0;

    fields = new Field[3,3];

    for (var i = 0; i < 3; i++) {
      for (var j = 0; j < 3; j++) {
        fields[i,j] = PopulateField ((i-1), (j-1));
      }
    }
  }
	
  private Field PopulateField(int x, int y)
  {
    var field = new Field();
    field.obstacles = new List<GameObject> ();
    field.fishes = new List<GameObject> ();

    var rect = new Rect ((fieldSize * x) - (fieldSize / 2), (fieldSize * y) - (fieldSize / 2), fieldSize, fieldSize);

    if (y > -1)
      return field;

    var r = new MyRandom ((x)+(100000*y));

    for (int i = 0; i < (-y + additionalObjectsPerLevel) + baseObjectCount; i++)
    {
      SpawnARandomObstacle(rect, field, r);
    }

    for (int i = 0; i < (-y + additionalFishPerLevel) + baseFishCount; i++)
    {
      SpawnARandomFish(rect, field, r);
    }

    return field;
  }

  private void DeallocateField(Field field)
  {
    foreach(GameObject go in field.obstacles)
    {
      Destroy(go);
    }

    foreach(GameObject go in field.fishes)
    {
      Destroy(go);
    }
  }

  void SpawnARandomObstacle(Rect area, Field field, MyRandom r)
  {
    var go = Instantiate(
      primitives[r.Next(0, primitives.Length)],
      new Vector2(
        r.NextFloat(area.xMin, area.xMax),
        r.NextFloat(area.yMin, area.yMax)
      ),
      Quaternion.identity
    );

    float slc = r.NextFloat(minScale, maxScale);
    go.transform.localScale = new Vector3(slc, slc, 1);
    go.transform.localEulerAngles = new Vector3(0, 0, r.NextFloat(0, 360));
    go.transform.parent = transform;

    var body = go.GetComponent<Rigidbody2D> ();
    body.velocity = new Vector2 (
      r.NextFloat(-maxSpeed, maxSpeed),
      r.NextFloat(-maxSpeed, maxSpeed)
    );
    body.angularVelocity = (r.NextFloat(-maxSpin, maxSpin));

    body.drag = 0;
    body.angularDrag = 0;
    field.obstacles.Add(go);
  }

  void SpawnARandomFish(Rect area, Field field, MyRandom r)
  {
    var go = Instantiate(
      prefab_fishes[r.Next(0, prefab_fishes.Length)],
      new Vector2(
        r.NextFloat(area.xMin, area.xMax),
        r.NextFloat(area.yMin, area.yMax)
      ),
      Quaternion.identity
    );

    float slc = r.NextFloat(0.5f, 1.5f);
    go.transform.localScale = new Vector3(slc, slc, 1);
    go.transform.parent = transform;

    var body = go.GetComponent<Rigidbody2D>();
    body.AddForce(new Vector2(r.NextFloat(-0.01f, 0.01f), r.NextFloat(-0.01f, 0.01f)), ForceMode2D.Impulse);

    field.fishes.Add(go);
  }

  void Update()
  {
    var newField = new Rect (currentField);
    var moved = false;

    if (player.position.x > currentField.xMax) {
      Debug.Log ("Moving right");

      // Deallocate the left fields
      for (int i = 0; i < 3; i++) {
        if (fields [0, i] != null)
          DeallocateField (fields [0, i]);
      }

      // Shift everything left
      fields [0, 0] = fields [1, 0];
      fields [1, 0] = fields [2, 0];
      fields [2, 0] = null;
      fields [0, 1] = fields [1, 1];
      fields [1, 1] = fields [2, 1];
      fields [2, 1] = null;
      fields [0, 2] = fields [1, 2];
      fields [1, 2] = fields [2, 2];
      fields [2, 2] = null;

      newField.x += fieldSize;
      currentX++;
      moved = true;
    }

    if (player.position.x < currentField.xMin) {
      Debug.Log ("Moving left");

      // Deallocate the right fields
      for (int i = 0; i < 3; i++) {
        if (fields [2, i] != null)
          DeallocateField (fields [2, i]);
      }

      // Shift everything right
      fields [2, 0] = fields [1, 0];
      fields [1, 0] = fields [0, 0];
      fields [0, 0] = null;
      fields [2, 1] = fields [1, 1];
      fields [1, 1] = fields [0, 1];
      fields [0, 1] = null;
      fields [2, 2] = fields [1, 2];
      fields [1, 2] = fields [0, 2];
      fields [0, 2] = null;

      newField.x -= fieldSize;
      currentX--;
      moved = true;
    }

    if (player.position.y > currentField.yMax) {
      Debug.Log ("Moving up");

      // Deallocate the bottom fields
      for (int i = 0; i < 3; i++) {
        if (fields [i, 0] != null)
          DeallocateField (fields [i, 0]);
      }

      // Shift everything down
      fields [0, 0] = fields [0, 1];
      fields [0, 1] = fields [0, 2];
      fields [0, 2] = null;
      fields [1, 0] = fields [1, 1];
      fields [1, 1] = fields [1, 2];
      fields [1, 2] = null;
      fields [2, 0] = fields [2, 1];
      fields [2, 1] = fields [2, 2];
      fields [2, 2] = null;

      newField.y += fieldSize;
      currentY++;
      moved = true;
    }

    if (player.position.y < currentField.yMin) {
      Debug.Log ("Moving down");

      // Deallocate the top fields
      for (int i = 0; i < 3; i++) {
        if (fields [i, 2] != null)
          DeallocateField (fields [i, 2]);
      }

      // Shift everything up
      fields [0, 2] = fields [0, 1];
      fields [0, 1] = fields [0, 0];
      fields [0, 0] = null;
      fields [1, 2] = fields [1, 1];
      fields [1, 1] = fields [1, 0];
      fields [1, 0] = null;
      fields [2, 2] = fields [2, 1];
      fields [2, 1] = fields [2, 0];
      fields [2, 0] = null;

      newField.y -= fieldSize;
      currentY--;
      moved = true;
    }

    currentField = newField;

    // Populate all null fields
    if (moved) {
      for (int i = 0; i < 3; i++) {
        for (int j = 0; j < 3; j++) {
          if (fields [i, j] == null) {
            fields [i, j] = PopulateField (i - 1 + currentX, j - 1 + currentY);
          }
        }
      }

      Debug.Log ("Now at " + currentX + "," + currentY);
    }
  }
}
