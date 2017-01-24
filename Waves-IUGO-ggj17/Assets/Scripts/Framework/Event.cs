using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace SNS
{
  public class Event
  {
    public struct EventArgs
    {
      public object[] args;

      public EventArgs(object[] objs)
      {
        args = objs;
      }

      public EventArgs(object obj1)
      {
        args = new object[1];
        args[0] = obj1;
      }

      public EventArgs(object obj1, object obj2)
      {
        args = new object[2];
        args[0] = obj1;
        args[1] = obj2;
      }

      public EventArgs(object obj1, object obj2, object obj3)
      {
        args = new object[3];
        args[0] = obj1;
        args[1] = obj2;
        args[2] = obj3;
      }

      public EventArgs(object obj1, object obj2, object obj3, object obj4)
      {
        args = new object[4];
        args[0] = obj1;
        args[1] = obj2;
        args[2] = obj3;
        args[3] = obj4;
      }
    }

    public static readonly EventArgs EventArgs0 = new EventArgs();

    protected Action<GameObject, EventArgs> observers;

    public Event() { }

    public void AddObserver(Action<GameObject, EventArgs> action)
    {
      observers += action;
    }

    public void RemoveObserver(Action<GameObject, EventArgs> action)
    {
      observers -= action;
    }

    public void Invoke(GameObject invoker, EventArgs args)
    {
      if (observers != null)
        observers(invoker, args);
    }
  }
}
