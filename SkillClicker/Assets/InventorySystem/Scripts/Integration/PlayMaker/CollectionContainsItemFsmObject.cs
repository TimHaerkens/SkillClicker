﻿#if PLAYMAKER

using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;


namespace Devdog.InventorySystem.Integration.PlayMaker
{
    [ActionCategory("Inventory Pro")]
    [HutongGames.PlayMaker.Tooltip("Check if a given collection contains an item")]
    public class CollectionContainsItemFsmObject : FsmStateAction
    {
        public FsmObject obj;
        public FsmObject collection;

        [UIHint(UIHint.Variable)]
        public FsmBool result;

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

            result.Value = col.GetItemCount(item.ID) > 0;
            Finish();
        }
    }
}

#endif