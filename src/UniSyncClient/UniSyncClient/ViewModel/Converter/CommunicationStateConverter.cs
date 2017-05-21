using System;
using System.Globalization;
using System.ServiceModel;
using Xamarin.Forms;

namespace UniSyncClient.ViewModel.Converter
{
    public class CommunicationStateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value as CommunicationState? ?? CommunicationState.Created)
            {
                case CommunicationState.Closed:
                    return "Połączenie zakończone";
                case CommunicationState.Closing:
                    return "Kończenie połączenia...";
                case CommunicationState.Created:
                    return "Gotowy do połączenia";
                case CommunicationState.Faulted:
                    return "Połączenie przerwane";
                case CommunicationState.Opened:
                    return "Połączony";
                case CommunicationState.Opening:
                    return "Łączenie...";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}