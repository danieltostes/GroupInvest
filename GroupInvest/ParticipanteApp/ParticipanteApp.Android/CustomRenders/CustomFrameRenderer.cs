#pragma warning disable CS0612 // Type or member is obsolete
using ParticipanteApp.Components;
using ParticipanteApp.Droid.CustomRenders;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;

[assembly: ExportRenderer(typeof(CustomFrame), typeof(CustomFrameRenderer))]
#pragma warning restore CS0612 // Type or member is obsolete

namespace ParticipanteApp.Droid.CustomRenders
{
    [System.Obsolete]
    public class CustomFrameRenderer : Xamarin.Forms.Platform.Android.AppCompat.FrameRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);
            var element = e.NewElement as CustomFrame;

            if (element == null)
                return;

            if (element.HasShadow)
            {
                Elevation = element.Elevation;

                TranslationZ = 0.0f;
                SetZ(element.Elevation);
            }
        }
    }
}