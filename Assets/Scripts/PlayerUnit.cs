using UnityEngine;

public class PlayerUnit : MonoBehaviour
{
    public bool inUse { get; private set; }
    public void SetInUse(bool _isInUse) => inUse = _isInUse;

    private PlayerBase controllingPlayer;
    public PlayerBase GetControllingPlayer() => controllingPlayer;
    public void SetControllingPlayer(PlayerBase _player) => controllingPlayer = _player;
}
