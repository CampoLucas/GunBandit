using System;
using System.Collections;
using UnityEngine;

public class SlideDoor : Observer
{
    private IEnumerator _moveDoor;
    private bool _opening;
    private CmdSlideDoor _slideDoor;
    private Invoker _invoker = new();
    
    [SerializeField] private Transform startPivot;
    [SerializeField] private Transform endPivot;
    [SerializeField] private Transform doorPivot;
    [SerializeField] private float speed = 1f;

    private void Awake()
    {
        
    }

    private void Start()
    {
        doorPivot.position = startPivot.position;
        _slideDoor = new CmdSlideDoor(doorPivot, startPivot.position, endPivot.position, speed);
    }

    private void Update()
    {
        if (_invoker == null) return;
        if (_opening)
            _invoker.AddCommand(_slideDoor);
        else
            _invoker.UndoCommand();
            
    }

    public void ToggleDoor()
    {
        if(!_opening)
            OpenDoor();
        else
            CloseDoor();
    }
    public void OpenDoor()
    {
        if(_opening) return;
        _opening = true;
       
    }
    public void CloseDoor() 
    {
        if(!_opening) return;
        _opening = false;
    }

    public override void OnNotify(string message, params object[] args)
    {
        switch (message)
        {
            case "ON":
                OpenDoor();
                break;
            case "OFF": 
                CloseDoor();
                break;
        }
#if UNITY_EDITOR
        Debug.Log(message + ": " + this.gameObject.name);
#endif
    }
}
