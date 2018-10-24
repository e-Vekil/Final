////////////////MURAD END/////////////////

var box = $('#navPrimary');

$('.navToggle').on('click', function () {
    if (box.hasClass('hidden')) {
        box.removeClass('hidden');
        setTimeout(function () {
            box.removeClass('navHidden');
        }, 20);
    } else {
        box.addClass('navHidden');
        box.one('transitionend', function (e) {
            box.addClass('hidden');
        });
    }
});

////////////////MURAD END/////////////////

////////////////TERLAN START/////////////////
var navbar = document.querySelector(".header");
var headerContent = document.querySelector(".headerContent");
var iconBurger = document.querySelector(".navToggle");
window.addEventListener("scroll", function () {
    if (document.body.scrollTop > 20 || document.documentElement.scrollTop > 20) {
        navbar.style.backgroundColor = "black";
        navbar.style.height = 70 + "px";
        navbar.style.transition = "all 0.3s ease-in-out";
        headerContent.style.top = "5px";
        headerContent.style.transition = "all 0.3s ease-in-out";
        iconBurger.style.top = "13px"
    }
    else {
        navbar.style.backgroundColor = "transparent";
        navbar.style.height = 109 + "px";
        navbar.style.transition = "all 0.3s ease-in-out";
        headerContent.style.top = "36px";
    }
})

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
////////////////TERLAN END/////////////////