using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MomentaryButton : Button
{
    protected override void Press()
    {
        base.Press();
        NotifyAll("PRESSED");
    }
    
    protected override void Release()
    {
        base.Release();
        NotifyAll("RELEASED");
    }
    
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        _inputs.OnInteractStarted += Press;
        _inputs.OnInteractCancelled += Release;
    }

    protected override void OnTriggerExit2D(Collider2D other)
    {
        base.OnTriggerExit2D(other);
        _inputs.OnInteractStarted -= Press;
        _inputs.OnInteractCancelled -= Release;
    }
}
