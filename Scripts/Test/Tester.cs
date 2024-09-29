using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Tester : MonoBehaviour
{
    [SerializeField] private int _damageAndHeal = 10;

    [SerializeField] private List<Player> _players;
    [SerializeField] private List<Dummy> _dummys;

    private List<Unit.Factory> _unitFactory;

    [Inject]
    private void Construct(List<Unit.Factory> unitFactory)
    {
        _unitFactory = unitFactory;
    }

    private void Start()
    {
        PlayerCreate();
        DummyCreate();
    }

    [ContextMenu("PlayerCreate")]
    private void PlayerCreate()
    {
        _players.RemoveAll(x => x == null);

        for (int i = 0; i < 1; i++)
            _players.Add((Player)_unitFactory[0].Create());
    }

    [ContextMenu("PlayerDamagable")]
    private void PlayerDamagable()
    {
        foreach (var player in _players) 
            player.HealthHandler.TakeDamage(_damageAndHeal);
    }

    [ContextMenu("PlayerHealeble")]
    private void PlayerHealeble()
    {
        foreach (var player in _players)
            player.HealthHandler.TakeHeal(_damageAndHeal);
    }

    [ContextMenu("PlayerJump")]
    private void PlayerJump()
    {
        InvokeRepeating("Jump", 1, 1);
    }

    [ContextMenu("DummyCreate")]
    private void DummyCreate()
    {
        _dummys.RemoveAll(x => x == null);

        for (int i = 0; i < 1; i++)
            _dummys.Add((Dummy)_unitFactory[1].Create());
    }

    [ContextMenu("DummyDamagable")]
    private void DummyDamagable()
    {
        foreach (var dummy in _dummys)
            dummy.HealthHandler.TakeDamage(_damageAndHeal);
    }

    [ContextMenu("DummyHealeble")]
    private void DummyHealeble()
    {
        foreach (var dummy in _dummys)
            dummy.HealthHandler.TakeHeal(_damageAndHeal);
    }

    [ContextMenu("DummyShoot")]
    private void DummyShoot()
    {
        foreach (var dummy in _dummys)
            dummy.Shoot();
    }




    //// Change Behavior
    //[ContextMenu("MovebleNullBehavior")]
    //private void Test1()
    //{
    //    foreach (var player in _players)
    //        player.MovementFacade.SetMovableBehaviour(new MovebleNullBehavior());
    //}
    //[ContextMenu("MovebleSimpleRelativelyCameraBehavior")]
    //private void Test2()
    //{
    //    foreach (var player in _players)
    //        player.MovementFacade.SetMovableBehaviour(new MovebleSimpleRelativelyCameraBehavior(player));
    //}



    //// Заполнение List<Player> при старте игры
    //// Заполнение через код = FromComponentInHierarchy | FromComponentsInHierarchy
    //// Заполнение через инстектор = ZenjectBinding (повесить на обьект)
    //[Inject] private List<Player> _players;
    //[ContextMenu("Test1")]
    //private void Test1()
    //{
    //    Debug.Log(_players.Count);
    //}
}
