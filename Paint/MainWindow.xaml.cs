using Paint.elements.classes;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Paint
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ITool _currentTool;
        public MainWindow()
        {
            InitializeComponent();

            _currentTool = new PencilTool();

            DrawingCanvas.MouseLeftButtonDown += DrawingCanvas_MouseLeftButtonDown;
            DrawingCanvas.MouseMove += DrawingCanvas_MouseMove;
            DrawingCanvas.MouseLeftButtonUp += DrawingCanvas_MouseLeftButtonUp;

            BtnPencil.Click += BtnPencil_Click;
            BtnEraser.Click += BtnEraser_Click;
            BtnClear.Click += BtnClear_Click;

        }

        //Обработчик событий мыши на холсте
        private void DrawingCanvas_MouseLeftButtonDown(object sender, MouseEventArgs e)
        {
            Point position = e.GetPosition(DrawingCanvas);
            DrawingCanvas.CaptureMouse();
            _currentTool.OnMouseDown(position, DrawingCanvas);

        }

        private void DrawingCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (DrawingCanvas.IsMouseCaptured)
            {
                Point position = e.GetPosition(DrawingCanvas);
                _currentTool.OnMouseMove(position, DrawingCanvas);
            }
        }

        private void DrawingCanvas_MouseLeftButtonUp(object sendet, MouseEventArgs e)
        {
            DrawingCanvas.ReleaseMouseCapture();

            Point position = e.GetPosition(DrawingCanvas);
            _currentTool.OnMouseUp(position, DrawingCanvas);
        }
        
        
        //Обработка кнопок интерфейса

        private void BtnPencil_Click(object sender, RoutedEventArgs e)
        {
            _currentTool = new PencilTool();
        }

        private void BtnEraser_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("WIP!");
             _currentTool = new EraserTool();
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            DrawingCanvas.Children.Clear();
        }
    }
}