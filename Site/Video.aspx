<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Video.aspx.cs" Inherits="Site.Video" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="js/video-js/video-js.css" rel="stylesheet" />
    
    <style type="text/css">
        .videoWrapper {
            position: absolute;
            left: 0;
            right: 0;
            top: 0;
            bottom: 0;
        }
 
        video {
          position: absolute !important;
          top: 0;
          left: 0;
          bottom: 0 !important;
          right: 0 !important;
        }    
 
        .videoWrapper .video-js {
            position: absolute;
            top: 0;
            left: 0;
            right: 0 !important;
            bottom: 0 !important;
            width: auto !important;
            height: auto !important;
            background: #000000;
        }         
 
        .videoWrapper object,
        .videoWrapper embed {
          position: absolute;
          top: 0;
          left: 0;
          right: 0;
          bottom: 0 !important;
        }        
 
        .vjs-spinner {
          display: none !important;
        }
 
        .video-js img.vjs-poster {
          height: 100% !important;
          width: 100% !important;
          max-width: 100%;
        }

        .vjs-big-play-button {
          top:0;
          left:0;
          right:0;
          bottom:0;
          margin:auto;
        }
    </style>

    <script src="js/video-js/video.js"></script>
    <script src="js/modernizr.js"></script>

    <script>
        videojs.options.flash.swf = "js/video-js/video-js.swf"
    </script>    
    
    <title></title>
</head>
<body>
    <%if(ShowVideo){ %>
    <div class="videoWrapper">
        <video id="training_video_1" class="video-js vjs-default-skin" controls preload="none" style="position: absolute; left: 0; right: 0; top: 0; bottom: 0;"    
            data-setup='{"autoplay": false}'>
            <source src="Videos/prescriber-training.mp4" type='video/mp4' />
            <p class="vjs-no-js">To view this video please enable JavaScript, and consider upgrading to a web browser that <a href="http://videojs.com/html5-video-support/" target="_blank">supports HTML5 video</a></p>
        </video>
    </div>
    <%} else {%>
    <div>
        It appears the your link has expired.
    </div>
    <%} %>
</body>
</html>
