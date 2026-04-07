public enum EntityState { Idle, Moving, Grabbing, Striking, Stunned }

public class EntityStateManager : MonoBehaviour 
{
    public EntityState currentState = EntityState.Idle;

    public void SetState(EntityState newState) 
    {
        currentState = newState;
        // Logic for when a state changes 
        Debug.Log("Prince is now: " + newState);
    }
}