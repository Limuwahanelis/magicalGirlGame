using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractionWithListDetection : MonoBehaviour
{
    private class Interactable
    {
        public Collider col;
        public TriggerDetectable triggerDetectable;
        public IInteractableWithList interactableWithList;
    }
    [SerializeField] InteractionsWithList _inteactions;
#if UNITY_EDITOR
    [SerializeField] bool _debug;
#endif
    List<Interactable> _interactables = new List<Interactable>();

    private void OnTriggerEnter(Collider other)
    {
#if UNITY_EDITOR
        Logger.Log("Detected: " + other.gameObject.name);
#endif
        TriggerDetectable detectable = null;
        IInteractableWithList interaction = null;
        detectable = other.GetComponent<TriggerDetectable>();
        interaction = other.GetComponent<IInteractableWithList>();

        if (detectable != null)
        {
            if (_interactables.Find(x => x.triggerDetectable == detectable) == null)
            {
                if (interaction != null)
                {
                    _interactables.Add(new Interactable()
                    {
                        triggerDetectable = detectable,
                        interactableWithList = interaction,
                        col = other,
                    });
                    _inteactions.AddItemToInteract(interaction);
                    detectable.OnDisabled.AddListener(DetectableDisable);
                }
                else Logger.Error($"{other.gameObject} lacks InteractionsWithListComponent");
            }
        }
        else Logger.Error($"{other.gameObject} lacks TriggerDetectable component");

        
    }
    private void OnTriggerStay(Collider other)
    {

        TriggerDetectable detectable = null;
        IInteractableWithList interaction = null;
        detectable = other.GetComponent<TriggerDetectable>();
        interaction = other.GetComponent<IInteractableWithList>();
        if (detectable != null)
        {
            if (_interactables.Find(x => x.triggerDetectable == detectable) == null)
            {
                if (interaction != null )
                {
                    _interactables.Add(new Interactable()
                    {
                        triggerDetectable = detectable,
                        interactableWithList = interaction,
                        col = other,
                    });
                    _inteactions.AddItemToInteract(interaction);
                    detectable.OnDisabled.AddListener(DetectableDisable);
#if UNITY_EDITOR
                    Logger.Log("Detected: " + other.gameObject.name);
#endif
                }
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
#if UNITY_EDITOR
        Logger.Log("Left " + other.gameObject.name);
#endif
        TriggerDetectable detectable = other.GetComponent<TriggerDetectable>();
        if (detectable != null)
        {
            Interactable interact = _interactables.Find(x => x.triggerDetectable == detectable);
            if (interact != null)
            {

                _interactables.Remove(interact);
                _inteactions.RemoveInteractable(interact.interactableWithList);
                detectable.OnDisabled.RemoveListener(DetectableDisable);

            }
        }

    }

    private void DetectableDisable(TriggerDetectable detectable)
    {
        Interactable interact = _interactables.Find(x => x.triggerDetectable == detectable);
        _interactables.Remove(interact);
        _inteactions.RemoveInteractable(interact.interactableWithList);
        Collider col = detectable.GetComponent<Collider>();
#if UNITY_EDITOR
        Logger.Log("Left " + col.gameObject.name);
#endif
    }
    private void OnDestroy()
    {
        foreach (var detectable in _interactables)
        {
            detectable.triggerDetectable.OnDisabled.RemoveListener(DetectableDisable);
        }
    }
}
