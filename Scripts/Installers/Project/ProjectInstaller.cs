using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller<ProjectInstaller>
{
    public override void InstallBindings()
    {
        BindInput();
    }

    private void BindInput()
    {
        Container.BindInterfacesAndSelfTo<InputHandler>().AsSingle();

        if (SystemInfo.deviceType == DeviceType.Desktop)
            Container.Bind<IInput>().To<DesktopInput>().AsSingle();
        else if (SystemInfo.deviceType == DeviceType.Handheld)
            Container.Bind<IInput>().To<HandheldInput>().AsSingle();
        else if (SystemInfo.deviceType == DeviceType.Console)
            Container.Bind<IInput>().To<DesktopInput>().AsSingle();
    }
}
