﻿using System;
using Android.App;
using Android.Content;
using Android.Speech;
using Assignment.Droid;
using Assignment.Interfaces;
using Plugin.CurrentActivity;

[assembly: Xamarin.Forms.Dependency(typeof(SpeechToTextAndroid))]

namespace Assignment.Droid
{
	public class SpeechToTextAndroid : ISpeechToText
	{
        private readonly int VOICE = 10;
        private Activity _activity;
        public SpeechToTextAndroid()
        {
            _activity = CrossCurrentActivity.Current.Activity;

        }



        public void StartSpeechToText()
        {
            StartRecordingAndRecognizing();
        }

        private void StartRecordingAndRecognizing()
        {
            string rec = global::Android.Content.PM.PackageManager.FeatureMicrophone;
            if (rec == "android.hardware.microphone")
            {
                try
                {
                    var voiceIntent = new Intent(RecognizerIntent.ActionRecognizeSpeech);
                    voiceIntent.PutExtra(RecognizerIntent.ExtraLanguageModel, RecognizerIntent.LanguageModelFreeForm);


                    voiceIntent.PutExtra(RecognizerIntent.ExtraPrompt, "Speak now");

                    voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputCompleteSilenceLengthMillis, 1500);
                    voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputPossiblyCompleteSilenceLengthMillis, 1500);
                    voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputMinimumLengthMillis, 15000);
                    voiceIntent.PutExtra(RecognizerIntent.ExtraMaxResults, 1);
                    voiceIntent.PutExtra(RecognizerIntent.ExtraLanguage, Java.Util.Locale.Default);
                    _activity.StartActivityForResult(voiceIntent, VOICE);
                }
                catch (ActivityNotFoundException ex)
                {
                    String appPackageName = "com.google.android.googlequicksearchbox";
                    try
                    {
                        Intent intent = new Intent(Intent.ActionView, global::Android.Net.Uri.Parse("market://details?id=" + appPackageName));
                        _activity.StartActivityForResult(intent, VOICE);

                    }
                    catch (ActivityNotFoundException e)
                    {
                        Intent intent = new Intent(Intent.ActionView, global::Android.Net.Uri.Parse("https://play.google.com/store/apps/details?id=" + appPackageName));
                        _activity.StartActivityForResult(intent, VOICE);
                    }
                }

            }
            else
            {
                throw new Exception("No mic found");
            }
        }


        public void StopSpeechToText()
        {

        }
    }
}


