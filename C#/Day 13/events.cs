using System;
class Button
{
    public delegate void ClickHandler();

    public event ClickHandler Clicked;
    public event ClickHandler Back;

    public void Click()
    {
        Clicked?.Invoke();
        Back?.Invoke();
    }
}