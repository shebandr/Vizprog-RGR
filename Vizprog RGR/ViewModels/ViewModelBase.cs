using ReactiveUI;
using Vizprog_RGR.Models;

namespace Vizprog_RGR.ViewModels
{
    public class ViewModelBase : ReactiveObject
    {
        public readonly static Mapper map = new();
        private static Project? current_proj;
        protected static Project? CurrentProj
        {
            get => current_proj;
            set
            {
                if (value == null) return;
                current_proj = value;
                map.current_scheme = value.GetFirstScheme();
            }
        }


        public static Project? TopSecretGetProj() => current_proj;
    }
}
