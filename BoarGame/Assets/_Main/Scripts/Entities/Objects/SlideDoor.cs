using System;
using System.Collections;
using UnityEngine;

public class SlideDoor : MonoBehaviour, IObserver
{
    private IEnumerator _moveDoor;
    
    [SerializeField] private Transform startPivot;
    [SerializeField] private Transform endPivot;
    [SerializeField] private Transform doorPivot;
    [SerializeField] private float speed = 1f;
    private int _dir = 1;

    public bool opening;
    private void Start()
    {
        doorPivot.position = startPivot.position;
    }

    private IEnumerator MoveDoor(Vector3 pos)
    {
        opening = true;
        while (Vector3.Distance(doorPivot.position, pos) >= .1f)
        {
            Debug.Log("Opening");
            //doorPivot.position = Vector3.Lerp(doorPivot.position, pos, speed * Time.deltaTime);
            doorPivot.position += (transform.right * _dir) * (speed * Time.deltaTime);
            if (Vector3.Distance(doorPivot.position, pos) >= .1f)
                yield return null;
        }
        opening = false;
    }
    private IEnumerator MoveDoor()
    {
        opening = true;
        while (Vector3.Distance(doorPivot.position, endPivot.position) >= .1f && _dir < 0 || Vector3.Distance(doorPivot.position, startPivot.position) >= .1f && _dir > 0)
        {
            Debug.Log("Opening");
            //doorPivot.position = Vector3.Lerp(doorPivot.position, pos, speed * Time.deltaTime);
            doorPivot.position += (transform.right * _dir) * (speed * Time.deltaTime);
            if (Vector3.Distance(doorPivot.position, endPivot.position) >= .1f || Vector3.Distance(doorPivot.position, startPivot.position) >= .1f)
                yield return null;
        }
        opening = false;
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
        _moveDoor = MoveDoor(endPivot.position);
        StartCoroutine(_moveDoor);
       
    }
    public void CloseDoor() 
    {
        if (_moveDoor != null)
            StopCoroutine(_moveDoor);
        _moveDoor = MoveDoor(startPivot.position);
        StartCoroutine(_moveDoor);
    }

    public void OnNotify(ISubject subject)
    {
        if (((Button)subject).Pressed)
        {
            ToggleDoor();
        }
    }
}
