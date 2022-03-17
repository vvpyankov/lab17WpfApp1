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

namespace lab17zadachaColorsWpfApp1
{
    /// <summary>
    /// Логика взаимодействия для ColorPicker.xaml
    /// </summary>
    public partial class ColorPicker : UserControl
    {
        //регистрация маршрутизируемого события изменения цвета
        public static readonly RoutedEvent ColorChangedEvent = EventManager.RegisterRoutedEvent(
                nameof(ColorChanged),
                RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<Color>),
                typeof(ColorPicker));
        //оболочка события для добавления (удаления) обработчиков
        public event RoutedPropertyChangedEventHandler<Color> ColorChanged
        {
            add { AddHandler(ColorChangedEvent, value); }
            remove { RemoveHandler(ColorChangedEvent, value); }
        }
        //регистрация свойства зависимости хранящее цвет (объект Color)
        public static readonly DependencyProperty MyColorProperty = DependencyProperty.Register(
            nameof(MyColor),
            typeof(Color),
            typeof(ColorPicker),
            new FrameworkPropertyMetadata(Colors.Black, new PropertyChangedCallback(OnColorChanged)));
        //регистрация свойства зависимости хранящее красную составляющую цвета
        public static readonly DependencyProperty MyRedProperty = DependencyProperty.Register(
            nameof(MyRed),
            typeof(byte),
            typeof(ColorPicker),
            new FrameworkPropertyMetadata(new PropertyChangedCallback(OnColorRGBChanged)));
        //регистрация свойства зависимости хранящее зеленую составляющую цвета
        public static readonly DependencyProperty MyGreenProperty = DependencyProperty.Register(
           nameof(MyGreen),
           typeof(byte),
           typeof(ColorPicker),
           new FrameworkPropertyMetadata(new PropertyChangedCallback(OnColorRGBChanged)));
        //регистрация свойства зависимости хранящее синюю составляющую цвета
        public static readonly DependencyProperty MyBlueProperty = DependencyProperty.Register(
           nameof(MyBlue),
           typeof(byte),
           typeof(ColorPicker),
           new FrameworkPropertyMetadata(new PropertyChangedCallback(OnColorRGBChanged)));
        //свойства цвета и составляющих
        public Color MyColor
        {
            get => (Color)GetValue(MyColorProperty);
            set => SetValue(MyColorProperty, value);
        }

        public byte MyRed
        {
            get => (byte)GetValue(MyRedProperty);
            set => SetValue(MyRedProperty, value);
        }

        public byte MyGreen
        {
            get => (byte)GetValue(MyGreenProperty);
            set => SetValue(MyGreenProperty, value);
        }

        public byte MyBlue
        {
            get => (byte)GetValue(MyBlueProperty);
            set => SetValue(MyBlueProperty, value);
        }
        //метод обратного вызова изменяющий значение MyColor
        private static void OnColorRGBChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ColorPicker colorPicker = (ColorPicker)d;
            Color color = colorPicker.MyColor;
            if (e.Property == MyRedProperty)
                color.R = (byte)e.NewValue;
            else if (e.Property == MyGreenProperty)
                color.G = (byte)e.NewValue;
            else if (e.Property == MyBlueProperty)
                color.B = (byte)e.NewValue;
            colorPicker.MyColor = color;
        }
        //метод обратного вызова изменяющий значение составляющих цвета

        private static void OnColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Color newColor = (Color)e.NewValue;
            ColorPicker colorPicker = (ColorPicker)d;
            colorPicker.MyRed = newColor.R;
            colorPicker.MyGreen = newColor.G;
            colorPicker.MyBlue = newColor.B;
            RoutedPropertyChangedEventArgs<Color> eventArgs = new RoutedPropertyChangedEventArgs<Color>(colorPicker.MyColor, newColor);
            eventArgs.RoutedEvent = ColorChangedEvent;
            colorPicker.RaiseEvent(eventArgs);
        }

        public ColorPicker()
        {
            InitializeComponent();
        }
    }
}
