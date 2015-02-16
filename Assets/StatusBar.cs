using UnityEngine;
using System.Collections;

public class StatusBar : MonoBehaviour {

    public delegate void UpdateMoney(int money);

    public static event UpdateMoney OnMoneyChanged;





}
