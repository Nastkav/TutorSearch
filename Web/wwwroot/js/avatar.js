// https://keith.gaughan.ie/detecting-broken-images-js.html
const avatar_path = "/avatars/default.png";

var elements = document.getElementsByClassName('img-load-control');
for (var i = 0; i < elements.length; ++i) {
    elements[i].addEventListener("error", function () {
        // console.log(this)
        this.src = avatar_path
    })
    if (typeof elements[i].naturalWidth != "undefined" && elements[i].naturalWidth === 0) {
        elements[i].src = avatar_path;
    }
}


