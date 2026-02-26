using System;
using System.Collections.Generic;
using UnityEngine;

public class ConstructPlacement : MonoBehaviour
{
    public Construct ConstructToplace => _currentlyPlacingConstruct;
    [SerializeField] ItemSpawner _constructSpawner;
    private Construct _currentlyPlacingConstruct;
    private List<Construct> _placedConstructs = new List<Construct>();
    private void Update()
    {

    }
    public void SpawnConstruct()
    {
        Construct cons = _constructSpawner.GetItem().GetComponent<Construct>();
        cons.transform.position = HelperClass.MousPosWorld2D;
        cons.transform.rotation = Quaternion.identity;
        _currentlyPlacingConstruct = cons;
    }
    public void DestroyConstruct()
    {
        if (_currentlyPlacingConstruct == null) return;
        _currentlyPlacingConstruct.GetComponent<SpawnableItem>().ReturnToPool();
        _currentlyPlacingConstruct = null;
    }
    public void RotateConstruct()
    {
        _currentlyPlacingConstruct.Rotate();
    }
    public bool PlaceConstruct()
    {
        if (_currentlyPlacingConstruct.CanBePlaced)
        {
            Logger.Log("Placed");
            _currentlyPlacingConstruct.ChageTriggerToCol();

            if (_placedConstructs.Count >= 1)
            {
                Destroy(_placedConstructs[0].gameObject);
                _placedConstructs.RemoveAt(0);

            }
            _placedConstructs.Add(_currentlyPlacingConstruct);

            _currentlyPlacingConstruct = null;
            return true;
        }
        else
        {
            return false;
        }

    }
}
