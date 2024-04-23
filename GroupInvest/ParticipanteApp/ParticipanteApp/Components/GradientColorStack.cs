using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ParticipanteApp.Components
{
    public class GradientColorStack : StackLayout
    {
        // Necessário criar dessa forma para ser uma propriedade que pode ser usada com Binding
        public static readonly BindableProperty StartColorProperty = BindableProperty.Create(nameof(StartColor), typeof(Color), typeof(Color), null);
        public static readonly BindableProperty EndColorProperty = BindableProperty.Create(nameof(StartColor), typeof(Color), typeof(Color), null);

        public Color StartColor
        {
            get => (Color)GetValue(StartColorProperty);
            set => SetValue(StartColorProperty, value);
        }

        public Color EndColor
        {
            get => (Color)GetValue(EndColorProperty);
            set => SetValue(EndColorProperty, value);
        }
    }
}
