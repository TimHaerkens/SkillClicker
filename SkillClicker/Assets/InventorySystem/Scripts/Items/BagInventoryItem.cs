using UnityEngine;
using System.Collections;
using System.Linq;
using Devdog.InventorySystem.Models;

namespace Devdog.InventorySystem
{
    /// <summary>
    /// Used to represent a bag that extend a collection.
    /// </summary>
    public partial class BagInventoryItem : InventoryItemBase
    {
        public uint extendBySlots = 4;
        public InventoryAudioClip playOnEquip;

        //public bool isEquipped { get; protected set; }

        public override System.Collections.Generic.LinkedList<InventoryItemInfoRow[]> GetInfo()
        {
            var list = base.GetInfo();

            list.AddFirst(new []
            {
                new InventoryItemInfoRow("Extra slots", extendBySlots.ToString())
            });

            return list;
        }

        public override int Use()
        {
            int used = base.Use();
            if(used < 0)
                return used; // Item cannot be used

            var extenderCollection = GetExtenderCollection();
            if (extenderCollection == null)
            {
                Debug.Log("Can't use bag, no collection found with interface " + typeof(ICollectionExtender));
                return -2;
            }

            bool added = extenderCollection.extenderCollection.AddItemAndRemove(this);
            if (added)
            {
                return 1;
            }

            return -2;
        }

        private ICollectionExtender GetExtenderCollection()
        {
            var player = InventoryPlayerManager.instance.currentPlayer;
            if (player == null)
            {
                Debug.Log("No current player, can't get collections.");
                return null;
            }

            var collectionExtenders = FindObjectsOfType<ItemCollectionBase>();
            var interfaces = collectionExtenders.OfType<ICollectionExtender>();
            foreach (var i in interfaces)
            {
                if (player.inventoryCollections.Contains(i.extendingCollection))
                {
                    return i;
                }
            }

            return null;
        }

        public void NotifyItemEquipped()
        {
            NotifyItemUsed(1, false);

            var extenderCollection = GetExtenderCollection();
            if (extenderCollection == null)
            {
                Debug.Log("Can't use bag, no inventory found with extender collection");
                return;
            }

            // Used from some collection, equip
            bool added = extenderCollection.extendingCollection.AddSlots(extendBySlots);
            if (added)
            {
                InventoryAudioManager.AudioPlayOneShot(playOnEquip);
            }
        }

        public bool NotifyItemUnEquipped()
        {
            var extenderCollection = GetExtenderCollection();
            if (extenderCollection == null)
            {
                Debug.Log("Can't unequip bag, no inventory found with extender collection");
                return false;
            }

            return extenderCollection.extendingCollection.RemoveSlots(extendBySlots);
        }

        public bool CanUnEquip()
        {
            var extenderCollection = GetExtenderCollection();
            if (extenderCollection == null)
            {
                Debug.Log("Can't unequip bag, no inventory found with extender collection");
                return false;
            }

            return extenderCollection.extendingCollection.CanRemoveSlots(extendBySlots);
        }
    }
}