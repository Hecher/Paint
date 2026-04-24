using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Paint
{
    public interface ITool
    {
        void OnMouseDown(Point position, Canvas canvas);
        void OnMouseMove(Point position, Canvas canvas);
        void OnMouseUp(Point position, Canvas canvas);
    }
}
