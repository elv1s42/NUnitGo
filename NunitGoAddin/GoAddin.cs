using NUnit.Core.Extensibility;

namespace NunitGoAddin
{
    [NUnitAddin(Name = "NunitGo Adapter", Type = ExtensionType.Core)]
    public class GoAddin : IAddin
    {
        public bool Install(IExtensionHost host)
        {
            var listeners = host.GetExtensionPoint("EventListeners");
            listeners.Install(new NunitGoEventListener());
            return true;
        }
    }
}
