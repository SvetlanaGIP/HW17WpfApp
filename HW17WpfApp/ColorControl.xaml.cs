using System;
using System.Collections.Generic;
using System.Linq;
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

namespace HW17WpfApp
{
    /// <summary>
    /// Логика взаимодействия для ColorControl.xaml
    /// </summary>
    public partial class ColorControl : System.Windows.Controls.UserControl
    {
        public static DependencyProperty ColorProperty;
        public static DependencyProperty RedProperty;
        public static DependencyProperty GreenProperty;
        public static DependencyProperty BlueProperty;

        private static readonly RoutedEvent ColorChangedEvent;

        public ColorControl()
        {
            InitializeComponent();
        }
        static ColorControl()
        {
            ColorProperty = DependencyProperty.Register("Color", typeof(Color), typeof(ColorControl),
                new FrameworkPropertyMetadata(Colors.Black, new PropertyChangedCallback(OnColorChaned)));
            RedProperty = DependencyProperty.Register("Red", typeof(Color), typeof(ColorControl),
                new FrameworkPropertyMetadata(Colors.Black, new PropertyChangedCallback(OnColorRGBChaned)));
            GreenProperty = DependencyProperty.Register("Green", typeof(Color), typeof(ColorControl),
                new FrameworkPropertyMetadata(Colors.Black, new PropertyChangedCallback(OnColorRGBChaned)));
            BlueProperty = DependencyProperty.Register("Blue", typeof(Color), typeof(ColorControl),
                new FrameworkPropertyMetadata(Colors.Black, new PropertyChangedCallback(OnColorRGBChaned)));

            ColorChangedEvent = EventManager.RegisterRoutedEvent("ColorChanged", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<Color>), typeof(ColorControl));
        }
        public Color Color
        {
            get { return (Color)GetValue(ColorProperty); }  
            set { SetValue(ColorProperty, value); }
        }
        public byte Red
        {
            get { return (byte)GetValue(RedProperty); }
            set { SetValue(RedProperty, value); }
        }
        public byte Green
        {
            get { return (byte)GetValue(GreenProperty); }
            set { SetValue(RedProperty, value); }
        }
        public byte Blue
        {
            get { return (byte)GetValue(BlueProperty); }
            set { SetValue(RedProperty, value); }
        }

        private static void OnColorRGBChaned(DependencyObject sender,
            DependencyPropertyChangedEventArgs e)
        {
            ColorControl colorPicker =(ColorControl)sender;
            Color color = colorPicker.Color;
            if (e.Property == RedProperty)
                color.R=(byte)e.NewValue;
            else if (e.Property == GreenProperty)
                color.G = (byte)e.NewValue;
            else if (e.Property == BlueProperty)
                color.B = (byte)e.NewValue;

            colorPicker.Color = color;
        }
        private static void OnColorChaned(DependencyObject sender,
            DependencyPropertyChangedEventArgs e)
        {
            Color newColor = (Color)e.NewValue;
            ColorControl colorcontrol = (ColorControl)sender;
            colorcontrol.Red = newColor.R;
            colorcontrol.Green = newColor.R;
            colorcontrol.Blue = newColor.R;

            RoutedPropertyChangedEventArgs<Color> args = new RoutedPropertyChangedEventArgs<Color>(colorcontrol.Color, newColor);
            args.RoutedEvent = ColorChangedEvent;
            colorcontrol.RaiseEvent(args);
        }

        public event RoutedPropertyChangedEventHandler<Color> ColorChanged
        {
            add { AddHandler(ColorChangedEvent, value); }
            remove { RemoveHandler(ColorChangedEvent, value); }
        }
    }
}
