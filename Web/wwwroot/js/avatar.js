// https://keith.gaughan.ie/detecting-broken-images-js.html
var avatarImg = document.images.avatar;
if (avatarImg != null && typeof avatarImg.naturalWidth != "undefined" && avatarImg.naturalWidth === 0
) {
    avatarImg.src = "/avatars/default.png";
}
