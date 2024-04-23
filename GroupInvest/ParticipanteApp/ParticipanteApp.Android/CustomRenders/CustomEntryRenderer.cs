using Android.Graphics;
using Android.Graphics.Drawables;
using ParticipanteApp.Components;
using ParticipanteApp.Droid.CustomRenders;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

#pragma warning disable CS0612 // Type or member is obsolete
[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]
#pragma warning restore CS0612 // Type or member is obsolete

namespace ParticipanteApp.Droid.CustomRenders
{
    [Obsolete]
    public class CustomEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                CustomEntry customEntry = e.NewElement as CustomEntry;
                Xamarin.Forms.Color borderColor = customEntry.BorderColor;

                var nativeEditText = (global::Android.Widget.EditText)Control;
                var shape = new ShapeDrawable(new Android.Graphics.Drawables.Shapes.RectShape());
                shape.Paint.Color = borderColor.ToAndroid();
                shape.Paint.SetStyle(Paint.Style.Stroke);
                nativeEditText.Background = customEntry.BorderNone ? null : shape;
            }
        }
    }
}