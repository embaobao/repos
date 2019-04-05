 $(document).ready(function () {

        layer.tips('登录注册切换按钮', '#tranLog');

    // console.log(getCookie("emb_log"));
    if(getCookie("emb_log")==="login")
            {
        setCookie("emb_log", "log", 1);
    transfer();

}
var page = 0;
var isMail = false;
var LoginBt = $("#login");
var RegesterBt = $("#regester");
var emialTxt = $("#email");
var pwTxt = $("#password");
var roleVal = $("#role");
var emilIco = $("#emailico");
var pwIco = $("#pwico");
var BaseUrl = "http://localhost:55810/User/";
            var user = {
        id: -1,
    email: '',
    role: 0,
    pw: ''
};


            function changeIco(e, img) {
        e.attr("src", "images/app/" + img + ".png");
    }



    //                 changeIco(emilIco,"Lock-Mail");
    //                 changeIco(pwIco,"Lock-Screen")
    //


            roleVal.bind("input propertychange", function (event) {
        user.role = roleVal.val();
    console.log("角色设定:" + user.role);
});



$('[data-toggle="tooltip"]').tooltip();
// $("#loginbtg").hide();
var autoData = [];
            emialTxt.bind("input propertychange", function (event) {
                if (emialTxt.val().toString().indexOf("@") < 0) {
        autoData.push(emialTxt.val() + "@qq.com");
    autoData.push(emialTxt.val() + "@163.com");
    autoData.push(emialTxt.val() + "@126.com");
    autoData.push(emialTxt.val() + "@gmail.com");
    autoData.push(emialTxt.val() + "@outlook.com");
}
                if (emialTxt.val().trim().length < 1) {
        changeIco(emilIco, "email");
    }


});
            emialTxt.change(function () {
        user.email = emialTxt.val();
    console.log("用户邮箱设定:" + user.email);
                $.post(BaseUrl + "IsMail", {email: user.email },
                    function (data, status) {
                        if (status === "success") {
        console.log(page);
    isMail = data
                            if (isMail) {

                                if (page === 0) {
        changeIco(emilIco, "erro");
    } else {
        changeIco(emilIco, "true");
    }
}
                            else {
                                if (page === 0) {
        changeIco(emilIco, "true");
    } else {
        changeIco(emilIco, "erro");
    }

}
}
                        else {
        changeIco(emilIco, "email");
    }

    console.log(data);

});
})

            pwTxt.bind("input propertychange", function () {
        user.pw = pwTxt.val();
    console.log("用户密码设定:" + user.pw);

})

            emialTxt.autocomplete({
        source: autoData,
                change: function (event, ui) {
        autoData = [];
    },
                select: function (event, ui) {
        autoData = [];
    }


});



            function transfer() {
                var logbtg = $("#loginbtg");
    var regesbtg = $("#regesterbtg");
    var titleTxt = $("#titleTxt");
                if (logbtg.css("display") === "none") {
        regesbtg.animate({
            // opacity: 'toggle',
        });
    page = 1;

    titleTxt.text("EMB-用户登录");
    regesbtg.hide();
    // regesbtg.css("display","none");
    logbtg.show();
                } else {
        logbtg.animate({
            // opacity: 'toggle',
        });
    page = 0;
    titleTxt.text("EMB-用户注册");
    logbtg.hide();
    // logbtg.css("display","none");
    regesbtg.show();
}
}


            $("#tranLog").click(function () {
        transfer();
    });




            RegesterBt.click(function () {
        user.email = emialTxt.val();
    user.role = roleVal.val();
    user.pw = pwTxt.val();


                try {

        $.post(BaseUrl + "Register", user,
            function (data, status) {
                if (data) {
                    console.log("ok");
                    pushCookie(user);
                    // window.location.href="index.html";
                }
                // alert("数据: \n" + data + "\n状态: " + status);
                console.log(data);
            });

    } 


    });



            LoginBt.click(function () {
        console.log("开始登陆");
    user.email = emialTxt.val();
    user.role = roleVal.val();
    user.pw = pwTxt.val();
                try{
        $.post(BaseUrl + "Login", user,
            function (data, status) {
                if (status === "success") {
                    if (data.id > 0) {
                        console.log("ok");
                        pushCookie(data);
                        window.location.href = "index.html";
                    }

                }
                console.log(data);
            });
    }catch(erro){

    }



    });






    // console.log(BCode.encode(JSON.stringify(user)));

    //                 ChckUserForCookie();


    // -----------------!

});
