using Avalonia.Controls;
using System.ComponentModel;

namespace Vizprog_RGR.Views.Shapes
{
    public partial class NOT : GateBase, IGate, INotifyPropertyChanged
    {
        public override int TypeId => 2;

        public override UserControl GetSelf() => this;
        protected override IGate GetSelfI => this;
        protected override int[][] Sides => new int[][] {
            System.Array.Empty<int>(),
            new int[] { 0 },
            new int[] { 1 },
            System.Array.Empty<int>()
        };

        protected override void Init() => InitializeComponent();

        /*
         * Мозги
         */

        public void Brain(ref bool[] ins, ref bool[] outs) => outs[0] = !ins[0];
    }
}
