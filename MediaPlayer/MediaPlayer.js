
﻿// get main background
var body = document.getElementById('body');
// Get the audio player object
var audio = document.getElementById('audioTag');

// music title
var musicTitle = document.getElementById('music-title');
// record image
var recordImg = document.getElementById('record-img');
// artist
var author = document.getElementById('author-name');

// progress bar
var progress = document.getElementById('progress');
// progress total
var progressTotal = document.getElementById('progress-total');

// played time
var playedTime = document.getElementById('playedTime');
// audio time
var audioTime = document.getElementById('audioTime');

// playmode
var mode = document.getElementById('playMode');
// fowrard
var skipForward = document.getElementById('skipForward');
// pause
var pause = document.getElementById('playPause');
// backward
var skipBackward = document.getElementById('skipBackward');
// total time
var audioTime = document.getElementById('audioTime');

// play mode
var mode = document.getElementById('playMode');
// next
var skipForward = document.getElementById('skipForward');
// play pause
var pause = document.getElementById('playPause');
// last
var skipBackward = document.getElementById('skipBackward');
// volume adjust
var volume = document.getElementById('volume');
// volume slider
var volumeTogger = document.getElementById('volume-togger');

// music list
var list = document.getElementById('list');
// speed
var speed = document.getElementById('speed');
// MV
var MV = document.getElementById('MV');

// close-list
var closeList = document.getElementById('close-list');
// music list
var musicList = document.getElementById('music-list');

// play/pause mode
pause.onclick = function (e) {
    if (audio.paused) {
        audio.play();
        rotateRecord();
        pause.classList.remove('icon-play');
        pause.classList.add('icon-pause');
    } else {
        audio.pause();
        rotateRecordStop();
        pause.classList.remove('icon-pause');
        pause.classList.add('icon-play');
    }
}

// update the progress bar
audio.addEventListener('timeupdate', updateProgress); // Monitor the audio playback time and update the progress bar
function updateProgress() {
    var value = audio.currentTime / audio.duration;
    progress.style.width = value * 100 + '%';
    playedTime.innerText = transTime(audio.currentTime);
}

//Audio playback time conversion
function transTime(value) {
    var time = "";
    var h = parseInt(value / 3600);
    value %= 3600;
    var m = parseInt(value / 60);
    var s = parseInt(value % 60);
    if (h > 0) {
        time = formatTime(h + ":" + m + ":" + s);
    } else {
        time = formatTime(m + ":" + s);
    }

    return time;
}

// Formatted time display, zero padding and alignment
function formatTime(value) {
    var time = "";
    var s = value.split(':');
    var i = 0;
    for (; i < s.length - 1; i++) {
        time += s[i].length == 1 ? ("0" + s[i]) : s[i];
        time += ":";
    }
    time += s[i].length == 1 ? ("0" + s[i]) : s[i];

    return time;
}

// Click the progress bar to jump to the specified point to play
progressTotal.addEventListener('mousedown', function (event) {
    // You can only adjust the music after it starts playing. It can also be adjusted after the music has been played but paused.
    if (!audio.paused || audio.currentTime != 0) {
        var pgsWidth = parseFloat(window.getComputedStyle(progressTotal, null).width.replace('px', ''));
        var rate = event.offsetX / pgsWidth;
        audio.currentTime = audio.duration * rate;
        updateProgress(audio);
    }
});

// Click on the list to expand the music list
list.addEventListener('click', function (event) {
    musicList.classList.remove("list-card-hide");
    musicList.classList.add("list-card-show");
    musicList.style.display = "flex";
    closeList.style.display = "flex";
    closeList.addEventListener('click', closeListBoard);
});

// Click Close Panel to close the music list
function closeListBoard() {
    musicList.classList.remove("list-card-show");
    musicList.classList.add("list-card-hide");
    closeList.style.display = "none";
}

// Store the currently playing music sequence number
var musicId = 0;

// Music list
let musicData = [['Luo ChunFu', 'Xi Yun'], ['Yesterday', 'Alok/Sofi Tukker'], ['Misty Rain', 'Yang'], ['Vision pt.II', 'Vicetone'], ['Let it be', 'The Beatles']];

