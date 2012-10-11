
AP.widget.xTab = {

    show: function (a) { if (typeof a == "string") { a = D.get(a) } a.style.display = a.$oldDisplay || "block" },
    hide: function (a) { if (typeof a == "string") { a = D.get(a) } var b = D.getStyle(a, "display"); if (b != "none") { a.$oldDisplay = b } a.style.display = "none" },
    switchTab: function (m, j, l) {
        
        if (typeof m == "string") {
            m = D.get(m)
        }
        var n = m.getElementsByTagName("a");
        var b = (document.location.hash.indexOf("#") > -1 && document.location.hash.split("#")[1] !== "") ? true : false;
        var h = (l == "onmouseover") ? "onmouseover" : "onclick";
        var a = document.location.hash.split("#")[1] || "";
        var k = n[j];
        if (b) {
            try {
                var o = D.query("a[href=#" + a + "]", m); if (o.length > 0) { k = o[0]; log(k) }
            } catch (g) { log(g) }
        }
        D.addClass(k.parentNode, "current");
        for (var d = 0; d < n.length; d++) {

            var f = D.get(n[d].hash.replace("#", ""));
            var p = n[d].hash.replace("#", "");
            if (b && (D.query("a[href=#" + a + "]", m).length > 0)) {
                if (p !== a) { AP.widget.xTab.hide(f) }
            } else {
                if (d != j) { AP.widget.xTab.hide(f) }
            }
            n[d][h] = n[d].onfucs = function () {
                if (this != k) {
                    D.addClass(this.parentNode, "current");
                    AP.widget.xTab.show(D.get(this.hash.replace("#", "")))
                } else { return }
                D.removeClass(k.parentNode, "current");
                AP.widget.xTab.hide(D.get(k.hash.replace("#", "")));
                k = this;
                return false
            }
        }
    }
};
Object.extend = function (a, b) {

    for (property in b) { a[property] = b[property] } return a
};
String.prototype.isFloat = function () {

    return /^[\d]+\.[\d]+$/.test(this.toString())
};
String.prototype.isInt = function () { return /^[\d]+$/.test(this.toString()) };
String.prototype.trim = function () { return this.replace(/^\s+|\s+$/g, "") };
Number.prototype.isFloat = function () { return /^[\d]+\.[\d]+$/.test(this.toString()) };
Number.prototype.isInt = function () { return /^[\d]+$/.test(this.toString()) };
AP.widget.PopBase = function () {

    var d = YAHOO.util.Dom;
    var f = YAHOO.util.Event;
    var a = {};
    var b = function (g) { a = Object.extend({ box_id: "PopPayment", box_class: "pop" }, g || {}) }; return { init: function (g) { b(g); this.container = null; this.makePopup() }, makePopup: function () { if (this.container) { return } var g = '<div class="pop-container" id="PopContainerBody"></div><div class="pop-shadow" style="height:28px"></div><div class="pop-angle"></div>'; this.container = Element.create("div", { id: a.box_id, "class": "pop pop-square pop-square-cue" }); this.container.innerHTML = g; d.addClass(this.container, a.box_class); d.setStyles(this.container, { width: "185px" }); this.hidden(); document.body.appendChild(this.container) }, resetWidth: function (g) { d.setStyles(this.container, { width: g }) }, setPosition: function (g) { d.setXY(this.container, g) }, show: function () { if (this.container) { d.setStyles(this.container, { display: "block" }) } }, hidden: function () { if (this.container) { d.setStyles(this.container, { display: "none" }) } }, buildUI: function (g) { this.container.appendChild(g) } }
} ();
AP.widget.AAPayment = function () {

    var g = YAHOO.util.Dom;
    var p = YAHOO.util.Event;
    var d = AP.widget.PopBase;
    var b = AP.widget.PopBase;
    var l = false;
    var m = [];
    var h;
    var q, r, f;
    var s, k;
    var j = true;
    _getPreviousValue = function (t) {

        var u = m.indexOf(t); var v = u - 1 >= 0 ? m[u - 1] : null; if (v != null) { if (!D.hasClass(v.parentNode.parentNode, "fm-error")) { return m[u - 1].value } } return ""
    }; var o = function (t) {

        _options = Object.extend({ box_container: "container", box_total_money: "totalMoney", box_total_person: "totalPerson", box_add_person: "addPerson" }, t || {})
    }; var a = function () {

        p.addListener(document.body, "click", function () { if (!j) { d.hidden() } })
    }; var n = function (u) {

        var t = u || window.event; if (t.preventDefault) { t.preventDefault() } else { t.returnValue = false } return false
    }; return { init: function (t) {

        o(t); a(); s = D.get(_options.box_container); r = D.get(_options.box_total_person); q = D.get(_options.box_total_money); _submit_form = D.get("AAPaymentForm"); p.addListener(q, "focus", function () { d.hidden(); this.countTotalMoney(); q.select(); var u = q.parentNode.parentNode; D.addClass(q.parentNode, "fm-focus"); D.removeClass(u, "fm-error"); D.setStyles(f, { display: "none" }); if (D.query(".fm-explain", u).length) { u.removeChild(D.query(".fm-explain", u)[0]) } }, "", this); p.addListener(q, "keydown", this.killEnter, q, this); p.addListener(q, "keyup", this.checkTotalMoney, q, this); p.addListener(q, "blur", function () { D.removeClass(q.parentNode, "fm-focus") })
    }, validate: function () {

        if (m.length) { m.forEach(function (w, x) { if (!w.value.length) { this.showError("金额或公式不能为空。如：20或100/5。", w) } }, this) } else { if (D.get("cannotEmpty")) { return } var t = Element.create("li", { appendTo: D.get("payers"), id: "cannotEmpty" }); var u = Element.create("span", { appendTo: t }); D.setStyles(D.get("payers"), { height: "auto" }); u.innerHTML = "必须添加一个联系人账号。"; D.addClass(t, "fm-error"); D.addClass(u, "fm-explain") }
    }, generateData: function () {

        partern = /(.*)\[.*\](.*)/; D.query("#payers li").forEach(function (v, t) { var u = v.getElementsByTagName("label")[0].getAttribute("rel"); Element.create("input", { type: "hidden", name: "full_name[]", value: partern.exec(u)[1], appendTo: v }); Element.create("input", { type: "hidden", name: "email[]", value: partern.exec(u)[2].trim(), appendTo: v }); Element.create("input", { type: "hidden", name: "amount[]", value: v.getElementsByTagName("input")[0].value, appendTo: v }); Element.create("input", { type: "hidden", name: "row_no[]", value: t + 1, appendTo: v }) })
    }, hasPersonAppend: function (u) {

        var t = false; m.forEach(function (w, x) { if (w.id == "user_" + u) { t = true } }); return t
    }, isFull: function (u, w) {

        if (m.length >= 30) { if (w) { if (D.get("user_" + u.id)) { D.get("user_" + u.id).focus() } return m.length >= 30 } var t = D.getElementsByClassName("contact_" + u.id); if (t[0].checked) { for (var v = 0; v < t.length; v++) { t[v].checked = ""; D.removeClass(t[v].parentNode, "current"); m.remove(t[v]) } } else { this.deletePerson(null, D.get("user_" + u.id)); return true } } return m.length >= 30
    }, resetPayerStyle: function () {

        var t = D.query("#payers div"); if (D.get("cannotEmpty")) { D.get("cannotEmpty").parentNode.removeChild(D.get("cannotEmpty")) } if (m.length >= 6 || t.length >= 4) { log(0); g.setStyles(D.get("payers"), { height: "230px" }) } else { if (D.query("#payers li").length <= 0) { log(1); g.setStyles(D.get("payers"), { "line-height": "0" }); return } log(2); g.setStyles(D.get("payers"), { height: "auto" }) }
    }, addPerson: function (A, y, v) {

        D.get("customPerson").value = ""; if (this.isFull(y, v)) { return } if (this.hasPersonAppend(y.id) && !v) { input = D.get("user_" + y.id); this.deletePerson(A, input); return } if (v) { if (D.get("user_" + y.id)) { D.get("user_" + y.id).focus(); return } var w = D.getElementsByClassName("contact_" + y.id); if (w.length) { for (var x = 0; x < w.length; x++) { w[x].checked = "checked"; D.addClass(w[x].parentNode, "current") } } } j = true; var C = Element.create("li"); if (y.real_name == y.nick_name || y.nick_name == "请输入联系人姓名" || !y.nick_name.trim().length) { var z = Element.create("label", { title: y.real_name + " " + y.account }) } else { var z = Element.create("label", { title: y.real_name + " [" + y.nick_name + "] " + y.account }) } z.setAttribute("rel", y.real_name + " [" + y.nick_name + "] " + y.account); var u = Element.create("span");
        var B = Element.create("input", { type: "text", id: "user_" + y.id, autocomplete: "off" }); var t = Element.create("a", { title: "删除" }); g.addClass(B, "i-text i-prize"); g.addClass(t, "ico ico-del"); if (y.real_name.len() > 8) { real_name = y.real_name.brief(8) + ".." } else { real_name = y.real_name } if (y.account.len() > 16) { account = y.account.brief(16) + ".." } else { account = y.account } z.innerHTML = "<em>" + real_name + "</em> (" + account + ")"; u.innerHTML = "&nbsp;元"; p.addListener(t, "click", this.deletePerson, B, this); u.insertBefore(B, u.firstChild); C.appendChild(z); C.appendChild(u); C.appendChild(t); s.appendChild(C); B.focus(); this.updateMoneyInputs(B); this.bindEvent(B); this.buildPop(B); B.value = _getPreviousValue(B); this.countTotalMoney(B.value); this.countTotalPerson(); this.resetPayerStyle(); this.showPop(null, B)
    }, updateMoneyInputs: function (t) {

        m.push(t)
    }, getUsersLen: function () {

        return m.length
    }, deletePerson: function (u, t) {

        m.remove(t); t.parentNode.parentNode.parentNode.removeChild(t.parentNode.parentNode); this.countTotalMoney(this.fomatValue(t.value)); this.countTotalPerson(); this.cannelChoose(t); this.resetPayerStyle(); d.hidden()
    }, cannelChoose: function (u) {

        var t = g.getElementsByClassName("contact_" + u.id.replace("user_", "")); for (var v = 0; v < t.length; v++) { t[v].checked = ""; g.removeClass(t[v].parentNode, "current") }
    }, bindEvent: function (t) {

        p.addListener(t, "blur", this.countMoney, t, this); p.addListener(t, "focus", this.showPop, t, this); p.addListener(t, "focus", this.resetInput, t, this); p.addListener(t, "keydown", this.killEnter, t, this)
    }, resetInput: function (u, t) {

        t.select(); D.addClass(t.parentNode, "fm-focus"); this.resetError(t)
    }, killEnter: function (x, t) {

        var x = window.event || arguments.callee.caller.arguments[0]; var v = x.keyCode || x.which || x.charCode; var w = String.fromCharCode(v); if (v >= 48 && v <= 57) { return } if (v > 95 && v < 106) { return } if (v == 9 || v == 8 || v == 37 || v == 39 || v == 229) { return } if (v == 191 || v == 190 || v == 111 || v == 144 || v == 110 || v == 46) { return } if (v == 13) { if (m.indexOf(k) >= m.length - 1) { var u = 0 } else { var u = m.indexOf(k) + 1 } log(u); m[u].focus() } n(x)
    }, buildPop: function (t) {

        if (d.container) { return } h = Element.create("div"); h.innerHTML = "金额或公式。如：20或100/5。"; if (d.container == null) { d.init(); d.buildUI(h) } document.getElementById("PopContainerBody").appendChild(h); this.buildTotalDom(); this.showPop(null, t)
    }, buildTotalDom: function () {

        f = Element.create("span"); _reset_money = Element.create("input", { type: "button", value: "确定" }); _cannel_btn = Element.create("input", { type: "button", value: "恢复" }); D.addClass(_reset_money, "btn-fixed"); D.addClass(_cannel_btn, "btn-fixed"); g.setStyles(f, { display: "none" }); f.appendChild(_reset_money); f.appendChild(_cannel_btn); g.getElementsByClassName("com-contacts-sum")[0].appendChild(f); p.addListener(_reset_money, "click", this.resetMoney, "", this); p.addListener(_cannel_btn, "click", function () { this.resetError(q); this.countTotalMoney(); D.setStyles(f, { display: "none" }) }, "", this)
    }, resetMoney: function (t) {

        j = false; D.setStyles(f, { display: "none" }); if (q.value.isInt() || q.value.isFloat()) { if (parseFloat(q.value) > parseFloat(m.length) * 2000) { this.showError("总金额不能超过" + parseFloat(m.length) * 2000 + "。", q, "span"); return } var u = parseFloat(q.value) / parseFloat(m.length); m.forEach(function (w, x) { w.value = this.fixMoney(u); this.resetError(w) }, this); this.countTotalMoney(); this.resetError(q) } else { this.showError("必须输入数字", q, "span") }
    }, innerMoney: function () { return },
        showPop: function (v, u) {

            j = true; if (m.length == 1) { d.show() } else { d.hidden() } k = u; var t = g.getX(u); var w = g.getY(u) + parseInt(D.getStyle(u, "height").replace("px", "")) + 6; d.setPosition([t, w])
        }, hasBlankInput: function () {

            var t = false; var u = 0; m.forEach(function (w, x) { if (!w.value.trim().length || w.value == _error_info) { u = u + 1; if (u >= 2) { t = true; return } } }); return t
        }, countTotalPerson: function () {

            var t = g.getElementsByClassName("com-contacts-sum")[0]; if (!m.length) { g.addClass(t, "fn-hide") } else { g.removeClass(t, "fn-hide") } r.innerHTML = m.length
        }, countTotalMoney: function () {

            var t = 0; m.forEach(function (u, w) { if (D.hasClass(u.parentNode.parentNode, "fm-error")) { t += 0 } else { t += this.fomatValue(u.value) * 100 } }, this); q.value = this.fixMoney(t / 100)
        }, checkTotalMoney: function (u, t) {

            D.setStyles(f, { display: "" }); this.resetError(q, "span")
        }, fomatValue: function (t) {

            if (t.isInt() || t.isFloat()) { return parseFloat(t) } return 0
        }, countMoney: function (y, t) {

            D.removeClass(t.parentNode, "fm-focus"); j = false; var x = /^(([\d]+.{0,1}[\d]+)|[\d]+)\/[\d]+$/; var v = t.value; if (x.test(v)) { var u = v.split("/")[0]; var w = v.split("/")[1]; var z = parseFloat(u) / parseInt(w); v = this.fixMoney(z); if (w <= 0) { this.showError("公式出错！请输入金额或公式。如：20或100/5。", t); return } if (parseFloat(v) == 0) { t.value = ""; return } if (parseFloat(v) > 2000) { this.showError("金额不能超过2000。", t); return } t.value = v; this.resetError(t) } else { if (v.isInt() || v.isFloat()) { if (parseFloat(v) == 0) { t.value = ""; return } if (parseFloat(v) > 2000) { this.showError("金额不能超过2000。", t); return } t.value = this.fixMoney(new Number(v)); this.resetError(t) } else { if (v.trim().length) { this.showError("公式出错！请输入金额或公式。如：20或100/5。", t) } } } this.resetError(q, "span"); this.countTotalMoney()
        }, keyupHandle: function (u, t) {

            t.value = t.value.replace(/[^(\d|\.|^\/)]/g, "")
        }, showError: function (v, u, w) {

            if (!w) { w = "div" } D.addClass(u.parentNode.parentNode, "fm-error"); var x = g.getElementsByClassName("fm-explain", w, u.parentNode.parentNode); if (x.length) { var t = x[0] } else { var t = Element.create(w, { appendTo: u.parentNode.parentNode }) } t.innerHTML = v; D.addClass(t, "fm-explain"); this.resetPayerStyle()
        }, resetError: function (t, v) {

            if (!v) { v = "div" } D.removeClass(t.parentNode.parentNode, "fm-error"); if (g.getElementsByClassName("fm-explain", v, t.parentNode.parentNode).length) { var u = g.getElementsByClassName("fm-explain", v, t.parentNode.parentNode)[0].parentNode; u.removeChild(g.getElementsByClassName("fm-explain", v, t.parentNode.parentNode)[0]) }
        }, fixMoney: function (u) {

            var t = u.toString().split("."); if (t.length > 1) { if (t[1].length > 2) { if (t[0].toString() == "0") { t = t[1].charAt(0) + t[1].charAt(1) } else { t = t[0].toString() + t[1].charAt(0) + t[1].charAt(1) } t = parseInt(t) + 1; return (t / 100).toFixed(2) } } return u.toFixed(2)
        }
    }
} ();
AP.widget.addContacts = new AP.Class({ initialize: function () {

    this.target = D.get("addAnthor"); this.editContent = D.get("editContent"); this.confirmContent = D.get("confirmContent"); this.firstPerson = D.get("firstPerson"); this.items = [this.firstPerson.parentNode.parentNode.parentNode]; AP.widget.formInputStyle.init(this.firstPerson); E.on(this.firstPerson, "blur", this.validateAccount, this.firstPerson, this); E.on(this.firstPerson, "focus", function (d, a) { var b = a.parentNode.parentNode.parentNode; if (D.query(".fm-explain", b).length) { D.removeClass(b, "fm-error"); b.removeChild(D.query(".fm-explain", b)[0]) } }, this.firstPerson, ""); this.togon = false; this.validated = true; this.up_target = null; E.on(this.target, "click", this.addPerson, "", this); E.on(this.target, "mouseover", function () { this.togon = true }, "", this); E.on(this.target, "mouseout", function () { this.togon = false }, "", this)
}, addPerson: function () {

    var a = this.getCreateEditItem(); this.editContent.appendChild(a[0]); this.items.push(a[0]); this.targgleDEL(); this.targgleADD(); E.on(a[1], "click", this.delPerson, a[1], this); E.on(a[2], "focus", function (f, b) { var d = b.parentNode.parentNode.parentNode; if (D.query(".fm-explain", d).length) { D.removeClass(d, "fm-error"); d.removeChild(D.query(".fm-explain", d)[0]) } }, a[2], ""); E.on(a[2], "blur", this.validateAccount, a[2], this); AP.widget.formInputStyle.init(a[2]); D.addClass(this.confirmContent, "fn-hide"); D.removeClass(this.editContent, "fn-hide"); D.removeClass(D.get("submitButtom"), "fn-hide"); D.addClass(D.get("confirmButtom"), "fn-hide"); a[2].select(); if (!this.validated) { if (D.query(".fm-explain", this.up_target).length) { return } this.up_target.appendChild(this.getCreateErrorInfo()); D.addClass(this.up_target, "fm-error") }
}, delPerson: function (d, a) {

    var b = this.getDelItemIndex(a); this.editContent.removeChild(this.items[b]); this.items.remove(this.items[b]); this.targgleDEL(); this.targgleADD()
}, targgleDEL: function () {

    if (this.items.length <= 1) { var d = this.items[0].getElementsByTagName("div")[0]; var a = d.getElementsByTagName("span"); if (a.length) { d.removeChild(a[0]) } } else { var d = this.items[0].getElementsByTagName("div"); var a = d[0].getElementsByTagName("span"); if (a.length) { return } var a = this.getCreateDel(); var b = Element.create("span", { append: a, appendTo: d[0] }); E.on(a, "click", this.delPerson, a, this) }
}, targgleADD: function () { if (this.items.length >= this.leftcount) { D.addClass(this.target, "fn-hide"); E.removeListener(this.target, "click") } else { D.removeClass(this.target, "fn-hide"); E.removeListener(this.target, "click"); E.on(this.target, "click", this.addPerson, "", this) } }, validateAccount: function (f, a) { var b = /^([a-zA-Z0-9_\.\-\+])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/; var g = /^1\d{10}$/; var d = a.parentNode.parentNode.parentNode; if (b.test(a.value.trim()) || g.test(a.value.trim()) || a.value.trim().length <= 0) { if (D.query(".fm-explain", d).length) { d.removeChild(D.query(".fm-explain", d)[0]); D.removeClass(d, "fm-error") } this.validated = true } else { if (!this.togon) { if (D.query(".fm-explain", d).length) { return } d.appendChild(this.getCreateErrorInfo()); D.addClass(d, "fm-error"); return false } this.validated = false; this.up_target = d } }, getDelItemIndex: function (a) { var b = a.parentNode.parentNode.parentNode; return this.items.indexOf(b) }, getCreateErrorInfo: function () { return Element.create("div", { innerHTML: "Email地址或者手机号码格式有误。", "class": "fm-explain" }) }, getCreateEditItem: function () { var h = Element.create("li", { "class": "edit" }); var f = Element.create("div", { "class": "contacts-item", appendTo: h }); var b = this.getCreateInput(); var d = Element.create("label", { append: b, appendTo: f }); var a = this.getCreateDel(); var g = Element.create("span", { append: a, appendTo: f }); return [h, a, b] }, getCreateInput: function () { return Element.create("input", { "class": "i-text", type: "text" }) }, getCreateDel: function () { return Element.create("a", { href: "javascript:void(0)", innerHTML: "删除" }) }
});
AP.widget.confirmContacts = AP.widget.addContacts.extend({ initialize: function (a) {

    this.leftcount = parseInt(a); this.parent(); this.submitBtn = D.get("submitPerson"); this.confirmBtn = D.get("confirmPerson"); this.invalidate_users = []; this.validate_users = []; E.on(this.submitBtn, "click", this.submitConfirm, this.submitBtn, this); E.on(this.confirmBtn, "click", this.addContacts, this.confirmBtn, this); E.on(D.get("continueAdd"), "click", this.addAgain, "", this)
}, addContacts: function () {

    if (!this.checkIsAllMobile()) { return } var b = new AP.core.api("user/contacts/addToGroups", { onAPISuccess: this.addSuccess, method: "POST" }, this); var d = []; var a = []; this.validate_users.forEach(function (f) { d.push(f.logonId) }); D.query("#chooseGroups a").forEach(function (f) { if (D.hasClass(f, "selected")) { a.push(f.getAttribute("rel")) } }); this.join_groups = a; if (this.validate_users.length) { b.call({ logonIds: d.join(","), groupIds: a.join(",") }) } else { D.addClass(D.query(".com-add-contacts")[0], "fn-hide"); D.removeClass(D.get("addSuccess"), "fn-hide"); D.get("topTitle").innerHTML = "邀请联系人"; D.addClass(D.get("tipInfo"), "fn-hide"); this.buildInviteDom() }
}, addAgain: function () {

    this.items = []; D.addClass(D.get("addSuccess"), "fn-hide"); D.addClass(this.confirmContent, "fn-hide"); D.addClass(D.get("confirmButtom"), "fn-hide"); D.removeClass(this.editContent, "fn-hide"); D.removeClass(D.get("submitButtom"), "fn-hide"); D.get("topTitle").innerHTML = "添加联系人"; D.removeClass(D.get("tipInfo"), "fn-hide"); D.removeClass(D.query(".com-add-contacts", D.get("container"))[0], "fn-hide"); D.get("editContent").innerHTML = ""; this.addPerson()
}, addSuccess: function (d, a) {

    var a = a[0]; D.addClass(D.query(".com-add-contacts")[0], "fn-hide"); D.removeClass(D.get("addSuccess"), "fn-hide"); D.get("topTitle").innerHTML = "邀请联系人"; D.addClass(D.get("tipInfo"), "fn-hide"); window.parent.mytip.show(a.msg); if (this.invalidate_users.length) { var b = 0; var f = 0; this.invalidate_users.forEach(function (g) { if (/^1\d{10}$/.test(g.logonId)) { b += 1 } else { f += 1 } }); if (f > 0) { this.buildInviteDom() } else { window.parent.location.href = "index.htm"; self.parent.AP.widget.xBox.hide() } } else { window.parent.location.href = "index.htm"; self.parent.AP.widget.xBox.hide() }
}, buildInviteDom: function () {

    D.get("invalidateUserNum").innerHTML = this.invalidate_users.length; var a = '<table><thead><tr><th class="first"><span>对方名称</span></th><th class="last"><span>邀请方式</span> </th></tr></thead><tbody>{body}</tbody></table>'; var b = ""; this.invalidate_users.forEach(function (f, h) { if (!/^1\d{10}$/.test(f.logonId)) { if (f.logonId.length > 30) { var d = f.logonId.split("@"); if (d[0].length > 17) { d[0] = d[0].substr(0, 14) + "..." } if (d[0].length > 12) { d[1] = d[0].substr(0, 9) + "..." } var g = d[0] + "@" + d[1] } else { var g = f.logonId } b += "<tr><td>" + g + '</td><td class="last"><a href="javascript:void(0)" class="invite" rel="email" title="' + g + '">发送邮件邀请</a><span class="m-success fn-hide"></span></td></tr>' } }); a = a.replace("{body}", b); D.get("invalidateUserBody").innerHTML = a; this.sendInvite()
}, sendInvite: function () {

    D.query(".invite").forEach(function (a) { E.on(a, "click", function () { var b = new AP.core.api("user/contacts/inviteContact", { onAPISuccess: function (g, d) { D.addClass(a, "fn-hide"); var f = D.query(".m-success", a.parentNode)[0]; f.innerHTML = d[0].msg; D.removeClass(f, "fn-hide") }, onAPIFailure: function (g, d) { D.addClass(a, "fn-hide"); var f = D.query(".m-success", a.parentNode)[0]; f.innerHTML = d[0].msg; D.removeClass(f, "m-success"); D.removeClass(f, "fn-hide"); D.addClass(f, "m-error") }, method: "POST" }); b.call({ logonId: a.title, type: a.getAttribute("rel"), cgIds: this.join_groups.join(",") }) }, a, this) }, this)
}, submitConfirm: function () {

    if (!this.validatorLength()) { return } if (this.hasError()) { return } this.validateUsers(); this.showSubmitBtn()
}, validatorLength: function () {

    var a = 0; this.items.forEach(function (b, d) { if (b.getElementsByTagName("input")[0].value.trim()) { a = a + 1 } }); return a
}, showSubmitBtn: function () {

    if (this.items.length < this.leftcount) { D.removeClass(this.target, "fn-hide"); E.removeListener(this.target, "click"); E.on(this.target, "click", this.addPerson, "", this) }
}, showConfirmContent: function () {

    D.addClass(this.editContent, "fn-hide"); D.addClass(D.get("submitButtom"), "fn-hide"); D.removeClass(this.confirmContent, "fn-hide"); D.removeClass(D.get("confirmButtom"), "fn-hide")
}, hasError: function () {

    return D.query("#editContent .fm-error").length
}, deleteItems: function (h, d) {

    var b = this.confirmItems.indexOf(d.parentNode.parentNode.parentNode); var g = this.confirmItems[b]; var f = this.items[b]; var a = f.getElementsByTagName("input")[0]; this.items.remove(f); this.confirmItems.remove(g); g.parentNode.removeChild(g); f.parentNode.removeChild(f); this.validate_users.forEach(function (j) { if (j.logonId == a.value) { this.validate_users.remove(j) } }, this); this.invalidate_users.forEach(function (j) { if (j.logonId == a.value) { this.invalidate_users.remove(j) } }, this); this.targgleCDEL(); this.targgleADD()
}, targgleCDEL: function () {

    if (this.confirmItems.length <= 1) { var d = this.confirmItems[0].getElementsByTagName("div")[0]; var b = d.getElementsByTagName("span")[0]; var a = D.query(".delete", d); if (a.length) { b.removeChild(a[0]) } } else { var d = this.confirmItems[0].getElementsByTagName("div"); var a = d[0].getElementsByTagName("span") }
}, deleteBlankItem: function () {

    var d = []; for (var b = 0; b < this.items.length; b++) { var f = this.items[b]; var a = f.getElementsByTagName("input")[0]; if (!a.value.trim().length) { this.editContent.removeChild(f); d.push(this.items[b]) } } d.forEach(function (g, h) { this.items.remove(g) }, this)
}, uniqueItems: function () {

    var f = []; this.items.forEach(function (k, j) { f.push(k) }); for (var d = 0; d < f.length; d++) { for (var b = d + 1; b < f.length; b++) { var h = f[b].getElementsByTagName("input")[0].value.trim(); var a = f[d].getElementsByTagName("input")[0].value.trim(); if (h == a) { try { f[b].parentNode.removeChild(f[b]); this.items.remove(f[b]) } catch (g) { } } } } this.targgleDEL()
}, validateUsers: function () {

    this.uniqueItems(); this.deleteBlankItem(); var b = []; this.invalidate_users = []; this.items.forEach(function (d, f) { b.push(d.getElementsByTagName("input")[0].value) }); var a = new AP.core.api("user/contacts/getValidateUsers", { onAPISuccess: this.buildConfirm, method: "POST" }, this); a.call({ account: b.join(",") })
}, buildConfirm: function (f, b) {

    b = b[0]; if (this.isValidateFalse(b)) { return } this.showConfirmContent(); this.confirmContent.innerHTML = ""; this.confirmItems = []; this.users = b.users; b.users.forEach(function (g, j) { var h = this.getCreateConfirmItem(g); this.confirmItems.push(h[0]); this.confirmContent.appendChild(h[0]); E.on(h[1], "click", this.showEdit, h[1], this); E.on(h[2], "click", this.deleteItems, h[2], this) }, this); if (this.items.length <= 1) { var a = D.query(".delete", this.confirmItems[0]); var d = D.query(".contacts-item", this.confirmItems[0])[0].getElementsByTagName("span")[0]; if (a.length) { d.removeChild(a[0]) } } this.checkIsAllMobile()
}, checkIsAllMobile: function () {

    var a = 0; var b = 0; if (this.validate_users.length <= 0) { this.invalidate_users.forEach(function (d) { if (/^1\d{10}$/.test(d.logonId)) { a += 1 } else { b += 1 } }); if (b == 0 && a > 0) { D.removeClass(this.confirmBtn.parentNode, "btn-ok"); D.addClass(this.confirmBtn.parentNode, "btn-ok-disabled"); return false } else { D.addClass(this.confirmBtn.parentNode, "btn-ok"); D.removeClass(this.confirmBtn.parentNode, "btn-ok-disabled") } } D.addClass(this.confirmBtn.parentNode, "btn-ok"); D.removeClass(this.confirmBtn.parentNode, "btn-ok-disabled"); return true
}, isValidateFalse: function (a) {

    var b = false; a.users.forEach(function (d) { if (!d.validateResult) { if (d.failedReason == "ERROR.CONTACTS.NOTALLOW_ADD" || d.failedReason == "ERROR.CONTACTS.ADDED" || /^1\d{10}$/.test(d.logonId)) { b = true; this.items.forEach(function (f) { if (D.query("input", f)[0].value.trim() == d.logonId) { if (/^1\d{10}$/.test(d.logonId) && d.failedReason != "ERROR.CONTACTS.ADDED") { if (d.failedReason == "ERROR.CONTACTS.NOTALLOW_ADD") { this.getCreateError(f, d.failedReasonDes) } else { this.getCreateError(f, "对方手机号不是支付宝会员，不能添加。") } } else { this.getCreateError(f, d.failedReasonDes) } } }, this) } } }, this); return b
}, showEdit: function (f, d) {

    var b = this.confirmItems.indexOf(d.parentNode.parentNode.parentNode); var a = this.items[b].getElementsByTagName("input")[0]; D.removeClass(this.editContent, "fn-hide"); D.removeClass(D.get("submitButtom"), "fn-hide"); D.addClass(this.confirmContent, "fn-hide"); D.addClass(D.get("confirmButtom"), "fn-hide"); a.focus()
}, getCreateConfirmItem: function (b) {

    var l = Element.create("li"); var a = Element.create("div", { "class": "contacts-item", appendTo: l }); if (b.logonId.length > 30) { var k = b.logonId.split("@"); if (k[0].length > 17) { k[0] = k[0].substr(0, 14) + "..." } if (k[0].length > 12) { k[1] = k[0].substr(0, 9) + "..." } var m = k[0] + "@" + k[1] } else { var m = b.logonId } if (b.validateResult) { var f = Element.create("label", { innerHTML: b.realName + " (" + m + ")", appendTo: a, title: b.logonId }) } else { var f = Element.create("label", { innerHTML: m, appendTo: a, title: b.logonId }) } var h = Element.create("span", { appendTo: a }); var g = Element.create("a", { href: "javascript:void(0)", appendTo: h, innerHTML: "修改" }); var j = Element.create("a", { href: "javascript:void(0)", appendTo: h, innerHTML: "删除", "class": "delete" }); if (!b.validateResult) { this.invalidate_users.push(b); var d = Element.create("div", { innerHTML: b.failedReasonDes, appendTo: l, "class": "fm-explain" }); D.addClass(l, "fm-message current") } else { this.validate_users.push(b) } return [l, g, j]
}, getCreateError: function (d, b) {
    var a = Element.create("div", { innerHTML: b, appendTo: d, "class": "fm-explain" }); D.addClass(d, "fm-error"); return a
}, newAPI: function (b, a) { return new AP.core.api(b, { onAPISuccess: a }) }
});
AP.widget.appendToGroup = new AP.Class({ initialize: function (a) {
    D.query("#chooseGroups a").forEach(function (b) {
        E.on(b, "click", this.changeStatus, b, this)
    }, this);
    E.on(D.get("createGroupBtn"), "click", function (b) { D.addClass(D.get("createGroupBtn").parentNode, "fn-hide"); D.removeClass(D.get("createGroupContainer"), "fn-hide"); AP.widget.formInputStyle.init(D.get("groupTxt")); if (!D.get("groupTxt").value.trim().length) { D.get("groupTxt").value = "未命名分组" } D.get("groupTxt").focus(); D.get("groupTxt").select(); E.preventDefault(b) }, "", this); E.on(D.get("cannelChoose"), "click", function (b) { D.removeClass(D.get("createGroupBtn").parentNode, "fn-hide"); D.addClass(D.get("createGroupContainer"), "fn-hide"); E.preventDefault(b) }); E.on(D.get("createBtn"), "click", this.createGroup, "", this); E.on(D.get("groupTxt"), "blur", function (d) { var b = E.getTarget(d); if (b.id == "groupTxt") { return } if (!this.value.trim().length) { this.value = "未命名分组" } }); E.on(D.get("groupTxt"), "focus", function (d) { var b = D.query(".fm-explain", this.parentNode)[0]; D.removeClass(this.parentNode, "fm-error"); b.innerHTML = "最多10个字。" }); this.options = this.setOptions(a); this.completeEvent = new U.CustomEvent("completeEvent"); this.completeEvent.subscribe(this.options.completeEvent, this, true)
}, setOptions: function (a) {

    return AP.hashExtend({ completeEvent: function () { } }, a || {})
}, createGroup: function () {

    if (!D.get("groupTxt").value.trim().length) { return } var b = { onAPISuccess: this.innerNewTag, onAPIFailure: this.createGroupFail, method: "POST" }; if (this.options.apiUrl) { AP.hashExtend(b, { api_url: this.options.apiUrl, format: "jsonp" }) } var a = new AP.core.api("user/contacts/createGroup", b, this); a.call({ groupName: D.get("groupTxt").value, _input_charset: "utf-8" })
}, createGroupFail: function (f, a) {

    var d = D.get("groupTxt").parentNode;
    var b = D.query(".fm-explain", d)[0];
    b.innerHTML = a[0].msg;
    D.addClass(d, "fm-error")
}, innerNewTag: function (f, d) {

    var b = Element.create("a", { appendTo: D.get("chooseGroups"), "class": "selected", rel: d[0].result.id }); Element.create("span", { innerHTML: d[0].result.name, appendTo: b }); E.on(b, "click", this.changeStatus, b, this); D.get("groupTxt").value = ""; D.removeClass(D.get("createGroupBtn").parentNode, "fn-hide"); D.addClass(D.get("createGroupContainer"), "fn-hide"); if (parseInt(d[0].groupCount) >= 10) { D.addClass(D.query(".p-add-grounp")[0], "fn-hide") } this.completeEvent.fire(d)
}, changeStatus: function (d, b) {

    if (D.hasClass(b, "selected")) {

        D.removeClass(b, "selected")
    } else { D.addClass(b, "selected") } E.preventDefault(d)
}
});

