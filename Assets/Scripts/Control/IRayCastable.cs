namespace RPG.Control
{
    public interface IRayCastable
    {
        CursorType GetCursorType();
        bool HandleRaycast(PlayerControler callingController);
    }
}