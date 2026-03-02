using UnityEngine;

public class SimpleDemonContext:EnemyContext
{
    public float speed;
    public float timeToRestAtPoint;
    public EnemyGroundCheck _nextStepCheck;
    public Rigidbody2D rb;
}
