using System.Collections.Generic;
public class Invoker
{
    private Stack<ICommand> _commandList;

    public Invoker()
    {
        _commandList = new Stack<ICommand>();
    }

    public void AddCommand(ICommand newCommand)
    {
        newCommand.Do();
        _commandList.Push(newCommand);
    }

    public void UndoCommand()
    {
        if (_commandList.Count <= 0) return;
        var lastCommand = _commandList.Pop();
        lastCommand.Undo();
    }
}
