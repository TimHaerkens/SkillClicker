﻿#if PLAYMAKER

using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;


namespace Devdog.InventorySystem.Integration.PlayMaker
{
    [ActionCategory("Inventory Pro")]
    [HutongGames.PlayMaker.Tooltip("Check if the given item can be added to the collection.")]
    public class CanAddItemToCollectionFsmObject : FsmStateAction
    {
        public FsmObject obj;
        public FsmObject collection;

        [UIHint(UIHint.Variable)]
        public FsmVar result;

        public override void Reset()
        {

        }

        public override void OnEnter()
        {
            var item = obj.Value as InventoryItemBase;
            if (item == null)
            {
                //                Debug.LogWarning("Item given is not an Inventory Pro item and can't be added to the collection.");
                Finish();
                return;
            }

            var col = collection.Value as ItemCollectionBase;
            if (col == null)
            {
                Finish();
                return;
            }

            result.boolValue = col.CanAddItem(item);

            Finish();
        }
    }
}

#endif