// Initialize the musics
function initMusic() {
    audio.src = "mp3/music" + musicId.toString() + ".mp3";
    audio.load();
    recordImg.classList.remove('rotate-play');
    audio.ondurationchange = function () {
        musicTitle.innerText = musicData[musicId][0];
        author.innerText = musicData[musicId][1];
        recordImg.style.backgroundImage = "url('./img/record" + musicId.toString() + ".jpg')";
        body.style.backgroundImage = "url('./img/bg" + musicId.toString() + ".png')";
        audioTime.innerText = transTime(audio.duration);
        // restore the progress bar
        audio.currentTime = 0;
        updateProgress();
        refreshRotate();
    }
}
initMusic();

// initialize and play
function initAndPlay() {
    initMusic();
    pause.classList.remove('icon-play');
    pause.classList.add('icon-pause');
    audio.play();
    rotateRecord();
}

// Play mode settings
var modeId = 1;
mode.addEventListener('click', function (event) {
    modeId = modeId + 1;
    if (modeId > 3) {
        modeId = 1;
    }
    mode.style.backgroundImage = "url('./img/mode" + modeId.toString() + ".png')";
});

audio.onended = function () {
    if (modeId == 2) {
        // click to next
        musicId = (musicId + 1) % 4;
    }
    else if (modeId == 3) {
        // randome generate the sequence number
        var oldId = musicId;
        while (true) {
            musicId = Math.floor(Math.random() * 3) + 0;
            if (musicId != oldId) { break; }
        }
    }
    initAndPlay();
}

// last song
skipForward.addEventListener('click', function (event) {
    musicId = musicId - 1;
    if (musicId < 0) {
        musicId = 4;
    }
    initAndPlay();
});

// next song
skipBackward.addEventListener('click', function (event) {
    musicId = musicId + 1;
    if (musicId > 4) {
        musicId = 0;
    }
    initAndPlay();
});

// speed mode (hard coded)
speed.addEventListener('click', function (event) {
    var speedText = speed.innerText;
    if (speedText == "1.0X") {
        speed.innerText = "1.5X";
        audio.playbackRate = 1.5;
        recordImg.style.animationDuration = "6.67s"
        //recordImg.style.animation.ro
    }
    else if (speedText == "1.5X") {
        speed.innerText = "2.0X";
        audio.playbackRate = 2.0;
        recordImg.style.animationDuration = "5s"
    }
    else if (speedText == "2.0X") {
        speed.innerText = "0.5X";
        audio.playbackRate = 0.5;
        recordImg.style.animationDuration = "20s"
    }
    else if (speedText == "0.5X") {
        speed.innerText = "1.0X";
        audio.playbackRate = 1.0;
        recordImg.style.animationDuration = "10s"
    }
});

// video player
//MV.addEventListener('click', function (event) {
//    // open new window
//    var storage_list = window.sessionStorage;
//    storage_list['musicId'] = musicId;
//    window.open("video.html");
//});

// hard coded to generate the music list`
document.getElementById("music0").addEventListener('click', function (event) {
    musicId = 0;
    initAndPlay();
});
document.getElementById("music1").addEventListener('click', function (event) {
    musicId = 1;
    initAndPlay();
});
document.getElementById("music2").addEventListener('click', function (event) {
    musicId = 2;
    initAndPlay();
});
document.getElementById("music3").addEventListener('click', function (event) {
    musicId = 3;
    initAndPlay();
});
document.getElementById("music4").addEventListener('click', function (event) {
    musicId = 4;
    initAndPlay();
});

// Refresh record rotation angle
function refreshRotate() {
    recordImg.classList.add('rotate-play');
}

// make record spin when play
function rotateRecord() {
    recordImg.style.animationPlayState = "running"
}

// stop record spin when pause
function rotateRecordStop() {
    recordImg.style.animationPlayState = "paused"
}

// store last volume
var lastVolume = 70

// slider for volume
audio.addEventListener('timeupdate', updateVolume);
function updateVolume() {
    audio.volume = volumeTogger.value / 70;
}

// click to set volume
volume.addEventListener('click', setNoVolume);
function setNoVolume() {
    if (volumeTogger.value == 0) {
        if (lastVolume == 0) {
            lastVolume = 70;
        }
        volumeTogger.value = lastVolume;
        volume.style.backgroundImage = "url('./img/volume.png')";
    }
    else {
        lastVolume = volumeTogger.value;
        volumeTogger.value = 0;
        volume.style.backgroundImage = "url('./img/mute.png')";
    }
}
