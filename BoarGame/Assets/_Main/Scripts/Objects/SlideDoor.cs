using System;
using System.Collections;
using UnityEngine;

public class SlideDoor : MonoBehaviour
{
    private IEnumerator _moveDoor;
    
    [SerializeField] private Transform startPivot;
    [SerializeField] private Transform endPivot;
    [SerializeField] private Transform doorPivot;
    [SerializeField] private float speed = 1f;

    public bool opening;
    private void Start()
    {
        doorPivot.position = startPivot.position;
    }
    
    private void MoveDoors(Vector3 pos)
    {
        var dist = Vector3.Distance(doorPivot.position, pos);
        if (dist > .1f)
        {
            doorPivot.position = Vector3.Lerp(doorPivot.position, pos, speed * Time.deltaTime);
        }
    }

    private IEnumerator MoveDoor(Vector3 pos)
    {
        opening = true;
        while (Vector3.Distance(doorPivot.position, pos) >= .1f)
        {
            Debug.Log("Opening");
            doorPivot.position = Vector3.Lerp(doorPivot.position, pos, speed * Time.deltaTime);
            if (Vector3.Distance(doorPivot.position, pos) >= .1f)
                yield return null;
        }
        opening = false;
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
}
