using UnityEngine;

public class CmdSlideDoor : ICommand
{
    private readonly Transform _transform;
    private readonly Vector3 _startPos;
    private readonly Vector3 _endPos;
    private readonly float _speed;
    

    public CmdSlideDoor(Transform transform, Vector3 startPos, Vector3 endPos, float speed = 1)
    {
        _transform = transform;
        _startPos = startPos;
        _endPos = endPos;
        _speed = speed;
    }
    
    public void Do()
    {
        if(Vector3.Distance(_transform.position, _endPos) < .1f) return;
        //_transform.position = Vector3.Lerp(_transform.position, _pos, _speed * Time.deltaTime);
        _transform.position = Vector3.MoveTowards(_transform.position, _endPos, _speed * Time.deltaTime);
    }

    public void Undo()
    {
        if(Vector3.Distance(_transform.position, _startPos) <= .1f) return;
        //_transform.position = Vector3.Lerp(_transform.position, _startPos, _speed * Time.deltaTime);
        _transform.position = Vector3.MoveTowards(_transform.position, _startPos, _speed * Time.deltaTime);
    }
}
