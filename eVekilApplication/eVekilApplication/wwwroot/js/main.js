// MAIN VIDEO WIDTH
var w = $(window).width();
$('#mainVideo').css('width', w);


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
        if (_opened === true && !clickover.hasClass("side-bar-content") && !clickover.hasClass("icon-burger-wrapper") && !clickover.hasClass("icon-burger") && !clickover.hasClass("close-icon") && clicked != 'LI' && clicked != 'UL') {
            $(".side-bar").removeClass("open");
            //console.log("Hello");
        }
    });
});

    
                                                                 //Scroll Transition


//function init() {
//    new SmoothScroll(document, 220, 42)
//}

//function SmoothScroll(target, speed, smooth) {
//    if (target == document)
//        target = (document.documentElement || document.body.parentNode || document.body) // cross browser support for document scrolling
//    var moving = false
//    var pos = target.scrollTop
//    target.addEventListener('mousewheel', scrolled, false)
//    target.addEventListener('DOMMouseScroll', scrolled, false)

//    function scrolled(e) {
//        e.preventDefault(); // disable default scrolling
//        var delta = e.delta || e.wheelDelta;
//        if (delta === undefined) {
//            //we are on firefox
//            delta = -e.detail;
//        }
//        delta = Math.max(-1, Math.min(1, delta)) // cap the delta to [-1,1] for cross browser consistency

//        pos += -delta * speed
//        pos = Math.max(0, Math.min(pos, target.scrollHeight - target.clientHeight)) // limit scrolling
//        //console.log(5);

//        if (!moving) {
//            update();
//            //console.log(6);
//        }
//    }

//    function update() {
//        moving = true
//        var delta = (pos - target.scrollTop) / smooth
//        target.scrollTop += delta
//        if (Math.abs(delta) > 2) {
//            //console.log("delta value: " + Math.abs(delta));
//            requestFrame(update);
//            //console.log(1);
//        }
//        else {
//            //console.log("delta value: " + Math.abs(delta));
//            moving = false;
//            //console.log(2);
//        }
//    }

//    var requestFrame = function () { // requestAnimationFrame cross browser
//        return (
//            window.requestAnimationFrame ||
//            window.webkitRequestAnimationFrame ||
//            window.mozRequestAnimationFrame ||
//            window.oRequestAnimationFrame ||
//            window.msRequestAnimationFrame ||
//            function (func) {
//                window.setTimeout(func, 1000 / 50);
//            }
//        );
//    }()
//}


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

