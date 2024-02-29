using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;
using Assignment.Interfaces;
using Assignment.Models;
using Assignment.Services;
using Plugin.AudioRecorder;
using Xamarin.Essentials;
using Xamarin.Forms;
using static Xamarin.Forms.Internals.GIFBitmap;

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

        private ISpeechToText _speechRecongnitionInstance;

        public ICommand AutoRecordAudioCommand { get; }

        bool isRecordingInProgress { get; set; }

        bool disableAutoRecord { get; set; }

        private Timer timer1;

        public AudioRecordViewModel()
        {
            Title = "Audio recorder";
            RecordAudioCommand = new Command(RecordAudio);
            PlayAudioCommand = new Command(PlayAudio);
            AutoRecordAudioCommand = new Command(AutoRecordAudio);
            GetPermissions();
            GetAudiofromCache();
            ButtonText = "Start Recording";

            try
            {
                _speechRecongnitionInstance = DependencyService.Get<ISpeechToText>();
            }
            catch (Exception ex)
            {
                //recon.Text = ex.Message;
            }


            MessagingCenter.Subscribe<ISpeechToText, string>(this, StringConstants.SPEECH_TO_TEXT, (sender, args) =>
            {
                SpeechToTextFinalResultRecieved(args);
            });

            MessagingCenter.Subscribe<ISpeechToText>(this, "Final", (sender) =>
            {
               // start.IsEnabled = true;
            });

            MessagingCenter.Subscribe<IMessageSender, string>(this, StringConstants.SPEECH_TO_TEXT, (sender, args) =>
            {
                SpeechToTextFinalResultRecieved(args);
            });

        }

        private async void GetPermissions()
        {
            var status = await Permissions.RequestAsync<Permissions.Microphone>();
        }

        private void AutoRecordAudio(object obj)
        {
            disableAutoRecord = false;
            AutoRecord(disableAutoRecord);
        }
        private async Task AutoRecord(bool disable)
        {
            try
            {
                if (!disable)
                    _speechRecongnitionInstance.StartSpeechToText();
            }
            catch (Exception ex)
            {
            }
        }

        private async void SpeechToTextFinalResultRecieved(string args)
        {
            
            if (args != StringConstants.NO_INPUT || args.Length > 1)
            {
                if (isRecordingInProgress) return;
                await Task.Delay(1000);
                StartRecording();
                timer1 = new Timer();
                timer1.Interval = 5000; // Interval set to 10 seconds
                timer1.Elapsed += OnTimedEvent; ;
                timer1.Start();
            }

        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            isRecordingInProgress = false;
             timer1.Stop();
            timer1.Dispose();
            StopRecording();
        }
     


        private void PlayAudio(object obj)
        {
            disableAutoRecord = true;
            AudioItem audioItem = (AudioItem)obj;
            string tempFileName = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N") + ".wav");
            File.WriteAllBytes(tempFileName, audioItem.AudioData);
             audioPlayer.Play(tempFileName);
        }

        private async void RecordAudio(object obj)
        {

            if (ButtonText == StringConstants.START_RECORDING)
            {
                StartRecording();
            }
            else
            {
                StopRecording(false);
            }

         }

        private async Task StartRecording()
        {
            var status = await Permissions.RequestAsync<Permissions.Microphone>();

            if (status != PermissionStatus.Granted)
                return;
            ButtonText = StringConstants.STOP_RECORDING;
            await audioRecorderService.StartRecording();
        }

        private async Task StopRecording(bool showGoogleSTT=true)
        {
           
             await audioRecorderService.StopRecording();
            try
            {
               // audioPlayer.Play(audioRecorderService.GetAudioFilePath());
                await Task.Delay(1000);
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

                if (showGoogleSTT) AutoRecord(disableAutoRecord);
                isRecordingInProgress = false;
                ButtonText = StringConstants.START_RECORDING;
            }
            catch (Exception ex)
            {
                ButtonText = StringConstants.START_RECORDING;
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
                
            }
            catch (Exception ex)
            {

            }
        }

       
    }
}
