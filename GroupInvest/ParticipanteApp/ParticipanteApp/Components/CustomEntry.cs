using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ParticipanteApp.Components
{
    public class CustomEntry : Entry
    {
        public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(Color), null);
        public static readonly BindableProperty BorderNoneProperty = BindableProperty.Create(nameof(BorderNone), typeof(bool), typeof(bool), null);

        public Color BorderColor
        {
            get => (Color)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }

        public bool BorderNone
        {
            get => (bool)GetValue(BorderNoneProperty);
            set => SetValue(BorderNoneProperty, value);
        }
    }
}
