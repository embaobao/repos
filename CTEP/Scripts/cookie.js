function setCookie(cname, cvalue, exdays) {
    var d = new Date();
    d.setTime(d.getTime() + exdays * 24 * 60 * 60 * 1000);
    var expires = "expires=" + d.toGMTString();
    document.cookie = cname + "=" + cvalue + "; " + expires;
}

function getCookie(cname) {
    var name = cname + "=";
    var ca = document.cookie.split(";");
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i].trim();
        if (c.indexOf(name) === 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}

function checkCookie() {
    var user = getCookie("username");
    if (user !== "") {
        alert("欢迎 " + user + " 再次访问");
    } else {
        user = prompt("请输入你的名字:", "");
        if (user !== "" && user !== null) {
            setCookie("username", user, 30);
        }
    }
}

function pushCookie(userObj) {
    var BCode = new Base64();
    setCookie("emb_user", BCode.encode(JSON.stringify(userObj)), 2);
}

function ChckUserForCookie() {

    //if (window.location.href.indexOf("index.html") > 0) {
    //    return false;
    //}
    //if (window.location.href.indexOf("register.html") > 0) {
    //    return false;
    //}
    var BCode = new Base64();
    try {

        user = JSON.parse(BCode.decode(getCookie("emb_user")));
        return user;
    }
    catch (err) {
        console.log("ChckUserForCookie:" + err);
        return false;
    }
}
//ChckUserForCookie() ;

function _SaveData(key, val, fun) {
    localforage.setItem(key, val).then(function (data) {
        fun(data);
    });
}

function _GetData(key, fun) {
    localforage.getItem(key).then(function (value) {
        fun(value);
    }).catch(function (err) { console.log(err); });
}

function SaveUser(val,fun) {
    _SaveData("emb_user", val, fun);
}
function SaveInfo(val, fun) {
    _SaveData("emb_info", val, fun);
}
