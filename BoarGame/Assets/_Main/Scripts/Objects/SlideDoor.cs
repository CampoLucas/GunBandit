using System;
using System.Collections;
using UnityEngine;

public class SlideDoor : MonoBehaviour
{
    [SerializeField] private Transform startPivot;
    [SerializeField] private Transform endPivot;
    [SerializeField] private Transform doorPivot;
    [SerializeField] private float speed = 1f;
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
        var dist = Vector3.Distance(doorPivot.position, pos);
        if (dist > .1f)
        {
            doorPivot.position = Vector3.Lerp(doorPivot.position, pos, speed * Time.deltaTime);
        }

        yield return null;
    }

    public void OpenDoor() => StartCoroutine(MoveDoor(endPivot.position));
    public void CloseDoor() => StartCoroutine(MoveDoor(startPivot.position));
}
