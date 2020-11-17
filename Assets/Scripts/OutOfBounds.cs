using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class OutOfBounds : Exception
{
    public OutOfBounds() {}
    public OutOfBounds(string message) : base(message) {}
    public OutOfBounds(string message, Exception inner) : base(message, inner) {}    
}
