using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ParticleListWrapper
{
    public List<ParticleSystem> particles = new List<ParticleSystem>();
}
public abstract class Spell 
{
    public abstract void StartAttack();
    public abstract void Attack();
    public abstract void EndAttack();
}
