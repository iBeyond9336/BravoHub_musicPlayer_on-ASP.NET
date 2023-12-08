const HOUR_IN_SECONDS = 3600;
const HOUR_IN_MINUTES = 60;
const MINUTES_IN_SECONDS = 60;

let progressInterval;           // variable to handle the progressbar filling animation
let progressBar;                // variable that represents the progressbar HTML element
let currentProgress;            // variable that represents the current progress of the song that is playing
let audioFile;                  // variable that represents the audio HTML element
let audioTime;                  // variable that represents the audio time in HH:MM:SS format
let audioCurrentTime;           // variable that represents the current audio time in HH:MM:SS format
let playBtn;
let isPlaying = false;          // variable to toogle the button from pause to play and viceversa

function initJSVariables() {
    progressBar = document.getElementById("pb");
    audioFile = document.getElementById("audioFile");
    audioTime = document.getElementById("total-time");
    audioCurrentTime = document.getElementById("current-time");
    playBtn = document.getElementById("playBtn");
}

function printAudioDuration(time) {
    audioTime.innerHTML = time;
}

function printAudioCurrentTime(time) {
    audioCurrentTime.innerHTML = time;
}

function mapToHours(time) {
    if (time < HOUR_IN_SECONDS) {
        return 0;
    } else {
        return parseInt(time / HOUR_IN_SECONDS);
    }
}

function mapToMinutes(time) {
    if (time < HOUR_IN_MINUTES) {
        return 0;
    } else {
        return parseInt(time / HOUR_IN_MINUTES);
    }
}

function mapToSeconds(time) {
    return parseInt(time % MINUTES_IN_SECONDS);
}

function mapFromAudioTimeToHHMMSSFormat(audioTime) {
    let hours = mapToHours(audioTime);
    let totalHours = hours * HOUR_IN_SECONDS;       // total hours in seconds

    let mins = mapToMinutes(audioTime - totalHours);
    let totalminutes = mins * HOUR_IN_MINUTES;       // total mins in seconds

    let seconds = mapToSeconds(audioTime - totalHours - totalminutes);

    let hoursStr = hours > 9 ? `${hours}` : `0${hours}`;
    let minsStr = mins > 9 ? `${mins}` : `0${mins}`;
    let secondsStr = seconds > 9 ? `${seconds}` : `0${seconds}`;

    if (hours === 0) {
        return `${minsStr}:${secondsStr}`;
    }
    return `${hoursStr}:${minsStr}:${secondsStr}`;

}

function toogleStartPauseBtn() {
    if (!isPlaying) {
        isPlaying = true;
        playBtn.innerHTML = 'Pause';
        start();
    } else {
        isPlaying = false;
        playBtn.innerHTML = 'Play';
        stop();
    }
}

function start() {
    audioFile.play();
    printAudioDuration(mapFromAudioTimeToHHMMSSFormat(audioFile.duration));
    printAudioCurrentTime(mapFromAudioTimeToHHMMSSFormat(audioFile.currentTime));
    // This code block is to filling the progress bar in the GUI.
    // this is executed each 1000ms until a clearInterval(progressInterval) call happens
    progressInterval = setInterval(() => {
        printAudioCurrentTime(mapFromAudioTimeToHHMMSSFormat(audioFile.currentTime));
        if (audioFile.currentTime <= audioFile.duration) {
            currentProgress = 100 * (audioFile.currentTime / audioFile.duration);
            progressBar.style.width = currentProgress + '%';
        }
    }, 1000);
}

function reset() {
    // reset the progressbar un the GUI to 0, so start from beginning
    currentProgress = 0;
    progressBar.style.width = currentProgress + '%';
    // restarts the song from the beginning
    audioFile.currentTime = 0;
    // reset the value of the audioCurrentTime HTML element
    printAudioCurrentTime(mapFromAudioTimeToHHMMSSFormat(audioFile.currentTime));
}

function stop() {
    // Stop moving the progressbar un the GUI
    clearInterval(progressInterval);
    audioFile.pause();
}