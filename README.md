# Audio Record Page Functionality

## Overview
This project is designed to simplify the process of recording and transcribing audio through a user-friendly web interface. It utilizes two primary features: manual audio recording and automatic transcription via Google's Speech-to-Text API. This README provides a comprehensive guide on how to interact with these features and navigate the project.

## Features

### Manual Recording
- A button is available for users to start and stop audio recordings manually. 
- This feature is ideal for controlled environments where the user prefers to manage the recording process.

### Automatic Recording and Transcription
- Another button triggers automatic recording if valid human speech is detected.
- The auto-recording lasts for 5 seconds before pausing to allow Google's Speech-to-Text service to process and transcribe the audio.
- After transcription, the cycle automatically restarts, allowing for continuous recording and transcription sessions.
- The page dynamically updates with a list of recorded and transcribed audio files for easy access and management.

### Playing Recorded Audio
- Users can play back recorded audio files directly from the page.
- During playback, the automatic transcription feature is temporarily disabled to ensure uninterrupted audio review.
- This feature allows users to verify the accuracy of transcriptions and make any necessary corrections.

## Rich Text Editor Features

### Accessing the Editor
The rich text editor is accessible via the 'Editor' tab, designed to enhance your document creation and editing experience.

### Capabilities
- **Text Input and Formatting**: Easily input and format your text with a variety of styles and options to choose from.
- **Inline Images**: Incorporate images directly into your text, allowing for a seamless integration of visual content.
- **XLS File Integration** In progress

## Getting Started
To get started with this project, follow these steps:
1. Clone the repository to your local machine.
2. Install the necessary dependencies (a list of required dependencies is provided in the `requirements.txt` file).
3. Follow the setup instructions for configuring Google's Speech-to-Text API with your project.
4. Run the application locally and navigate to the Audio Record page to begin using the features described above.

 