AP.widget.autoMatchContacts = function () {

    var a = D.get("ipt-search-key");
    var f = false; E.on(a, "focus", function () {

        if (this.value == _("search_from_contacts")) { this.value = ""; this.select() } D.removeClass(a, "i-text-gray")
    }, "", "");
    E.on(a, "blur", function (h) {

        var g = E.getTarget(h); if (f) { return } if (this.value != _("search_from_contacts") && this.value.trim().length) { return } this.value = _("search_from_contacts"); D.addClass(a, "i-text-gray")
    }, "", "");

    E.on(D.get("J_SearchResult"), "click", function () {

        if (a.value.trim().length) { if (a.value.trim() != _("search_from_contacts")) { D.get("search-contact").submit() } }
    });
    E.on(D.get("autoCompleteContainer"), "mouseover", function () { f = true });
    E.on(D.get("autoCompleteContainer"), "mouseout", function () { f = false });
    E.on(D.get("J_SearchResult"), "mouseover", function () { f = true });
    E.on(D.get("J_SearchResult"), "mouseout", function () { f = false });
    var d = new YAHOO.util.XHRDataSource("/user/contacts/searchResult.json");
    d.responseType = YAHOO.util.XHRDataSource.TYPE_JSON;
    d.connMethodPost = true; d.maxCacheEntries = 60;
    d.responseSchema = { resultsList: "result", fields: ["contactLogonId", "contactNickName", "ownerCardNo", "contactRealName"] };
    var b = new YAHOO.widget.AutoComplete("ipt-search-key", "autoCompleteContainer", d);
    b.queryDelay = 0.5;
    b.queryDelay = 0;
    b.resultTypeList = false;
    b.autoHighlight = false;
    b.generateRequest = function (g) {
        return "keyword=" + g
    };
    b.formatResult = function (j, n, h) {
        var g = D.query("#autoCompleteContainer ul")[0];
        D.setStyle(g, "width", "174px"); D.setStyles(g, { display: "" });
        if (j.contactNickName) {
            var l = j.contactNickName
        } else { var l = "" } if (j.contactRealName) {
            var m = j.contactRealName; if (m.len() > 8) { m = m.brief(8) + ".." } else { m = m }
        } else {
            var m = ""
        }
        var k = j.contactLogonId;
        if (k.len() > 16) { k = k.brief(16) + ".." }
        else { k = k } return '<span title="' + l + '" rel="' + k + '">' + m + "(" + k + ")</span>"
    };
    b.itemSelectEvent.subscribe(function (j, h) {
        var g = h[1]; D.get("search-contact").submit()
    });
    b.dataErrorEvent.subscribe(function (h, j) {
        var g = D.query("#autoCompleteContainer ul")[0]; D.setStyles(g, { display: "none" })
    }); return { oDS: d, oAC: b }
};
AP.widget.appendContacts = function () {
    var j = YAHOO.util.Event; _request_url = null; _require_fields = null; _target_btn = null; var d = function () { var n = _require_fields.length; for (var m = 0; m < _require_fields.length; m++) { if (!_require_fields[m].checked) { n-- } } return n }; var a = function () { var n = ""; for (var m = 0; m < _require_fields.length; m++) { if (_require_fields[m].checked) { n += "emails=" + _require_fields[m].value + "&" } } return n }; var h = function () { var n = d(); if (!n) { return } var m = a(); AP.ajax.asyncRequest("POST", _request_url, l, m) }; var k = function () { _target_btn.parentNode.setAttribute("onmouseout", ""); _target_btn.parentNode.setAttribute("onmouseover", ""); j.removeListener(_target_btn, "mouseout"); j.removeListener(_target_btn, "mouseover"); D.removeClass(_target_btn.parentNode, "btn-ok"); D.addClass(_target_btn.parentNode, "btn-ok-disabled") }; var g = function () { D.removeClass(_target_btn.parentNode, "btn-ok-disabled"); D.addClass(_target_btn.parentNode, "btn-ok"); j.addListener(_target_btn, "mouseout", function (m) { this.parentNode.className = "btn btn-ok" }); j.addListener(_target_btn, "mouseover", function (m) { this.parentNode.className = "btn btn-ok-hover" }) }; var l = { success: function (p) {

        var m = p.responseText.split(","); try {

            if (D.get("addWaiting")) {

                D.get("addWaiting").parentNode.removeChild(D.get("addWaiting"))
            }
            if (D.get("addSuccessBox")) {

                D.removeClass(D.get("addSuccessBox"), "fn-hide")
            }
            if (D.get("addSuccessNum")) {

                D.get("addSuccessNum").innerHTML = m[1]
            }
        } catch (n) { log(n) }
    }
    };
    var f = function () {

        j.addListener(_target_btn, "click", h); for (var m = 0; m < _require_fields.length; m++) {

            _require_fields[m].checked = "checked"; j.addListener(_require_fields[m], "click", b)
        }
    };
    var b = function () {

        var m = d(); if (m) { g() } else { k() }
    }; return { init: function (o, n, m) { _request_url = m; _target_btn = o; _require_fields = n; f() } }
} ();
AP.widget.choosePerson = function () {

    var b = YAHOO.util.Dom;
    var t = YAHOO.util.Event;
    var p = null;
    var a = null;
    var q = null;
    var f = null;
    var k = null;
    var n = null;
    var d = null;
    var s = null;
    var j = function () { if (D.hasClass(k.parentNode, "fm-error") && f.innerHTML == "关闭联系人") { D.addClass(q, "fn-hide") } else { D.hasClass(q, "fn-hide") ? D.removeClass(q, "fn-hide") : D.addClass(q, "fn-hide") } D.hasClass(q, "fn-hide") ? f.innerHTML = "从我的联系人中添加" : f.innerHTML = "关闭联系人" }; var o = function () { var w = k.value.trim(); if (!w.length || D.hasClass(k.parentNode, "fm-error") || f.innerHTML == "从我的联系人中添加") { j(); return } if (w == d) { g() } else { if (n != null) { n.checked = ""; D.removeClass(n.parentNode, "current") } var v = m(w); if (v) { u(v) } else { l(w) } } if (D.hasClass(D.get("personWaiting"), "fn-hide")) { D.addClass(q, "fn-hide") } }; var m = function (x) { for (var w = 0; w < _contacts.length; w++) { for (var v = 0; v < _contacts[w].users.length; v++) { if (x == _contacts[w].users[v].account) { return _contacts[w].users[v] } } } return false }; var l = function (x) { var v = "email=" + x + "&row_no=1"; var w = AP.ajax.asyncRequest("POST", s, h, v) }; var h = { success: function (x) { var w = x.responseText.split(","); if (w[1].length == 0) { var v = { real_name: "", account: k.value} } else { var v = { real_name: w[1], account: k.value} } u(v) } }; var g = function () { b.addClass(p, "fn-hide"); b.removeClass(p, "fm-error"); b.removeClass(a, "fn-hide") }; var u = function (v) { var x = Element.create("span"); var w = Element.create("a", { href: "javascript:void(0);" }); x.innerHTML = "<em>" + v.real_name + "</em> (" + v.account + ")&nbsp;&nbsp;"; w.innerHTML = "重新选择联系人"; k.value = v.account; d = v.account; a.innerHTML = ""; a.appendChild(x); a.appendChild(w); t.addListener(w, "click", r, v, this); g(); try { D.get("contact_" + v.id).checked = "checked"; n = D.get("contact_" + v.id); D.addClass(n.parentNode, "current") } catch (y) { } if (D.hasClass(D.get("personWaiting"), "fn-hide")) { D.addClass(q, "fn-hide") } return [x, w] }; var r = function (w, v) { b.addClass(a, "fn-hide"); b.removeClass(p, "fn-hide"); b.removeClass(q, "fn-hide"); f.innerHTML = "关闭联系人"; D.removeClass(D.getAncestorByClassName(k, "fm-item"), "fm-error") }; return { init: function (w, v) { p = D.get("personWaiting"); a = D.get("personEditing"); q = D.get("personContact"); k = D.get("personInput"); f = D.get("extendContacts"); s = v; _contacts = w; t.addListener(D.get("closeContacts"), "click", function () { f.innerHTML = "关闭联系人"; o(); b.addClass(q, "fn-hide") }); t.addListener(f, "click", o) }, addPerson: function (y, v) { if (n != null) { n.checked = ""; D.removeClass(n.parentNode, "current") } var z = u(v); var x = z[0]; var w = z[1]; b.addClass(q, "fn-hide"); b.addClass(p, "fn-hide"); b.removeClass(p, "fm-error"); b.removeClass(a, "fn-hide") } }
} ();
AP.widget.buildGroupDom = new AP.Class({

    render: function () { },
    setHTML: function () {
        html = ""; return html
    },
    getElements: function () { this.body = D.query(".c-group-all")[0]; this.head = D.query(".c-group-select-title")[0] }, show: function () { D.removeClass(this.container, "fn-hide") }, hidden: function () { D.addClass(this.container, "fn-hide") }
});
AP.widget.viewChoosedGroup = AP.widget.buildGroupDom.extend({ initialize: function (a) {
    this.render(); this.getElements(); a.forEach(function (b) { E.on(b, "click", this.viewGroup, b, this) }, this); this.current_el = null; this.tripdown = new AP.widget.tipdone(); document.body.appendChild(this.container)
}, render: function () { if (this.container) { return } this.container = Element.create("div", { "class": "c-group-select fn-hide" }); var a = this.setHTML(); this.container.innerHTML = a; document.body.appendChild(this.container) }, setHTML: function () { D.addClass(this.container, "view-group-contanier"); html = '<div class="c-group-select-container  view-group-contanier"><div class="c-group-select-content"><div class="c-group-all"></div></div><div class="c-group-select-title mid"></div></div>'; return html }, setHead: function () { this.head.innerHTML = "分组："; var a = Element.create("a", { innerHTML: "关闭查看", appendTo: this.head }); E.on(a, "click", this.hidden, a, this); E.on(a, "click", function () { this.showBottomGroup() }, a, this) }, viewGroup: function (b, a) { if (this.current_el != null) { this.showBottomGroup() } this.current_el = a; this.setHead(); this.innerData(a); this.show(); this.position(a); this.hiddenBottomGroup(a); E.preventDefault(b) }, hiddenBottomGroup: function (b) { var a = b.parentNode.getElementsByTagName("span")[0]; D.addClass(a, "fn-hide") }, showBottomGroup: function () { var a = this.current_el.parentNode.getElementsByTagName("span")[0]; D.removeClass(a, "fn-hide") }, innerData: function (a) { var b = this.getChoosedData(a); this.num = 0; this.body.innerHTML = ""; b.forEach(function (f, h) { if (this.num >= 36) { this.num = 0 } if (f.name.unescapeHTML().length > 10) { var g = f.name.unescapeHTML().substr(0, 7).escapeHTML() + "..." } else { var g = f.name } this.getCreateTag(f); this.num += g.unescapeHTML().len(); if (h < b.length - 1) { if (D.hasClass(a, "view-choose-group")) { } else { if (this.num < 36) { this.getCreateEm() } } } }, this) }, getCreateTag: function (g) { if (g.name.unescapeHTML().length > 10) { var f = g.name.unescapeHTML().substr(0, 7).escapeHTML() + ".." } else { var f = g.name } var b = Element.create("a", { title: g.name, appendTo: this.body, rel: g.id, href: "index_group.htm?src=contactGroup&groupId=" + g.id }); Element.create("span", { innerHTML: f, appendTo: b }); return b }, getCreateEm: function () { return }, getChoosedData: function (a) { if (a.parentNode.getAttribute("rel")) { var b = AP.cache.usergroups[a.parentNode.getAttribute("rel")]; if (b === undefined) { b = [] } } else { var b = [] } return b }, position: function (b) { var d = D.getRegion(this.container); var g = d.right - d.left - 193; var a = D.getX(b) - 110 - g; var f = D.getY(b) + 19; D.setXY(this.container, [a, f]); this.show() }, show: function () { D.removeClass(this.container, "fn-hide"); D.addClass(D.query(".edit-group-contanier")[0], "fn-hide") }
});
AP.widget.tagChooseGroup = AP.widget.viewChoosedGroup.extend({ initialize: function (b, a) {

    this.groups = a; this.parent(b); this.body.id = "chooseGroupTag"
}, render: function () {

    if (this.container) { return } this.container = Element.create("div", { "class": "c-group-select fn-hide" }); var a = this.setHTML(); this.container.innerHTML = a; document.body.appendChild(this.container)
}, getElements: function () {

    this.body = D.query(".edit-group-body")[0]; this.head = D.query(".edit-group-head")[0]; E.on(D.query(".new-group-tag")[0], "click", function (a) { D.addClass(D.get("newGroupAction"), "fn-hide"); D.removeClass(D.get("newGroupButton"), "fn-hide"); if (!D.get("groupTxt").value.trim().length) { D.get("groupTxt").value = "未命名分组" } D.get("groupTxt").select(); E.preventDefault(a) }, "", this); E.on(D.get("createBtn"), "click", this.createGroup, "", this); E.on(D.get("cannelBtn"), "click", function (a) { D.removeClass(D.get("newGroupAction"), "fn-hide"); D.addClass(D.get("newGroupButton"), "fn-hide"); E.preventDefault(a) }, "", this); E.on(D.get("saveGroupBtn"), this.save, "", this); AP.widget.formInputStyle.init(D.get("groupTxt")); D.query(".create-group-btn", this.container).forEach(function (a) { E.on(a, "click", this.save, "", this) }, this); D.query(".cannel_choose", this.container).forEach(function (a) { E.on(a, "click", this.cannelChoose, "", this) }, this); E.on(D.get("groupTxt"), "focus", function (a) { D.query(".fm-explain", this.parent)[0].innerHTML = "最多10个字。" })
}, createGroup: function () {

    if (!D.get("groupTxt").value.trim().length) { return } var a = new AP.core.api("user/contacts/createGroup", { onAPISuccess: this.innerNewTag, onAPIFailure: this.createGroupFail, method: "POST" }, this); a.call({ groupName: D.get("groupTxt").value })
}, innerNewTag: function (b, a) {

    this.num += a[0].result.name.unescapeHTML().len(); if (this.num >= 40) { this.getCreateTag(a[0].result, true); this.num = 0 } else { this.getCreateEm(); this.getCreateTag(a[0].result, true) } if (parseInt(a[0].groupCount) >= 10) { D.addClass(D.query(".new-group-tag")[0], "fn-hide") } this.groups.push(a[0].result); D.removeClass(D.get("newGroupAction"), "fn-hide"); D.addClass(D.get("newGroupButton"), "fn-hide"); D.get("groupTxt").value = ""
}, createGroupFail: function (f, a) {

    var a = a[0]; var d = D.get("groupTxt").parentNode; D.addClass(d, "fm-error"); var b = D.query(".fm-explain", d)[0]; b.innerHTML = a.msg
}, cannelChoose: function () {

    var a = D.query("#chooseGroupTag a"); a.forEach(function (b) { D.removeClass(b, "selected") })
}, setHTML: function () {

    D.addClass(this.container, "edit-group-contanier"); if (this.groups.length >= 10) { var a = "" } else { var a = '<a class="fn-left new-group-tag" href="#">新建分组</a>' } html = '<div class="c-group-select-container"><div class="c-group-select-title edit-group-head"></div><div class="c-group-select-content"><div class="c-group-all edit-group-body"></div><div class="c-group-select-action ft-right" id="newGroupAction">' + a + '<input value="保存" type="button" class="btn-2cn create-group-btn"> <a href="javascript:void(0)" class="cannel_choose">[清空选择]</a></div><div class="c-group-select-action fn-hide" id="newGroupButton"><ul id="createGroup" class="fm-input fm-add-group"><li class="fm-item"><input id="groupTxt" class="i-text i-text-s" maxlength="10" value="未命名分组"/> <input id="createBtn" class="btn-2cn" type="button" value="创建"> <a href="#" id="cannelBtn">取消</a><div class="fm-explain">最多10个字。</div></li></ul><div class="c-group-save"><input value="保存" type="button" class="btn-2cn create-group-btn"> <a href="javascript:void(0)" class="cannel_choose">[清空选择]</a></div></div></div></div>'; return html
}, setHead: function () {

    this.head.innerHTML = "分组：  "; var a = Element.create("a", { innerHTML: "取消", appendTo: this.head }); E.on(a, "click", this.hidden, "", this); E.on(a, "click", function () { this.showBottomGroup() }, "", this)
}, save: function () {

    if (!D.hasClass(D.get("newGroupButton"), "fn-hide")) { var a = new AP.core.api("user/contacts/createGroup", { onAPISuccess: this.groupCreateOk, onAPIFailure: this.createGroupFail, method: "POST" }, this); a.call({ groupName: D.get("groupTxt").value }) } else { this.saveContactHandle() }
}, groupCreateOk: function (b, a) {

    this.innerNewTag(b, a); this.saveContactHandle()
}, saveContactHandle: function () {

    var a = this.getChoosedTags(); if (D.hasClass(this.current_el, "save-contact")) { var b = new AP.core.api("user/contacts/addSingleContact", { onAPISuccess: this.saveContactOK, onAPIFailure: this.systemException, method: "POST" }, this); b.call({ groupIds: a, logonId: D.query(".account", this.current_el.parentNode.parentNode)[0].title }) } else { var b = new AP.core.api("user/contacts/changeUserGroup", { onAPISuccess: this.addContactOK, method: "POST", cache: false }, this); b.call({ groupIds: a, logonIds: D.query(".account", this.current_el.parentNode.parentNode)[0].title }) } this.hidden()
}, systemException: function (b, a) {

    if (a[0].msg) { new AP.widget.errorXbox({ error_info: a[0].msg, url_info: '<a href="/user/contacts/index.htm">返回我的联系人 </a>' }) }
}, saveContactOK: function (k, f) {

    this.showBottomGroup(); this.changeText(f); var f = f[0]; this.current_el.innerHTML = "[修改]"; mytip.show(f.msg); D.removeClass(this.current_el, "ico-plus"); D.removeClass(this.current_el, "save-contact"); var d = D.query(".J_item", this.current_el.parentNode.parentNode.parentNode)[0]; var a = []; f.contactGroupList.forEach(function (l, m) { a.push({ name: l.name.escapeHTML(), id: l.id }) }); var j = "user_" + f.crmUserId; this.current_el.parentNode.setAttribute("rel", j); AP.cache.usergroups[j] = a; d.disabled = ""; d.id = "contacts_item_" + f.crmUserId; D.addClass(d, "J_item"); var h = D.query("label", this.current_el.parentNode.parentNode.parentNode)[0]; var b = D.query(".nickname_edit", this.current_el.parentNode.parentNode.parentNode)[0]; var g = D.query(".delete", this.current_el.parentNode.parentNode.parentNode)[0]; h.setAttribute("for", d.id); D.removeClass(g, "fn-hide"); D.removeClass(b, "fn-hide")
}, addContactOK: function (b, a) {

    this.showBottomGroup(); this.changeText(a); mytip.show("修改分组成功。")
}, changeText: function (d) {

    var h = this.current_el.parentNode.getElementsByTagName("span")[0]; html = []; var a = []; d[0].contactGroupList.forEach(function (k, l) { a.push({ name: k.name.escapeHTML(), id: k.id }) }); var g = this.current_el.parentNode.getAttribute("rel"); AP.cache.usergroups[g] = a; var b = 0; var j = 0; d[0].contactGroupList.forEach(function (m, o) { if (b >= 10 || o > 2) { return false } j = o + 1; var k = b; b = b + m.name.unescapeHTML().length; if (b > 10) { var l = m.name.unescapeHTML().substring(0, 10 - k).escapeHTML() + "..." } else { var l = m.name } html.push('<a href="index_group.htm?src=contactGroup&groupId=' + m.id + '" title="' + m.name + '">' + l + "</a>") }); if (d[0].contactGroupList.length > 3) { h.innerHTML = "分组：" + html.join("，") + " 等" + d[0].contactGroupList.length + "个"; var f = D.query(".view-choose-group", this.current_el.parentNode)[0]; D.removeClass(f, "fn-hide") } else { if (d[0].contactGroupList.length == 0) { var f = D.query(".view-choose-group", this.current_el.parentNode)[0]; h.innerHTML = "分组：无"; D.addClass(f, "fn-hide") } else { var f = D.query(".view-choose-group", this.current_el.parentNode)[0]; if (j < d[0].contactGroupList.length) { h.innerHTML = "分组：" + html.join("，") + " 等" + d[0].contactGroupList.length + "个"; D.removeClass(f, "fn-hide") } else { h.innerHTML = "分组：" + html.join("，"); D.addClass(f, "fn-hide") } } }
}, changeStatus: function (d, b) {

    if (D.hasClass(b, "selected")) { D.removeClass(b, "selected") } else { D.addClass(b, "selected") } E.preventDefault(d)
}, getChoosedTags: function () {

    var b = D.query("#chooseGroupTag a"); var a = []; b.forEach(function (d) { if (D.hasClass(d, "selected")) { a.push(d.getAttribute("rel")) } }); return a.join(",")
}, innerData: function (a) {

    if (a.parentNode.getAttribute("rel") == "[]") { var f = [] } else { var f = this.getChoosedData(a) } var b = []; var d = this.groups; this.num = 0; this.body.innerHTML = ""; f.forEach(function (g) { b.push(g.id.toString()) }); d.forEach(function (g, h) { if (this.num >= 36) { this.num = 0 } if (b.has(g.id.toString())) { this.getCreateTag(g, true) } else { this.getCreateTag(g) } this.num += g.name.unescapeHTML().len(); if (h < d.length - 1) { if (D.hasClass(a, "view-choose-group")) { this.body.appendChild(document.createTextNode("，")) } else { if (this.num < 36) { this.getCreateEm() } } } }, this)
}, position: function (b) {

    var d = D.getRegion(b); var a = d.right - 373; var f = D.getY(b) + 23; D.setXY(this.container, [a, f]); this.show()
}, getCreateTag: function (h, f) {

    if (h.name.unescapeHTML().length > 10) { var g = h.name.unescapeHTML().substr(0, 7).escapeHTML() + ".." } else { var g = h.name } g = "<span>" + g + "</span>"; if (f) { var b = Element.create("a", { title: h.name, innerHTML: g, appendTo: this.body, "class": "selected", rel: h.id, href: "#" }) } else { var b = Element.create("a", { title: h.name, innerHTML: g, appendTo: this.body, rel: h.id, href: "#" }) } E.on(b, "click", this.changeStatus, b, this)
}, show: function () {

    D.removeClass(D.get("newGroupAction"), "fn-hide"); D.addClass(D.get("newGroupButton"), "fn-hide"); D.removeClass(this.container, "fn-hide"); D.addClass(D.query(".view-group-contanier")[0], "fn-hide")
}
});
AP.widget.contactsPayer = function () {

    var l = YAHOO.util.Dom; var s = YAHOO.util.Event; var t; var d = null; var h; var m = function (w, x) { if (w.real_name == w.nick_name || w.nick_name == "请输入联系人姓名") { var v = Element.create("li", { title: w.real_name + " " + w.account }) } else { var v = Element.create("li", { title: w.real_name + " [" + w.nick_name + "] " + w.account }) } var u = Element.create("input", { type: "checkbox", id: "contact_" + w.id + "_" + x, value: w.real_name + "|" + w.nick_name + "|" + w.account + "|" + w.id }); D.addClass(u, "contact_" + w.id); if (w.real_name.len() > 8) { real_name = w.real_name.brief(8) + ".." } else { real_name = w.real_name } if (w.account.len() > 16) { account = w.account.brief(18) + ".." } else { account = w.account } v.innerHTML = '<label for="contact_' + w.id + "_" + x + '"><em>' + real_name + "</em> (" + account + ")</label>"; v.insertBefore(u, v.firstChild); s.addListener(v, "mouseover", function () { l.addClass(this, "hover") }); s.addListener(v, "mouseout", function () { l.removeClass(this, "hover") }); s.addListener(u, "click", h.addPerson, w, h); s.addListener(u, "click", _changeChooseStatus); s.addListener(u, "click", function (y) { q(this); g() }); return v }; var g = function () { D.get("customPerson").value = ""; var u = l.getElementsByClassName("fm-explain", "div", D.get("customAddPerson-error").parentNode)[0]; u.innerHTML = "支付宝账户名是Email地址或者手机号码。"; l.removeClass(u.parentNode, "fm-error"); l.removeClass(D.get("customPerson").parentNode, "fm-hover") }; var q = function (u) { var w = l.getElementsByClassName(u.className); if (u.checked) { for (var v = 0; v < w.length; v++) { l.addClass(w[v].parentNode, "current"); w[v].checked = "checked" } } else { for (var v = 0; v < w.length; v++) { l.removeClass(w[v].parentNode, "current"); w[v].checked = "" } } }; var p = function (w) { var v = document.createElement("div"); var u = document.createElement("ul"); w.forEach(function (y, z) { var x = m(y, z); u.appendChild(x) }); l.addClass(v, "fn-overflow"); v.appendChild(u); return v }; var f = function (u) { var v = Element.create("h3"); v.innerHTML = '<span class="switch">' + u.name + "<em>(" + u.user_num + ")</em></span>"; return v }; var o = function (u) { if (!t.length) { D.get("myContacts").innerHTML = '<div class="com-mycontacts-empty">您还没有联系人，请直接手动输入。</div>'; return } t.forEach(function (w, A) { var x = Element.create("div"); var y = f(w); var z = p(w.users); D.addClass(x, "com-mycontacts-group"); x.appendChild(y); x.appendChild(z); x.appendChild(k()); u.appendChild(x) }, this) }; _changeChooseStatus = function (y) { var x = this.parentNode.parentNode.parentNode.parentNode; var v = 0; var w = x.getElementsByTagName("input"); var z = l.getElementsByClassName("action", "div", x)[0].getElementsByTagName("a")[0]; for (var u = 0; u < w.length; u++) { if (w[u].checked) { v += 1 } } if (v == w.length) { z.innerHTML = "取消全选"; s.removeListener(z, "click"); s.addListener(z, "click", a) } else { z.innerHTML = "全选"; s.removeListener(z, "click"); s.addListener(z, "click", r) } }; var k = function () { var v = Element.create("div"); var u = Element.create("a", { href: "javascript:void(0);" }); u.innerHTML = "全选"; D.addClass(v, "action"); s.addListener(u, "click", r); v.appendChild(u); return v }; var r = function (w) { if (D.get("customPerson")) { D.get("customPerson").value = "" } if (_payment_obj.getUsersLen() >= 30) { return } j(this); s.removeListener(this, "click"); s.addListener(this, "click", a); var x = this.parentNode.parentNode.getElementsByTagName("input"); for (var v = 0; v < x.length; v++) { if (_payment_obj.getUsersLen() >= 30) { return } var u = { real_name: x[v].value.split("|")[0], nick_name: x[v].value.split("|")[1], account: x[v].value.split("|")[2], id: x[v].value.split("|")[3] }; x[v].checked = "checked"; q(x[v]); _payment_obj.addPerson("", u, true) } }; var a = function (w) { j(this); s.removeListener(this, "click"); s.addListener(this, "click", r); var x = this.parentNode.parentNode.getElementsByTagName("input"); for (var v = 0; v < x.length; v++) { if (x[v].checked) { var u = D.get("user_" + x[v].value.split("|")[3]); _payment_obj.deletePerson(w, u) } } }; var j = function (u) { u.innerHTML == "全选" ? u.innerHTML = "取消全选" : u.innerHTML = "全选" }; var n = function () { l.getElementsByClassName("tab-node").forEach(function (u, w) { s.addListener(u, "click", function () { if (l.hasClass(u, "current")) { return } b(u, "current") }, u) }) }; var b = function (u, w) { l.getElementsByClassName("tab-node").forEach(function (x, y) { c = document.getElementById(x.id.replace("Tab", "")); l.hasClass(x, w) ? l.removeClass(x, w) : l.addClass(x, w); l.hasClass(x, w) ? l.removeClass(c, "fn-hide") : l.addClass(c, "fn-hide") }) }; return { init: function (u) { h = u.call_obj; n() }, appendContacts: function (v) { var u = document.getElementById(v.append_box); t = v.contacts; if (v.has_group) { o(u); return } u.appendChild(p(t)) }, markGiftUser: function () { m = function (w, x) { if (w.real_name == w.nick_name || w.nick_name == "请输入联系人姓名") { var v = Element.create("li", { title: w.real_name + " " + w.account }) } else { var v = Element.create("li", { title: w.real_name + " [" + w.nick_name + "] " + w.account }) } var u = Element.create("input", { type: "radio", id: "contact_" + w.id, name: "contact" }); v.innerHTML = '<label for="contact_' + w.id + '"><em>' + w.real_name + "</em> (" + w.account + ")</label>"; v.insertBefore(u, v.firstChild); s.addListener(u, "click", h.addPerson, w, h); s.addListener(v, "mouseover", function () { l.addClass(this, "hover") }); s.addListener(v, "mouseout", function () { l.removeClass(this, "hover") }); s.addListener(u, "click", function () { if (d != null) { l.removeClass(d, "current") } l.addClass(this.parentNode, "current"); d = this.parentNode }); return v }; o = function (u) { t.forEach(function (w, A) { var x = Element.create("div"); var y = f(w); var z = p(w.users); D.addClass(x, "com-mycontacts-group"); x.appendChild(y); x.appendChild(z); u.appendChild(x) }, this) } } }
} ();
AP.widget.CustomAddPerson = function () {

    var d = YAHOO.util.Dom;
    var t = YAHOO.util.Event;
    var r = null;
    var l = false;
    var j = null;
    var h = null;
    var a = null;
    var k = null;
    var s = null;
    var b = null;
    var u = function () {
        var x = D.get("customPerson").value; if (!x.trim().length) { return } if (k == x) { m("不能添加自己的帐号"); return } if (o(x)) { _payment_obj.addPerson("", o(x), true); return } var v = "email=" + x + "&row_no=1"; var w = AP.ajax.asyncRequest("POST", r, f, v)
    };
    var o = function (x) {
        for (var w = 0; w < b.length; w++) { for (var v = 0; v < b[w].users.length; v++) { if (b[w].users[v].account == x) { return b[w].users[v] } } } return false
    };
    var f = { success: function (w) {

        var v = w.responseText.split(","); if (v[1].length == 0) { m("输入的用户不存在") } else { person = { real_name: v[1], id: D.get("customPerson").value, account: D.get("customPerson").value, nick_name: "" }; _payment_obj.addPerson("", person, true); D.get("customPerson").value = "" }
    }
    };
    var m = function (v) {
        var w = d.getElementsByClassName("fm-explain", "div", D.get("customAddPerson-error").parentNode)[0]; w.innerHTML = v; d.addClass(w.parentNode, "fm-error")
    };
    var q = function () {
        D.get("customPerson").select(); var v = d.getElementsByClassName("fm-explain", "div", D.get("customAddPerson-error").parentNode)[0]; v.innerHTML = "支付宝账户名是Email地址或者手机号码。"; d.removeClass(v.parentNode, "fm-error"); d.removeClass(D.get("customPerson").parentNode, "fm-hover"); d.addClass(D.get("customPerson").parentNode, "fm-focus")
    };
    var p = function (y) {
        var z = D.get("autoCompleteContainer"); var w = z.getElementsByTagName("ul")[0].getElementsByTagName("li"); var v = []; for (var x = 0; x < w.length; x++) { if (d.getStyle(w[x], "display") != "none") { v.push(w[x]) } } for (var x = 0; x < v.length; x++) { if (v[x] == y) { return x } }
    };
    _uniqueResult = function (v) {

        for (var x = 0; x < v.length; x++) { for (var w = x + 1; w < v.length; w++) { if (v[x].account == v[w].account) { v.remove(v[w]) } } } return v
    };
    var g = function (w) {
        var v = w || window.event; if (v.preventDefault) { v.preventDefault() } else { v.returnValue = false } return false
    }; var n = function (x) {

        var x = window.event || arguments.callee.caller.arguments[0]; var v = x.keyCode || x.which || x.charCode; var w = String.fromCharCode(v); if (v == 13) { g(x) }
    };
    return { init: function (v) {

        r = v.request_url; _payment_obj = v.payment_obj; k = v.currentUser; b = v.datasource; h = D.get(v.require_field); this.autoComplete(v.datasource); E.on(h, "focus", function (w) { D.removeClass(D.getAncestorByClassName(h, "fm-item"), "fm-error") }); if (r == null) { return } a = D.get(v.trigger); t.on(a, "click", u, "", this); t.on(D.get("customPerson"), "focus", q); t.on(D.get("customPerson"), "keydown", n); t.on(D.get("customPerson"), "blur", function () { D.removeClass(this.parentNode, "fm-focus") }); t.on(D.get("customPerson"), "mouseover", function () { if (D.hasClass(this.parentNode, "fm-focus")) { return } D.addClass(this.parentNode, "fm-hover") }); t.on(D.get("customPerson"), "mouseout", function () { if (D.hasClass(this.parentNode, "fm-focus")) { return } D.removeClass(this.parentNode, "fm-hover") })
    }, buildData: function (w) {

        var v = []; w.forEach(function (x, y) { x.users.forEach(function (A, B) { var z = {}; if (A.real_name.len() > 8) { real_name = A.real_name.brief(8) + ".." } else { real_name = A.real_name } if (A.account.len() > 16) { account = A.account.brief(16) + ".." } else { account = A.account } z.context = real_name + " (" + account + ")"; z.id = A.id; z.account = A.account; z.nick_name = A.nick_name; z.real_name = A.real_name; v.push(z) }) }); return v
    }, autoComplete: function (v) {

        var y = this.buildData(v); var x = new YAHOO.util.LocalDataSource(y); x.responseSchema = { fields: ["context", "id", "account", "nick_name", "real_name"] }; YAHOO.widget.AutoComplete.prototype.filterResults = this.filterResults; YAHOO.widget.AutoComplete.prototype._onContainerClick = this.onContainerClick; YAHOO.widget.AutoComplete.prototype._updateValue = this.enterHandle; var w = new YAHOO.widget.AutoComplete(h, "autoCompleteContainer", x); w.prehighlightClassName = "yui-ac-prehighlight"; w.useShadow = false; w.animVert = false; E.on(a, "click", function () { var z = "请输入对方的支付宝账户"; if (this.value == z) { this.value = "" } else { if (!AP.fn.regExp.email.test(this.value)) { alert("error2") } } })
    }, enterHandle: function (z) {

        if (!this.suppressInputUpdate) { var C = this._elTextbox; var B = (this.delimChar) ? (this.delimChar[0] || this.delimChar) : null; var y = z._sResultMatch; var A = ""; if (B) { A = this._sPastSelections; A += y + B; if (B != " ") { A += " " } } else { A = y } if (C.type == "textarea") { C.scrollTop = C.scrollHeight } var v = C.value.length; this._selectText(C, v, v); this._elCurListItem = z; var x = p(z); var w = s[x]; h.value = ""; _payment_obj.addPerson("", w, true) }
    }, onContainerClick: function (w, A) {

        if (D.get("customAddPerson-error")) { var B = d.getElementsByClassName("fm-explain", "div", D.get("customAddPerson-error").parentNode)[0]; B.innerHTML = "支付宝账户名是Email地址或者手机号码。"; d.removeClass(B.parentNode, "fm-error") } var C = YAHOO.util.Event.getTarget(w); var z = C.nodeName.toLowerCase(); var y = p(C); var x = s[y]; while (C && (z != "table")) { switch (z) { case "body": return; case "li": A._toggleHighlight(C, "to"); A._selectItem(C); h.value = ""; _payment_obj.addPerson("", x, true); return; default: break } C = C.parentNode; if (C) { z = C.nodeName.toLowerCase() } }
    }, filterResults: function (H, J, O, I) {

        if (H && H !== "") {

            O = YAHOO.widget.AutoComplete._cloneObject(O);
            var F = I.scope, N = this, w = O.results, K = [], z = false, G = (N.queryMatchCase || F.queryMatchCase), v = (N.queryMatchContains || F.queryMatchContains);
            for (var x = 2; x < this.responseSchema.fields.length; x++) {
                for (var y = w.length - 1; y >= 0; y--) {
                    var B = w[y]; var A = null; if (YAHOO.lang.isString(B)) { A = B } else { if (YAHOO.lang.isArray(B)) { A = B[0] } else { if (this.responseSchema.fields) { var M = this.responseSchema.fields[x].key || this.responseSchema.fields[x]; A = B[M] } else { if (this.key) { A = B[this.key] } } } } if (YAHOO.lang.isString(A)) { var C = (G) ? A.indexOf(decodeURIComponent(H)) : A.toLowerCase().indexOf(decodeURIComponent(H).toLowerCase()); if ((!v && (C === 0)) || (v && (C > -1))) { K.unshift(B) } }
                }
            } O.results = _uniqueResult(K); s = _uniqueResult(K)
        } else { } return O
    }
    }
} ();
AP.widget.nickname = function (g) {

    var b = "&nbsp;-&nbsp;";
    var f = g.parentNode;
    if (f.nodeName == "SPAN") { f = g.parentNode.parentNode }
    var d = D.getElementsBy(function (r) {
        return D.hasClass(r, "truename")
    }, "span", f)[0];
    var k = D.getElementsBy(function (r) {

        return D.hasClass(r, "mark")
    }, "span", f)[0];
    var l = D.getElementsBy(function (r) {

        return D.hasClass(r, "nickname")
    }, "span", f)[0];
    var n = D.getElementsBy(function (r) {

        return D.hasClass(r, "input")
    }, "", f)[0];
    var m = D.getElementsBy(function (r) {

        return D.hasClass(r, "save")
    }, "", f)[0]; var p = D.getElementsBy(function (r) {

        return D.hasClass(r, "cancel")
    }, "", f)[0];
    var a = D.hasClass(g, "addNickname") ? g : null;
    var h = D.hasClass(g, "modifyNickname") ? g : null; D.query("#myContacts li").forEach(function (r) {

        E.on(r, "mouseover", function (t) { target = E.getTarget(t); var s = D.query("input", r); if (s.length) { s = D.query("input", r)[0] } if (D.query(".addNickname", r).length && D.hasClass(s, "fn-hide")) { D.removeClass(D.query(".addNickname", r)[0], "fn-hide"); D.removeClass(D.query(".delete", r)[0], "fn-hide") } else { D.addClass(D.query(".addNickname", r)[0], "fn-hide") } if (D.query(".modifyNickname", r).length && D.hasClass(s, "fn-hide")) { D.removeClass(D.query(".delete", r)[0], "fn-hide"); D.removeClass(D.query(".modifyNickname", r)[0], "fn-hide") } else { D.addClass(D.query(".modifyNickname", r)[0], "fn-hide") } }, r, this); E.on(r, "mouseout", function (s) { target = E.getTarget(s); if (D.query(".addNickname", r).length) { D.addClass(D.query(".delete", r)[0], "fn-hide"); D.addClass(D.query(".addNickname", r)[0], "fn-hide") } if (D.query(".modifyNickname", r).length) { D.addClass(D.query(".delete", r)[0], "fn-hide"); D.addClass(D.query(".modifyNickname", r)[0], "fn-hide") } }, "", this)
    });
    E.addListener(g, "click", function (r) {


        showElement([n, m, p]); showElement(g, false); D.addClass(g.parentNode, "modify"); if (l) { showElement(l, false); showElement(k, false); if (n.value.trim().length > 10) { n.value = n.value.trim().replace("...", "") } else { n.value = l.innerHTML.trim().unescapeHTML() } } else { n.value = "" } D.addClass(n.parentNode, "fm-focus"); n.select(); E.preventDefault(r); E.stopEvent(r)
    });
    var j = function (s, r) {

        showElement([n, m, p], false); showElement(l); showElement(k); showElement(h ? h : a); E.preventDefault(s); D.removeClass(p.parentNode, "modify"); if (r) { new AP.widget.errorXbox({ error_info: r[0].msg, url_info: '<a href="/user/contacts/index.htm">返回我的联系人 </a>' }) }
    };
    E.addListener(m, "click", function () { o() });
    E.on(n, "keydown", function (r) {

        if (r.keyCode == 13) { o(); E.stopEvent(r); n.blur() }
    });
    function o() {

        n.value = n.value.trim(); showElement([n, m, p], false); var s = new AP.core.api("user/contacts/editNickName", { onAPISuccess: q, onAPIFailure: j, method: "POST" }, this); var r = D.query(".J_item", n.parentNode.parentNode.parentNode)[0].id.replace("contacts_item_", ""); n.value = n.value.substr(0, 10); s.call({ nickName: n.value, crmUserId: r }); showElement(g); D.removeClass(g.parentNode, "modify")
    }
    var q = function (t, r) {

        if (l == null) { var s = "" } else { var s = l.innerHTML; if (n.value == l.innerHTML) { var s = "" } } if (n.value != "" && n.value != s) { if (h) { l.innerHTML = n.value.escapeHTML() } else { k = Element.create("span"); D.addClass(l, "mark"); k.innerHTML = b; D.insertAfter(k, d); l = Element.create("span"); D.addClass(l, "nickname"); l.innerHTML = n.value.escapeHTML(); D.insertAfter(l, k); h = a; a = null; D.replaceClass(h, "addNickname", "modifyNickname"); h.innerHTML = "[修改昵称]" } showElement(l); showElement(k) } else { log("1"); if (h) { a = h; h = null; D.replaceClass(a, "modifyNickname", "addNickname"); a.innerHTML = "[添加昵称]"; f.removeChild(l); f.removeChild(k); k = l = null } n.value = "" }
    };
    E.addListener(p, "click", j)
};
AP.widget.autoScroll = function (scrollBodyId, scrollBoxId, lineHeight, blockHeight) {

    this.obj = document.getElementById(scrollBodyId); this.box = document.getElementById(scrollBoxId); this.style = this.obj.style; this.defaultHeight = blockHeight; this.scrollUp = doScrollUp; this.stopScroll = false; this.obj.innerHTML += this.obj.innerHTML; this.curLineHeight = 0; this.lineHeight = lineHeight; this.curStopTime = 0; this.stopTime = 300; this.speed = 10; this.style.marginTop = lineHeight + "px"; this.object = scrollBodyId + "Object"; eval(this.object + "=this"); setInterval(this.object + ".scrollUp()", 10); this.obj.onmouseover = new Function(this.object + ".stopScroll=true"); this.obj.onmouseout = new Function(this.object + ".stopScroll=false")
};
function doScrollUp() {

    if (this.stopScroll == true) { return }
    if (this.curLineHeight >= this.lineHeight) {

        this.curStopTime += 1; if (this.curStopTime >= this.stopTime) { this.curLineHeight = 0; this.curStopTime = 0 }
    } else { this.curLineHeight += 10; this.style.marginTop = parseInt(this.style.marginTop) - 10 + "px"; if (-parseInt(this.style.marginTop) >= this.defaultHeight) { this.style.marginTop = 0 } }
}
AP.widget.autoTab = function () {
    debugger;
    var currentClass = "current"; var menuList = []; var contentList = []; var autoKey = false; var currentTab = 0; var currentTabEl; var timeoutID; var config; var switchTo = function (iTabIndex) { if (iTabIndex.innerHTML) { for (var i = 0; i < menuList.length; i++) { if (menuList[i] == iTabIndex) { iTabIndex = i; break } } } D.removeClass(menuList[currentTab], currentClass); AP.widget.xTab.hide(contentList[currentTab]); D.addClass(menuList[iTabIndex], currentClass); AP.widget.xTab.show(contentList[iTabIndex]); currentTab = iTabIndex }; var autoSwitch = function () { if (autoKey) { return false } switchTo((currentTab + 1) % menuList.length); return true }; return { setAutoKey: function (bFlag) { autoKey = bFlag }, init: function (oConfig) { config = oConfig || {}; if (!(config.name && config.mainId && config.menuId && config.contentId)) { return false } if (!config.defaultTab) { config.defaultTab = 0 } else { currentTab = config.defaultTab } if (!config.timer) { config.timer = 5000 } if (!config.delay) { config.delay = 50 } D.get(config.mainId).onmouseover = D.get(config.mainId).onfocus = function () { eval(config.name + ".setAutoKey(true)") }; D.get(config.mainId).onmouseout = function () { eval(config.name + ".setAutoKey(false)") }; var menuDom = D.get(config.menuId); var contentDom = D.get(config.contentId); for (var i = 0; i < menuDom.childNodes.length; i++) { if (menuDom.childNodes[i].nodeType === 1) { D.removeClass(menuDom.childNodes[i], currentClass); menuDom.childNodes[i].onmouseover = menuDom.childNodes[i].onfocus = function () { currentTabEl = this; timeoutID = setTimeout(config.name + ".switchTo()", config.delay) }; menuDom.childNodes[i].onmouseout = function () { clearTimeout(timeoutID) }; menuList[menuList.length] = menuDom.childNodes[i] } } for (var j = 0; j < contentDom.childNodes.length; j++) { if (contentDom.childNodes[j].nodeType === 1) { AP.widget.xTab.hide(contentDom.childNodes[j]); contentList[contentList.length] = contentDom.childNodes[j] } } D.addClass(menuList[config.defaultTab], currentClass); AP.widget.xTab.show(contentList[config.defaultTab]); setInterval(config.name + ".autoSwitch()", config.timer); return true }, autoSwitch: autoSwitch, switchTo: function () { switchTo(currentTabEl) } }
};
AP.widget.dropDown = new AP.Class({ initialize: function (a, b) {

    this.targets = a; this.options = this.setOptions(b); this.tmp = null; this.tag = false; this.targets.forEach(function (d, f) { this.bindEvents(d, this.getDrop(d)) }, this); this.onTargetClickEvent = new U.CustomEvent("onTargetClickEvent"); this.onHiddenClickEvent = new U.CustomEvent("onhiddenEvent"); this.onTargetClickEvent.subscribe(this.options.targetClick, this, true); this.onHiddenClickEvent.subscribe(this.options.hideEvent, this, true)
}, setOptions: function (a) { return AP.hashExtend({ isposition: false, offset: [0, 0], styles: {}, targetClick: function () { }, hideEvent: function () { }, mousehandle: false, outcontent: false, iframe: false }, a || {}) }, getDrop: function (d) { if (this.options.outcontent) { if (D.get("J_" + d.id + "_Box")) { return D.get("J_" + d.id + "_Box") } if (D.get(d.id + "Extend")) { var b = D.get(d.id + "Extend"); var a = b.cloneNode(true); a.id = "J_" + d.id + "_Box"; b.parentNode.removeChild(b); document.body.appendChild(a); return a } throw "function getDrop not retrun any dropbox" } return D.get(d.id + "Extend") }, bindEvents: function (b, a) { if (this.options.mousehandle) { E.on(b, "mouseover", this.show, b, this); E.on(b, "mouseout", this.hide, a, this) } else { E.on(b, "click", this.taggleShow, b, this); E.on(b, "mouseover", this.setTagTrue, b, this); E.on(b, "mouseout", this.setTagFalse, b, this); E.on(a, "mouseover", this.setTagTrue, a, this); E.on(a, "mouseout", this.setTagFalse, a, this); E.on(document.body, "click", this.bodyClick, a, this) } }, bodyClick: function (b, a) { if (!this.tag) { this.hide(b, a) } }, setTagTrue: function () { this.tag = true }, setTagFalse: function () { this.tag = false }, taggleShow: function (b, a) { D.hasClass(this.getDrop(a), "fn-hide") ? this.show(false, a) : this.hide(false, this.getDrop(a)); E.preventDefault(b) }, show: function (d, b) { var a = this.getDrop(b); this.current_target = b; if (this.tmp) { this.hide(d, this.tmp) } this.tmp = a; if (this.options.isposition) { this.setPosition(this.current_target, a); E.on(window, "resize", function () { this.setPosition(this.current_target, a) }, "", this) } D.removeClass(a, "fn-hide"); if (AP.env.browser.msie6 && this.options.iframe) { this.iframeCfg(a) } this.onTargetClickEvent.fire(this.current_target) }, iframeCfg: function (b) { if (D.query("iframe", b).length) { return } var f = Element.create("iframe"); f.src = "javascript:false;"; var d = D.getStyle(b, "width"); var a = D.getStyle(b.firstChild, "height"); D.setStyle(f, "opacity", "0"); D.setStyle(f, "width", d); D.setStyle(f, "height", a); b.appendChild(f) }, setPosition: function (d, a) { D.setStyles(a, { position: "absolute", "z-index": "900" }); D.setStyles(a, this.options.styles); var b = D.getRegion(d); D.setStyles(a, { left: b.left + this.options.offset[0] + "px", top: b.bottom + this.options.offset[1] + "px" }) }, hide: function (b, a) { D.addClass(a, "fn-hide"); this.onHiddenClickEvent.fire() }
});
AP.widget.jointDropDown = AP.widget.dropDown.extend({ initialize: function (a, b) {

    this.extend_id = b.extend_id; this.parent(a, b)
}, getDrop: function (a) { return D.get(this.extend_id) }, taggleShow: function (d, b) { var a = this.getDrop(b); this.current_target = b; this.show(a); this.setPosition(b, a); E.preventDefault(d) }, show: function (a) { if (this.tmp) { this.hide(this.tmp) } this.tmp = a; if (this.options.isposition) { this.setPosition(this.current_target, a); E.on(window, "resize", function () { this.setPosition(this.current_target, a) }, "", this) } this.onTargetClickEvent.fire(this.current_target, a) }, bodyClick: function () { }
});
AP.pk.pa.animDropDown = AP.widget.dropDown.extend({ bindEvents: function (b, a) {

    this.parent(b, a); if (this.options.mousehandle) { E.on(this.options.animTarget, "mouseout", this.hide, a, this); E.on(this.options.animTarget, "mouseover", this.show, b, this) }
}, setOptions: function (a) { return AP.hashExtend({ isposition: false, offset: [0, 0], styles: {}, targetClick: function () { }, hideEvent: function () { }, mousehandle: false, outcontent: false, animTarget: D.get("J-nav-sub-life") }, a || {}) }, show: function (f, d) { if (this.hideTimer) { window.clearTimeout(this.hideTimer) } var a = this.getDrop(d); this.current_target = d; if (this.options.isposition) { E.on(window, "resize", function () { this.setPosition(this.current_target, a) }, "", this); this.setPosition(this.current_target, a) } D.removeClass(a, "fn-hide"); if (!this.dropboxOffsetHeight) { this.dropboxOffsetHeight = a.offsetHeight } var b = new YAHOO.util.Anim(this.options.animTarget, { top: { to: 0} }, 0.3, YAHOO.util.Easing.easeNone); b.animate(); this.onTargetClickEvent.fire(this.current_target) }, hide: function (g, a) { var f = D.get(a.id.replace("Extend", "")); var d = new YAHOO.util.Anim(this.options.animTarget, { top: { to: -this.dropboxOffsetHeight} }, 0.3, YAHOO.util.Easing.easeNone); if (this.hideTimer) { window.clearTimeout(this.hideTimer) } this.hideTimer = window.setTimeout(function () { d.animate() }, 300); var b = this; d.onComplete.subscribe(function () { if (parseInt(b.options.animTarget.style.top) == -b.dropboxOffsetHeight) { D.addClass(a, "fn-hide"); b.onHiddenClickEvent.fire() } }) }, setPosition: function (d, a) { D.setStyles(a, { position: "absolute", "z-index": "900" }); D.setStyles(a, this.options.styles); var b = D.getRegion(d); if (AP.fn.browser.msie) { D.setStyles(a, { left: b.left + this.options.offset[0] - 2 + "px", top: b.bottom + this.options.offset[1] - 2 + "px" }) } else { D.setStyles(a, { left: b.left + this.options.offset[0] + "px", top: b.bottom + this.options.offset[1] + "px" }) } }
});
AP.widget.tipdone = new AP.Class({ setOptions: function (a) {

    return AP.hashExtend({ timeout: 5 }, a || {})
}, initialize: function (a) { this.options = this.setOptions(a) }, remove: function () { var a = D.query(".tip-done")[0]; if (a) { a.parentNode.removeChild(a) } }, show: function (b) { this.remove(); var a = this.createDom(b); D.setStyle(a, "left", (document.documentElement.clientWidth - a.offsetWidth) / 2 + "px"); this.position(a); this.scollChange(a); if (this.timeout) { clearTimeout(this.timeout) } this.timeout = setTimeout(this.remove, 5000) }, scollChange: function (a) { E.on(window, "scroll", function () { this.position(a) }, "", this) }, position: function (a) { D.setStyle(a, "top", document.documentElement.scrollTop + "px") }, getTip: function () { return D.query(".tip-done")[0] }, createDom: function (a) { html = '<div class="container">' + a + "</div>"; return Element.create("div", { innerHTML: html, appendTo: document.body, "class": "tip-done" }) }
});
AP.widget.combo = function () {

    var d = function (k) { var j = D.get(k); this.value = (document.all ? j.getAttributeNode("value").specified : j.hasAttribute("value")) ? j.value : j.text; this.text = j.text; this.url = j.getAttribute(f.linkAttributeName); this.selected = j.selected; this.el = j }; var f = { linkAttributeName: "data-link", comboClass: "com-popmenu", titleClass: "com-popmenu-arrow", hiddenClass: "fn-hide", activateEvent: "click" }; var b = []; var h; var a = 0; var g = function () { D.addClass(f.transform, f.hiddenClass); for (var m = 0; m < f.transform.options.length; m++) { b[m] = new d(f.transform.options[m]); if (b[m].selected) { a = m } } h = document.createElement("div"); D.addClass(h, f.comboClass); var o = document.createElement("a"); D.addClass(o, f.titleClass); o.innerHTML = f.title; var p = document.createElement("ul"); D.addClass(p, f.hiddenClass); for (var l = 0; l < b.length; l++) { var n = b[l].url ? b[l].url : "#"; p.innerHTML += '<li><a href="' + n + '" value="' + b[l].value + '">' + b[l].text + "</a></li>" } h.appendChild(o); h.appendChild(p); E.on(o, f.activateEvent, function (j) { D.removeClass(p, f.hiddenClass); E.preventDefault(j) }); E.on([p, o], "mouseover", function () { AP.cache._mouseout_ = false }); E.on([p, o], "mouseout", function () { AP.cache._mouseout_ = true; var j = this == o ? p : this; setTimeout(function () { if (AP.cache._mouseout_) { D.addClass(j, f.hiddenClass) } }, 600) }); D.getElementsBy(function () { return true }, "a", p, function (j) { E.on(j, "click", function (q) { for (var k = 0; k < b.length; k++) { if (b[k].text == j.innerHTML) { f.transform.options[k].selected = true; if (b[k].url) { window.location.href = b[k].url } else { if (f.onselect) { f.onselect(b[k]) } } D.addClass(p, f.hiddenClass); break } } E.preventDefault(q) }) }); if (f.onselect) { f.onselect(b[a]) } }; return { init: function (j) { j = j || {}; for (var k in j) { f[k] = j[k] } if (!f.transform) { return false } f.transform = D.get(f.transform); f.title = f.title ? f.title : f.transform.getAttribute("title"); if (f.wrapMethod) { h = f.wrapMethod.call(this, f) } else { g() } D.insertBefore(h, f.transform); if (f.onwrap) { f.onwrap(h) } return true }, getCombo: function () { return h } }
};
AP.pk.pa.counter = function (f) {

    var a = this, b = [], d;
    this.type = ["sms", "phone", "email"];
    this.btnDisable = "btn-normal-disabled";
    this.btnAble = "btn-normal";
    this.sec = AP.cache.countdown = 60;
    this.count = f.count || 60;
    this.action = f.action || false;
    this.text = { sms: ["重发短信校验码", "重新获取短信"], phone: ["语音获取校验码", "使用语音获取", "免费语音获取校验码"], email: ["点击重新发送邮件", "重新发送邮件"] };
    this.setMobile = function (g) { this.mobilePhone = g };
    this.initType = function () {

        this.type.forEach(function (g) { if (f.hasOwnProperty(g)) { b.push(g) } }); return b
    };
    this.setCount = function (g) { a.sec = AP.cache.countdown = g };
    this.encode = function (g) {

        if (!/^([0-9]+)[0-9]{4}([0-9]{4})/g.test(g)) { return g } return g.replace(/^([0-9]+)[0-9]{4}([0-9]{4})/g, "$1****$2")
    };
    this.getLink = function (g) {

        if (parseInt(g) == 1) { return '（<a href="/user/mobile/modifyMobile.htm">修改号码</a>）' } else { if (parseInt(g) == 0) { return '（<a href="/user/mobile/bindMobile.htm">修改号码</a>）' } else { return "" } }
    };
    this.updateText = function (g) {

        var h; if (g === "phone") { h = '<p class="t-explain">支付宝向您的手机：' + this.encode(this.mobilePhone) + (f.noChangeLink ? "" : this.getLink(f.isBind)) + '拨打电话并播报校验码。<p class="t-explain"><em>来电号码：057126883721，057185020555。</em>如果2分钟内没接到语音校验码电话，请点击按钮重新获取。</p>' } else { if (g === "sms") { h = '<p class="t-explain"><em>6位数字校验码短信</em>已经免费发送到您的手机：' + this.encode(this.mobilePhone) + (f.noChangeLink ? "" : this.getLink(f.isBind)) + '。</p><p class="t-explain">如果1分钟内没有收到校验码短信，请点击按钮重新获取。</p>' } else { if (g === "email") { h = '<p class="t-explain">请进入邮箱查收邮件，若无法收到邮件，请点击重新发送邮件。<a href="emailChange.htm">修改Email</a></p>' } } } D.get(f.txtId).innerHTML = f.text || h
    };
    this.countdown = function (g) {

        if (this.sec === null) { return } if (this.sec === 0) { this.countDownUI(g, false); D.get(f.txtId).innerHTML = d; D.addClass(D.get(f[g] + "-notice"), "fn-hide"); this.sec = AP.cache.countdown; return } this.countDownUI(g, true); this.sec--; clearTimeout(AP.cache.countTimer); AP.cache.countTimer = setTimeout(function () { a.countdown(g) }, 1000)
    };
    this.countDownUI = function (h, g) {

        var g = g === true || false; b.forEach(function (k) { var j = D.get(f[k]); if (!g) { j.parentNode.className = a.btnAble } else { j.parentNode.className = a.btnDisable } j.disabled = g; j.value = g ? "（" + a.sec + "秒后）" + a.text[k][1] : a.text[k][0] })
    };
    this.request = function (l) {

        var h = null, j = "", m, l = l + "-" + (f.status || "");
        switch (l) {

            case "email-": j = "user/loginName/resendEmail"; m = { resendType: f.emailType }; break;
            case "email-emailChangeActiveOnSelfhelpprod": j = "selfhelp/loginName/resendEmail"; m = { resendType: f.emailType }; break;
            case "email-activate": j = "user/reg/resendEmailToActivate"; m = { email: f.emailAddress }; break;
            case "phone-bindMobile": j = "user/mobile/resendIvrAckCodeToBind"; m = { mobilePhone: f.mobilePhone }; break;
            case "phone-unbindMobile": j = "user/mobile/resendIvrAckCodeToUnBind"; m = { mobilePhone: f.mobilePhone }; break;
            case "sms-activate": j = "user/reg/resendSmsAckCodeToActivate"; m = { mobilePhone: f.mobilePhone }; break;
            case "sms-bindMobile": j = "user/mobile/resendSmsAckCodeToBind"; m = { mobilePhone: f.mobilePhone }; break;
            case "sms-unbindMobile": j = "user/mobile/resendSmsAckCodeToUnBind"; m = { mobilePhone: f.mobilePhone }; break;
            case "sms-openMobileSwitch": j = "user/mobile/resendSmsAckCodeOpenMobileSwitch"; m = { businessType: f.businessType, mobilePhone: f.mobilePhone }; break;
            case "phone-openMobileSwitch": j = "user/mobile/resendSmsAckCodeOpenMobileSwitchIVR"; m = { businessType: f.businessType, mobilePhone: f.mobilePhone }; break;
            case "sms-modifyMobile": j = "user/mobile/resendSmsAckCodeToModify"; m = { mobilePhone: f.mobilePhone }; break;
            case "sms-": j = "user/mobile/resendSmsAckCode"; if (typeof f.smsParams == "undefined") { m = { businessType: f.businessType, businessId: f.businessId} } else { m = { businessType: f.businessType, businessId: f.businessId, smsParams: f.smsParams} } break;
            case "phone-": j = "user/mobile/resendIvrAckCode"; m = { businessType: f.businessType, businessId: f.businessId }; break;
            case "sms-revokeCert": j = "cert/resendSmsAckCodeToRevoke"; m = { mobilePhone: f.mobilePhone }; break;
            case "sms-updateCert": j = "cert/resendSmsAckCodeToUpdate"; m = { mobilePhone: f.mobilePhone, AckCodeId: f.businessId }; break;
            case "sms-scBindMobile": j = "sc/mobile/resendSmsAckCodeToBind"; m = { mobilePhone: f.mobilePhone }; break;
            case "phone-scBindMobile": j = "sc/mobile/resendIvrAckCodeToBind"; m = { mobilePhone: f.mobilePhone }; break;
            case "sms-stopConvoy": j = "sc/mobile/resendSmsAckCodeToStopConvoy"; m = { mobilePhone: f.mobilePhone }; break;
            case "sms-setConvoyLimit": j = "sc/mobile/resendSmsAckCodeToSetConvoyLimit"; m = { mobilePhone: f.mobilePhone }; break;
            case "phone-stopConvoy": j = "sc/mobile/resendIvrAckCodeToStopConvoy"; m = { mobilePhone: f.mobilePhone }; break;
            case "phone-setConvoyLimit": j = "sc/mobile/resendIvrAckCodeToSetConvoyLimit"; m = { mobilePhone: f.mobilePhone }; break;
            case "sms-sendSMS": j = "sc/mobile/resendIvrAckCodeToStopConvoy"; m = { mobilePhone: a.mobilePhone }; break;
            case "sms-revokeOtpByMobile": j = "sc/mobile/reSendSmsAckCodeForRevokeOtp"; m = { mobilePhone: f.mobilePhone }; break;
            case "sms-applyOtpMobile": j = "sc/mobile/reSendSmsAckCodeForApplyOtp"; m = { mobilePhone: f.mobilePhone }; break;
            case "sms-bindThirdMobile": j = "sc/mobile/reSendSmsAckCodeForBindThird"; m = { mobilePhone: f.mobilePhone }; break;
            case "sms-activateUkeyMobile": j = "sc/mobile/reSendSmsAckCodeForActivateUkey"; m = { mobilePhone: f.mobilePhone }; break;
            case "sms-certBindMobile": j = "sc/mobile/reSendSmsAckCodeForBindMobile"; m = { mobilePhone: f.mobilePhone }; break;
            case "phone-certBindMobile": j = "sc/mobile/reSendIvrAckCodeForBindMobile"; m = { mobilePhone: f.mobilePhone }; break;
            case "sms-certApplyCert": j = "sc/mobile/reSendSmsAckCodeForApplyCert"; m = { mobilePhone: f.mobilePhone }; break;
            case "phone-certApplyCert": j = "sc/mobile/reSendIvrAckCodeForApplyCert"; m = { mobilePhone: f.mobilePhone }; break;
            case "sms-installByMobile": j = "sc/mobile/reSendSmsAckCodeForInstallCert"; m = { mobilePhone: f.mobilePhone }; break; case "sms-revokeByMobile": j = "sc/mobile/reSendSmsAckCodeForRevokeCert"; m = { mobilePhone: f.mobilePhone }; break;
            case "sms-security": j = "standard/payment/sendAckCodeSms"; m = { requestId: f.businessId }; break;
            case "phone-security": j = "standard/payment/sendAckCodeIvr"; m = { requestId: f.businessId }; break;
            case "phone-loginFind": j = "user/pwdFind/resendQVoiceAckCode"; m = { logonId: f.logonId }; break;
            case "sms-loginFind": j = "user/pwdFind/resendQSmsAckCode"; m = { logonId: f.logonId }; break;
            case "phone-payFind": j = "user/pwdFind/resendPVoiceAckCode"; m = { logonId: f.logonId }; break;
            case "sms-payFind": j = "user/pwdFind/resendPSmsAckCode"; m = { logonId: f.logonId }; break;
            case "email-loginFind": j = "user/pwdFind/resendQEmail"; m = { logonId: f.logonId, method: f.emailType, email: f.emailAddress }; break;
            case "email-payFind": j = "user/pwdFind/resendPEmail"; m = { logonId: f.logonId, method: f.emailType, email: f.emailAddress }; break;
            case "sms-modifyEmailAcct": j = "user/loginName/unlongin/changeEmailResendAckCode"; m = { logonId: f.logonId, mobilePhone: f.mobilePhone }; break;
            case "email-modifyEmailAcct": j = "user/loginName/unlongin/changeEmailResendEmail"; m = { logonId: f.logonId }; break; case "phone-loginFindSelfHelp": j = "selfhelp/pwdFind/resendQVoiceAckCode"; m = { logonId: f.logonId }; break;
            case "sms-loginFindSelfHelp": j = "selfhelp/pwdFind/resendQSmsAckCode"; m = { logonId: f.logonId }; break;
            case "phone-payFindSelfHelp": j = "selfhelp/pwdFind/resendPVoiceAckCode"; m = { logonId: f.logonId }; break;
            case "sms-payFindSelfHelp": j = "selfhelp/pwdFind/resendPSmsAckCode"; m = { logonId: f.logonId }; break;
            case "email-loginFindSelfHelp": j = "selfhelp/pwdFind/resendQEmail"; m = { logonId: f.logonId, method: f.emailType, email: f.emailAddress }; break;
            case "email-payFindSelfHelp": j = "selfhelp/pwdFind/resendPEmail"; m = { logonId: f.logonId, method: f.emailType, email: f.emailAddress }; break;
            case "sms-modifyEmailAcctSelfHelp": j = "selfhelp/loginName/unlongin/changeEmailResendAckCode"; m = { logonId: f.logonId, mobilePhone: f.mobilePhone }; break;
            case "email-modifyEmailAcctSelfHelp": j = "selfhelp/loginName/unlongin/changeEmailResendEmail"; m = { logonId: f.logonId }; break;
            case "sms-modifyEmailAcctOnSelfhelpprod": j = "selfhelp/loginName/unlongin/changeEmailResendAckCode"; m = { logonId: f.logonId, mobilePhone: f.mobilePhone }; break; case "email-modifyEmailAcctOnSelfhelpprod": j = "selfhelp/loginName/unlongin/changeEmailResendEmail"; m = { logonId: f.logonId }; break;
            case "sms-snsCheckCodeQ": j = "sns/reSendCheckCode"; m = { mobile: f.mobilePhone, isSendAgain: "true" }; break; case "phone-couponValiCode": j = "coupon/mobile/resendIvrAckCode"; m = { mobilePhone: f.mobilePhone, businessId: f.businessId, businessType: f.businessType, resultCode: f.resultCode }; break;
            case "sms-couponValiCode": j = "coupon/mobile/resendSmsAckCode"; m = { mobilePhone: f.mobilePhone, businessId: f.businessId, businessType: f.businessType, resultCode: f.resultCode }; break; case "sms-lifeGetCheckCodeQ": j = "life/mobile/getCheckCodeQ"; m = { mobile: f.mobilePhone }; break; case "sms-aaCheckCodeQ": j = "transfer/mobile/getCheckCodeQ"; m = { mobile: f.mobilePhone, isSendAgain: "true" }; break;
            case "sms-fundpayCheckCode": j = "trade/account/sendSMSCode"; break;
            case "phone-fundpayCheckCode": j = "trade/account/sendIVRCode"; break;
            case "sms-airmng-security": j = "/security/9002/900203/product.json"; m = { mobilePhone: f.mobilePhone, templateId: f.businessId }; break;
            case "phone-airmng-security": j = "/security/9002/900203/product.json"; m = { mobilePhone: f.mobilePhone, templateId: f.businessId }; break;
            case "email-messageResendEmail": j = "messager/channel/modifyEmail/resendEmail"; m = {}; break;
            case "sms-morderprod": j = "order/validateCode"; m = { action: "sendMobileValidateCode", mobile: f.mobilePhone }; break; case "phone-morderprod": j = "order/validateCode"; m = { action: "sendIvrValidateCode", mobile: f.mobilePhone }; break;
            case "sms-authcenterQ": j = "login/mobile/getCheckCodeQ"; m = { mobile: f.mobilePhone }; break; case "sms-katongQReg": j = "user/reg/resendSmsAckCodeToActivate"; m = { mobilePhone: f.mobilePhone }; break;
            case "phone-katongQReg": j = "user/reg/mobile/resendIvrAckCodeToActivate"; m = { mobilePhone: f.mobilePhone }; break; case "sms-acCheckCodeQ": j = "transfer/mobile/getAcCheckCodeQ"; m = { mobile: f.mobilePhone, isSendAgain: "true" }; break;
            case "sms-directpay": j = "directpay/mobile/getCheckCodeQ"; m = { mobile: f.mobilePhone, isSendAgain: "true" }; break
        }
        var g = f.domain && f.domain.indexOf(".alipay.") > -1 ? f.domain : AP.PageVar.app_domain;
        if (typeof f.resend_ackcode_url != "undefined") {

            j = f.resend_ackcode_url; m = f.params; m.type = l; g = ""
        }
        if (j) {

            var k = new AP.core.api(j, { api_url: g, method: "POST", cache: false, format: "jsonp", onAPISuccess: function (o, n) { } }, this); k.call(m)
        }
    };
    this.fireEvent = function (j, g, h) {

        if (g === "phone") { a.setCount(120) } else { a.setCount(60) } if (f.beforeCountEvent) { if (f.beforeCountEvent.call(a) == false) { return false } } a.countdown(f[g]); a.request(g); if (!f.noUpdateText) { a.updateText(g) } if (D.get(f[g] + "-notice")) { D.addClass(f[g] + "-notice", "fn-hide") }
    };
    this.init = function (g) {

        d = D.get(g.txtId) && D.get(g.txtId).innerHTML || ""; this.setMobile(g.mobilePhone); this.request = g.request || this.request; this.initType(); b.forEach(function (h) { D.get(g[h]).disabled = false; E.on(g[h], "click", a.fireEvent, h, this); if (h === "phone") { E.on(g[h], "mouseover", function () { D.removeClass(g[h] + "-notice", "fn-hide") }); E.on(g[h], "mouseout", function () { D.addClass(g[h] + "-notice", "fn-hide") }) } if (h === "sms" || h === "email") { a.setCount(60); if (!g.auto || g.auto == false) { a.countdown(h) } } })
    };
    this.fire = function (g) {

        if (["sms", "phone", "email"].has(g)) { a.countdown(g) }
    };
    this.restart = function (h, g) {

        this.sec = 60; AP.cache.countdown = 60; clearTimeout(AP.cache.countTimer); b.forEach(function (k) {

            var j = D.get(f[k]); j.parentNode.className = a.btnAble; j.disabled = false; j.value = a.text[k][0]
        });
        a.fireEvent(h, g)
    };
    this.init(f)
};
AP.widget.confirmBox = new AP.Class({ initialize: function (a) {
    this.widget = a.widget || D.get("deletebox"); this.confirm = a.confirm || D.getElementsByClassName("confirm", "input", this.widget)[0]; this.cancel = a.cancel || D.getElementsByClassName("cancel", "input", this.widget)[0]; this.triggers = a.triggers || D.getElementsByClassName("delete"); if (!this.triggers.length) { this.triggers = [this.triggers] } this.positionAdjust = a.positionAdjust || [0, 0]; this.fireType = a.fireType || "click"; a.preventDefault = a.preventDefault || false; if (!a.preventDefault) { try { this.triggers.forEach(function (d) { E.on(d, this.fireType, a.onCall || this.onCall, d, this) }, this) } catch (b) { E.on(this.triggers, this.fireType, a.onCall || this.onCall, "", this) } } E.on(this.confirm, "click", a.onConfirm || this.onConfirm, this.data || "", this); E.on(this.cancel, "click", a.onCancel || this.onCancel, this.data || "", this)
}, showElement: function (b, a) { a ? D.removeClass(b, "fn-hide") : D.addClass(b, "fn-hide") }, onConfirm: function () { this.showElement(this.widget, false); this.currentDelete = null }, onCancel: function () { this.showElement(this.widget, false); this.currentDelete = null }, onCall: function (b, a) { this.data = arguments[2] || ""; this.showElement(this.widget, true); D.setXY(this.widget, this.getPosition(a)); this.currentDelete = a; this.cancel.focus(); E.preventDefault(b) }, getPosition: function (a) { return [D.getXY(a)[0] + this.positionAdjust[0], D.getXY(a)[1] + this.positionAdjust[1]] }, addTrigger: function (a) { a.forEach(function (b) { E.on(b, this.fireType, this.onCall, b, this) }, this) }
});
AP.pk.pa.highlight = new AP.Class({ setOptions: function (a) {
    return AP.hashExtend({ target: "faq", warnClass: "m-warn", iconClass: "m-cue", duartion: 1, scrollPage: true, autoShow: true, originalColor: "#FFF" }, a || {})
}, initialize: function (a) { this._options = this.setOptions(a); this.icons = D.query("." + this._options.iconClass, D.get(this._options.target)); if (this._options.autoShow) { this.show() } }, show: function () { this.animation() }, animation: function () { var b = new YAHOO.util.ColorAnim(this._options.target, { backgroundColor: { to: "#FFFFA4"} }, this._options.duartion); var a = new YAHOO.util.ColorAnim(this._options.target, { backgroundColor: { to: this._options.originalColor} }, this._options.duartion); this.changeIcon(); b.animate(); b.onComplete.subscribe(function () { a.animate() }); var d = this; a.onComplete.subscribe(function () { d.changeIcon("done") }, d); if (this._options.scrollPage) { AP.util.scrollPage(this._options.target, 1) } }, changeIcon: function (a) { if (a == "done") { this.icons.forEach(function (b, d) { D.replaceClass(b, this._options.warnClass, this._options.iconClass) }, this) } else { this.icons.forEach(function (b, d) { D.replaceClass(b, this._options.iconClass, this._options.warnClass) }, this) } }
});
AP.pk.pa.searchBox = new AP.Class({ setOptions: function (a) {
    return AP.hashExtend({ textDefaultClass: "i-text-gray", textDefaultExplain: "data-explain" })
}, initialize: function (b, a) { this.form = b; this.input = D.query("input[type='text']", b)[0]; this.options = this.setOptions(a); this.defaultText = this.input.getAttribute(this.options.textDefaultExplain); this.bindEvents() }, bindEvents: function () { this.blur(); E.on(this.input, "focus", this.focus, "", this); E.on(this.input, "blur", this.blur, "", this); E.on(this.form, "submit", function (a) { if (this.input.value.trim() == "" || this.input.value.trim() == this.defaultText) { E.preventDefault(a) } }, {}, this) }, focus: function () { if (this.input.value.trim() == this.defaultText) { this.input.value = "" } D.removeClass(this.input, this.options.textDefaultClass) }, blur: function () { if (this.input.value.trim() == "" || this.input.value.trim() == this.defaultText) { this.input.value = this.defaultText; D.addClass(this.input, this.options.textDefaultClass) } }
});
AP.widget.popNotice = new AP.Class({ setOptions: function (a) {
    return AP.hashExtend({ pop_class: "pop-info", pop_id: "J_popContainer", message: "no message", click: false, offset: [0, 0], customEvent: false, styles: {}, onshow: function () { }, onhide: function () { } }, a || {})
}, initialize: function (a, b) {

    this.options = this.setOptions(b); this.current_el = null; if (D.get("J_popContainer")) { this.pop = D.get("J_popContainer"); this.pop_b = D.get("J_popBottom") } else { this.pop = this.getBuildDom(); this.pop_b = this.getBuildBottomEl() } this.bodyBindEvent(); this.setStyles(this.options.styles); if (a) { this.bindEvents(a) } this.targets = a; this.onShowEvent = new U.CustomEvent("onShowEvent"); this.onHideEvent = new U.CustomEvent("onHideEvent"); this.onShowEvent.subscribe(this.options.onshow, this, true); this.onHideEvent.subscribe(this.options.onhide, this, true)
}, bindEvents: function (a) {

    if (this.options.customEvent) { return } a.forEach(function (b) { if (this.options.click) { E.on(b, "click", this.show, b, this) } else { E.on(b, "mouseover", this.show, b, this); E.on(b, "mouseout", this.mouseHiddenEvent, b, this) } }, this); if (!this.options.click) { E.on(this.pop, "mouseout", this.mouseHiddenEvent, "", this); E.on(this.pop_b, "mouseout", this.mouseHiddenEvent, "", this) }
}, mouseHiddenEvent: function (d) {

    try { var b = E.getTarget(d); var a = E.getRelatedTarget(d); if (D.query("*", this.pop).contains(a) || a == this.pop || a == this.current_el || a == this.pop_b) { return } } catch (d) { } this.hide()
}, setPosition: function (b) {

    var h = D.getRegion(this.pop); var f = h.bottom - h.top; var g = h.right - h.left; var d = D.getRegion(b); var a = d.left + this.options.offset[0]; var j = d.top - f - 3; D.setStyles(this.pop_b, { height: f + 5 + "px", width: g + "px" }); D.setXY(this.pop, [a, j]); D.setXY(this.pop_b, [a, j])
}, show: function (b, a) {

    this.current_el = a; if (D.get("J_popContainer") === null) { this.pop = this.getBuildDom(); this.pop_b = this.getBuildBottomEl(); this.setStyles(this.options.styles) } D.removeClass(this.pop, "fn-hide"); D.removeClass(this.pop_b, "fn-hide"); if (!this.options.click) { E.on(this.pop, "mouseout", this.mouseHiddenEvent, "", this); E.on(this.pop_b, "mouseout", this.mouseHiddenEvent, "", this) } this.innerMessage(); this.setPosition(a); this.onShowEvent.fire({ trigger: a, pop: this.pop })
}, innerMessage: function () {

    if (D.query(".pop_extend_txt", this.current_el).length) { var b = D.query(".pop_extend_txt", this.current_el)[0] } else { var b = D.query(".pop_extend_txt", this.current_el.parentNode)[0] } if (b) { var a = b.innerHTML } else { var a = this.options.message } D.query(".container", this.pop)[0].innerHTML = a
}, bodyBindEvent: function () {

    E.on(document.body, "click", function (d) { var b = E.getTarget(d); var a = D.query("*", this.pop); var f = false; a.forEach(function (g) { if (g == b) { f = true } }); if (b.parentNode == this.pop || b == this.current_el || f) { return } this.hide(d) }, "", this)
}, hide: function () {

    if (this.options.click) { D.addClass(this.pop, "fn-hide"); D.addClass(this.pop_b, "fn-hide") } else { try { this.pop.parentNode.removeChild(this.pop) } catch (a) { } try { this.pop_b.parentNode.removeChild(this.pop_b) } catch (a) { } } this.onHideEvent.fire()
}, setStyles: function (a) {

    if (D.query(".container", this.pop).length > 0) { D.setStyles(D.query(".container", this.pop)[0], a) }
}, getBuildDom: function () {

    var a = '<div class="container"></div><div style="left: 20px;" class="angle"/>'; var b = Element.create("div", { innerHTML: a, appendTo: document.body, id: this.options.pop_id, "class": "fn-hide" }); D.addClass(b, this.options.pop_class); return b
}, getBuildBottomEl: function (a) {

    var b = Element.create("div", { appendTo: document.body, id: "J_popBottom", "class": "fn-hide" }); D.setStyles(b, { position: "absolute", "z-index": "488" }); return b
}
});
AP.widget.seniorPop = AP.widget.popNotice.extend({ show: function (d, b, a) {
    if (D.get("J_popContainer") === null) { this.pop = this.getBuildDom(); this.pop_b = this.getBuildBottomEl(); this.setStyles(this.options.styles) } D.removeClass(this.pop, "fn-hide"); D.removeClass(this.pop_b, "fn-hide"); this.current_el = b; var a = a || {}; if (a.hideAngle) { this.hideAngle() } else { this.showAngle() } if (!this.options.click) { E.on(this.pop, "mouseout", this.mouseHiddenEvent, "", this); E.on(this.pop_b, "mouseout", this.mouseHiddenEvent, "", this) } this.innerMessage(); this.setPosition(b, a); this.onShowEvent.fire({ trigger: b, pop: this.pop })
}, hideAngle: function () { D.addClass(D.query(".angle", this.pop)[0], "fn-hidden") }, showAngle: function () { D.removeClass(D.query(".angle", this.pop)[0], "fn-hidden") }, setPosition: function (b, a) { var f = D.getRegion(this.pop); var d = D.getRegion(b); if (a.center) { var g = this.remoteCenter(f) } else { var g = this.remoteXY(f, d) } D.setXY(this.pop, g); D.setXY(this.pop_b, g) }, remoteCenter: function (g) { var b = g.bottom - g.top; var f = g.right - g.left; var d = D.getViewportWidth(); var j = D.getViewportHeight(); var a = d / 2 - f / 2; var h = j / 2 - b / 2; return [a, h] }, remoteXY: function (g, d) { var b = g.bottom - g.top; var f = g.right - g.left; var a = d.left + this.options.offset[0]; var h = d.top - b - 3; this.setStyles(this.options.styles); D.removeClass(D.query(".angle", this.pop)[0], "angle-top"); D.setStyles(D.query(".angle", this.pop)[0], { left: "20px", right: "auto" }); if (b > (d.top - D.getDocumentScrollTop())) { h = d.bottom + 10; D.addClass(D.query(".angle", this.pop)[0], "angle-top") } if ((a + f) > D.getViewportWidth()) { D.setStyles(D.query(".angle", this.pop)[0], { left: "auto", right: "20px" }); a = d.right - f - this.options.offset[0] } D.setStyles(this.pop_b, { height: b + 5 + "px", width: f + "px" }); return [a, h] }
});
AP.widget.errorXbox = new AP.Class({ initialize: function (a) {
    this.error_info = a.error_info; this.url_info = a.url_info; this.xbox = new AP.widget.xBox({ type: "dom", value: "#errorXbox", width: 500, height: 100 }); if (!AP.cache.xnumber) { AP.cache.xnumber = 1 } else { AP.cache.xnumber++ } this.build()
}, build: function () { this.buildHTML(); this.xbox.fire(); this.getElements(); this.buildClose(); this.counter() }, getElements: function () { this.xbox_container = D.get("errorXbox"); this.close_el = D.get("J_xbox_colse"); this.url_container = D.query(".n-explain", this.xbox_container)[0] }, buildClose: function () { E.on(this.close_el, "click", function (a) { AP.widget.xBox.hide(); E.preventDefault(a) }, "", this) }, counter: function () { var d = D.query("a", this.url_container); if (!d.length) { var f = this; var a = 10; f.close_el.setAttribute("xnumber", AP.cache.xnumber); var b = function (g) { a = a - 1; f.close_el.innerHTML = a + "秒后自动关闭"; if (a <= 0 && AP.cache.xnumber == parseInt(f.close_el.getAttribute("xnumber"))) { AP.widget.xBox.hide(); a = 10 } }; window.setInterval(b, 1000) } }, buildHTML: function () { var a = D.get("errorXboxContainer"); if (a) { a.parentNode.removeChild(a) } var d = Element.create("div", { "class": "fn-hide", id: "errorXboxContainer" }); var b = '<div class="container-xbox" id="errorXbox"><div class="no-title"><a class="xbox-close-link" id="J_xbox_colse" href="#">关闭</a></div><div class="notice n-error-xbox"><h3>{error_info}</h3><p class="n-explain">{url_info}</p></div></div>'; b = b.replace("{error_info}", this.error_info); b = b.replace("{url_info}", this.url_info); d.innerHTML = b; document.body.appendChild(d) }
});
AP.widget.Editable = new AP.Class({ setOptions: function (a) {

    return AP.hashExtend({ edit_type: "input", edit_target: null, class_edit: "", class_cannel: "", class_focus: "", default_txt: "点击添加内容", defalut_show: true }, a || {})
}, initialize: function (a, b) { this.options = this.setOptions(b); this.bindTargetEvents(a); this.onSaveButtonClickEvent = new U.CustomEvent("onSaveButtonClickEvent"); if (b.saveEvent) { this.onSaveButtonClickEvent.subscribe(b.saveEvent, this, true) } }, bindTargetEvents: function (a) { a.forEach(function (d) { if (this.options.defalut_show) { var b = D.query(".J_editable_target", d.parentNode)[0]; E.on(b, "click", this.onClickEvent, b, this) } else { E.on(d, "mouseover", this.onMouseOverEvent, d, this); E.on(d, "mouseout", this.onMouseOutEvent, d, this); E.on(d, "click", this.onClickEvent, d, this) } }, this) }, onMouseOverEvent: function (d, b) { if (this.options.edit_target) { var a = this.getEditTarget(b); D.removeClass(a, "fn-hide") } else { D.addClass(b, this.options.class_focus) } }, onMouseOutEvent: function (d, b) { if (this.options.edit_target) { var a = this.getEditTarget(b); D.addClass(a, "fn-hide") } else { D.removeClass(b, this.options.class_focus) } }, onClickEvent: function (d, b) { this.current_target = b; var a = b.parentNode.getAttribute("rel"); if ((this.options.edit_target && D.hasClass(b, "J_editable_target")) || !this.options.edit_target) { this.showEditable(b) } D.query("input[type=text]", b.parentNode.parentNode)[0].value = a; E.preventDefault(d) }, onSaveEvent: function (b, a) { this.onSaveButtonClickEvent.fire(a); E.preventDefault(b) }, onCannelEvent: function (b, a) { this.hideEditable(a); E.preventDefault(b) }, getEditTarget: function (b) { var a = D.query(".J_editable_target", b)[0]; if (!a) { a = this.BuildTargetButton(); b.appendChild(a) } return a }, getEditableTarget: function (d) {

    var b = d.parentNode.parentNode; if (!D.hasClass(b, "J_editable")) { var a = D.query(".J_editable", b)[0]; if (!a) { var a = this.BuildEditableDom(b) } } else { var a = d.parentNode } return a
}, showEditable: function (d) {
    var b = this.getEditableTarget(d); D.removeClass(b, "fn-hide"); D.addClass(d.parentNode, "fn-hide"); var a = D.query("input[type=text]", b)[0]; a.select()
}, hideEditable: function (b) { var a = this.getEditableTarget(b); var d = D.query(":first", b.parentNode.parentNode); D.removeClass(d, "fn-hide"); D.addClass(a, "fn-hide") }, BuildEditableDom: function (d) { var b = Element.create("span", { "class": "J_editable fn-hide" }); var g = this.BuildEditInputDom(); var f = this.BuildSaveButton(); var a = this.BuildCannelButton(); AP.widget.formInputStyle.init(g); b.appendChild(g); b.appendChild(f); b.appendChild(a); d.appendChild(b); return b }, BuildEditInputDom: function () { var a = this.options.edit_type; var a = Element.create(a, { "class": "i-text", value: this.current_target.parentNode.getAttribute("rel"), type: "text", maxLength: "10" }); return a }, BuildSaveButton: function () { var a = Element.create("input", { type: "button", value: "保存", "class": "btn-2cn" }); E.on(a, "click", this.onSaveEvent, a, this); return a }, BuildCannelButton: function () { var a = Element.create("a", { innerHTML: "取消", href: "#" }); E.on(a, "click", this.onCannelEvent, a, this); return a }, BuildTargetButton: function () { var a = Element.create("a", { innerHTML: this.options.edit_target, "class": "fn-hide J_editable_target", href: "#" }); E.on(a, "click", this.onClickEvent, a, this); return a }
});
AP.widget.reviewCardID = new AP.Class({ setOptions: function (a) {
    return AP.hashExtend({ blurCustomEvent: function () { } }, a || {})
}, initialize: function (a, b) {

    this.targets = a; E.on(this.targets, "focus", this.focusEvent, "", this); E.on(this.targets, "blur", this.blurEvent, "", this); E.on(this.targets, "keyup", this.keyupEvent, "", this); a.forEach(function (d) { this.getSplitCardID(d.value) }, this); this.options = b || {}; this.options = this.setOptions(this.options); this.blurCustomEvent = new U.CustomEvent("blurCustomEvent"); this.blurCustomEvent.subscribe(this.options.blurCustomEvent, this, true); this.buidDom()
}, focusEvent: function (b) {

    var a = E.getTarget(b); this.createIframe(a); this.reviewCardID(a)
}, blurEvent: function (b) {

    var a = E.getTarget(b); a.value = this.getSplitCardID(a.value); this.hide(); this.blurCustomEvent.fire(a)
}, keyupEvent: function (b) {

    var a = E.getTarget(b); this.reviewCardID(a)
}, reviewCardID: function (a) {

    if (a.value.trimAll().length) { this.numberBox.innerHTML = this.getSplitCardID(a.value); this.show(a) } else { this.hide() }
}, getSplitCardID: function (b) {

    var b = b.trimAll().split(""); var a = ""; b.forEach(function (d, f) { f = f + 1; a = a + d; if (f % 4 == 0) { a = a + " " } }); return a
}, setExplain: function (a) {

    return false; this.explainBox.innerHTML = a
}, position: function (f) {

    var d = D.getRegion(this.container); var a = D.getRegion(f); var b = a.top - (d.bottom - d.top); D.setXY(this.container, [a.left, b])
}, createIframe: function (g) {

    if (D.query("iframe", this.container).length) { return } var d = Element.create("iframe");
    d.src = "javascript:false;"; var f = D.getRegion(g);
    var b = f.right - f.left + "px";
    var a = f.bottom - f.top + "px";
    D.setStyle(d, "opacity", "0");
    D.setStyle(d, "width", b);
    D.setStyle(d, "height", a);
    D.setStyle(d, "position", "absolute");
    D.setStyle(d, "top", "-5px");
    D.setStyle(d, "left", "-5px");
    D.setStyle(d, "z-index", "0");
    this.outBox.appendChild(d)
}, show: function (a) {

    D.removeClass(this.container, "fn-hide"); this.position(a)
}, hide: function () {

    D.addClass(this.container, "fn-hide")
}, buidDom: function () {

    this.numberBox = this.getCreateNumberDom(); this.explainBox = this.getCreateExplainDom(); this.outBox = this.getCreateBoxDom(); this.container = Element.create("div", { "class": "bank-card-review fn-hide" }); this.outBox.appendChild(this.numberBox); this.container.appendChild(this.outBox); D.setStyles(this.container, { position: "absolute" }); document.body.appendChild(this.container)
}, getCreateBoxDom: function () {

    var a = Element.create("div"); D.setStyles(a, { position: "relative" }); return a
}, getCreateNumberDom: function () {

    var a = Element.create("div", { "class": "cardid" }); D.setStyle(a, "font-size", "18px"); D.setStyle(a, "font-family", "tahoma"); return a
}, getCreateExplainDom: function () {

    var a = Element.create("div", { "class": "explain-info" }); return a
}
});
AP.widget.formInputStyle = function () {

    var a = function (b, d) { E.on(b, "focus", function () { D.removeClass(this.parentNode, "fm-hover"); D.removeClass(this.parentNode, "fm-error"); D.addClass(this.parentNode, "fm-focus") }); E.on(b, "mouseover", function () { D.addClass(this.parentNode, "fm-hover") }); E.on(b, "mouseout", function () { D.removeClass(this.parentNode, "fm-hover") }); E.on(b, "blur", function () { D.removeClass(this.parentNode, "fm-hover"); D.removeClass(this.parentNode, "fm-focus") }) }; return { init: function (b) { a(b) } }
} ();
AP.widget.autoFontsize = new AP.Class({ setOptions: function (a) {

    debugger;
    return AP.hashExtend({ minSize: 12, target: D.query(".aside-amount em")[0], maxWidth: 106 }, a || {})
}, initialize: function (a) { this.options = this.setOptions(a); if (this.options.target.constructor == Array) { this.options.target.forEach(function (b) { this.resize(b) }, this) } else { this.resize(this.options.target) } }, resize: function (a) { var b = parseInt(D.getStyle(a, "fontSize")); while (a.offsetWidth > this.options.maxWidth) { if (--b < this.options.minSize) { break } else { D.setStyle(a, "fontSize", b + "px") } } this.resizeComplete() }, resizeComplete: function () { }
});
AP.pk.pa.asidePortrait = function () {
    var a; function n(o) {
        return AP.hashExtend({ my: D.get("J_portrait"), container: D.get("J_aside_portraits"), pointer: {}, ul: D.query("ul", D.get("J_aside_portraits"))[0], timer: {}, curUserPicClass: "J_portrait" }, o || {})
    } function j() { a.pointer = Element.create("span", { "class": "ico-angleR", title: "展开", appendTo: a.container }) } function l() { D.removeClass(a.container, "fn-hide"); a.ul.className = "hover"; a.pointer.className = "ico-angleR"; a.pointer.title = "展开" } function h(o) { return Math.floor(Math.random() * o) + 1 } function f() { D.removeClass(a.container, "fn-hide"); a.ul.className = "expand"; a.pointer.className = "ico-angleL"; a.pointer.title = "关闭"; if (typeof Tracker != "undefined") { Tracker.click("aside-portrait-expand") } } function d(o) {

        if (a.ul.className == "hover" || o == true) { D.addClass(a.container, "fn-hide") }
    } function g() {

        E.on(a.my, "mouseover", l); E.on([a.pointer, D.query("li", a.ul)], "click", function (o) { a.pointer.className == "ico-angleR" ? f() : d(true); E.stopEvent(o) }); E.on(document.body, "click", function (o) { if (a.pointer.className == "ico-angleL") { d(true) } }); E.on([a.ul, a.pointer], "mouseout", function () { a.timer = setTimeout(d, 100) }); E.on([a.ul, a.pointer], "mouseover", function () { clearTimeout(a.timer) }); if (D.query("li", a.ul).length > 2) { E.on(D.query("li", a.ul)[1], "click", m, { type: "old" }) }
    } function m(q, r) {

        var o = D.get("J_aside_acctname").getAttribute("data-host"); var p = new AP.core.api("user/modiPortraitUrl", { onAPISuccess: b, api_url: o, method: "POST", format: "jsonp" }); p.call({ type: r.type }); a.my.src = D.query("img", D.query("li", a.ul)[1])[0].src; E.preventDefault(q)
    } function b(q, o) {


        var r = D.query("img", a.ul); r[0].src = AP.fn.url.imgTfs + "/" + o[0].cur; log(r[0].width, r[0].height); if (o[0].old) { if (D.query("li", a.ul).length == 2) { var p = document.createElement("li"); p.innerHTML = '<a href="#"><img height="54" width="54" alt="上次头像" src="' + AP.fn.url.imgTfs + "/" + o[0].old + '" /></a>'; D.insertAfter(p, D.query("li", a.ul)[0]); E.on(p, "click", m, { type: "old" }) } else { r[1].src = AP.fn.url.imgTfs + "/" + o[0].old } } else { if (D.query("li", a.ul).length == 3) { a.ul.removeChild(D.query("li", a.ul)[1]) } } a.my.src = AP.fn.url.imgTfs + "/" + o[0].cur; D.query("img." + a.curUserPicClass, D.get("container")).forEach(function (s, t) { s.src = AP.fn.url.imgTfs + "/" + o[0].cur })
    } function k() {

        var o = D.get("J_aside_acctname").getAttribute("data-host");
        var p = new AP.core.api("user/getPortraitUrl", { onAPISuccess: b, api_url: o, method: "POST", format: "jsonp" }); p.call({ get: "all" })
    } return { init: function (o) {

        a = n(o); if (!a.my) { return } if (!a.container) { return } j(); g()
    }, renew: function () { k() }
    }
} ();
AP.pk.pa.remindScene = function () {
    var t = { listContainer: "J_asidemlist", totalNum: 0, canDelTotalNum: 0, totalPages: 1, currentPage: 1, onePage: 10, pre: {}, next: {} }; var n; var a; var o; var p; var h, b; var d = function (v) { var u = D.query("a", v)[0]; var w = D.query(".ico-del-aside", v)[0]; E.on(v, "mouseover", function (y, x) { D.addClass(x, "hover") }, v); E.on(v, "mouseout", function (y, x) { D.removeClass(x, "hover") }, v); E.on(w, "click", m, v); if (u.type == "REMIND") { E.on(u, "click", m, v) } }; var m = function (w, x) { var z = D.query(".ico-del-aside", x)[0].href.toQueryParams().delId; var u = D.get("J_aside_acctname") ? D.get("J_aside_acctname").getAttribute("data-host") : AP.PageVar.app_domain; try { var v = new AP.core.api("home/statusbar/deleteRemindScene", { onAPISuccess: function (B, A) { s(A, x) }, api_url: u, method: "POST", format: "jsonp" }); v.call({ id: z }) } catch (y) { log(y) } if (typeof Tracker != "undefined") { Tracker.click("aside-reminddel") } E.getTarget(w).type ? "" : E.stopEvent(w) }; var s = function (u, v) { k(v) }; var k = function (w) { var u = { height: { to: 0 }, opacity: { to: 0} }; AP.fn.browser.msie ? u = { opacity: { to: 0}} : ""; var v = new YAHOO.util.Anim(w, u, 0.3); v.animate(); v.onComplete.subscribe(function () { l(w) }) }; var l = function (v) { t.canDelTotalNum--; t.totalNum--; if (t.totalNum == 0) { j() } log(t.totalNum, n.length); if (t.totalPages > 1) { var u = n.indexOf(v.parentNode.lastChild); if (u < t.totalNum) { v.parentNode.appendChild(n[u + 1]) } } q(); n.remove(v); v.parentNode.removeChild(v) }; var j = function () { D.addClass(D.query(".aside-main", D.get("aside"))[0], "aside-notice-hide") }; var g = function () { o = D.query("ul", D.get(t.listContainer))[0]; D.addClass(o, "fn-hide"); var u = D.query(".aside-page a", D.get("aside")); t.pre = u[0]; t.next = u[1]; E.on(t.pre, "click", function (v) { r(t.currentPage - 1); E.preventDefault(v) }); E.on(t.next, "click", function (v) { r(t.currentPage + 1); E.preventDefault(v) }); D.removeClass(t.next, "fn-hide"); h = document.createElement("ul"); b = document.createElement("ul"); p = Element.create("div", { style: { width: "500px" }, appendTo: D.get(t.listContainer) }); n.forEach(function (v, w) { w < t.onePage ? h.appendChild(v) : "" }); p.appendChild(h); p.appendChild(b) }; var r = function (u) { if (typeof Tracker != "undefined") { Tracker.click("aside-remindpage") } if (u > t.currentPage) { D.query("li", b).forEach(function (v) { o.appendChild(v) }); n.forEach(function (v, w) { if (parseInt(w / t.onePage) == u - 1) { b.appendChild(v) } }); f("next") } if (u < t.currentPage) { D.query("li", h).forEach(function (v) { o.appendChild(v) }); n.forEach(function (v, w) { if (parseInt(w / t.onePage) == u - 1) { h.appendChild(v) } }); f("pre") } t.currentPage = u }; var f = function (v) { if (v == "next") { var u = { marginLeft: { from: 0, to: -222}} } else { var u = { marginLeft: { from: -222, to: 0}} } var w = new YAHOO.util.Anim(p, u, 0.3, YAHOO.util.Easing.easeOut); w.animate(); w.onComplete.subscribe(function () { q() }) }; var q = function () { t.totalPages = Math.ceil(t.totalNum / t.onePage); if (t.totalPages == 1 && t.currentPage < t.totalPages + 1) { D.removeClass(D.get(t.listContainer), "maxheight"); D.addClass([t.pre, t.next], "fn-hide") } else { if (t.currentPage == 1) { D.addClass(t.pre, "fn-hide"); D.removeClass(t.next, "fn-hide") } else { if (t.currentPage > t.totalPages - 1) { D.addClass(t.next, "fn-hide"); D.removeClass(t.pre, "fn-hide") } else { D.removeClass(t.pre, "fn-hide"); D.removeClass(t.next, "fn-hide") } } } D.get("J_aside_current_page").innerHTML = t.currentPage; D.get("J_aside_total_page").innerHTML = t.totalPages }; return { init: function (u) { AP.fn.apply(t, u); if (!D.get(t.listContainer)) { return } n = D.query("li", D.get(t.listContainer)); a = D.query("a.ico-del-aside", D.get(t.listContainer)); t.canDelTotalNum = a.length; t.totalNum = n.length; t.totalPages = Math.ceil(t.totalNum / t.onePage); D.get("J_aside_total_page").innerHTML = t.totalPages; n.forEach(function (v, w) { d(v) }); t.totalPages > 1 ? g() : "" }, gotoPage: function (u) { r(u) } }
} ();

