using ShareInstances.Services.Interfaces;

namespace ShareInstances.Services.Entities;
public class UIControlHolder<T>
{
    private int size;
    public ReadOnlyMemory<char> ServiceName {get; init;} = "ControlsHolder";    
    private T[] controls;

    public UIControlService(int size = 5)
    {
        this.size = size;
        controls = new T[size];
    }

    public void AddControl(T control)
    {
        var index = controls.Length - 1 ;            
        if (index < 0)
        {
            index = 0;
            controls[index] = control;
        }
    }

    public void Dispose() =>
        controls = default;   
}
