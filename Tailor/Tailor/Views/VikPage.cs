using SkiaSharp;
using SkiaSharp.Views.Forms;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Tailor.Models;
using Xamarin.Forms;

namespace Tailor.Views
{
    public class VikPage : ContentPage
    {

        int n = 15;
        //private Button button;
        private SKCanvasView _sKCanvasView;
        //private ListView _listView;
        private Client _client;

        string _path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "DB.db");
        //private float clientt;
        private readonly Dictionary<long, SKPath> temporaryPaths = new Dictionary<long, SKPath>();
        private readonly List<SKPath> paths = new List<SKPath>();

        public VikPage(Client clientt)
        {

            _client = clientt;
            this.Title = "Выкройка";
            var data = new SQLiteConnection(_path);

            StackLayout stackLayout = new StackLayout();
            //StackLayout sl = new StackLayout();
            //_listView = new ListView();
            //_listView.ItemsSource = data.Table<Client>().OrderBy(l => l.Name).ToList();
            //_listView.ItemSelected += _listView_ItemSelected;

            _sKCanvasView = new SKCanvasView();

            _sKCanvasView.PaintSurface += OnPainting;
            _sKCanvasView.EnableTouchEvents = true;
            _sKCanvasView.Touch += OnTouch;
            _sKCanvasView.VerticalOptions = LayoutOptions.FillAndExpand;
            stackLayout.Children.Add(_sKCanvasView);

            //button = new Button();
            //button.BorderColor = Color.LightSeaGreen;
            //button.CornerRadius = 25;
            //button.BorderWidth = 1;
            //button.BackgroundColor = Color.YellowGreen;
            //button.Text = "Выкройка";
            ////button.Clicked += _button_Clicked2;
            //stackLayout.Children.Add(button);

            Content = stackLayout;

        }
        private void OnPainting(object sender, SKPaintSurfaceEventArgs e)
        {
            // CLEARING THE SURFACE

            // we get the current surface from the event args
            var surface = e.Surface;
            // then we get the canvas that we can draw on
            var canvas = surface.Canvas;
            // clear the canvas / view
            canvas.Clear(SKColors.White);

            //var data = new SQLiteConnection(_path);
            //Client client = new Client();

            /*
                  using (var image = surface.Snapshot())
                  using (var datata = image.Encode(SKEncodedImageFormat.Png, 80))
                  using (var stream = File.OpenWrite(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "1.png")))
                  {
                      // save the data to a stream
                      datata.SaveTo(stream);
                  }
                  */

            //// DRAWING SHAPES

            //// create the paint for the filled circle
            //var circleFill = new SKPaint
            //{
            //	IsAntialias = true,
            //	Style = SKPaintStyle.Fill,
            //	Color = SKColors.Blue
            //};
            //// draw the circle fill
            //canvas.DrawCircle(100, 100, 40, circleFill);

            //// create the paint for the circle border
            //var circleBorder = new SKPaint
            //{
            //	IsAntialias = true,
            //	Style = SKPaintStyle.Stroke,
            //	Color = SKColors.Red,
            //	StrokeWidth = 5
            //};
            //// draw the circle border
            //canvas.DrawCircle(100, 100, 40, circleBorder);

            // DRAWING PATHS

            // create the paint for the path
            var pathStroke = new SKPaint
            {
                IsAntialias = true,
                Style = SKPaintStyle.Stroke,
                Color = SKColors.Green,
                StrokeWidth = 5
            };

            // create a path

            var path = new SKPath();
            path.MoveTo(100, 100);
            path.LineTo(100, 100 + float.Parse(_client.DlinaIzdeliya) * n);
            path.MoveTo(100, 100 + float.Parse(_client.DlinaIzdeliya) * n);
            path.LineTo(100 + float.Parse(_client.Grud) / 4 * n, 100 + float.Parse(_client.DlinaIzdeliya) * n);
            path.MoveTo(100 + float.Parse(_client.Grud) / 4 * n, 100 + float.Parse(_client.DlinaIzdeliya) * n);
            path.LineTo(100 + float.Parse(_client.Grud) / 4 * n, 100);
            path.MoveTo(100 + float.Parse(_client.Grud) / 4 * n, 100);
            path.LineTo(100, 100);

            path.MoveTo(100, 100 + ((float.Parse(_client.Sheya) / 6) + 1) * n);

            path.ArcTo(100 + (float.Parse(_client.Sheya) / 6) * n, 100 + ((float.Parse(_client.Sheya) / 6) + 1) * n, 0, SKPathArcSize.Small, SKPathDirection.CounterClockwise, 100 + (float.Parse(_client.Sheya) / 6) * n, 100);

            path.MoveTo(100 + (float.Parse(_client.Sheya) / 6) * n, 100);
            path.LineTo(100 + float.Parse(_client.Plecho) * n + (float.Parse(_client.Sheya) / 6) * n, 100 + 2 * n);
            path.MoveTo(100 + float.Parse(_client.Plecho) * n + (float.Parse(_client.Sheya) / 6) * n, 100 + 2 * n);
            path.ArcTo(100 + float.Parse(_client.Plecho), 100 + (float.Parse(_client.Grud) / 6 + 5) * n, 0, SKPathArcSize.Small, SKPathDirection.CounterClockwise, 100 + float.Parse(_client.Grud) / 4 * n, 100 + (float.Parse(_client.Grud) / 6 + 5) * n);

            // draw the path
            canvas.DrawPath(path, pathStroke);

            //// DRAWING TEXT

            //// create the paint for the text
            //var textPaint = new SKPaint
            //{
            //    IsAntialias = true,
            //    Style = SKPaintStyle.Fill,
            //    Color = SKColors.Orange, 
            //    TextSize = 80
            //};
            //// draw the text (from the baseline)
            //canvas.DrawText("Tailor", 60, 160 + 80, textPaint);

            // DRAWING TOUCH PATHS

            // create the paint for the touch path

            var touchPathStroke = new SKPaint
            {
                IsAntialias = true,
                Style = SKPaintStyle.Stroke,
                Color = SKColors.Purple,
                StrokeWidth = 5
            };

            // draw the paths
            foreach (var touchPath in temporaryPaths)
            {
                canvas.DrawPath(touchPath.Value, touchPathStroke);
            }
            foreach (var touchPath in paths)
            {
                canvas.DrawPath(touchPath, touchPathStroke);
            }
        }
        private void OnTouch(object sender, SKTouchEventArgs e)
        {
            switch (e.ActionType)
            {
                case SKTouchAction.Pressed:
                    // start of a stroke
                    var p = new SKPath();
                    p.MoveTo(e.Location);
                    temporaryPaths[e.Id] = p;
                    break;
                case SKTouchAction.Moved:
                    // the stroke, while pressed
                    if (e.InContact && temporaryPaths.TryGetValue(e.Id, out
                        var moving))
                        moving.LineTo(e.Location);
                    break;
                case SKTouchAction.Released:
                    // end of a stroke
                    if (temporaryPaths.TryGetValue(e.Id, out
                        var releasing))
                        paths.Add(releasing);
                    temporaryPaths.Remove(e.Id);
                    break;
                case SKTouchAction.Cancelled:
                    // we don't want that stroke
                    temporaryPaths.Remove(e.Id);
                    break;
            }

            // update the UI
            if (e.InContact)
                ((SKCanvasView)sender).InvalidateSurface();

            // we have handled these events
            e.Handled = true;
        }
        private void _listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            _client = (Client)e.SelectedItem;
        }

    }
}