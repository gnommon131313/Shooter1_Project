using UnityEngine;
using Zenject;
using Cinemachine;
using System;
using System.Collections.Generic;
using TMPro;

public class SceneGameplayInstaller : MonoInstaller<SceneGameplayInstaller>
{
    [SerializeField] private List<Unit> _unitPrefabs;
    [SerializeField] private List<Weapon> _weaponPrefabs;
    [SerializeField] private List<Projectile> _bulletPrefabs;

    [SerializeField] private List<Transform> _spawnPoints;

    [SerializeField] private CinemachineVirtualCamera _virtualCamera;

    public override void InstallBindings()
    {
        Signals();
        Factoryes();
        Handlers();
        Uncategorized();
        Testing();
    }

    private void Signals()
    {
        SignalBusInstaller.Install(Container);

        Container.DeclareSignal<HealthChangedSignal>();
    }

    private void Factoryes()
    {
        int i =0;
        foreach (var unit in _unitPrefabs)
        {
            Container.BindFactory<Unit, Unit.Factory>()
                .FromComponentInNewPrefab(unit)
                .UnderTransform(_spawnPoints[i])
                .AsCached();
            i++;
        }

        foreach (var weapon in _weaponPrefabs)
        {
            Container.BindFactory<Weapon, Weapon.Factory>()
                .FromComponentInNewPrefab(weapon).AsCached();
        }

        foreach (var bullet in _bulletPrefabs)
        {
            Container.BindFactory<Projectile, Projectile.Factory>()
                .FromMonoPoolableMemoryPool(
                    x => x.WithInitialSize(2)
                    .FromComponentInNewPrefab(bullet)
                    .UnderTransformGroup("BulletPool"));
        }
    }

    private void Handlers()
    {
        Container.BindInterfacesAndSelfTo<MovementHandlerCharacterController>().AsTransient();
        Container.BindInterfacesAndSelfTo<MovementHandlerRigidbody>().AsTransient();
        Container.BindInterfacesAndSelfTo<RotateHandler>().AsTransient();
        Container.BindInterfacesAndSelfTo<HealthHandler>().AsTransient();
    }

    private void Uncategorized()
    {
        Container.BindInstance(_virtualCamera);
        Container.QueueForInject(_virtualCamera);
    }

    private void Testing()
    {
        //// BindInterfacesTo and BindInterfacesAndSelfTo
        //// 1 - ����� ����������� ������ ��������� ������ ���������, ������� ������������ � ����, �� ���� ����� ���
        //Container.Bind<IInitializable>().To<DesktopInput>().AsSingle(); // ��������� ���� ���������� �� ���
        //Container.Bind<IDisposable>().To<DesktopInput>().AsSingle(); // ��������� ���� ���������� �� ���
        //// 2 - ����� ��������� ��� ����������, ������� ������������ � ����, �� ���� ����� ���
        //Container.BindInterfacesTo<DesktopInput>(); // ���� ���� ����������� ���� �� ���� ���
        //Container.Bind<DesktopInput>().AsSingle(); // ���� ������ ����, ����� ��������� ����� ���� ����� �� ���� ���������
        //// 3 - ����� ������� ��� ���������� ����, ������� ������������ � ����, �� ���� ����� ��� + ��������� ��� ���
        //Container.BindInterfacesAndSelfTo<DesktopInput>();

        //// ��� ���������� ��������� ������ ����  -   public Tester(List<Weapon> weapons)   - � ��   -   public Tester(Weapon weapon)  
        //Container.Bind<Weapon>().To<Gun1>().FromInstance(_gun1);
        //Container.Bind<Weapon>().To<Gun2>().FromInstance(_gun2);

        //// WithId
        //Container.Bind<Weapon>().WithId("Gun111").To<Gun1>().FromInstance(_gun1);
        //Container.Bind<Weapon>().WithId("Gun222").To<Gun2>().FromInstance(_gun2);

        //// When
        //Container.Bind<Weapon>().To<Gun1>().FromInstance(_gun1).When(x => x.ObjectType == typeof(Player));
        //Container.Bind<Weapon>().To<Gun1>().FromInstance(_gun1).When(x => x.ObjectType == typeof(Projectile));

        //// WhenInjectedInto
        //Container.Bind<Weapon>().To<Gun1>().FromInstance(_gun1).WhenInjectedInto<Player>();
        //Container.Bind<Weapon>().To<Gun1>().FromInstance(_gun1).WhenInjectedInto<Projectile>();
    }
}
