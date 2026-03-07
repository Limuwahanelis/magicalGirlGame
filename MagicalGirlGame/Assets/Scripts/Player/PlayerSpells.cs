using System.Collections.Generic;
using UnityEngine;

public class PlayerSpells : MonoBehaviour
{
    public enum SpellTypes
    {
        FIRE = 1,WATER,ELECTRICYTY,WIND,EARTH, BEAM, PROJECTILES, PEN
    }
    public enum SpellForm
    {
        NORMAL = 1,BEAM, PROJECTILES, PEN
    }
    public Dictionary<SpellTypes, Spell> AvailableSpells => _availableSpells;

    private Dictionary<SpellTypes, Spell> _availableSpells;

    private SpellTypes _selectedSpellType;

    public SpellTypes SelectedSpellType=> _selectedSpellType;

    private SpellForm _selectedSpellForm = SpellForm.NORMAL;

    public SpellForm SelectedSpellForm => _selectedSpellForm;

    [SerializeField] AudioSource _loopedAudioSource;
    [SerializeField] Transform _playerMainBOdy;

    [Header("Fire"),Space]
    [SerializeField]private ParticleSystem _fireParticles;
    [SerializeField]private PolygonCollider2D _fireTrigger;
    [SerializeField]private float _fireRange = 5;
    [SerializeField]private float _fireAngle = 50;
    [SerializeField] private ElementInfo _fireElementInfo;
    [SerializeField]private AudioEvent _fireAttackAudioEvent;

    [Header("Water"), Space]
    [SerializeField] private ParticleSystem _waterParticles;
    [SerializeField] private PolygonCollider2D _waterTrigger;
    [SerializeField] private float _waterRange = 5;
    [SerializeField] private float _waterAngle = 50;
    [SerializeField] private ElementInfo _waterElementInfo;
    [SerializeField] private AudioEvent _waterAttackAudioEvent;

    [Header("Electricity")]
    [Tooltip("Spread in degrees"), SerializeField] float _spread = 5f;
    [SerializeField] AudioEvent _electricityAudioEvent;
    [SerializeField] float _thunderDuration = 0.1f;
    [SerializeField] float _thunderCooldown = 0.5f;
    [SerializeField] Transform _thunderAttackDetection;
    [SerializeField] ParticleSystem _thunderParticlesPrefab;
    [SerializeField] int _count;
    [SerializeField] float _startLifetime;
    [SerializeField] float _length;
    [SerializeField] private ElementInfo _electricityElementInfo;
    [SerializeField] BoxCollider2D _electricityTrigger;
    [SerializeField] List<ParticleListWrapper> _allparticles = new List<ParticleListWrapper>();
    private List<float> angles = new List<float>() { 0, 0 };

    [Header("Wind"), Space]
    [SerializeField] private ItemSpawner _windPushSpawner;
    [SerializeField] private AudioEvent _windSpellAudtioEvent;
    [SerializeField] private Transform _windSpellSpawnTran;

    [Header("Beam"), Space]
    [SerializeField] private Transform _beamTransform;
    [SerializeField] private BoxCollider2D _beamTrigger;
    [SerializeField] private float _beamrange = 5;
    [SerializeField] private float _beamdamageMult = 0.3f;
    [SerializeField] private float _beamAattackCooldown = 0.25f;
    [SerializeField] LayerMask _toHit;
    [SerializeField] private AudioEvent _beamAttackAudtioEvent;

    [Header("Missile"), Space]
    [SerializeField] ItemSpawner _missileSpawner;
    [SerializeField] float _missileCooldown;
    [SerializeField] Transform _missileSpawnTran;

    [Header("Pen Missile"), Space]
    [SerializeField] ItemSpawner _penMissileSpawner;
    [SerializeField] Transform _penMissileSpawnTran;

    //[SerializeField] List<ParticleSystem> _paritcles = new List<ParticleSystem>();


