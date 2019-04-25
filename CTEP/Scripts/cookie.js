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
    }).catch(function (err) { console.log(err); });
}

function _GetData(key, fun) {
    localforage.getItem(key).then(function (value) {
        fun(value);
        return true;
    }).catch(function (err) { console.log(err); return false; });
}
function getdate(date) {

    var y = date.getFullYear();
    var m = date.getMonth() + 1;
    var d = date.getDate();
    var h = date.getHours();
    var minutes = date.getMinutes();
    var s = date.getSeconds();
    return y + "/" + m + "/" + d + " " + h + ":" + minutes + ":" + s;
}
// 说明：将C#时间戳，格式为：/Date(-62135596800000)，转换为js时间。
// 参数：timeSpan 字符串 例如：'/Date(-62135596800000)'
// 结果：JS的Date

function parseDate(timeSpan) {
    var d = null;
    if (timeSpan !== null) {
        timeSpan = timeSpan.replace('Date', '').replace('(', '').replace(')', '').replace(/\//g, '');
        d = new Date(parseInt(timeSpan));
    } else {
        d = new Date();
    }
    
    return d;
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



   


    GetUser(function (u) {

        if (u === null) {
            layer.msg("还未登录哦！");
        } else {
            user = u;
            console.log('emb_user', user);
            $("#welcomTxt").text("尊敬的 " + user.MAIL + "欢迎您使用!");
            $("#user_name").text(user.MAIL);
            GetInfo(function (info) {

                if (info === null) {
                    if (window.location.href.toString().indexOf("Edit") < 0) {
                        layer.open({
                            title: '完善信息去吧？'
                            , content: '您的个人信息还未完善</br>您无法绑定您的评价呦！</br></hr><small><em>提示： 绑定您的班级信息可以自动帮你找到要评价的课程(=@__@=)哦！</em></small>'
                            , yes: function (index, layero) {
                                window.location.href = "../Home/Edit";
                                //do something
                                layer.close(index); //如果设定了yes回调，需进行手工关闭
                            }

                        });

                    }
                }
                userInfo = info;
                console.log('emb_info', userInfo);
                $("#welcomTxt").text("尊敬的 " + userInfo.S_Name + "欢迎您使用!");
                $("#user_name").text(userInfo.S_Name);
            });


        }






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
                    let ass = result[i].body;
                    for (let ii in ass) {
                        //console.log(parseDate(ass[ii].TIME_CREATE) );
                        if (ass[ii].TIME_CREATE === null) {
                            ass[ii].TIME_CREATE = getdate(parseDate(ass[ii].TIME_CREATE));
                        }
                        
                    }
                
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
