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
        return true;
    }).catch(function (err) { console.log(err); return false; });
}

function SaveUser(val, fun) {
    _SaveData("emb_user", val, fun);
}

function SaveSchool(val, fun) {
    _SaveData("emb_school", val, fun);
}

function SaveSchools(val, fun) {
    _SaveData("emb_schools", val, fun);
}

function SaveInfo(val, fun) {
    _SaveData("emb_info", val, fun);
}

function SaveBund(val, fun) {
    _SaveData("emb_bund", val, fun);
}
function SaveCourseTemp(val, fun) {
    _SaveData("emb_coursetemp", val, fun);
}
function GetUser(fun) {
    _GetData("emb_user", fun);
}
function GetCourseTemp(fun) {
    _GetData("emb_coursetemp", fun);
}
function GetInfo(fun) {
    _GetData("emb_info", fun);
}

function GetSchool(fun) {
    _GetData("emb_school", fun);
}
function GetSchools(fun) {
    _GetData("emb_schools", fun);
}

function GetBund(fun) {
    _GetData("emb_bund", fun);
}


$(document).ready(function () {
   

    var user = {};
    var userInfo = {};
    localforage.getItem('emb_user').then(function (value) {
        // 当离线仓库中的值被载入时，此处代码运行

        user = value;
        console.log('emb_user↓');
        console.log(value);
        $("#welcomTxt").text("尊敬的 " + value.MAIL + "欢迎您使用!");
        $("#user_name").text(value.MAIL);

    }).catch(function (err) { console.log(err); });

    localforage.getItem('emb_info').then(function (value) {
        // 当离线仓库中的值被载入时，此处代码运行

        userInfo = value;
        console.log('emb_info↓');
        console.log(value);
        $("#welcomTxt").text("尊敬的 " + userInfo.S_Name + "欢迎您使用!");
        $("#user_name").text(userInfo.S_Name);
    }).catch(function (err) { console.log(err); });

    GetUser(function (user) {
       
              
        $.ajax({
            url: "../UserBundTables/Bund/",
            type: "post",
            data: user,
            success: function (result) {
                _SaveData("emb_bund", result, function () { });
                console.log("绑定数据", result);
            }
        });

        $.ajax({
            url: "../EvalutionForms/EvaluForm/",
            type: "post",
            data: user,
            success: function (result) {
                _SaveData("emb_form", result, function () { });
                console.log("评价表数据", result);
            }
        });

        $.ajax({
            url: "../Evalutions/assessment/",
            type: "post",
            data: user,
            success: function (result) {

                for (var i in result) {

                    let d = result[i];
                    result[i].body = JSON.parse(d.body);
                }
                _SaveData("emb_assess", result, function () { });
                console.log("评价-数据", result);
            }
        });

        $.post(" ../Public/School/", { uName: '' },
            function (data, status) {
               
                SaveSchool(data, function () {
                    console.log("学校数据", data);

                });
            });


        $.get("../Public/Attr", function (data, status) {
            SaveCourseTemp(data, function (x) {
                console.log("课程模板", x);
            });
          

        });

        $.get("../Public/schools", function (data, status) {
            SaveSchools(data, function (x) {
                console.log("学校数据", x);
            });
        });


        
    });


});
