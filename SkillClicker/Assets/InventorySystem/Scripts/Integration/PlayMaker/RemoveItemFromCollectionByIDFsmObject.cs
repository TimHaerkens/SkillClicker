﻿#if PLAYMAKER

using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;

namespace Devdog.InventorySystem.Integration.PlayMaker
{

    [ActionCategory("Inventory Pro")]
    [HutongGames.PlayMaker.Tooltip("Remove an item from a collection.")]
    public class RemoveItemFromCollectionByIDFsmObject : FsmStateAction
    {
        public FsmInt itemID;
        public FsmInt amountToRemove;

        public FsmObject collection;

        public override void Reset()
        {

        }

        public override void OnEnter()
        {
            var col = collection.Value as ItemCollectionBase;
            if (col == null)
            {
                Finish();
                return;
            }

            col.RemoveItem((uint)itemID.Value, (uint)amountToRemove.Value);
            Finish();
        }
    }
}

#endif