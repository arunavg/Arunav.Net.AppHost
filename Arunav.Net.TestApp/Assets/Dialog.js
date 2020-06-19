function showDialog(showId, focusId) {
    var dialog = document.getElementById(showId);
    var txtInput = focusId != null ? document.getElementById(focusId) : null;

    addHandler(window, 'resize', function () {
        screenLoad(dialog, txtInput);
    });
    screenLoad(dialog, txtInput);
}

var _resizeTimeout = null;

function screenLoad(elShow, elFocus) {
    if (_resizeTimeout)
        window.clearTimeout(_resizeTimeout);
    _resizeTimeout = window.setTimeout(function () {
        elShow.style.height = 'auto';
        elShow.style.overflow = 'hidden';

        var width = elShow.offsetWidth < document.body.clientWidth ? elShow.offsetWidth : document.body.clientWidth;
        var height = elShow.offsetHeight < document.body.clientHeight ? elShow.offsetHeight : document.body.clientHeight;

        elShow.style.marginLeft = (-1 - width / 2) + 'px';
        elShow.style.marginTop = (-1 - height / 2) + 'px';

        if (height == document.body.clientHeight) {
            elShow.style.height = height + 'px';
            elShow.style.overflowX = 'hidden';
            elShow.style.overflowY = 'auto';
        }
        _resizeTimeout = null;
        elShow.style.visibility = 'visible';

        if (elFocus != null && elFocus.value == '')
            elFocus.focus();
        fadeIn(elShow);
    }, 100);
};

function fadeIn(el) {
    var op = 0.1;
    var timer = setInterval(function () {
        if (op >= 1) {
            clearInterval(timer);
        }
        el.style.opacity = op;
        el.style.filter = 'alpha(opacity=' + op * 100 + ")";
        op += op * 0.1;
    }, 10);
};

function addHandler(el, ev, fn) {
    if (el.addEventListener)
        el.addEventListener(ev, fn, false);
    else if (el.attachEvent)
        el.attachEvent('on' + ev, fn);
    else
        el[ev] = fn;
};