using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttributes : MonoBehaviour, iDataPersistence
{
    //Save data
    public void LoadData(GameData data)
    {
        this.PlayerHealth = data.PlayerHealth;
        this.Slime1Speed = data.Slime1Speed;
        this.Slime2Speed = data.Slime2Speed;
        this.ImpulseSpeed = data.ImpulseSpeed;
        this.KeyCard1 = data.KeyCard1;
        this.KeyCard2 = data.KeyCard2;
        this.KeyCard3 = data.KeyCard3;
        this.HasFlashlight = data.HasFlashlight;
        this.InLight = false;
        this.Forward = true;
        this.CanSplit = data.CanSplit;
    }
    //Load Data
    public void SaveData(ref GameData data)
    {
        data.PlayerHealth = this.PlayerHealth;
        data.Slime1Speed = this.Slime1Speed;
        data.Slime2Speed = this.Slime2Speed;
        data.ImpulseSpeed = this.ImpulseSpeed;
        data.KeyCard1 = this.KeyCard1;
        data.KeyCard2 = this.KeyCard2;
        data.KeyCard3 = this.KeyCard3;
        data.HasFlashlight = this.HasFlashlight;
        data.CanSplit = this.CanSplit;
    }


    //Player Health
    public event Action<int> OnPlayerHealthChange;
    private int _playerHealth;
    public int PlayerHealth
    {
        get => _playerHealth;
        set
        {
            if (_playerHealth != value)
            {
                _playerHealth = value;
                OnPlayerHealthChange?.Invoke(_playerHealth);
            }
        }
    }
    //Slime1Speed
    public event Action<float> OnSlime1SpeedChange;
    private float _Slime1Speed;
    public float Slime1Speed
    {
        get => _Slime1Speed;
        set
        {
            if (!Mathf.Approximately(_Slime1Speed, value))
            {
                _Slime1Speed = value;
                OnSlime1SpeedChange?.Invoke(_Slime1Speed);
            }
        }
    }
    //Slime2Speed
    public event Action<float> OnSlime2SpeedChange;
    private float _Slime2Speed;
    public float Slime2Speed
    {
        get => _Slime2Speed;
        set
        {
            if (!Mathf.Approximately(_Slime2Speed, value))
            {
                _Slime2Speed = value;
                OnSlime2SpeedChange?.Invoke(_Slime2Speed);
            }
        }
    }
    //Slime Streatch
    public event Action<bool> OnIsStreachedChange;
    private bool _isStreached;
    public bool IsStreached
    {
        get => _isStreached;
        set
        {
            if (_isStreached != value)
            {
                _isStreached = value;
                OnIsStreachedChange?.Invoke(_isStreached);
            }
        }
    }
    //Slime Split
    public event Action<bool> OnIsSplitChange;
    private bool _isSplit;
    public bool IsSplit
    {
        get => _isSplit;
        set
        {
            if (_isSplit != value)
            {
                _isSplit = value;
                OnIsSplitChange?.Invoke(_isSplit);
            }
        }
    }
    //ImpulseSpeed
    public event Action<float> OnImpulseSpeedChange;
    private float _impulseSpeed;
    public float ImpulseSpeed
    {
        get => _impulseSpeed;
        set
        {
            if (!Mathf.Approximately(_impulseSpeed, value))
            {
                _impulseSpeed = value;
                OnImpulseSpeedChange?.Invoke(_impulseSpeed);
            }
        }
    }
    //Added Impulse
    public event Action<bool> OnAddedImpulseChange;
    private bool _addedImpulse;
    public bool AddedImpulse
    {
        get => _addedImpulse;
        set
        {
            if (_addedImpulse != value)
            {
                _addedImpulse = value;
                OnAddedImpulseChange?.Invoke(_addedImpulse);
            }
        }
    }
    //Carrying Item
    public event Action<bool> OnCarryItemChange;
    private bool _carryItem;
    public bool CarryItem
    {
        get => _carryItem;
        set
        {
            if (_carryItem != value)
            {
                _carryItem = value;
                OnCarryItemChange?.Invoke(_carryItem);
            }
        }
    }
    //Keycards
    public event Action<bool> OnKeyCard1Change;
    private bool _KeyCard1;
    public bool KeyCard1
    {
        get => _KeyCard1;
        set
        {
            if (_KeyCard1 != value)
            {
                _KeyCard1 = value;
                OnKeyCard1Change?.Invoke(_KeyCard1);
            }
        }
    }
    public event Action<bool> OnKeyCard2Change;
    private bool _KeyCard2;
    public bool KeyCard2
    {
        get => _KeyCard2;
        set
        {
            if (_KeyCard2 != value)
            {
                _KeyCard2 = value;
                OnKeyCard2Change?.Invoke(_KeyCard2);
            }
        }
    }
    public event Action<bool> OnKeyCard3Change;
    private bool _KeyCard3;
    public bool KeyCard3
    {
        get => _KeyCard3;
        set
        {
            if (_KeyCard3 != value)
            {
                _KeyCard3 = value;
                OnKeyCard3Change?.Invoke(_KeyCard3);
            }
        }
    }
    public event Action<bool> OnInLightChange;
    private bool _InLight;
    public bool InLight
    {
        get => _InLight;
        set
        {
            if (_InLight != value)
            {
                _InLight = value;
                OnInLightChange?.Invoke(_InLight);
            }
        }
    }
    public event Action<bool> OnFlashlightGet;
    private bool _HasFlashlight;
    public bool HasFlashlight
    {
        get => _HasFlashlight;
        set
        {
            if (_HasFlashlight != value)
            {
                _HasFlashlight = value;
                OnFlashlightGet?.Invoke(_HasFlashlight);
            }
        }
    }
    public event Action<bool> OnForwardChange;
    private bool _Forward;
    public bool Forward
    {
        get => _Forward;
        set
        {
            if (_Forward != value)
            {
                _Forward = value;
                OnForwardChange?.Invoke(_Forward);
            }
        }
    }
    public event Action<bool> OnCanSplitChange;
    private bool _CanSplit;
    public bool CanSplit
    {
        get => _CanSplit;
        set
        {
            if (_CanSplit != value)
            {
                _CanSplit = value;
                OnCanSplitChange?.Invoke(_CanSplit);
            }
        }
    }
    //Request functions for if you want to change and read the same value
    public void RequestPlayerHealthChange(int newValue)
    {
        if (_playerHealth != newValue)
        {
            _playerHealth = newValue;
            OnPlayerHealthChange?.Invoke(_playerHealth);
        }
    }
    public void RequestSlime1SpeedChange(float newValue)
    {
        if (!Mathf.Approximately(_Slime1Speed, newValue))
        {
            _Slime1Speed = newValue;
            OnSlime1SpeedChange?.Invoke(_Slime1Speed);
        }
    }
    public void RequestSlime2SpeedChange(float newValue)
    {
        if (!Mathf.Approximately(_Slime2Speed, newValue))
        {
            _Slime2Speed = newValue;
            OnSlime2SpeedChange?.Invoke(_Slime2Speed);
        }
    }
    public void RequestIsStreachedChange(bool newValue)
    {
        if (_isStreached != newValue)
        {
            _isStreached = newValue;
            OnIsStreachedChange?.Invoke(_isStreached);
        }
    }
    public void RequestIsSplitChange(bool newValue)
    {
        if (_isSplit != newValue)
        {
            _isSplit = newValue;
            OnIsSplitChange?.Invoke(_isSplit);
        }
    }
    public void RequestImpulseSpeedChange(float newValue)
    {
        if (!Mathf.Approximately(_impulseSpeed, newValue))
        {
            _impulseSpeed = newValue;
            OnImpulseSpeedChange?.Invoke(_impulseSpeed);
        }
    }
    public void RequestAddedImpulseChange(bool newValue)
    {
        if (_addedImpulse != newValue)
        {
            _addedImpulse = newValue;
            OnAddedImpulseChange?.Invoke(_addedImpulse);
        }
    }
    public void RequestCarryItemChange(bool newValue)
    {
        if (_carryItem != newValue)
        {
            _carryItem = newValue;
            OnCarryItemChange?.Invoke(_carryItem);
        }
    }
    public void RequestKeyCard1Change(bool newValue)
    {
        if (_KeyCard1 != newValue)
        {
            _KeyCard1 = newValue;
            OnKeyCard1Change?.Invoke(_KeyCard1);
        }
    }
    public void RequestKeyCard2Change(bool newValue)
    {
        if (_KeyCard2 != newValue)
        {
            _KeyCard2 = newValue;
            OnKeyCard2Change?.Invoke(_KeyCard2);
        }
    }
    public void RequestKeyCard3Change(bool newValue)
    {
        if (_KeyCard3 != newValue)
        {
            _KeyCard3 = newValue;
            OnKeyCard3Change?.Invoke(_KeyCard3);
        }
    }
    public void RequestInLightChange(bool newValue)
    {
        if (_InLight != newValue)
        {
            _InLight = newValue;
            OnInLightChange?.Invoke(_InLight);
        }
    }
    public void RequestFlashLightGet(bool newValue)
    {
        if (_HasFlashlight != newValue)
        {
            _HasFlashlight = newValue;
            OnFlashlightGet?.Invoke(_HasFlashlight);
        }
    }
    public void RequestForwardChange(bool newValue)
    {
        if (_Forward != newValue)
        {
            _Forward = newValue;
            OnForwardChange?.Invoke(_Forward);
        }
    }
    public void RequestCanSplitChange(bool newValue)
    {
        if (_CanSplit != newValue)
        {
            _CanSplit = newValue;
            OnCanSplitChange?.Invoke(_CanSplit);
        }
    }
}
