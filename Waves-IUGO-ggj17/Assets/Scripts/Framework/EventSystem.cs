using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

using System.Runtime.CompilerServices;
[assembly: InternalsVisibleToAttribute("ServiceLocator")]

namespace SNS
{
  public class EventSystem
  {
    private Dictionary<string, Event> events;

    internal EventSystem()
    {
      if (events == null)
        events = new Dictionary<string, Event>();
    }

    public void StartListening(string eventName, Action<GameObject, Event.EventArgs> action)
    {
      Event uevent = null;
      if (events.TryGetValue(eventName, out uevent))
      {
        uevent.AddObserver(action);
      }
      else
      {
        uevent = new Event();
        uevent.AddObserver(action);
        events.Add(eventName, uevent);
      }
    }

    public void StopListening(string eventName, Action<GameObject, Event.EventArgs> action)
    {
      Event uevent = null;
      if (events.TryGetValue(eventName, out uevent))
        uevent.RemoveObserver(action);
    }

    public void TriggerEvent(string eventName, GameObject obj, Event.EventArgs args)
    {
      Event uevent = null;
      if (events.TryGetValue(eventName, out uevent))
        uevent.Invoke(obj, args);
    }
  }
}
