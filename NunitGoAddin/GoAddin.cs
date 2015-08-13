using NUnit.Core.Extensibility;

namespace NunitGoAddin
{
    [NUnitAddin(Name = "NunitGo", Type = ExtensionType.Core)]
    public class GoAddin : IAddin
    {
        public bool Install(IExtensionHost host)
        {
            var listeners = host.GetExtensionPoint("EventListeners");
            if (listeners == null)
                return false;
            listeners.Install(new NunitGoEventListener());
            return true;
        }
    }
}
