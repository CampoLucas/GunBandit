using System;
using System.Collections;
using UnityEngine;

public class SlideDoor : Observer
{
    private IEnumerator _moveDoor;
    private int _dir = 1;
    
    [SerializeField] private Transform startPivot;
    [SerializeField] private Transform endPivot;
    [SerializeField] private Transform doorPivot;
    [SerializeField] private float speed = 1f;

    private void Start()
    {
        doorPivot.position = startPivot.position;
        _moveDoor = MoveDoor();
    }
    private IEnumerator MoveDoor()
    {
        while (Vector3.Distance(doorPivot.position, endPivot.position) >= .1f && _dir < 0 || Vector3.Distance(doorPivot.position, startPivot.position) >= .1f && _dir > 0)
        {
            Debug.Log("Opening");
            //doorPivot.position = Vector3.Lerp(doorPivot.position, pos, speed * Time.deltaTime);
            doorPivot.position += (transform.right * _dir) * (speed * Time.deltaTime);
            if (Vector3.Distance(doorPivot.position, endPivot.position) >= .1f && _dir < 0 || Vector3.Distance(doorPivot.position, startPivot.position) >= .1f && _dir > 0)
                yield return null;
        }
    }

    public void ToggleDoor()
    {
        if (_moveDoor != null)
            StopCoroutine(_moveDoor);
        _dir *= -1;
        _moveDoor = MoveDoor();
        StartCoroutine(_moveDoor);
    }
    public void OpenDoor()
    {
        if (_moveDoor != null)
            StopCoroutine(_moveDoor);
        _dir = -1;
        _moveDoor = MoveDoor();
        StartCoroutine(_moveDoor);
       
    }
    public void CloseDoor() 
    {
        if (_moveDoor != null)
            StopCoroutine(_moveDoor);
        _dir = 1;
        _moveDoor = MoveDoor();
        StartCoroutine(_moveDoor);
    }

    public override void OnNotify(string message, params object[] args)
    {
        ToggleDoor();
#if UNITY_EDITOR
        Debug.Log(message + ": " + this.gameObject.name);
#endif
    }
}
