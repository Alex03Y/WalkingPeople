﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObservable
{
    void OnObjectChanged(IObserver observer);

}
