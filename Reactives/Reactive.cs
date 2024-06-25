using System;

namespace TaigaGames.Kit
{
    [Serializable]
    public class Reactive<T>
    {
        [NonSerialized] protected T _value;

        public event OnValueChangedDelegate OnValueChanged;
        public event OnValueChangedExtendedDelegate OnValueChangedExtended;

        public T Value => _value;

        public Reactive()
        {
            _value = default;
        }

        public Reactive(T value)
        {
            _value = value;
        }

        public void SetValue(T newValue)
        {
            if (Equals(_value, newValue))
                return;

            var oldValue = _value;
            _value = newValue;
            OnValueChanged?.Invoke(_value);
            OnValueChangedExtended?.Invoke(_value, oldValue);
        }

        public void SetValueWithoutNotify(T newValue)
        {
            _value = newValue;
        }

        public void SubscribeAndExecute(OnValueChangedExtendedDelegate callback)
        {
            OnValueChangedExtended += callback;
            callback(_value, _value);
        }
        
        public void Subscribe(OnValueChangedExtendedDelegate callback)
        {
            OnValueChangedExtended += callback;
        }

        public void Unsubscribe(OnValueChangedExtendedDelegate callback)
        {
            OnValueChangedExtended -= callback;
        }

        public void SubscribeAndExecute(OnValueChangedDelegate callback)
        {
            OnValueChanged += callback;
            callback(_value);
        }
        
        public void Subscribe(OnValueChangedDelegate callback)
        {
            OnValueChanged += callback;
        }

        public void Unsubscribe(OnValueChangedDelegate callback)
        {
            OnValueChanged -= callback;
        }
        
        public static implicit operator T(Reactive<T> field) => field._value;
        
        public delegate void OnValueChangedDelegate(T value);
        public delegate void OnValueChangedExtendedDelegate(T newValue, T oldValue);
    }
}