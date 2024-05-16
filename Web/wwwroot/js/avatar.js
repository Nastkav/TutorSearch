// https://keith.gaughan.ie/detecting-broken-images-js.html
var avatarImg = document.images.avatar;
if (typeof avatarImg.naturalWidth != "undefined" && avatarImg.naturalWidth === 0) {
    document.images.avatar.src = "/avatars/default.png";
}
