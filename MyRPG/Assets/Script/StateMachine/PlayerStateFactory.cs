public class PlayerStateFactory 
{
    PlayerStateMachine _context;

    public PlayerStateFactory(PlayerStateMachine currentContext)
    {
        _context = currentContext;
    }

    public PlayerBaseState Idle() 
    {
        return new PlayerIdleState(_context, this);
    }
    public PlayerBaseState Walk() 
    {
        return new PlayerWalkState(_context, this);
    }
    public PlayerBaseState OnAir() 
    {
        return new PlayerOnAirState(_context, this);
    }
    public PlayerBaseState Ground() 
    {
        return new PlayerGroundState(_context, this);
    }
}
