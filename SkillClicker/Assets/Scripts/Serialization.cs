using UnityEngine;
using System.Collections;
using Devdog.InventorySystem.Models;
using Devdog.InventorySystem;

public class Serialization : MonoBehaviour {

    [System.Serializable]
    public class MyItemCollectionSerializationModel : ItemCollectionSerializationModel
    {
        public override void FillItemsUsingCollection(ItemCollectionBase collection)
        {
            base.FillItemsUsingCollection(collection);
            // Fill extra data
        }
        public override void FillCurrenciesUsingCollection(ItemCollectionBase collection)
        {
            base.FillCurrenciesUsingCollection(collection);
            // Fill extra data
        }
        /// <summary>
        /// Fill a collection using this data object.
        /// </summary>
        /// <param name="collection">The collection to fill using this (ItemCollectionSerializationModel) object.</param>
        public override void FillCollectionUsingThis(ItemCollectionBase collection)
        {
            base.FillCollectionUsingThis(collection);
            // Set data into collection using this object.
        }
    }
}
