using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Assignment.Models
{
    public class AudioItem : INotifyPropertyChanged
    {
        private Guid _id;
        public Guid Id
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged(nameof(Id));
                }
            }
        }

        private byte[] _audioData;
        public byte[] AudioData
        {
            get { return _audioData; }
            set
            {
                if (_audioData != value)
                {
                    _audioData = value;
                    OnPropertyChanged(nameof(AudioData));
                }
            }
        }

        private DateTime _timestamp;
        public DateTime Timestamp
        {
            get { return _timestamp; }
            set
            {
                if (_timestamp != value)
                {
                    _timestamp = value;
                    OnPropertyChanged(nameof(Timestamp));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}