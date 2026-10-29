using System;
using System.Collections.Generic;
using UnityEngine;

//Una varibile public è accessibile da 

public class VariablesTest : MonoBehaviour
{
    [SerializeField] public int shopIndex = 8;

    #region INCAPSULAMENTO
    //INCAPSULAMENTO
    public int shopIndexBis { get ; private set; } //Incapsulamento con il get pubblico e settabile solo nella stessa classe
    public int ShopIndex => shopIndex; //Incapsulamento in lettura del valore di una variabile privata

    public void setShopIndexValue(int _shopIndex)
    {
        if(shopStateBase != ShopState.CLOSING)
        {
            shopIndex = _shopIndex;
        }
    }

    //
    #endregion

    [SerializeField] float shopRevenues = 50.5f;
    [SerializeField] bool shopOpen = false;

    //enum = elenco di valori definiti//
    public enum  ShopState { OPENING = 0, SELLING, RESTOCKING, CLOSING }
    [SerializeField] ShopState shopStateBase = ShopState.OPENING;
    [SerializeField] ShopState shopStateAdvanced = ShopState.RESTOCKING;

    [SerializeField] string shopname = "shop";

    //Array bool+[] > nome variabile
    [SerializeField] int[] productArray = new int[5];

    [SerializeField] List<int> productList = new List<int>();

    //keys = nome prodotto; float = prezzo prodotto;
    Dictionary<string, float> producDictionary = new Dictionary<string, float>();
    HashSet<Product> productHashSet = new HashSet<Product>();

    [SerializeField] List<Product> productsList = new List<Product>();
}

[Serializable]
public class  Product
{
    public string Name;
    public float Price;
}