    List<IDamagable> _damageablesInRange = new List<IDamagable>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _availableSpells = new Dictionary<SpellTypes, Spell>
        {
            {
                SpellTypes.FIRE,
                new FireSpell(this, _fireParticles, _damageablesInRange, _fireTrigger, _playerMainBOdy, _fireRange, _fireAngle, _fireElementInfo.Damage, _fireElementInfo.AttackCooldown
            , _fireAttackAudioEvent, _loopedAudioSource,2)
            },
            {
                SpellTypes.ELECTRICYTY,
                new ElectricitySpell(this, _spread, _damageablesInRange, angles, this, _thunderParticlesPrefab,
            _electricityTrigger,_allparticles, _electricityAudioEvent,_loopedAudioSource)
            },
            {
                SpellTypes.BEAM,
                new BeamSpell(_beamTransform,_damageablesInRange,_beamTrigger,_beamTransform,2,_beamAattackCooldown,_beamAttackAudtioEvent,_loopedAudioSource,0.2f,_toHit)
            },
            {
                SpellTypes.WIND,
                new WindSpell(_windSpellAudtioEvent,_loopedAudioSource,_windSpellSpawnTran,_windPushSpawner)
            },
            {
                SpellTypes.PROJECTILES,
                new MissileSpell(_missileSpawner,_missileCooldown,_missileSpawnTran,this)
            },
            {
                SpellTypes.PEN,
                new PenetratingMissileSpell(_penMissileSpawner,_penMissileSpawnTran)
            }
        };
        SelectSpell(1);
        SelectSpellForm(1);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetSpellElement(Spell spell)
    {
        switch (SelectedSpellType)
        {
            case SpellTypes.FIRE: spell.SetSpellType(DamageInfo.DamageType.FIRE, _fireElementInfo); break;
            case SpellTypes.WATER: spell.SetSpellType(DamageInfo.DamageType.WATER, _waterElementInfo); break;
            case SpellTypes.ELECTRICYTY: spell.SetSpellType(DamageInfo.DamageType.ELECTRICITY, _electricityElementInfo); break;
        }
    }
    public void SelectSpell(int index)
    {
        _selectedSpellType = (SpellTypes)index;
    }
    public void SelectSpellForm(int index)
    {
        _selectedSpellForm = (SpellForm)index;
        Logger.Log(_selectedSpellForm);
    }

    public void SetEnemyForAttack(Collider2D enemy)
    {
        if (enemy.GetComponent<IDamagable>() != null)
        {
            _damageablesInRange.Add(enemy.GetComponent<IDamagable>());
            enemy.GetComponent<IDamagable>().OnDeath += OnEnemyDied;
            Logger.Log("ADDed");
        }
        else
        {
            if(enemy.attachedRigidbody !=null)
            {
                if(enemy.attachedRigidbody.GetComponent<IDamagable>()!=null)
                {
                    _damageablesInRange.Add(enemy.attachedRigidbody.GetComponent<IDamagable>());
                    enemy.attachedRigidbody.GetComponent<IDamagable>().OnDeath += OnEnemyDied;
                    Logger.Log("ADDed");
                }
            }
        }
    }
    public void RemoveEnemyFromAttack(Collider2D enemy)
    {
        if (_damageablesInRange.Contains(enemy.GetComponent<IDamagable>()))
        {
            _damageablesInRange.Remove(enemy.GetComponent<IDamagable>());
            enemy.GetComponent<IDamagable>().OnDeath -= OnEnemyDied;
            Logger.Log("Removed");
        }
        else
        {
            if (enemy.attachedRigidbody != null)
            {
                if (enemy.attachedRigidbody.GetComponent<IDamagable>() != null)
                {
                    _damageablesInRange.Remove(enemy.attachedRigidbody.GetComponent<IDamagable>());
                    enemy.attachedRigidbody.GetComponent<IDamagable>().OnDeath -= OnEnemyDied;
                    Logger.Log("Removed");
                }
            }

        }
    }
    private void OnEnemyDied(IDamagable damagable, DamageInfo info)
    {
        damagable.OnDeath -= OnEnemyDied;
        _damageablesInRange.Remove(damagable);
    }
    private void RemoveEnemiesFromRange()
    {
        _damageablesInRange.Clear();
    }
}
