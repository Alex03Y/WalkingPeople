﻿using System.Collections.Generic;
using UnityEngine;

namespace WalkingPeople.Scripts.Core.MVC.ObserverLogic
{
    public class Observer : IObserver
    {
        private readonly List<IObservable> _observers = new List<IObservable>();
    
        public void AddObserver(IObservable observable)
        {
            var index = _observers.FindIndex(x => x.GetHashCode() == observable.GetHashCode());
            if (index != -1) Debug.Log("MVC subscrible duplication");
            else _observers.Add(observable);
        }

        public void RemoveObservable(IObservable observable)
        {
            var index = _observers.FindIndex(x => x.GetHashCode() == observable.GetHashCode());
            if (index == -1) Debug.Log("MVC observer not found, unsubscribe failed");
            else _observers.Remove(observable);
        }

        public void RemoveAll()
        {
            _observers.Clear();
        }

        public void SetChanged()
        {
            _observers.ForEach(x => x.OnObjectChanged(this));
        }
    }
}
