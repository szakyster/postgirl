using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Postgirl.Command;
using Postgirl.Models;
using Postgirl.Services;

namespace Postgirl.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public MainWindowViewModel(RequestService requestService, string url = "")
        {
            _requestService = requestService;
            _url = url;
            SendRequestCommand = new DelegateCommandAsync(SendRequest);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private string _url;
        private string _method = "GET";
        private readonly RequestService _requestService;
        private readonly bool _SendBlocked = false;

        public ObservableCollection<string> Methods => ["GET", "POST", "PUT"];

        public string Url
        {
            get => _url;
            set => SetField(ref _url, value);
        }

        public string Method
        {
            get => _method;
            set => SetField(ref _method, value);
        }

        public DelegateCommandAsync SendRequestCommand { get; }

        //---------methods

        private async Task SendRequest(object? parameter)
        {
            var request = new RequestModel()
            {
                Url = Url,
                Method = Method switch
                {
                    "GET" => HttpMethod.Get,
                    "POST" => HttpMethod.Post,
                    "PUT" => HttpMethod.Put,
                    _ => throw new ArgumentOutOfRangeException()
                }
            };

            _requestService.SendRequest(request);
        }

        //---------common
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
            {
                return false;
            }
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
