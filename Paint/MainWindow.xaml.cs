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
using Microsoft.Win32;
using System.IO;


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

            BtnSave.Click += BtnSave_Click;
            BtnOpen.Click += BtnOpen_Click;

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

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Сохранить рисунок";
            saveFileDialog.Filter = "PNG Image (*.png)|*.png|JPEG Image (*.jpg)|*.jpg|BMP Image (*.bmp)|*.bmp";

            if (saveFileDialog.ShowDialog() == true)
            {
                int width = (int)DrawingCanvas.ActualWidth;
                int height = (int)DrawingCanvas.ActualHeight;

                if (width == 0 || height == 0) return;

                RenderTargetBitmap renderBitmap = new RenderTargetBitmap(width, height, 96d, 96d, System.Windows.Media.PixelFormats.Default);

                renderBitmap.Render(DrawingCanvas);

                BitmapEncoder encoder;

                string extension = System.IO.Path.GetExtension(saveFileDialog.FileName).ToLower();

                switch (extension)
                {
                    case ".jpg":
                    case ".jpeg":
                        encoder = new JpegBitmapEncoder();
                        break;
                    case ".bmp":
                        encoder = new BmpBitmapEncoder();
                        break;
                    default:
                        encoder = new PngBitmapEncoder();
                        break;
                }

                encoder.Frames.Add(BitmapFrame.Create(renderBitmap));

                using (FileStream fileStream = File.Create(saveFileDialog.FileName))
                {
                    encoder.Save(fileStream);
                }
            }

        }

        private void BtnOpen_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}