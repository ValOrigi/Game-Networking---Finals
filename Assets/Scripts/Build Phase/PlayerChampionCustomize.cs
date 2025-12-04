using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerChampionCustomize : MonoBehaviour
{
    //[SerializeField] Transform championPos;
    //[SerializeField] private int[] _armor;
    [SerializeField] Player player;

    //[SerializeField] GameObject _championPref;
    [SerializeField] ChampionEquipment _championEquipment;
    [SerializeField] GameManager _gameManager;
    [SerializeField] Champion champion;
    [SerializeField] SceneManager _sceneManager;

    [SerializeField] TextMeshProUGUI _bluntPoint, _sharpPoint, _rangePoint, _remainingPoint;
    public TextMeshProUGUI playerScoreText;

    [SerializeField] Image currentArmorImage;

    //[SerializeField] PlayerChampionCustomizeUI UIScript;

    private void Start()
    {
        //player = GameObject.Find("Player").GetComponent<Player>();

        //GameObject NewChamp = Instantiate(_championPref, championPos);
        //player._champion = NewChamp;

        //_championEquipment = NewChamp.GetComponent<ChampionEquipment>();
        
        //UIScript = GetComponent<PlayerChampionCustomizeUI>();

        player.playerScore = 0;
        ResetChampionWeaponPoints();
    }

    public void PlayerReady()
    {
        if (_championEquipment._championCurrentPointVal == _championEquipment._championMaxPointVal && _championEquipment._championArmor != 0) 
        {
            player._isReady = true;
        }
        else if (_championEquipment._championArmor == 0)
        {
            //UIScript.MissingArmor();
            Debug.Log("Missing armor");
        }
        else if (_championEquipment._championCurrentPointVal != _championEquipment._championMaxPointVal)
        {
            //UIScript.UnusedPoints();
            Debug.Log("Unused Points");
        }
    }

    public void AddWeaponStatButton(int index)
    {
        if (!player._isReady)
        {
            switch (index)
                    {
                        case 0:
                            Debug.Log(index + "is Clicked on Add sharp");
                            //AddWeaponStat(_championEquipment._sharpWeaponVal, _sharpPoint);
                            if (_championEquipment._championMaxPointVal > _championEquipment._championCurrentPointVal)
                            {
                                ++_championEquipment._sharpWeaponVal;
                                ++_championEquipment._championCurrentPointVal;
                                _sharpPoint.text = _championEquipment._sharpWeaponVal.ToString();
                            }
                            break;
                        case 1:
                            Debug.Log(index + "is Clicked on Add blunt");
                            //AddWeaponStat(_championEquipment._bluntWeaponVal, _bluntPoint);
                            if (_championEquipment._championMaxPointVal > _championEquipment._championCurrentPointVal)
                            {
                                ++_championEquipment._bluntWeaponVal;
                                ++_championEquipment._championCurrentPointVal;
                                _bluntPoint.text = _championEquipment._bluntWeaponVal.ToString();
                            }
                            break;
                        case 2:
                            Debug.Log(index + "is Clicked on Add range");
                            //AddWeaponStat(_championEquipment._rangeWeaponVal, _rangePoint);
                            if (_championEquipment._championMaxPointVal > _championEquipment._championCurrentPointVal)
                            {
                                ++_championEquipment._rangeWeaponVal;
                                ++_championEquipment._championCurrentPointVal;
                                _rangePoint.text = _championEquipment._rangeWeaponVal.ToString();
                            }
                            break;
                    }

            UpdateRemainingPoints();
        }
        
    }

    public void SubtractWeaponStatButton(int index)
    {
        if (!player._isReady)
        {
            switch (index)
            {
                case 0:
                    Debug.Log(index + "is Clicked on Minus sharp");
                    //SubtractWeaponStat(_championEquipment._sharpWeaponVal, _sharpPoint);
                    if (_championEquipment._championCurrentPointVal > 0 && _championEquipment._sharpWeaponVal > 0)
                    {
                        --_championEquipment._sharpWeaponVal;
                        --_championEquipment._championCurrentPointVal;
                        _sharpPoint.text = _championEquipment._sharpWeaponVal.ToString();
                    }
                    break;
                case 1:
                    Debug.Log(index + "is Clicked on Minus blunt");
                    //SubtractWeaponStat(_championEquipment._bluntWeaponVal, _bluntPoint);
                    if (_championEquipment._championCurrentPointVal > 0 && _championEquipment._bluntWeaponVal > 0)
                    {
                        --_championEquipment._bluntWeaponVal;
                        --_championEquipment._championCurrentPointVal;
                        _bluntPoint.text = _championEquipment._bluntWeaponVal.ToString();
                    }
                    break;
                case 2:
                    Debug.Log(index + "is Clicked on Minus range");
                    //SubtractWeaponStat(_championEquipment._rangeWeaponVal, _rangePoint);
                    if (_championEquipment._championCurrentPointVal > 0 && _championEquipment._rangeWeaponVal > 0)
                    {
                        --_championEquipment._rangeWeaponVal;
                        --_championEquipment._championCurrentPointVal;
                        _rangePoint.text = _championEquipment._rangeWeaponVal.ToString();
                    }
                    break;
            }

            UpdateRemainingPoints();
        }
    }

    public void ResetChampionWeaponPoints()
    {
        //can be optimized, dont judge me >:(
        _championEquipment._championCurrentPointVal = 0;
        _championEquipment._rangeWeaponVal = 0;
        _championEquipment._sharpWeaponVal = 0;
        _championEquipment._bluntWeaponVal = 0;
        _bluntPoint.text = "0";
        _sharpPoint.text = "0";
        _rangePoint.text = "0";
        UpdateRemainingPoints();
    }

    public void UpdateRemainingPoints() //should be on UI UPDATES SCRIPT
    {
        _remainingPoint.text = _championEquipment._championCurrentPointVal.ToString() + "/" + _championEquipment._championMaxPointVal;
    }
    public void AddArmorToChamp(int index)
    {
        _championEquipment._championArmor = index;
        currentArmorImage.sprite = _gameManager.armorSprite[index-1];
        //currentArmorImage.sprite = _armor[index].GetComponent<ChampionArmor>().armorSprite;
        //Better sana eto
    }

    //NOT WORKING IDK WHY (WORKING ON FIX)
    /*    void AddWeaponStat(int weaponTypePoint, TextMeshProUGUI weaponPointTxt)
        {
            if (_championEquipment._championMaxPointVal > _championEquipment._championCurrentPointVal)
            {
                ++weaponTypePoint;//this is not updating
                ++_championEquipment._championCurrentPointVal;
                weaponPointTxt.text = weaponTypePoint.ToString();//also this idk why|| Should also be on a diff script for UI UPDATES

            }
        }
        void SubtractWeaponStat(int weaponTypePoint, TextMeshProUGUI weaponPointTxt)
        {
            if (_championEquipment._championCurrentPointVal > 0 && weaponTypePoint > 0)
            {
                --weaponTypePoint;
                --_championEquipment._championCurrentPointVal;
                weaponPointTxt.text = weaponTypePoint.ToString();//also this idk why|| Should also be on a diff script for UI UPDATES
            }
        }
    */
}
