using UnityEngine;
using System.Collections;

public enum CommandCode : uint
{
    W = 1,
    S = 1 << 1,   // 2
    A = 1 << 2,   // 4
    D = 1 << 3,   // 8
    RETURN = 1 << 4,   // 16
    SPACE = 1 << 5,    // 32
    MOUSEMOVE = 1 << 6, // 64
    MOUSELEFTPRESS = 1 << 7, // 128
    MOUSERIGHTPRESS = 1 << 8, // 256
    SHIFT = 1 << 9, // 512
}

public class InputHandler 
{
  Vector2 _mousePos;

  public InputHandler()
  {
    _mousePos = Input.mousePosition;
  }

  public uint Evaluate()
  {
    uint frameCmd = 0;
    if (Input.GetKey(KeyCode.W))
      frameCmd |= (uint)CommandCode.W;

    if (Input.GetKey(KeyCode.S))
      frameCmd |= (uint)CommandCode.S;

    if (Input.GetKey(KeyCode.A))
      frameCmd |= (uint)CommandCode.A;

    if (Input.GetKey(KeyCode.D))
      frameCmd |= (uint)CommandCode.D;

    if (Input.GetKey(KeyCode.Return))
      frameCmd |= (uint)CommandCode.RETURN;

    if (Input.GetKey(KeyCode.Space))
      frameCmd |= (uint)CommandCode.SPACE;

    if (GetMouseDelta().magnitude > 0)
      frameCmd |= (uint)CommandCode.MOUSEMOVE;

    if (Input.GetMouseButtonDown(0))
      frameCmd |= (uint)CommandCode.MOUSELEFTPRESS;

    if (Input.GetMouseButtonDown(1))
      frameCmd |= (uint)CommandCode.MOUSERIGHTPRESS;

    if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
      frameCmd |= (uint)CommandCode.SHIFT;

    return frameCmd;
  }

  public Vector2 GetMouseDelta()
  {
    Vector2 currMousePos = Input.mousePosition;
    Vector2 delta = new Vector2(currMousePos.x - _mousePos.x, currMousePos.y - _mousePos.y);
    _mousePos = currMousePos;
    return delta;
  }
}
