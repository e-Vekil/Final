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




$(document).ready(function () {
    $(window).scroll(function () {
        if (document.body.scrollTop > 100 || document.documentElement.scrollTop > 100) {
            $('#mainheader').addClass('headerScroll');
        } else {
            $('#mainheader').removeClass('headerScroll');
        }
        
    });
});

////////////////MURAD END/////////////////

////////////////TERLAN START/////////////////

////////////////TERLAN END/////////////////