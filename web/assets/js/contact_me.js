// $(function () {
    // $("input,textarea").jqBootstrapValidation({
        // preventSubmit: !0, submitError: function (a, b, c) { }, submitSuccess: function (a, b) {
            // $("#error-msg").html(""), b.preventDefault(); var d = $.trim($("input#name").val()), e = $.trim($("input#email").val()), f = $.trim($("textarea#message").val()), h = ($.trim(d), 1 == !!d), i = 1 == !!e, j = 1 == !!f; console.log(h + " " + i + " " + j), h && i && j ? ($("#Button").val("Please Wait..."), $("#Button").prop("disabled", !0), $.ajax({
                // url: "ContactUs.aspx/SendMailtoAdmin", type: "POST", data: JSON.stringify({ name: d, email: e, message: f }), contentType: "application/json; charset=utf-8", success: function (a) {
                    // $("#Button").prop("disabled", !1), $("#MailForm").addClass("fadeOut animated zeroheight"), setTimeout(function () {
                        // $('.interact-notice').html("Thank You!")
                    // }, 1e3), 1 == a.success || (1 == a.errorCode ? $("#name").addClass("error").focus() : 2 == a.errorCode ? $("#email").addClass("error").focus() : 3 == a.errorCode && $("#messsage").addClass("error").focus())
                // }, error: function (a) { $("#Button").val("Send Message"), $("#Button").prop("disabled", !1) }
            // })) : ($("#error-msg").text("Please Provide Valid Information"), $("#Button").val("Send Message"), $("#Button").prop("disabled", !1))
        // }, filter: function () { return $(this).is(":visible") }
    // })
// }), $('a[data-toggle="tab"]').click(function (a) { a.preventDefault(), $(this).tab("show") }), $("#name").focus(function () { $("#success").html("") });