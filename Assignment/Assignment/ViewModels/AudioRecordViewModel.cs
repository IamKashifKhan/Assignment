using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;
using Assignment.Interfaces;
using Assignment.Models;
using Plugin.AudioRecorder;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Assignment.ViewModels
{
    public class AudioRecordViewModel : BaseViewModel
    {
        private ObservableCollection<AudioItem> _audioItems;

        public ObservableCollection<AudioItem> AudioItems
        {
            get { return _audioItems; }
            set
            {
                if (_audioItems != value)
                {
                    _audioItems = value;
                    OnPropertyChanged(nameof(AudioItems));
                }
            }
        }

        private string _buttonText;

        public string ButtonText
        {
            get { return _buttonText; }
            set
            {
                if (_buttonText != value)
                {
                    _buttonText = value;
                    OnPropertyChanged(nameof(ButtonText));
                }
            }
        }

        private readonly AudioRecorderService audioRecorderService = new AudioRecorderService();

        private readonly AudioPlayer audioPlayer = new AudioPlayer();

        public ILocalCache localCache => DependencyService.Get<ILocalCache>();

        public ICommand RecordAudioCommand { get; }

        public ICommand PlayAudioCommand { get; }


        

        public AudioRecordViewModel()
        {
            Title = "Audio recorder";
            RecordAudioCommand = new Command(RecordAudio);
            PlayAudioCommand = new Command(PlayAudio);
            GetAudiofromCache();
            ButtonText = "Start Recording";
        }

        private void PlayAudio(object obj)
        {
            AudioItem audioItem = (AudioItem)obj;
            string tempFileName = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N") + ".wav");
            File.WriteAllBytes(tempFileName, audioItem.AudioData);
             audioPlayer.Play(tempFileName);
        }

        private async void RecordAudio(object obj)
        {
            var status = await Permissions.RequestAsync<Permissions.Microphone>();

            if (status != PermissionStatus.Granted)
                return;

            if (audioRecorderService.IsRecording)
            {
                ButtonText = "Start Recording";
                await audioRecorderService.StopRecording();
                try
                {
                    audioPlayer.Play(audioRecorderService.GetAudioFilePath());
                    var audiofilepath = audioRecorderService.GetAudioFilePath();
                    byte[] fileBytes = File.ReadAllBytes(audiofilepath);
                  //  await localCache.SaveItemInCache<byte[]>("test", fileBytes);
                    var newAudioItem = new AudioItem
                    {
                        Id = Guid.NewGuid(),
                        AudioData = fileBytes,
                       Timestamp = DateTime.Now
                    };

                    // Add the new AudioItem to the ObservableCollection
                    AudioItems.Add(newAudioItem);
                  
                    //await localCache.SaveItemInCache<byte[]>("test", fileBytes);
                    await localCache.SaveItemInCache<ObservableCollection<AudioItem>>("AudioList", AudioItems);

                }
                catch (Exception ex)
                {

                }

            }
            else
            {
                ButtonText = "Stop Recording";
                await audioRecorderService.StartRecording();
            }
        }

        private async void GetAudiofromCache()
        {
            try
            {

               var audiolist  = await localCache.GetCacheItem<ObservableCollection<AudioItem>>("AudioList");

                if (audiolist != null)
                {
                    AudioItems = audiolist;
                }
                else
                {
                    AudioItems = new ObservableCollection<AudioItem>();
                }
                //  var localcache = DependencyService.Get<ILocalCache>();
                //var audiobyte = await localCache.GetCacheItem<byte[]>("test");
                //if (audiobyte != null)
                //{
                //    string tempFileName = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N") + ".wav");
                //    File.WriteAllBytes(tempFileName, audiobyte);
                //    audioPlayer.Play(tempFileName);
                //}
            }
            catch (Exception ex)
            {

            }
        }

       
    }
}
