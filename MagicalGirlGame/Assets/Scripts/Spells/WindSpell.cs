using System.Collections.Generic;
using UnityEngine;

public class WindSpell : Spell
{
    AudioEvent _airAttackAudioEvent;
    AudioSource _audioSource;
    ItemSpawner _windPushSpawner;
    Transform _windPushSpawnPos;
    public WindSpell( AudioEvent airAttackAudioEvent, AudioSource audioSource, Transform windPushSpawnPos, ItemSpawner windPushSpawner)
    {
        _audioSource = audioSource;
        _windPushSpawnPos = windPushSpawnPos;
        _airAttackAudioEvent = airAttackAudioEvent;
        _windPushSpawner = windPushSpawner;
    }
    public override void StartAttack()
    {
        WindPush windPush= _windPushSpawner.GetItem().GetComponent<WindPush>();
        //_airAttackAudioEvent.Play(_audioSource);

        windPush.transform.position = _windPushSpawnPos.transform.position;
        windPush.transform.up = (HelperClass.MousPosWorld2D - _windPushSpawnPos.position).normalized;
        windPush.transform.Rotate(windPush.transform.forward, 45f);
        windPush.StartPushing();
        windPush.GetComponent<Animator>().SetTrigger("Push");
        windPush.transform.SetParent(null);

    }
    public override void Attack()
    {
       
    }

    public override void EndAttack()
    {
        
    }




}
