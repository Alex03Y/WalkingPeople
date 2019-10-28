using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObserver
{
    void AddObserver(IObservable observable);
    void RemoveObservable(IObservable observable);
    void SetChanged();


}
