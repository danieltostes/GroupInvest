using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace ParticipanteApp.Components
{
    public class CustomActivityIndicator : ContentView
    {
        private const string AnimationName = "ActivityIndicatorRotation";

        public static readonly BindableProperty IsRunningProperty = BindableProperty.Create(nameof(IsRunning), typeof(bool), typeof(CustomActivityIndicator), default(bool));

        private readonly Animation _animation;
        private Image _innerImage;
        private Image _outerImage;

        public bool IsRunning
        {
            get => (bool)GetValue(IsRunningProperty);
            set => SetValue(IsRunningProperty, value);
        }

        public CustomActivityIndicator()
        {
            InitView();
            _animation = new Animation(v => _outerImage.Rotation = v, 0, 360);
        }

        private void InitView()
        {
            _innerImage = new Image { Source = "imagem_activity.png" };
            _outerImage = new Image { Source = "imagem_externa_activity.png" };

            Content = CreateContent();
            HorizontalOptions = LayoutOptions.Center;
            VerticalOptions = LayoutOptions.Center; ;
            Scale = 0;
        }

        private View CreateContent()
        {
            var grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Star });

            _innerImage.SetValue(Grid.RowProperty, 0);
            _innerImage.HorizontalOptions = LayoutOptions.Center;
            _innerImage.VerticalOptions = LayoutOptions.Center;
            grid.Children.Add(_innerImage);

            _outerImage.SetValue(Grid.RowProperty, 0);
            _outerImage.HorizontalOptions = LayoutOptions.Center;
            _outerImage.VerticalOptions = LayoutOptions.Center;
            grid.Children.Add(_outerImage);

            return grid;
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == nameof(IsRunning) && IsEnabled)
            {
                if (IsRunning) StartAnimation();
                else StopAnimation();
            }

            if (propertyName == nameof(IsEnabled) && !IsEnabled && IsRunning)
                StopAnimation();
        }

        private void StartAnimation()
        {
            this.ScaleTo(1, 500);

            _animation.Commit(this, AnimationName, 16, 1000, Easing.Linear, (v, c) => Rotation = 0, () => true);
        }

        private async void StopAnimation()
        {
            await this.ScaleTo(0, 500);
            this.AbortAnimation(AnimationName);
        }
    }
}
