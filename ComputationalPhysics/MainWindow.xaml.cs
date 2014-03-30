using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ComputationalPhysics {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            double angle = 80 * Math.PI / 180;
            Func<double, double, double> generator = (x, y) => NoFrictionProjectile.InitialSpeed(new Primitives.Vec2(x, y), angle, 200);
            var r = Visualizer.Visualize(generator, 0, 20, 0, 20, new System.Drawing.Size(500, 500));
            var source = loadBitmap(r); 
            this.img.Source = source;
        }

        [DllImport("gdi32")]
        static extern int DeleteObject(IntPtr o);

        public static BitmapSource loadBitmap(System.Drawing.Bitmap source) {
            IntPtr ip = source.GetHbitmap();
            BitmapSource bs = null;
            try {
                bs = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(ip,
                   IntPtr.Zero, Int32Rect.Empty,
                   System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());
            } finally {
                DeleteObject(ip);
            }
            
            return bs;
            
        }
    }


}
