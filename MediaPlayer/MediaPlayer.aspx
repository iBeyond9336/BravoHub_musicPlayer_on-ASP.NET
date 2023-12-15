<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MediaPlayer.aspx.cs" Inherits="BravoHub.Player.MediaPlayer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Sound Player</title>
    <link rel="stylesheet" href="styles.css" />
</head>
<body id="body" background="../img/bg0.png">
    <form id="form1" runat="server" class="media-player-container">
        <div class="upper-container">
            <!-- record background -->
            <div class="record-container">
                <div class="record-bg">
                    <div id="record-img" class="rotate-play"></div>
                </div>
            </div>
            <!-- music info -->
            <div class="introduction-container">
                <div class="text-container">
                    <div id="music-title">Music Title</div>
                    <div class="author-container">
                        Artist：
                    <span id="author-name">Unknown</span>
                    </div>
                </div>
            </div>
        </div>
        <!-- media player components -->
        <div class="audio-box">
            <div class="audio-container">
                <audio id="audioTag"></audio>
                <!-- progress bar -->
                <div class="a-progress">
                    <div class="pgs-total" id="progress-total">
                        <div class="pgs-play" id="progress" style="width: 0%;"></div>
                    </div>
                </div>
                <!-- bottom line -->
                <div class="a-controls">
                    <!-- played time -->
                    <div class="time-container">
                        <span class="played-time" id="playedTime">00:00</span>&nbsp;/&nbsp;
                    <span class="audio-time" id="audioTime">00:00</span>
                    </div>
                    <!-- center button container -->
                    <div class="center-button-container">
                        <!-- play mode -->
                        <div id="playMode" class="center-icon mode"></div>
                        <!-- forward -->
                        <div id="skipForward" class="center-icon s-right"></div>
                        <!-- play/pause -->
                        <div id="playPause" class="icon-play"></div>
                        <!-- backward -->
                        <div id="skipBackward" class="center-icon s-left"></div>
                        <!-- volume adjust -->
                        <div id="volume" class="center-icon volume"></div>
                        <!-- volume togger  -->
                        <input type="range" id="volumn-togger" name="change" value="70" min="0" max="100" step="1">
                    </div>
                    <!-- back buttons -->
                    <div class="bottom-button-container">
                        <!-- list -->
                        <div id="list" class="bottom-icon list"></div>
                        <!-- speed mode -->
                        <div id="speed" class="speed">1.0X</div>
                       <%--<!-- MV -->
                        <div id="MV" class="bottom-icon MV"></div>--%>
                    </div>
                </div>
            </div>
        </div>
        <!-- music list -->
        <div class="close-list" id="close-list"></div>
        <div class="music-list" id="music-list">
            <div class="music-list-container">
                <div class="music-list-title">PlayList</div>
                <hr class="line" />
                <div class="all-list">
                    <div id="music0">Luo ChunFu</div>
                    <div id="music1">Yesterday</div>
                    <div id="music2">Misty Rain</div>
                    <div id="music3">Vision pt.II</div>
                    <div id="music4">Let it be</div>
                </div>
            </div>
        </div>
    </form>
    <script src="./MediaPlayer.js"></script>
</body>
</html>
