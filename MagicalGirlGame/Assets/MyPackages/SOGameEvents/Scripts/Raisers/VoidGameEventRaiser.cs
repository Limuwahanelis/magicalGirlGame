using UnityEngine;

public class VoidGameEventRaiser : MonoBehaviour
{
    [SerializeField] GameEventVoidSO _eventToRaise;

    public void RasieEvent()
    {
        _eventToRaise.Raise();
    }
}
