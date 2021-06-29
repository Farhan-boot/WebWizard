
'use strict';

$("#search-contact").on("keyup", function() {
    var value = $(this).val().toLowerCase();
    $("#chatsidebar ul li").filter(function() {
      $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
    });
});

jQuery(window).on('load resize', function () {

    // Variable Declarations

    var right_sidebar = $('.right-sidebar').width();
    var left_sidebar = $('.left-sidebar').width();
    var chat_bar = $('.chat').width();
    var win_width = $(window).width();

    $(".user-list-item").on('click', function () {
    if ($(window).width() < 992) {
        $('.left-sidebar').addClass('hide-left-sidebar');
        $('.chat').addClass('show-chatbar');
      }
    });

    $(".dream_profile_menu").on('click', function () {
      $('.right-sidebar').addClass('show-right-sidebar');
      $('.right-sidebar').removeClass('hide-right-sidebar');
        if ( $(window).width() > 991 && $(window).width() < 1201) {
        $(".chat").css('margin-left', - chat_bar);
      }
      if ($(window).width() < 992) {
        $('.chat').addClass('hide-chatbar');
      }
    });

    $(".close_profile").on('click', function () {
      $('.right-sidebar').addClass('hide-right-sidebar');
      $('.right-sidebar').removeClass('show-right-sidebar');
      if ( $(window).width() > 991 && $(window).width() < 1201) {
        $(".chat").css('margin-left', 0);
      }
      if ($(window).width() < 992) {
        $('.chat').removeClass('hide-chatbar');
      }
    });
    $(".nav-tabs a").on('click', function () {
      $(this).tab('show');
    });

    $(".chat-header .left_side i").on('click', function () {
      $('.left-sidebar').removeClass('hide-left-sidebar');
      $('.chat').removeClass('show-chatbar');
    });
      
  });

  //Rightside accordian
  $('.accordion-col .accordion-title').on('click', function () {
    $(this).next().slideToggle();
    $(this).toggleClass('active');
  });
  //Custom modal click for status view
  $('*[data-target="#status-modal"]').on('click', function () {
    $('body').addClass('custom-model-open');
  });
  $('.custom-status-close').on('click', function () {
    $('body').removeClass('custom-model-open');
  });
  
  // Tooltip
  if($('[data-toggle="tooltip"]').length > 0 ){
    $('[data-toggle="tooltip"]').tooltip();
  }

  //Custom scroll bar
  if ($(window).width() > 992) {
    if($('.chat-body, .left-sidebar .sidebar-body, .right-sidebar').length > 0 ){
      $('.chat-body, .left-sidebar .sidebar-body, .right-sidebar').mCustomScrollbar();
    }
  }

  $("#video_call").modal({
    show:false,
    backdrop:'static'
  });

  $('#cancelbtn').on('click', function () {
    $(this).closest('.install-banner').fadeOut();
  });

  //function that gets the location and returns itfunction getLocation() {
  function getLocation() {
    if(navigator.geolocation) {
      navigator.geolocation.getCurrentPosition(showPosition);
    } else {
      console.log("Geo Location not supported by browser");
    }
  }
  function showPosition(position) {
    var location = {
      longitude: position.coords.longitude,
      latitude: position.coords.latitude
    }
    console.log(location)
  }//request for location
  getLocation();
  function locationError(error) {
    switch(error.code) {
        case error.PERMISSION_DENIED:
            return "User denied the request for Geolocation."
            break;
        case error.POSITION_UNAVAILABLE:
            return "Location information is unavailable."
            break;
        case error.TIMEOUT:
            return "The request to get user location timed out."
            break;
        case error.UNKNOWN_ERROR:
            return "An unknown error occurred."
            break;
    }
}

function showInMap(pos) {
    var latlon = pos.coords.latitude + "," + pos.coords.longitude;
    var img_url = "https://maps.googleapis.com/maps/api/staticmap?center="+latlon+"&zoom=14&size=400x300&sensor=false&key=YOUR_:KEY";    
    var map = document.querySelector("mapholder");
    map.innerHTML = "<img src='"+img_url+"'>";
}


//Video call
const video2 = document.querySelector("#bgVideo");
const video = document.querySelector('#smVideo');


const captureVideoButton = document.querySelector('.capture-button');
const stopVideoButton = document.querySelector('#stopcamera');
  
//Capture Video
function captureVideo(){

     navigator.mediaDevices.getUserMedia({
      audio: true,
      video: true
    })
    .then(stream => {
      window.localStream = stream;
      video.srcObject = stream;
      audio.srcObject = stream;
    })
    .catch((err) => {
      console.log(err);
         });

};

  stopVideoButton.onclick = function() {
    localStream.getVideoTracks()[0].stop();
    video.src = '';
    
    localStream.getAudioTracks()[0].stop();
    audio.src = '';
  };

function init() {
}

init();
