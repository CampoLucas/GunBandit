using UnityEngine;

public class CmdSlideDoor : ICommand
{
    private Transform _transform;
    private Vector3 _openPos;
    private Vector3 _closePos;
    private float _speed;

    public CmdSlideDoor(Transform transform, Vector3 openPos, Vector3 closePos, float speed = 1)
    {
        _transform = transform;
        _openPos = openPos;
        _closePos = closePos;
        _speed = speed;
    }
    
    public void Do()
    {
        if(Vector3.Distance(_transform.position, _openPos) < .1f) return;
        _transform.position = Vector3.Lerp(_transform.position, _openPos, _speed * Time.deltaTime);
    }

    public void Undo()
    {
        if(Vector3.Distance(_transform.position, _closePos) < .1f) return;
        _transform.position = Vector3.Lerp(_transform.position, _closePos, _speed * Time.deltaTime);
    }
}
