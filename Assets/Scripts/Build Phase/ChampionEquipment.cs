using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ChampionEquipment : MonoBehaviour
{
    public int _championArmor;
    public Image _championArmorSprite;

    public int _championMaxPointVal;
    public int _championCurrentPointVal;

    public int _bluntWeaponVal;
    public int _sharpWeaponVal;
    public int _rangeWeaponVal;

    public List<int> Deck = new List<int>();
}
