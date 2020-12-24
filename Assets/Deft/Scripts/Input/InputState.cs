namespace Deft.Input
{
    public enum InputState
    {
        None = 0,
        Pressed = 1, // Pressed this frame
        Down = 2, // Currently pressed (frame 1+)
        Released = 3, // Stopped being pressed this frame
    }
}
