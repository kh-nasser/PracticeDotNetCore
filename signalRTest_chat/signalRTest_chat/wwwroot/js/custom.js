$("#register-btn").click(function () {
    $("#login").slideUp();
    $("#register").fadeIn();
})
$("#login-btn").click(function () {
    $("#register").slideUp();
    $("#login").slideDown();
})
$(document).ready(function () {
    //auth#register
    var routeSelector = location.href.split("#")[1];
    var routePath = location.href.split("#")[4];

    if (
        //routeSelector or routePath is not null
        (routeSelector || routePath) && (
            //routeSelector or routePath is equal to "Register"
            (routeSelector.toUpperCase() === "Register".toUpperCase()) ||
            (routePath.toUpperCase() === "Register".toUpperCase()))) {
        $("#login").slideUp();
        $("#register").fadeIn();

    } else {
        $("#register").slideUp();
        $("#login").slideDown();
    }
});