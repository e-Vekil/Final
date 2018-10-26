////////////////MURAD END/////////////////

var box = $('#navPrimary');

$('.navToggle').on('click', function () {
    if (box.hasClass('hidden')) {
        box.removeClass('hidden');
        box.addClass('navOpen');
        setTimeout(function () {
            box.removeClass('navHidden');
        }, 20);
    } else {
        box.removeClass('navOpen');
        box.addClass('navHidden');
        box.one('transitionend', function (e) {
            box.addClass('hidden');
        });
    }
    
    

});



$(document).ready(function () {
    $(window).scroll(function () {
        if (document.body.scrollTop > 40 || document.documentElement.scrollTop > 40) {
            $('#mainheader').addClass('headerScroll');
        } else {
            $('#mainheader').removeClass('headerScroll');
        }

    });
    //Scroll
    $("#link").click(function () {
        $path = $("#Test").offset().top-200;
        console.log($path);
        $("body").animate({ "scrollTop": $path},2000);
    })
});


//Close Navbar On Windows Click

$(document).ready(function () {
    $(document).click(function (event) {
        var clickover = $(event.target);
        var _opened = $(".navPrimary").hasClass("navPrimary navOpen");
        if (_opened === true && !clickover.hasClass("navPrimaryContent")) {
            $(".navToggle").click();
        }
    });
});

////////////////MURAD END/////////////////

////////////////TERLAN START/////////////////
//var navbar = document.querySelector(".header");
//var headerContent = document.querySelector(".headerContent");
//var iconBurger = document.querySelector(".navToggle");
//window.addEventListener("scroll", function () {
//    if (document.body.scrollTop > 20 || document.documentElement.scrollTop > 20) {
//        navbar.style.backgroundColor = "black";
//        navbar.style.height = 70 + "px";
//        navbar.style.transition = "all 0.3s ease-in-out";
//        headerContent.style.top = "5px";
//        headerContent.style.transition = "all 0.3s ease-in-out";
//        iconBurger.style.top = "13px"
//    }
//    else {
//        navbar.style.backgroundColor = "transparent";
//        navbar.style.height = 109 + "px";
//        navbar.style.transition = "all 0.3s ease-in-out";
//        headerContent.style.top = "36px";
//    }
//})

//SLIDER

var icons = document.querySelectorAll(".slider-wrapper li");
var activeIcon = document.querySelector(".slider-wrapper li .slider.active");
for (var icon of icons) {
    icon.addEventListener("click", function () {
        activeIcon.classList.remove("active");
        this.firstChild.classList.add("active");
        activeIcon = this.firstChild;
    })
}

$(document).ready(function () {
    $("li:has(a.slider)").on('click', function (event) {
        if (this.firstElementChild.hash !== "") {
            event.preventDefault();
            var hash = this.firstElementChild.hash;
            $('html, body').animate({
                scrollTop: $(hash).offset().top
            }, 800, function () {
                window.location.hash = hash;
            });
        }
    });
});

window.addEventListener("scroll", function () {
    if (document.documentElement.scrollTop >= window.innerHeight-400) {
        for (var icon of icons) {
            icon.firstChild.classList.add("change");
        }
    }
    else {
        for (var icon of icons) {
            icon.firstChild.classList.remove("change");
        }
    }
})


//Scroll Transition
if (window.addEventListener) window.addEventListener('DOMMouseScroll', wheel, false);
window.onmousewheel = document.onmousewheel = wheel;

function wheel(event) {
    var delta = 0;
    if (event.wheelDelta) delta = event.wheelDelta / 120;
    else if (event.detail) delta = -event.detail / 3;

    handle(delta);
    if (event.preventDefault) event.preventDefault();
    event.returnValue = false;
}

function handle(delta) {
    var time = 450;
    var distance = 300;

    $('html, body').stop().animate({
        scrollTop: $(window).scrollTop() - (distance * delta)
    }, time);
}
////////////////TERLAN END/////////////////