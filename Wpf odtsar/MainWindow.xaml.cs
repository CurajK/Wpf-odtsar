using System.Configuration;
using System.Diagnostics;
using System.Runtime.Serialization;
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
using System.Windows.Threading;

namespace Wpf_odtsar
{


    public partial class MainWindow : Window
    {
        int ticker = 1;
        double playerReaction = 0;
        bool TimersTicking = false;

        private readonly DispatcherTimer _lightTimer = new();
        private readonly DispatcherTimer _reactTimer = new();
        private Stopwatch ReactionCounter = new Stopwatch();

        private Random _randomStarter = new Random();

        List<Ellipse> balls;
        public MainWindow()
        {
            InitializeComponent();

            balls = new List<Ellipse>() { _0, _1, _2, _3, _4 };
            _lightTimer.Interval = TimeSpan.FromSeconds(1);
            _lightTimer.Tick += _lightTimer_Tick;
            _reactTimer.Interval = TimeSpan.FromMilliseconds(1);
            _reactTimer.Tick += _reactTimer_Tick;
        }

        private void _reactTimer_Tick(object? sender, EventArgs e)
        {
            TimerLable.Text = ReactionCounter.Elapsed.Seconds + ":" + ReactionCounter.Elapsed.Milliseconds + "s";
        }
        private void _lightTimer_Tick(object? sender, EventArgs e)
        {
            balls[ticker].Fill = Brushes.DarkGreen;
            ticker++;

            if (ticker == balls.Count)
            {
                _lightTimer.Stop();
                RandomStarter();

            }
        }
        private async void RandomStarter()
        {
            int delay = 0;
            delay = _randomStarter.Next(1000, 3001);

            await Task.Delay(delay);
            ReactionCounter.Start(); //fffffffffffff
            _reactTimer.Start();
            foreach (var item in balls)
            {
                item.Fill = Brushes.DarkRed;
            }
            TimersTicking = true;
        }



        private void aftermatch()
        {

            TimerLable.Text = "0:000";
            TimersTicking = false;
            ticker = 0;
            playerReaction = ReactionCounter.Elapsed.TotalSeconds;
            ReationResults();
        }
        private void ReationResults()
        {
            string whatYouveGot = string.Empty;

            if (playerReaction > 1.0)
            {
                whatYouveGot = "You did horrible";
            }
            else if (playerReaction > 0.75)
            {
                whatYouveGot = "not THAT bad";
            }
            else if (playerReaction > 0.5)
            {
                whatYouveGot = "You did good";
            }
            else if (playerReaction > 0.3)
            {
                whatYouveGot = " you are pretty good";
            }
            else if (playerReaction > 0.2)
            {
                whatYouveGot = " you are just the GOAT!";
            }
            else if (playerReaction > 0.1)
            {
                whatYouveGot = " HOW!?";
            }
            else
            {
                whatYouveGot = "uliaty start";
            }

            MessageBox.Show(whatYouveGot);
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            playerReaction = 0;
            

            ReactionCounter.Reset();
            Time_Shower.Content = ReactionCounter.Elapsed.Seconds + ":" + ReactionCounter.Elapsed.Milliseconds + "s";

            _lightTimer.Start();
            balls[0].Fill = Brushes.DarkGreen;

        }

        private void GoButton_Click(object sender, RoutedEventArgs e)
        {
            if (TimersTicking)
            {
                ReactionCounter.Stop();
                _reactTimer.Stop();
                Time_Shower.Content = ReactionCounter.Elapsed.Seconds + ":" + ReactionCounter.Elapsed.Milliseconds + "s";
                aftermatch();

                return;
            }



            _lightTimer.Stop();
            _reactTimer.Stop();
            ReactionCounter.Reset();

            TimersTicking = false;
            ticker = 1;

            foreach (var item in balls)
            {
                item.Fill = Brushes.Gray;
            }

            MessageBox.Show(" Uliaty štart! Klikol si priskoro!");
        }
    }
}

            

            
        
    



                
     