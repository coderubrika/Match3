using System;
using System.Collections.Generic;

namespace Test3
{
    public class FieldContext
    {
        private readonly HashSet<CircleObject> unitsOnField = new();
        
        public TriggerNotifier[,] Notifiers { get; }
        public CircleObject[,] Units { get; }
        public int[] ColumnLevels { get; }

        public int UnitsCount => unitsOnField.Count;
        
        public event Action<(CircleObject, CircleObject, CircleObject)> OnExcludeUnits;
        public event Action<CircleObject, (int RowIdx, int ColumnIdx)> OnTriggerUnit;
        public event Action OnNext;
        public event Action OnFinish;
        
        public IDisposable WaitDropUnit { get; private set; }
        public IDisposable WaitDropAllUnits { get; private set; }
        
        public FieldContext(TriggerNotifier[,] notifiers)
        {
            Notifiers = notifiers;
            Units = new CircleObject[notifiers.GetLength(0), notifiers.GetLength(1)];
            ColumnLevels = new int[notifiers.GetLength(1)];
        }

        public void ResetUnits()
        {
            for (int i = 0; i < Units.GetLength(0); i++)
            {
                for (int j = 0; j < Units.GetLength(1); j++)
                {
                    Units[i, j] = null;
                }
            }
        }

        public void SetActiveNotifiers(bool isOn)
        {
            for (int i = 0; i < Notifiers.GetLength(0); i++)
            {
                for (int j = 0; j < Notifiers.GetLength(1); j++)
                {
                    var notifier = Notifiers[i, j];
                    notifier.gameObject.SetActive(isOn);
                }
            }
        }
        
        public void SetActiveNotifiersRow(bool isOn, int rowIdx)
        {
            for (int j = 0; j < Notifiers.GetLength(1); j++)
            {
                var notifier = Notifiers[rowIdx, j];
                notifier.gameObject.SetActive(isOn);
            }
        }
        
        public void ClearNotifiers()
        {
            for (int i = 0; i < Notifiers.GetLength(0); i++)
            {
                for (int j = 0; j < Notifiers.GetLength(1); j++)
                {
                    var notifier = Notifiers[i, j];
                    notifier.ClearAllSubscriptions();
                }
            }
        }

        public void ResetColumnLevels()
        {
            for (int i = 0; i < ColumnLevels.Length; i++)
                ColumnLevels[i] = 0;
        }
        
        public void SetWaitDropUnit(IDisposable waitDisposable)
        {
            WaitDropUnit = waitDisposable;
        }
        
        public void SetWaitDropAllUnits(IDisposable waitDisposable)
        {
            WaitDropAllUnits = waitDisposable;
        }

        public void ExcludeUnits((CircleObject, CircleObject, CircleObject) units)
        {
            unitsOnField.Remove(units.Item1);
            unitsOnField.Remove(units.Item2);
            unitsOnField.Remove(units.Item3);
            OnExcludeUnits?.Invoke(units);
        }

        public void AddUnit(CircleObject unit)
        {
            unitsOnField.Add(unit);
        }

        public void ClearUnits()
        {
            unitsOnField.Clear();
        }

        public void SubscribeOnNotifiers()
        {
            for (int i = 0; i < Notifiers.GetLength(0); i++)
            {
                for (int j = 0; j < Notifiers.GetLength(1); j++)
                {
                    var notifier = Notifiers[i, j];
                    var coords = (i,j);
                    notifier.OnTriggerEnter += collider => OnTriggerUnit?.Invoke(
                        collider.GetComponent<CircleObject>(), coords);
                }
            }
        }

        public void ClearTriggerUnitSubscriptions()
        {
            OnTriggerUnit = null;
        }

        public void Next()
        {
            OnNext?.Invoke();
        }

        public void Finish()
        {
            OnFinish?.Invoke();
        }
    }
}