//ISOTOP DYNAMIC
$(document).ready(function () {
    $(".isotop a").on('click', function (event) {
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

                                                //Dynamic ScrollRight
$(window).scroll(function () {
    var icons = [].slice.call(document.querySelectorAll(".sect"), 0).reverse();
    var activeIcon = document.querySelector(".slider-wrapper li .slider.active");
    //console.log(activeIcon);
    for (var icon of icons) {
        if (icon.offsetTop-50 < $(window).scrollTop()) {
            if (icon.id != activeIcon.attributes[2].value){
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

                                        //Scroll Left(NAVBAR)

$(document).ready(function () {
    $(".side-bar-content li").on('click', function (event) {
        var sideBar = document.querySelector(".side-bar")
        if (this.firstElementChild.hash !== "") {
            event.preventDefault();
            sideBar.classList.remove("open");
            var hash = this.firstElementChild.hash;
            if ($(window).width() > 650) {
                $('html, body').animate({
                    scrollTop: $(hash).offset().top
                }, 800, function () {
                    window.location.hash = hash;
                });
            } else {
                $('html, body').animate({
                    scrollTop: $(hash).offset().top-80
                }, 800, function () {
                    //window.location.hash = hash;
                    history.replaceState('', 'https://localhost:44343/#main2', + hash);
                });
            }
           
        }
    });
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
    var old;
    $(".category-name").click(function () {
        if (old == null) old = $(this);
        if ($(this).next().hasClass("active")) {
            $("article.category.active").removeClass("active");
            old.children('i').css("transform", "rotate(0deg)");
        }
        else {
            old.children('i').css("transform", "rotate(0deg)");
            $(this).children('i').css("transform", "rotate(180deg)");
            $("article.category.active").removeClass("active");
            $(this).next().toggleClass("active");
        }
        old = $(this);
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

//ABOUT SLIDER
var articles = document.querySelectorAll("section.about article");
var articleWrapper = document.querySelector("#about-slider .about-slider-wrapper");
var aboutList = document.querySelectorAll(".about-nav li");
//var activeListItem = document.querySelector(".about-nav li span.active");
var dataId;
var oldId = 1;
var res = 0;
for (var item of aboutList) {
    item.addEventListener("click", function () {
        $(".about-nav li span.active").removeClass("active");
        this.firstElementChild.classList.add("active");
        activeListItem = this.firstElementChild;
        dataId = this.firstElementChild.getAttribute("data-id");
        for (var article of articles) {
            article.classList.remove("active");
            if (article.getAttribute("data-name") == dataId) {
                article.classList.add("active");
            }
        }
    })
}





var articles = document.querySelectorAll("section.about article");
var articleWrapper = document.querySelector("#about-slider .about-slider-wrapper");
var aboutList = document.querySelectorAll(".about-slider-wrapper div.accordion");
//var activeListItem = document.querySelector(".about-slider-wrapper div.accordion.active");
var dataId;
var oldId = 1;
var res = 0;
for (var item of aboutList) {
    item.addEventListener("click", function () {
        $(".about-slider-wrapper div.accordion.active").removeClass("active");
        this.classList.add("active");
        activeListItem = this;
        dataId = this.getAttribute("data-id");
        for (var article of articles) {
            article.classList.remove("active");
            if (article.getAttribute("data-name") == dataId) {
                article.classList.add("active");
            }
        }
    })
}

//console.log(screen.width);


                                        //Categories Header Parallax Effect

var i = 0;
var count = 0;
var previcon;
$(window).scroll(function () {
    var icons = [].slice.call(document.querySelectorAll(".sect"), 0).reverse();
    for (var icon of icons) {
        if (icon.offsetTop - 450 < $(window).scrollTop()) {
            //console.log("Now: " + $(window).scrollTop());
            //console.log("Previous: " + count);

            if (icon != previcon) {
                i = 0;
            }

            if (count < $(window).scrollTop()) {
                if (Math.abs(i) < 40) {
                    i--;
                }
            } else {
                if (Math.abs(i) < 40) {
                    i++;
                }
            }

            $("#" + icon.id + " h1.category-title").css("transform", "translateY(" + i + "px)");

            count = $(window).scrollTop();
            previcon = icon;
            break;
        }
    }
});


//REGISTIRATION DYNAMIC FADE
var forms = document.querySelectorAll(".form-wrapper > div");
var buttons = document.querySelectorAll(".registiration-wrapper .buttons a");

for (var button of buttons) {
    button.addEventListener("click", function () {
        if (this.nextElementSibling) {
            this.nextElementSibling.classList.add("active");
            this.classList.remove("active");
        }
        else {
            this.previousElementSibling.classList.add("active");
            this.classList.remove("active");
        }
       
        var dataId = this.getAttribute("data-id");
        for (var form of forms) {
            form.classList.remove("active");
            if (form.getAttribute("data-name") == dataId) {
                form.classList.add("active");
            }
        }

    })
}

//BASKET SIDE BAR
var basket = document.querySelector(".basket-wrapper");
var sideBarProduct = document.querySelector(".side-bar-product");
basket.addEventListener("click", function () {
    sideBarProduct.classList.add("open");
})

var sideBarProductCloseIcon = document.querySelector(".side-bar-product .close-icon-product");
sideBarProductCloseIcon.addEventListener("click", function () {
    sideBarProduct.classList.remove("open");
})


//Document Description Reviews and Descriptions

var descriptionAndReview = document.querySelectorAll(".document-desc-features .features");
var changeButtons = document.querySelectorAll(".document-desc-features span");

for (var button of changeButtons) {
    button.addEventListener("click", function () {
        for (var button of changeButtons) {
            button.classList.remove("active");
        }
        this.classList.add("active");

        var dataId = this.getAttribute("data-id");
        console.log(dataId)
        for (var div of descriptionAndReview) {
            div.classList.remove("active");
            if (div.getAttribute("data-name") == dataId) {
                div.classList.add("active");
            }
        }
    })
}