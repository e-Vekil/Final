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

////////////////TERLAN END/////////////////