AP.pk.pa.switchAcct = AP.widget.dropDown.extend({ initialize: function (a, b) {
    this.parent(a, b); this.acctListsDone = false
}, getAcctListsDone: function () { new AP.widget.xBox({ el: D.query(".aside-accounts li:not(.selected)"), type: "iframe", value: function (a) { return a.firstChild.href + "&goto=" + parent.location.href }, width: 400, height: 224, modal: true, hasHead: false, fixed: false, autoFitPosition: false }); this.acctListsDone = true }, getAcctLists: function (a) { var b = this; var d = D.get("J_aside_acctname") ? D.get("J_aside_acctname").getAttribute("data-host") : AP.PageVar.app_domain; var f = new AP.core.api("home/statusbar/getRelativeAccounts", { onAPISuccess: function (k, g) { if (g[0].stat == "ok") { var j = g[0].accts; var l = D.query("li a", a)[0].href; for (var h = 0; h < j.length; h++) { a.innerHTML += '<li><a title="' + j[h] + '" href="' + l + j[h] + '">' + AP.util.handleLongEmail(j[h]) + "</a></li>" } D.query("li", a).length > 10 ? D.addClass(a, "maxheight") : ""; E.on(D.query("li", a)[0], "click", function (m) { E.preventDefault(m) }); E.on(D.query("li", a), "click", function () { D.addClass(a, "fn-hide") }); b.getAcctListsDone() } }, api_url: d, method: "POST", format: "jsonp" }); f.call() }, bindEvents: function (b, a) { this.parent(b, a); E.on(b, "click", function () { if (!this.acctListsDone) { this.getAcctLists(a) } if (typeof Tracker != "undefined") { Tracker.click("aside-switchaccount") } }, a, this) }
});
AP.pk.pa.asideAmountFontsize = AP.widget.autoFontsize.extend({ resizeComplete: function () {

    D.removeClass(D.query(".aside-amount", D.get("aside"))[0], "fn-hidden"); if (typeof Tracker != "undefined") { Tracker.click("aside-amountresize") }
}
});
AP.pk.pa.aside = { initialize: function () {
    this.aside = D.get("aside"); if (!this.aside) { return } if (D.query(".aside-bar", D.get("aside"))[0]) { this.asideBar = D.query(".aside-bar", D.get("aside"))[0]; this.bindEvents(); var a = D.query("dt", this.asideBar)[0]; if ((!AP.fn.browser.msie) || (a.innerHTML == "")) { a.innerHTML = "查看账户信息" } D.removeClass(a, "fn-hide"); this.stat = 0; this.asideRegion = D.getRegion(this.aside); this.pwSwitch("hide"); this.asyncLoading = false } this.loadAsideContainer(); if (this.asideContainer.length > 1) { this.initComponents() }
},
    sendTrack: function (a) {

        if (typeof Tracker != "undefined") { Tracker.click(a) }
    },
    loadAsideContainer: function () {

        this.asideContainer = D.query("#aside>div", D.get("content")).concat(D.query("#aside>iframe", D.get("content")))
    }, endAsynLoading: function () { this.asyncLoading = false }, asyncLoad: function () {
        this.asyncLoading = true; var a = this; if (typeof (AP_ASIDE_URL) == "undefined") { AP_ASIDE_URL = "/tile/service/home:aside.tile" } YAHOO.util.Connect.asyncRequest("GET", AP_ASIDE_URL + "?isExpandAside=true&referPage=" + location.href + "&r=" + +new Date(), { success: function (b) { var h = b.responseText; h = h.replace(/\r\n/gi, ""); h = h.replace(/\n/gi, ""); var f; var d = h.indexOf("iframe") > -1 ? false : true; if (d) { f = /(<div class=\"aside-info\".*)<\/div>\s*<div class=\"aside-widget/gi } else { f = /(<iframe.*<\/iframe>)/gi } var g = f.exec(h); if (g) { var j = document.createElement("div"); j.className = "J-asyncload hasSecurityLevel " + (d ? "aside-main " : "") + (h.indexOf("aside-notice-hide") > -1 ? " aside-notice-hide" : ""); j.innerHTML = g[1]; D.insertBefore(j, a.asideBar); a.loadAsideContainer(); a.initComponents(); a.expandAnim() } a.endAsynLoading() } })
    }, bindEvents: function () { E.on(this.asideBar, "click", function (a) { (D.query(".aside-main", D.get("aside")).length || D.query(".J-asyncload", D.get("aside")).length || this.asyncLoading) ? this.expandAnim() : this.asyncLoad() }, {}, this); E.on(document.body, "click", function (a) { if (this.stat == 1 && this.checkFoldable(a)) { this.pwSwitch("hide"); this.foldAnim() } }, {}, this) }, foldAnim: function () { var b = { width: { form: 235, to: 30} }; var d = new YAHOO.util.Anim("aside", b, 0.3, YAHOO.util.Easing.easeIn); d.animate(); var a = this; d.onComplete.subscribe(function () { a.asideContainer.forEach(function (f) { f == a.asideBar ? D.removeClass(f, "fn-hide") : D.addClass(f, "fn-hide") }, a); a.stat = 0 }, a) }, expandAnim: function () {
        this.asideContainer.forEach(function (g) { g == this.asideBar ? D.addClass(g, "fn-hide") : D.removeClass(g, "fn-hide") }, this); if (AP.fn.browser.msie6 && this.aside.innerHTML.indexOf("<IFRAME") == -1) { var d = document.createElement("div"); d.className = "aside-layer-bg"; d.innerHTML = '<iframe src="javascript:\'\'" scrolling="no" height="900" frameborder="0"></iframe>'; this.aside.appendChild(d) } var b = { width: { form: 0, to: 235} }; var f = new YAHOO.util.Anim("aside", b, 0.3, YAHOO.util.Easing.easeOut); f.animate(); var a = this; f.onComplete.subscribe(function () {

            a.stat = 1; a.pwSwitch("show"); if (!a.autoFontsizeDone) { a.autoFontsizeDone = new AP.pk.pa.asideAmountFontsize() }
        }, a); this.sendTrack("aside-expand")
    }, checkFoldable: function (a) {

        return (!AP.fn.eInRegion(D.getRegion(this.aside), a)) && (!AP.fn.eInRegion(D.getRegion("J_mypop"), a))
    }, pwSwitch: function (a) {

        if (!window.asideLoginIframe) { return } try { if (a == "hide") { D.addClass(window.asideLoginIframe.document.getElementsByTagName("embed")[0], "fn-hide"); D.addClass(window.asideLoginIframe.document.getElementsByTagName("object")[0], "fn-hide") } else { D.removeClass(window.asideLoginIframe.document.getElementsByTagName("object")[0], "fn-hide"); D.removeClass(window.asideLoginIframe.document.getElementsByTagName("embed")[0], "fn-hide") } } catch (d) { log(d) }
    }, initComponents: function () {

        AP.pk.pa.remindScene.init(); var b = D.query(".aside-status li", D.get("aside")); if (D.query(".ico-coupon", D.get("aside")).length) { b.push(D.query(".ico-coupon", D.get("aside"))[0]) } new AP.widget.seniorPop(D.query("em.ico-vip"), { offset: [-6, 0] }); var a = this; new AP.widget.seniorPop(b, { offset: [-16, 0], onshow: function () { a.sendTrack("aside-" + this.current_el.className.substring(8)) } }); if (D.hasClass(D.query(".aside-amount", D.get("aside"))[0], "fn-hidden")) { new AP.pk.pa.asideAmountFontsize() } AP.pk.pa.asidePortrait.init(); if (D.hasClass("J_aside_acctname", "multi")) { new AP.pk.pa.switchAcct(D.query("#J_aside_acctname")); D.get("J_aside_acctname").title = "切换登录账户" } new AP.widget.xBox({ el: D.query(".aside-portraits li:contains(" + _("upload") + ") a", D.get("aside"))[0], type: "iframe", value: function (d) { return d.href }, modal: true, fixed: false }); D.query("#aside .aside-widget a").forEach(function (d) { if (d.href.indexOf("uisrc=") == -1) { d.href += "&uisrc=aside" } d.href += "&ref=" + location.href }); if (D.get("J_aside_acctnameExtend")) { E.on(D.query("em.ico-vip a"), "click", function (d) { window.open(this.href); E.stopEvent(d) }) }
    }
};
var AcValidataor = function (a) { this.init(a); this.run() };
AcValidataor.prototype = { init: function (a) {
    debugger;
    for (var b in a) { this[b] = a[b] }
}, beforeClick: function () { }, run: function () { var d = this; d.form = D.get("form"); d.explain = D.get(d.explainId); d.cardNum = D.get(d.inputId); d.explainItem = D.getAncestorByClassName(d.explain, d.fmItemId); var a = d.edit = AlieditControl.getAliedit(d.form); if (AlieditControl) { var b = function () { try { d.edit[0].PasswordMode = 0 } catch (f) { setTimeout(function () { b() }, 500) } }; b() } E.on(this.btnId, "click", function (k) { D.removeClass(d.explainItem, d.warnCls); D.removeClass(d.explainItem, d.errorCls); if (a) { d.editControl = d.edit[0] } else { var j = D.getAncestorByClassName(d, "ui-btn"); D.removeClass(j, "ui-btn-ok"); D.addClass(j, "ui-btn-cancel"); d.disabled = true } var f = D.get("J-userBankCard-N"); var g = f && f.checked || false; if (d.beforeClick(k, d.form) !== false && !g) { var h = new AP.core.api("fund/paydirect/checkCard", { onAPISuccess: function (n, m) { d.cardNum.value = d.editControl.TextData; if (m[0]["goto"] === "applyFastPayment") { d.form.target = "_top"; d.form.action = "/fund/paydirect/applyFastPayment.htm"; var l = D.query("input[name='institutionShortName']:first", d.explainItem); l.length > 0 && (l[0].value = m[0]["institutionShortName"]) } d.form.submit() }, onAPIFailure: function (m, l) { if (l[0].mark) { D.addClass(d.explainItem, d.warnCls); d.explain.innerHTML = l[0].msg + '<a href="https://www.alipay.com/static/bank/index.htm" target="_blank" seed="link-suported-bank">' + l[0].supportUrl + "</a>" } else { D.addClass(d.explainItem, d.errorCls); d.explain.innerHTML = l[0].msg } }, method: "POST" }); h.call({ cardNo: d.editControl.TextData }) } E.preventDefault(k) }) }
};
AP.PageVar = AP.PageVar || {}; if (!AP.PageVar.reset_domain) {
    try {

        var _ADOMAIN = document.domain.split(".");
        var _SDOMAIN = _ADOMAIN[_ADOMAIN.length - 2] + "." + _ADOMAIN[_ADOMAIN.length - 1];
        document.domain = _SDOMAIN
    } catch (e) { }
}
E.onDOMReady(function () {

    if ((D.get("viewFaq") || D.get("view-faq")) && D.get("faq")) {

        AP.widget.highligthFaq = new AP.pk.pa.highlight({ autoShow: false }); E.on(["viewFaq", "view-faq"], "click", function (k) { AP.widget.highligthFaq.show(); if (typeof Tracker != "undefined") { Tracker.click("pa-viewfaq") } E.preventDefault(k) })
    }
    try { AP.util.inputHack() } catch (g) { }
    try { AP.pk.pa.aside.initialize() } catch (g) { log(g) }
    if (D.get("faqsearch")) {

        new AP.pk.pa.searchBox(D.get("faqsearch"))
    } if (D.get("asidesearch")) {

        new AP.pk.pa.searchBox(D.get("asidesearch"))
    } if (D.get("error404search")) {
        new AP.pk.pa.searchBox(D.get("error404search"))
    } function f() {
        var m = D.getViewportHeight();
        var k = D.get("container").offsetHeight;
        var l = D.getY("content");
        if (k < m + l) { D.get("container").style.height = m + l + "px" } window.scroll(0, l)
    }
    if (D.query(".J-scroll-to-title").length > 0 || (D.query(".aside-bar", D.get("aside")).length > 0 && D.query(".title a.cancel", D.get("main")).length > 0)) { f() }
    E.on(D.query(".J-scroll-to-top"), "click", function (k) {
        f();
        E.preventDefault(k)
    });
    if (D.query(".J-scroll-to-top", D.get("faq")).length > 0 && D.getDocumentHeight() == D.getViewportHeight()) {
        D.addClass(D.query(".J-scroll-to-top", D.get("faq"))[0], "fn-hide")
    }
    E.on(D.query(".title a.return", D.get("content"))[0], "click", function (k) {
        if (typeof Tracker != "undefined") { Tracker.click("pa-return") } if (E.getTarget(k).href == "" || E.getTarget(k).href == window.location.href || E.getTarget(k).href == window.location.href + "#") { history.go(-1); E.preventDefault(k) }
    });
    var j = history.length + (AP.fn.browser.msie ? 1 : 0); if (j == 1 && D.query(".title a.action", D.get("content")).length > 0 && location.href.indexOf("_xbox") == -1) {
        D.addClass(D.query(".title a.action", D.get("content"))[0], "fn-hide")
    } if (D.hasClass(document.body, "xbox") && self.parent === self && D.query(".xbox-close-link").length) {
        D.addClass(D.query(".xbox-close-link")[0], "fn-hide")
    }
    if (D.query(".switch-lang").length) {
        var b = window.location.host.match(/^[-a-zA-Z0-9]+(\..+)/)[1] || ".alipay.com"; D.query(".switch-lang").forEach(function (k) {
            E.on(k, "click", function () {

                YAHOO.util.Cookie.set("_lang", k.getAttribute("data-lang"), { path: "/", domain: b, expires: new Date("April 14, 5262") }); setTimeout(function () { window.location.reload() }, 10)
            })
        })
    }
    try {
        (function () {
            var m = D.query("input:text").concat(D.query("textarea"));
            var l = m.length; if (l < 1) { return } for (var k = 0; k < l; k++) {
                if (m[k].offsetHeight > 0 && D.hasClass(m[k], "J-autofocus") && !D.hasClass(m[k].parentNode, "fm-error")) { window.setTimeout(function () { m[k].focus(); m[k].select() }, 200); if (D.hasClass(m[k].parentNode, "fm-item")) { D.addClass(m[k].parentNode, "fm-focus") } break }
            }
        })()
    }
    catch (g) { }
    AP.widget.scrollToAnchor = function () {
        var l = window.location.href; var m = /[?|&]anchor=([^&]*)/; if (m.test(l)) { var k = m.exec(l)[1]; if (D.get(k) || D.get("J_" + k)) { var n = D.get(k) ? D.get(k) : D.get("J_" + k); AP.util.scrollPage(n, 1) } }
    } ();
    if (D.get("J-nav-life") && D.get("J-nav-lifeExtend")) {

        new AP.pk.pa.animDropDown(D.query("#J-nav-life"), { isposition: true, offset: [3, -7], mousehandle: true, targetClick: function () { AP.pk.pa.aside.pwSwitch("hide") }, hideEvent: function () { AP.pk.pa.aside.pwSwitch("show") } }); E.on("J-nav-life", "mouseover", function () { if (typeof Tracker != "undefined") { Tracker.click("pa-showlifenav") } })
    }
    if (D.get("J-nav-life")) {

        E.on("J-nav-life", "click", function (k) { E.stopEvent(k) })
    }


    var a = D.query(".nav-master-life.current", D.get("nav")).length ? D.query(".nav-master", D.get("nav")) : D.query(".nav-master:not(li.nav-master-life)", D.get("nav"));
    var d = 0;
    var h;

    if (a.length) {
        a.forEach(function (k, l) {
            d = D.hasClass(k, "current") ? l : d;

            E.on(k, "mouseover", function () {
                if (h) {
                    clearTimeout(h)
                }
                h = setTimeout(function () {

                    for (var m = 0; m < a.length; m++) {
                        D.removeClass(a[m], "current");                        
                    }
                    D.addClass(k, "current");
                    //D.addClass(D.query(".nav-master-a", a[d])[0], "nav-master-a-hover");
                }, 200)
            })
            //click
            E.on(k, "click", function () {

                for (var m = 0; m < a.length; m++) {
                    D.removeClass(a[m], "current");
                }
                D.addClass(k, "current");
                //debugger;
                var seedname = k.children[0].attributes["seed"].value;
                if (k.children[0].attributes["href"].value.indexOf("master") < 0) {
                    k.children[0].attributes["href"].value = (k.children[0].attributes["href"].value.replace('#', '') + "?master=" + seedname + "").replace('#', '') + "$";
                }
            })
        });
        E.on("nav", "mouseout", function () {

            if (h) { clearTimeout(h) }
            h = setTimeout(function () {

                if (window.location.href.indexOf("master") < 0) {

                    if (h) { clearTimeout(h) } h = setTimeout(function () {
                        for (var k = 0; k < a.length; k++) { D.removeClass(a[k], "current");  }
                        D.addClass(a[d], "current");
                        D.addClass(D.query(".nav-master-a", a[d])[0], "nav-master-a-hover");

                    }, 200)
                }
                else {
                    LoadMain();
                }

            }, 200)
        });
        //D.addClass(D.query(".nav-master-a", a[d])[0], "nav-master-a-hover")
    }


    //子项菜单
    var x = D.query(".nav-sub li a", D.get("nav"));
    if (x.length) {
        x.forEach(function (k, l) {
            d = D.hasClass(k, "current") ? l : d;
            E.on(k, "click", function () {
                for (var m = 0; m < x.length; m++) {
                    D.removeClass(x[m], "current");
                }
                D.addClass(k, "current");
                var seedname = k.attributes["seed"].value;
                if (k.attributes["href"].value.indexOf("master") < 0) {
                    k.attributes["href"].value = (k.attributes["href"].value.replace('#', '') + "?master=" + seedname + "").replace('#', '') + "$";
                }
            })
        });

    }

    //解析url选中相应的菜单按钮
    //debugger;
    LoadMain();

    function LoadMain() {
        var mUrl = window.location.href;
        var mUrlList = mUrl.split('?');
        if (mUrlList.length > 1) {
            var mSeedName = mUrlList[1].split('&')[0] + "";
            if (mSeedName != "undefined") {
                var SeedName = mSeedName.split('=')[1].replace('$', '').replace('#', '');
                //load---
                LoadMenuType(SeedName);
            }
        }

    }

    //加载对应的菜单按钮
    function LoadMenuType(SeedName) {

        var flag = "";

        var v = D.query(".nav-master", D.get("nav"));
        if (v.length) {
            v.forEach(function () {
                for (var m = 0; m < v.length; m++) {
                    //上级菜单                    
                    if (v[m].children[0].attributes["seed"].value == SeedName) {
                        D.addClass(v[m], "current");
                        D.addClass(D.query(".nav-master-a", v[m])[0], "nav-master-a-hover");
                        //子菜单
                        D.addClass(v[m].children[1].children[0].children[0], "current");
                        flag = "F";
                    }
                    else {
                        D.removeClass(v[m], "current");
                    }
                }
            });

        }

        //子菜单
        if (flag == "") {
            var y = D.query(".nav-sub li a", D.get("nav"));
            if (y.length) {
                y.forEach(function () {
                    for (var m = 0; m < y.length; m++) {
                        //上级菜单                    
                        if (y[m].attributes["seed"].value == SeedName) {
                            D.addClass(y[m], "current");
                            D.addClass(y[m].parentNode.parentNode.parentNode, "current");
                        }
                        else {
                            D.removeClass(y[m], "current");
                        }
                    }
                });
            }
        }

    }

    new AP.widget.xBox({ el: D.query(".J-feedback"), type: "iframe", value: function (k) { return k.href }, modal: true, fixed: false, width: 485 })
});