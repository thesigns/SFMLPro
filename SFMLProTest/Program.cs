using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFMLPro;
using SFML.Graphics;
using SFML.System;
using SFML.Window;


namespace SFMLProTest {
    internal class Program {

        private static void Main(string[] args) {

            VideoMode mode = new VideoMode(VideoMode.DesktopMode.Width / 2, VideoMode.DesktopMode.Height / 2);
            ContextSettings contextSettings = new ContextSettings();
            contextSettings.AntialiasingLevel = 8;
            RenderWindow window = new RenderWindow(mode, "SFMLPro Test", Styles.Default, contextSettings);
            window.SetVerticalSyncEnabled(true);

            window.Closed += (sender, args) => window.Close();

            Polyline line1 = new Polyline();
            Polyline line2 = new Polyline();
            Polyline line3 = new Polyline();

            line1.AddVertex(new Vertex(new Vector2f(100, 44), Color.Cyan), 0.5f);
            line1.AddVertex(new Vertex(new Vector2f(33, 100), Color.Red), 1);
            line1.AddVertex(new Vertex(new Vector2f(200, 78), Color.Yellow), 2);
            line1.AddVertex(new Vertex(new Vector2f(100, 200), Color.Green), 4);
            line1.AddVertex(new Vertex(new Vector2f(400, 400), Color.White), 8);
            
            line2.AddVertex(new Vertex(new Vector2f(500, 500), Color.Red), 16);
            line2.AddVertex(new Vertex(new Vector2f(520, 480), Color.Red), 16);
            line2.AddVertex(new Vertex(new Vector2f(480, 460), Color.Red), 16);
            line2.AddVertex(new Vertex(new Vector2f(520, 440), Color.Red), 16);
            line2.AddVertex(new Vertex(new Vector2f(480, 420), Color.Red), 16);
            line2.AddVertex(new Vertex(new Vector2f(520, 400), Color.Red), 16);
            
            line3.AddVertex(new Vertex(new Vector2f(400, 200), Color.Blue), 10);
            line3.AddVertex(new Vertex(new Vector2f(500, 200), Color.Blue), 10);
            line3.AddVertex(new Vertex(new Vector2f(500, 300), Color.Blue), 10);
            line3.AddVertex(new Vertex(new Vector2f(400, 300), Color.Blue), 10);
            line3.AddVertex(new Vertex(new Vector2f(400, 200), Color.Blue), 10);
            line3.AddVertex(new Vertex(new Vector2f(500, 200), Color.Blue), 10);



            while (window.IsOpen) {
                window.DispatchEvents();
                window.Clear(Color.Black);

                window.Draw(line1);
                window.Draw(line2);
                window.Draw(line3);

                window.Display();

                if (Keyboard.IsKeyPressed(Keyboard.Key.Escape)) {
                    window.Close();
                }




            }

        }

    }
}
