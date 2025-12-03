using System.Data.SqlTypes;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] GameManagerIC gameManager;

    public bool _isReady;
    public GameObject _champion;
}
