// ON SCROLL NAVBAR FIXED
var navBarWrapper = document.querySelector(".nav-bar-wrapper");
var navBarHeader = document.querySelector(".nav-bar-wrapper .header");
window.addEventListener("scroll", function () {

    if (document.body.scrollTop > 20 || this.document.documentElement.scrollTop > 20) {
        navBarWrapper.classList.add("scroll-design");
        navBarHeader.classList.add("scroll-design");
    }
    else {
        navBarWrapper.classList.remove("scroll-design");
        navBarHeader.classList.remove("scroll-design");
    }

})
//SIDE BAR OPEN-CLOSE
var sideBar = document.querySelector(".side-bar")
var closeIcon = document.querySelector(".close-icon");
var iconBurger = document.querySelector(".icon-burger-wrapper")

iconBurger.addEventListener("click", function () {
    sideBar.classList.add("open");
})
closeIcon.addEventListener("click", function () {
    sideBar.classList.remove("open");
})


                                                                //Close Navbar On Windows Click

$(document).ready(function () {
    $(document).click(function (event) {
        var clickover = $(event.target);
        var clicked = event.target.tagName.toUpperCase();
        var _opened = $(".side-bar").hasClass("side-bar open");
        //console.log(clickover);
        if (_opened === true && !clickover.hasClass("side-bar-content") && !clickover.hasClass("icon-burger-wrapper") && !clickover.hasClass("close-icon") && clicked != 'LI' && clicked != 'UL') {
            $(".close-icon").click();
        }
    });
});

    
                                                                 //Scroll Transition


function init() {
    new SmoothScroll(document, 220, 42)
}

function SmoothScroll(target, speed, smooth) {
    if (target == document)
        target = (document.documentElement || document.body.parentNode || document.body) // cross browser support for document scrolling
    var moving = false
    var pos = target.scrollTop
    target.addEventListener('mousewheel', scrolled, false)
    target.addEventListener('DOMMouseScroll', scrolled, false)

    function scrolled(e) {
        e.preventDefault(); // disable default scrolling
        var delta = e.delta || e.wheelDelta;
        if (delta === undefined) {
            //we are on firefox
            delta = -e.detail;
        }
        delta = Math.max(-1, Math.min(1, delta)) // cap the delta to [-1,1] for cross browser consistency

        pos += -delta * speed
        pos = Math.max(0, Math.min(pos, target.scrollHeight - target.clientHeight)) // limit scrolling
        //console.log(5);

        if (!moving) {
            update();
            //console.log(6);
        }
    }

    function update() {
        moving = true
        var delta = (pos - target.scrollTop) / smooth
        target.scrollTop += delta
        if (Math.abs(delta) > 2) {
            //console.log("delta value: " + Math.abs(delta));
            requestFrame(update);
            //console.log(1);
        }
        else {
            //console.log("delta value: " + Math.abs(delta));
            moving = false;
            //console.log(2);
        }
    }

    var requestFrame = function () { // requestAnimationFrame cross browser
        return (
            window.requestAnimationFrame ||
            window.webkitRequestAnimationFrame ||
            window.mozRequestAnimationFrame ||
            window.oRequestAnimationFrame ||
            window.msRequestAnimationFrame ||
            function (func) {
                window.setTimeout(func, 1000 / 50);
            }
        );
    }()
}


                                                //ScrollDown ScrollUp

//$(document).ready(function () {
    $(window).scroll(function () {
        if (document.body.scrollTop > 40 || document.documentElement.scrollTop > 40) {
            $('#navHelperADown').addClass('hidden');
            $('#navHelperAUp').removeClass('hidden');
        } else {
            $('#navHelperAUp').addClass('hidden');
            $('#navHelperADown').removeClass('hidden');
        }

    });
//});


//$(document).ready(function () {
//    $("#scrollUp").click(function (event) {
//        $("body").off("init");
//        event.preventDefault();
//        $("html, body").animate({ scrollTop: 0 }, "slow");
//        return false;
//    });

//});


$(document).ready(function () {
    $("#navHelperADown").on('click', function (event) {
        if (this.hash !== "") {
            event.preventDefault();
            var hash = this.hash;
            $('html, body').animate({
                scrollTop: $(hash).offset().top
            }, 800, function () {
                window.location.hash = hash;
            });
        }
    });
});

$(document).ready(function () {
    $("#navHelperAUp").on('click', function (event) {
        if (this.hash !== "") {
            event.preventDefault();
            var hash = this.hash;
            $('html, body').animate({
                scrollTop: $(hash).offset().top
            }, 800, function () {
                window.location.hash = hash;
            });
        }
    });
});

                                                //ScrollRight

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

                                                //Dynamic ScrollRight
$(window).scroll(function () {
    var icons = [].slice.call(document.querySelectorAll(".sect"), 0).reverse();
    var activeIcon = document.querySelector(".slider-wrapper li .slider.active");
    //console.log(activeIcon);
    for (var icon of icons) {
        if (icon.offsetTop-50 < $(window).scrollTop()) {
            if (icon.id != activeIcon.attributes[2].value) {
                activeIcon.classList.remove("active");
                activeIcon.classList.remove("active");
                document.querySelector(".slider." + icon.id).classList.add("active");
                //window.location.hash = icon.id;
                //location.replace('https://localhost:44343/#' + icon.id);
                //parent.location.hash = icon.id;
                history.replaceState('', 'https://localhost:44343/#main2', '#'+icon.id);
                //setTimeout(function (e) { e.preventDefault();window.location.hash = icon.id; return false }, 700);
            }
            break;
        }
    }
});




// Slider Click
var icons = document.querySelectorAll(".slider-wrapper li");
var activeIcon = document.querySelector(".slider-wrapper li .slider.active");
for (var icon of icons) {
    icon.addEventListener("click", function () {
        activeIcon.classList.remove("active");
        this.firstElementChild.classList.add("active");
        activeIcon = this.firstElementChild;
    })
}

// Change Slider Color

window.addEventListener("scroll", function () {
    if (document.documentElement.scrollTop >= window.innerHeight - 400) {
        for (var icon of icons) {
            icon.firstElementChild.classList.add("change");
        }
    }
    else {
        for (var icon of icons) {
            icon.firstElementChild.classList.remove("change");
        }
    }
})

// ACCORDION
$(document).ready(function () {
    $(".category-name").click(function () {
        $(this).next().toggleClass("active");
    })
})
    // var acc = document.getElementsByClassName("category-name");
    // var i;

    // for (i = 0; i < acc.length; i++) {
    //   acc[i].addEventListener("click", function() {
    //     var panel = this.nextElementSibling;
    //     if (panel.style.maxHeight){
    //       panel.style.maxHeight = null;
    //     } else {
    //       panel.style.maxHeight = panel.scrollHeight + "px";
    //       panel.style.paddingTop = 40 + "px";
    //       panel.style.overflow = "inherit"
    //     } 
    //   });
    // }


