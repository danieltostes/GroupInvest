using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ParticipanteApp.Components
{
    public class CustomFrame : Frame
    {
        public static readonly BindableProperty ElevationProperty = BindableProperty.Create(nameof(Elevation), typeof(float), typeof(float), null);

        public float Elevation
        {
            get => (float)GetValue(ElevationProperty);
            set => SetValue(ElevationProperty, value);
        }
    }
}
