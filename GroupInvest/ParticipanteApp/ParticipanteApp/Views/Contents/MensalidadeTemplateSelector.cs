using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ParticipanteApp.Views.Contents
{
    public class MensalidadeTemplateSelector : DataTemplateSelector
    {
        public DataTemplate Mensalidade { get; set; }
        public DataTemplate MensalidadePaga { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            return ((GroupInvest.App.Domain.Entidades.Mensalidade)item).DataPagamento.HasValue ? MensalidadePaga : Mensalidade;
        }
    }
}
