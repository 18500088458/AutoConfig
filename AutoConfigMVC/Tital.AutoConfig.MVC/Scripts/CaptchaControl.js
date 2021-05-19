//Tital.AutoConfig.MVC项目中的代码，请勿修改，版本更新会自动覆盖

function HttpRequest() {
    /// <summary>ajax封装</summary>
    if (this == window) throw new Error(0, "HttpRequest is unable to call as a function.")
    var me = this;
    var asyncFlag = false;
    var typeFlag = false;
    var r;
    this.onFinish = function () { };
    this.onError = function () { };
    function onreadystatechange() {
        if (me.onreadystatechange) me.onreadystatechange.call(r);
        if (r.readyState == 4) {
            if (Number(r.status) >= 300) {
                if (me.onError) me.onError.call(r, new Error(0, "Http error:" + r.status + " " + r.statusText));
                if (typeFlag) r.onreadystatechange = Function.prototype;
                else r.onReadyStateChange = Function.prototype;
                r = null;
                return;
            }
            me.status = r.status;
            me.statusText = r.statusText;
            me.responseText = r.responseText;
            me.responseBody = r.responseBody;
            me.responseXML = r.responseXML;
            me.readyState = r.readyState;
            if (typeFlag) r.onreadystatechange = Function.prototype;
            else r.onReadyStateChange = Function.prototype;
            r = null;
            if (me.onFinish) me.onFinish();
        }
    }
    function creatHttpRequest() {
        var e;
        try {
            r = new window.XMLHttpRequest();
            typeFlag = true;
        } catch (e) {
            var ActiveXName = [
				'MSXML2.XMLHttp.6.0',
				'MSXML2.XMLHttp.3.0',
				'MSXML2.XMLHttp.5.0',
				'MSXML2.XMLHttp.4.0',
				'Msxml2.XMLHTTP',
				'MSXML.XMLHttp',
				'Microsoft.XMLHTTP'
			]
            function XMLHttpActiveX() {
                var e;
                for (var i = 0; i < ActiveXName.length; i++) {
                    try {
                        var ret = new ActiveXObject(ActiveXName[i]);
                        typeFlag = false;
                    } catch (e) {
                        continue;
                    }
                    return ret;
                }
                throw { "message": "XMLHttp ActiveX Unsurported." };
            }
            try {
                r = new XMLHttpActiveX();
                typeFlag = false;
            } catch (e) {
                throw new Error(0, "XMLHttpRequest Unsurported.");
            }
        }
    }
    creatHttpRequest();

    this.abort = function () {
        r.abort();
    }
    this.getAllResponseHeaders = function () {
        r.getAllResponseHeaders();
    }
    this.getResponseHeader = function (Header) {
        r.getResponseHeader(bstrHeader);
    }
    this.open = function (Method, Url, Async, User, Password) {
        asyncFlag = Async;
        try {
            r.open(Method, Url, Async, User, Password);
        } catch (e) {
            if (me.onError) me.onError(e);
            else throw e;
        }
    }
    this.send = function (Body) {
        try {
            if (typeFlag) r.onreadystatechange = onreadystatechange;
            else r.onReadyStateChange = onreadystatechange;

            r.send(Body);
            if (!asyncFlag) {
                this.status = r.status;
                this.statusText = r.statusText;
                this.responseText = r.responseText;
                this.responseBody = r.responseBody;
                this.responseXML = r.responseXML;
                this.readyState = r.readyState;

                if (typeFlag) r.onreadystatechange = Function.prototype;
                else r.onReadyStateChange = Function.prototype;

                r = null;
            }
        } catch (e) {
            if (me.onError) me.onError(e);
            else throw e;
        }
    }
    this.setRequestHeader = function (Name, Value) {
        r.setRequestHeader(Name, Value);
    }
}
function Captcha(id, imageId, inputId, flashId) { 
    this.id = id;
    this.imageId = imageId;
    this.inputId = inputId;
    this.flashId = flashId; 
    this.flash = function() {
        document.getElementById(this.imageId).src = "/Captcha?id=" + this.id + "&" + new Date();
    };
    this.regClick = function (captcha) {
        //<summary>注册刷新事件</summary>
        if (this.flashId != "")
            document.getElementById(this.flashId).onclick = function () { captcha.flash(); };
    };
    this.validate = function (input) {
        //<summary>Ajax验证验证码</summary>
        var ret = '';
        var request = new HttpRequest();
        if (typeof (input) == 'undefined' && this.inputId != '')
            input = document.getElementById(this.inputId).value; //赋值
        if (input == '')
            ret = '3';
        else {
            var url = "/Captcha/Valid?id=" + this.id + "&input=" + input + "&" + new Date();
            request.open("Post", url, false);
            request.send();
            ret = request.responseText;
        } 
        return ret;
    };
}